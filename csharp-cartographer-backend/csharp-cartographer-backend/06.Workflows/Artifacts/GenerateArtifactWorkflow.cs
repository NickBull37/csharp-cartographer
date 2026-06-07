using csharp_cartographer_backend._02.Utilities.ActionResponse;
using csharp_cartographer_backend._02.Utilities.Logging;
using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._03.Models.Insights;
using csharp_cartographer_backend._05.Services.Charts;
using csharp_cartographer_backend._05.Services.Files;
using csharp_cartographer_backend._05.Services.Insights;
using csharp_cartographer_backend._05.Services.SyntaxHighlighting;
using csharp_cartographer_backend._05.Services.Tokens;
using csharp_cartographer_backend._05.Services.Tokens.Maps;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;
using System.Diagnostics;

namespace csharp_cartographer_backend._06.Workflows.Artifacts
{
    public class GenerateArtifactWorkflow : IGenerateArtifactWorkflow
    {
        private readonly ICartographerLogger _cartographerLogger;
        private readonly IFileProcessor _fileProcessor;
        private readonly IInsightService _insightService;
        private readonly INavTokenGenerator _navTokenGenerator;
        private readonly ISyntaxHighlighter _syntaxHighlighter;
        private readonly ITokenChartGenerator _tokenChartGenerator;
        private readonly ITokenMapper _tokenMapper;
        private readonly ILogger<GenerateArtifactWorkflow> _logger;

        public GenerateArtifactWorkflow(
            ICartographerLogger cartographerLogger,
            IFileProcessor fileProcessor,
            IInsightService insightService,
            INavTokenGenerator navTokenGenerator,
            ISyntaxHighlighter syntaxHighlighter,
            ITokenChartGenerator tokenChartGenerator,
            ITokenMapper tokenMapper,
            ILogger<GenerateArtifactWorkflow> logger)
        {
            _cartographerLogger = cartographerLogger;
            _fileProcessor = fileProcessor;
            _insightService = insightService;
            _navTokenGenerator = navTokenGenerator;
            _syntaxHighlighter = syntaxHighlighter;
            _tokenChartGenerator = tokenChartGenerator;
            _tokenMapper = tokenMapper;
            _logger = logger;
        }

        public async Task<ActionResponse<Artifact>> GenerateDemoArtifact(string fileName, CancellationToken cancellationToken)
        {
            FileData fileData = _fileProcessor.GetDemoFileData(fileName);
            return await GenerateArtifact(fileData, cancellationToken);
        }

        public async Task<ActionResponse<Artifact>> GenerateUserArtifact(GenerateArtifactDto requestDto, CancellationToken cancellationToken)
        {
            FileData fileData = _fileProcessor.GetUploadedFileData(requestDto);
            return await GenerateArtifact(fileData, cancellationToken);
        }

        private async Task<ActionResponse<Artifact>> GenerateArtifact(FileData fileData, CancellationToken cancellationToken)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                TimeSpan checkpoint = TimeSpan.Zero;

                var navTokens = await _navTokenGenerator.GenerateNavTokens(fileData, cancellationToken);
                var tokenGenTime = TimeSinceCheckpoint(stopwatch, ref checkpoint);

                _tokenMapper.MapNavTokens(navTokens);
                var mapTime = TimeSinceCheckpoint(stopwatch, ref checkpoint);

                _tokenChartGenerator.GenerateTokenCharts(navTokens);
                var chartGenTime = TimeSinceCheckpoint(stopwatch, ref checkpoint);

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

                Insight? insight = null;
                if (fileData.IsDemo)
                    insight = _insightService.GetDemoFileInsight(fileData.FileName);

                var artifact = new Artifact(
                    fileData.FileName,
                    navTokens,
                    timings,
                    insight
                );

                _cartographerLogger.LogArtifactData(artifact);

                return ActionResponse<Artifact>.Success(artifact);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred during artifact generation.");
                return ActionResponse<Artifact>.Failure("An exception occurred during artifact generation.");
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
