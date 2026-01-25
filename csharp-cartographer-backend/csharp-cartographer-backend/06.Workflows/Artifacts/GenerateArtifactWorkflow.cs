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

        public async Task<Artifact> ExecGenerateDemoArtifact(string fileName)
        {
            FileData fileData = _fileProcessor.ReadInTestFileData(fileName);
            return await GenerateArtifact(fileData);
        }

        public async Task<Artifact> ExecGenerateUserArtifact(GenerateArtifactDto requestDto)
        {
            FileData fileData = _fileProcessor.ReadInFileData(requestDto);
            return await GenerateArtifact(fileData);
        }

        private async Task<Artifact> GenerateArtifact(FileData fileData)
        {
            /*
             *   Steps to generate an artifact:
             * 
             *   0. Read in source code from user uploaded file & generate FileData.
             *   1. Start stopwatch.
             *   2. Generate SyntaxTree with passed in FileData.
             *   3. Generate CompilationUnit with SyntaxTree.
             *   4. Generate SemanticModel with CompilationUnit & SyntaxTree.
             *   5. Turn the Roslyn data into a list of NavTokens.
             *   6. Generate TokenCharts.
             *   7. Map NavTokens
             *   8. Correct roslyn generated classifications that don't provide much info
             *   9. Generate TokenTags.
             *   10. Add TokenCharts highlight indices for highlighting in the code viewer.
             *   11. Add TokenTags definitions & insights.
             *   12. Add syntax highlighting for all NavTokens (should be last step in workflow).
             *   13. Trim charts that are useful for highlighting but not useful anymore.
             *   14. Stop stopwatch.
             *   15. Build & return artifact.
             *   
             */

            // Step 1. Start stopwatch.
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Step 2. Generate SyntaxTree with passed in FileData.
            var syntaxTree = _roslynAnalyzer.GetSyntaxTree(fileData);

            // Step 4. Generate SemanticModel with CompilationUnit & SyntaxTree.
            var semanticModel = _roslynAnalyzer.GetSemanticModel(syntaxTree);

            // Step 5. Turn the Roslyn data into a list of NavTokens.
            var navTokens = await _navTokenGenerator.GenerateNavTokens(semanticModel, syntaxTree, fileData.Document);

            //_roslynAnalyzer.AddTokenSemanticData(navTokens);

            // Step 6. Generate TokenCharts.
            _tokenChartGenerator.GenerateTokenCharts(navTokens);

            // Step 7. Map NavTokens
            _tokenMapper.MapNavTokens(navTokens);

            // Step 8. Correct roslyn generated classifications that don't provide much info
            //_classificationWizard.CorrectTokenClassifications(navTokens);

            // Step 9. Generate TokenTags.
            _tokenTagGenerator.GenerateTokenTags(navTokens);

            // Step 10. Add TokenCharts highlight indices for highlighting in the code viewer.
            _tokenChartWizard.AddHighlightValuesToNavTokenCharts(navTokens);

            // Step 11. Add TokenCharts definitions & insights.
            _tokenChartWizard.AddFactsAndInsightsToNavTokenCharts(navTokens);

            // Step 12. Add syntax highlighting for all NavTokens (should be last step in workflow).
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(navTokens);

            // Step 13. Trim charts that are useful for highlighting but not useful anymore.
            //_tokenChartWizard.RemoveExcessChartsFromNavTokens(navTokens);

            // Step 14. Stop stopwatch.
            stopwatch.Stop();

            // Step 15. Build & return artifact.
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
