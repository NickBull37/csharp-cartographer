using csharp_cartographer_backend._01.Configuration.Configs;
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

        public async Task<Artifact> ExecGenerateDemoArtifact(string fileName, CancellationToken cancellationToken)
        {
            FileData fileData = _fileProcessor.ReadInTestFileData(fileName);
            return await GenerateArtifact(fileData, cancellationToken);
        }

        public async Task<Artifact> ExecGenerateUserArtifact(GenerateArtifactDto requestDto, CancellationToken cancellationToken)
        {
            FileData fileData = _fileProcessor.ReadInFileData(requestDto);
            return await GenerateArtifact(fileData, cancellationToken);
        }

        private async Task<Artifact> GenerateArtifact(FileData fileData, CancellationToken cancellationToken)
        {
            /*
             *   Steps to generate an artifact:
             * 
             *   0. Read in source code from user uploaded file & generate FileData.
             *   1. Start stopwatch.
             *   2. Generate a list of NavTokens from source file's SyntaxTree.
             *   3. Generate ancestor charts for each NavToken.
             *   4. Map NavTokens.
             *   5. Add TokenTags definitions & insights.
             *   6. Add syntax highlighting for NavTokens.
             *   7. Build artifact.
             *   8. Stop stopwatch.
             *   9. Return artifact.
             *   
             */

            // Step 1. Start stopwatch.
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Step 2. Turn the Roslyn data into a list of NavTokens.
            var navTokens = await _navTokenGenerator.GenerateNavTokens(fileData, cancellationToken);

            // Step 3. Generate ancestor charts for each token.
            _tokenChartGenerator.GenerateTokenCharts(navTokens);

            // Step 4. Map NavTokens
            _tokenMapper.MapNavTokens(navTokens);

            // Step 5. Add token chart definitions & insights.
            //_tokenChartWizard.AddFactsAndInsightsToNavTokenCharts(navTokens);

            // Step 6. Add syntax highlighting for NavTokens.
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(navTokens);

            // Step 7. Build & return artifact.
            var artifact = new Artifact(fileData.FileName, stopwatch.Elapsed, navTokens);

            // Step 8. Stop stopwatch.
            stopwatch.Stop();

            // Bonus: Log artifact data (optional)
            LogArtifactData(artifact);

            // Step 9. Return artifact.
            return artifact;
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
