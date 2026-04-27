using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._02.Utilities.ActionResponse;
using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._05.Services.Charts;
using csharp_cartographer_backend._05.Services.Files;
using csharp_cartographer_backend._05.Services.Insights;
using csharp_cartographer_backend._05.Services.SyntaxHighlighting;
using csharp_cartographer_backend._05.Services.Tokens;
using csharp_cartographer_backend._05.Services.Tokens.Maps;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Json;

namespace csharp_cartographer_backend._06.Workflows.Artifacts
{
    public class GenerateArtifactWorkflow : IGenerateArtifactWorkflow
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly IInsightService _insightService;
        private readonly INavTokenGenerator _navTokenGenerator;
        private readonly ISyntaxHighlighter _syntaxHighlighter;
        private readonly ITokenChartGenerator _tokenChartGenerator;
        private readonly ITokenMapper _tokenMapper;
        private readonly CartographerConfig _config;
        private readonly ILogger<GenerateArtifactWorkflow> _logger;

        private readonly JsonSerializerOptions options = new() { WriteIndented = true };

        public GenerateArtifactWorkflow(
            IFileProcessor fileProcessor,
            IInsightService insightService,
            INavTokenGenerator navTokenGenerator,
            ISyntaxHighlighter syntaxHighlighter,
            ITokenChartGenerator tokenChartGenerator,
            ITokenMapper tokenMapper,
            IOptions<CartographerConfig> config,
            ILogger<GenerateArtifactWorkflow> logger)
        {
            _fileProcessor = fileProcessor;
            _insightService = insightService;
            _navTokenGenerator = navTokenGenerator;
            _syntaxHighlighter = syntaxHighlighter;
            _tokenChartGenerator = tokenChartGenerator;
            _tokenMapper = tokenMapper;
            _config = config.Value;
            _logger = logger;
        }

        public async Task<ActionResponse<Artifact>> GenerateDemoArtifact(string fileName, CancellationToken cancellationToken)
        {
            FileData fileData = _fileProcessor.ReadInTestFileData(fileName);

            var actionResponse = await GenerateArtifact(fileData, cancellationToken);

            var insight = _insightService.GetDemoFileInsight(fileName);
            if (insight is not null)
            {
                actionResponse.Content.Insight = insight;
            }

            return actionResponse;
        }

        public async Task<ActionResponse<Artifact>> GenerateUserArtifact(GenerateArtifactDto requestDto, CancellationToken cancellationToken)
        {
            FileData fileData = _fileProcessor.ReadInFileData(requestDto);
            return await GenerateArtifact(fileData, cancellationToken);
        }

        private async Task<ActionResponse<Artifact>> GenerateArtifact(FileData fileData, CancellationToken cancellationToken)
        {
            /*
             *   Steps to generate an artifact:
             * 
             *   0. Read in source code from user uploaded file & generate FileData.
             *   1. Start stopwatch and set first checkpoint.
             *   2. Generate a list of nav tokens from the source file.
             *   3. Generate a token chart for each token and its ancestors.
             *   4. Add semantic details to each token and it's map.
             *   5. Add syntax highlighting for each token.
             *   6. Stop stopwatch and capture total elapsed time.
             *   7. Build artifact timings.
             *   8. Build artifact.
             *   *  Log artifact data (optional)
             *   9. Return artifact.
             */

            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                TimeSpan checkpoint = TimeSpan.Zero;

                var navTokens = await _navTokenGenerator.GenerateNavTokens(fileData, cancellationToken);
                var tokenGenTime = TimeSinceCheckpoint(stopwatch, ref checkpoint);

                _tokenChartGenerator.GenerateTokenCharts(navTokens);
                var chartGenTime = TimeSinceCheckpoint(stopwatch, ref checkpoint);

                _tokenMapper.MapNavTokens(navTokens);
                var mapTime = TimeSinceCheckpoint(stopwatch, ref checkpoint);

                _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(navTokens);
                var highlightTime = stopwatch.Elapsed - checkpoint;

                stopwatch.Stop();
                var totalTime = stopwatch.Elapsed;

                var timings = new ArtifactTimes(
                    tokenGenTime,
                    chartGenTime,
                    mapTime,
                    highlightTime,
                    totalTime
                );

                var artifact = new Artifact(
                    fileData.FileName,
                    navTokens,
                    timings
                );

                LogArtifactData(artifact);

                return ActionResponse<Artifact>.Success(artifact);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                return ActionResponse<Artifact>.Failure("An exception occurred during artifact generation.");
            }
        }

        private void LogArtifactData(Artifact artifact)
        {
            if (_config.ShouldLogArtifact)
                _logger.LogInformation("Artifact: {@Artifact}", artifact);

            if (_config.ShouldLogSemanticData)
            {
                var identifiers = artifact.NavTokens
                    .Where(token => token.SemanticData is not null)
                    .Select(token => new
                    {
                        token.Index,
                    });

                var json = JsonSerializer.Serialize(identifiers, options);
                _logger.LogInformation("{newline}{json}", Environment.NewLine, json);
            }

            if (_config.ShouldLogUnidentifiedTokens)
            {
                var tokens = artifact.NavTokens
                    .Where(token => token.HighlightColor == "color-red")
                    .Select(token => new
                    {
                        token.Index,
                        token.Text,
                        token.Classifications.Original,
                        token.Classifications.Corrected,
                        token.Classifications.Final,
                        token.HighlightColor,
                        token.PrimaryKind,
                        token.SemanticRole,
                        token.Kind,
                    });

                var json = JsonSerializer.Serialize(tokens, options);
                _logger.LogInformation("{newline}{json}", Environment.NewLine, json);
            }
        }

        private static TimeSpan TimeSinceCheckpoint(Stopwatch stopwatch, ref TimeSpan checkpoint)
        {
            TimeSpan elapsed = stopwatch.Elapsed - checkpoint;
            checkpoint = stopwatch.Elapsed;
            return elapsed;
        }
    }
}
