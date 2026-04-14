using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._02.Utilities.ActionResponse;
using csharp_cartographer_backend._02.Utilities.Logging;
using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._05.Services.Charts;
using csharp_cartographer_backend._05.Services.Files;
using csharp_cartographer_backend._05.Services.SyntaxHighlighting;
using csharp_cartographer_backend._05.Services.Tokens;
using csharp_cartographer_backend._05.Services.Tokens.Maps;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace csharp_cartographer_backend._06.Workflows.Artifacts
{
    public class GenerateArtifactWorkflow : IGenerateArtifactWorkflow
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly INavTokenGenerator _navTokenGenerator;
        private readonly ISyntaxHighlighter _syntaxHighlighter;
        private readonly ITokenChartGenerator _tokenChartGenerator;
        private readonly ITokenMapper _tokenMapper;
        private readonly CartographerConfig _config;

        public GenerateArtifactWorkflow(
            IFileProcessor fileProcessor,
            INavTokenGenerator navTokenGenerator,
            ISyntaxHighlighter syntaxHighlighter,
            ITokenChartGenerator tokenChartGenerator,
            ITokenMapper tokenMapper,
            IOptions<CartographerConfig> config)
        {
            _fileProcessor = fileProcessor;
            _navTokenGenerator = navTokenGenerator;
            _syntaxHighlighter = syntaxHighlighter;
            _tokenChartGenerator = tokenChartGenerator;
            _tokenMapper = tokenMapper;
            _config = config.Value;
        }

        public async Task<ActionResponse<Artifact>> ExecGenerateDemoArtifact(string fileName, CancellationToken cancellationToken)
        {
            FileData fileData = _fileProcessor.ReadInTestFileData(fileName);
            return await GenerateArtifact(fileData, cancellationToken);
        }

        public async Task<ActionResponse<Artifact>> ExecGenerateUserArtifact(GenerateArtifactDto requestDto, CancellationToken cancellationToken)
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
             *   7. Build artifact.
             *   *  Log artifact data (optional)
             *   8. Return artifact.
             */

            try
            {
                // Step 1. Start stopwatch and set first checkpoint.
                Stopwatch stopwatch = Stopwatch.StartNew();
                TimeSpan lastCheckpoint = TimeSpan.Zero;

                // Step 2. Generate a list of nav tokens from the source file.
                var navTokens = await _navTokenGenerator.GenerateNavTokens(fileData, cancellationToken);
                var tokenGenTime = stopwatch.Elapsed - lastCheckpoint;
                lastCheckpoint = stopwatch.Elapsed;

                // Step 3. Generate a token chart for each token and it's ancestors.
                _tokenChartGenerator.GenerateTokenCharts(navTokens);
                var chartGenTime = stopwatch.Elapsed - lastCheckpoint;
                lastCheckpoint = stopwatch.Elapsed;

                // Step 4. Add semantic details to each token and it's map.
                _tokenMapper.MapNavTokens(navTokens);
                var mapTime = stopwatch.Elapsed - lastCheckpoint;
                lastCheckpoint = stopwatch.Elapsed;

                // Step 5. Add syntax highlighting for each token.
                _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(navTokens);
                var highlightTime = stopwatch.Elapsed - lastCheckpoint;
                //lastCheckpoint = stopwatch.Elapsed;

                // Step 6. Stop stopwatch and capture total elapsed time.
                stopwatch.Stop();
                var totalTime = stopwatch.Elapsed;

                // Step 7. Build artifact.
                var artifact = new Artifact(
                    fileData.FileName,
                    navTokens,
                    tokenGenTime,
                    chartGenTime,
                    mapTime,
                    highlightTime,
                    totalTime
                );

                // Bonus: Log artifact data (optional)
                LogArtifactData(artifact);

                // Step 8. Return artifact.
                return ActionResponse<Artifact>.Success(artifact);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                CartographerLogger.LogException(ex);
                return ActionResponse<Artifact>.Failure("An exception occurred during artifact generation.");
            }
        }

        private void LogArtifactData(Artifact artifact)
        {
            if (_config.ShouldLogArtifact)
                CartographerLogger.LogArtifact(artifact);

            if (_config.ShouldLogSemanticData)
            {
                var identifiers = artifact.NavTokens
                    .Where(token => token.SemanticData is not null);

                CartographerLogger.LogTokens(identifiers);
            }

            if (_config.ShouldLogUnidentifiedTokens)
            {
                var unidentifiedTokens = artifact.NavTokens
                    .Where(token => token.HighlightColor == "color-red");

                CartographerLogger.LogTokens(unidentifiedTokens);
            }
        }
    }
}
