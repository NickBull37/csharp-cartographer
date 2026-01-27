using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._02.Utilities.Logging;
using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._05.Services.Charts;
using csharp_cartographer_backend._05.Services.Files;
using csharp_cartographer_backend._05.Services.Roslyn;
using csharp_cartographer_backend._05.Services.SyntaxHighlighting;
using csharp_cartographer_backend._05.Services.Tags;
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
        private readonly IRoslynAnalyzer _roslynAnalyzer;
        private readonly ISyntaxHighlighter _syntaxHighlighter;
        private readonly ITokenChartGenerator _tokenChartGenerator;
        private readonly ITokenChartWizard _tokenChartWizard;
        private readonly ITokenTagGenerator _tokenTagGenerator;
        private readonly ITokenMapper _tokenMapper;
        private readonly CartographerConfig _config;

        public GenerateArtifactWorkflow(
            IFileProcessor fileProcessor,
            INavTokenGenerator navTokenGenerator,
            IRoslynAnalyzer roslynAnalyzer,
            ISyntaxHighlighter syntaxHighlighter,
            ITokenChartGenerator tokenChartGenerator,
            ITokenChartWizard tokenChartWizard,
            ITokenTagGenerator tokenTagGenerator,
            ITokenMapper tokenMapper,
            IOptions<CartographerConfig> config)
        {
            _fileProcessor = fileProcessor;
            _navTokenGenerator = navTokenGenerator;
            _roslynAnalyzer = roslynAnalyzer;
            _syntaxHighlighter = syntaxHighlighter;
            _tokenChartGenerator = tokenChartGenerator;
            _tokenChartWizard = tokenChartWizard;
            _tokenTagGenerator = tokenTagGenerator;
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
             *   2. Generate SyntaxTree with passed in FileData.
             *   3. Generate SemanticModel with CompilationUnit & SyntaxTree.
             *   4. Turn the Roslyn data into a list of NavTokens.
             *   5. Generate ancestor charts for each token.
             *   6. Map NavTokens
             *   7. Generate TokenTags.
             *   8. Add highlight indices to token charts for highlighting ancestors.
             *   9. Add TokenTags definitions & insights.
             *   10. Add syntax highlighting for all NavTokens (should be last step in workflow).
             *   11. Stop stopwatch.
             *   12. Build & return artifact.
             *   
             */

            // Step 1. Start stopwatch.
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Step 2. Generate SyntaxTree with passed in FileData.
            //var syntaxTree = _roslynAnalyzer.GetSyntaxTree(fileData);

            //// Step 3. Generate SemanticModel with CompilationUnit & SyntaxTree.
            //var semanticModel = _roslynAnalyzer.GetSemanticModel(syntaxTree);

            // Step 4. Turn the Roslyn data into a list of NavTokens.
            var navTokens = await _navTokenGenerator.GenerateNavTokens(fileData, cancellationToken);

            // Step 5. Generate ancestor charts for each token.
            _tokenChartGenerator.GenerateTokenCharts(navTokens);

            // Step 6. Map NavTokens
            _tokenMapper.MapNavTokens(navTokens);

            // Step 7. Generate TokenTags.
            _tokenTagGenerator.GenerateTokenTags(navTokens);

            // Step 8. Add highlight indices to token charts for highlighting ancestors.
            _tokenChartWizard.AddHighlightValuesToNavTokenCharts(navTokens);

            // Step 9. Add token chart definitions & insights.
            _tokenChartWizard.AddFactsAndInsightsToNavTokenCharts(navTokens);

            // Step 10. Add syntax highlighting for all NavTokens (should be last step in workflow).
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(navTokens);

            // Step 11. Stop stopwatch.
            stopwatch.Stop();

            // Step 12. Build & return artifact.
            var artifact = new Artifact(fileData.FileName, stopwatch.Elapsed, navTokens);

            // Bonus: Log artifact data (optional)
            LogArtifactData(artifact);

            return artifact;
        }

        private void LogArtifactData(Artifact artifact)
        {
            if (_config.ShouldLogArtifact)
                CartographerLogger.LogArtifact(artifact);

            if (_config.ShouldLogUnidentifiedTokens)
            {
                var unidentifiedTokens = artifact.NavTokens
                    .Where(token => token.HighlightColor == "color-red");

                CartographerLogger.LogTokens(unidentifiedTokens);
            }
        }
    }
}
