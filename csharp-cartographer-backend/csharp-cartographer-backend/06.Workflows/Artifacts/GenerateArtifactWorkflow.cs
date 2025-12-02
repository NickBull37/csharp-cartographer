using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._02.Utilities.Logging;
using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._05.Services.Charts;
using csharp_cartographer_backend._05.Services.Files;
using csharp_cartographer_backend._05.Services.SyntaxHighlighting;
using csharp_cartographer_backend._05.Services.Tags;
using csharp_cartographer_backend._05.Services.Tokens;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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
        private readonly ITokenChartWizard _tokenChartWizard;
        private readonly ITokenTagGenerator _tokenTagGenerator;
        private readonly CartographerConfig _config;

        public GenerateArtifactWorkflow(
            IFileProcessor fileProcessor,
            INavTokenGenerator navTokenGenerator,
            ISyntaxHighlighter syntaxHighlighter,
            ITokenChartGenerator tokenChartGenerator,
            ITokenChartWizard tokenChartWizard,
            ITokenTagGenerator tokenTagGenerator,
            IOptions<CartographerConfig> config)
        {
            _fileProcessor = fileProcessor;
            _navTokenGenerator = navTokenGenerator;
            _syntaxHighlighter = syntaxHighlighter;
            _tokenChartGenerator = tokenChartGenerator;
            _tokenChartWizard = tokenChartWizard;
            _tokenTagGenerator = tokenTagGenerator;
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
             *   5. Use SyntaxTree & SemanticModel to generate NavTokens for the artifact.
             *   6. Generate TokenTags.
             *   7. Generate TokenCharts.
             *   8. Add TokenCharts highlight indices for highlighting in the code viewer.
             *   9. Add TokenTags definitions & insights.
             *   10. Add syntax highlighting for all NavTokens (should be last step in workflow).
             *   11. Stop stopwatch.
             *   12. Build & return artifact.
             *   
             */

            // Step 1. Start stopwatch.
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Step 2. Generate SyntaxTree with passed in FileData.
            var syntaxTree = CSharpSyntaxTree.ParseText(fileData.Content);

            // Step 3. Generate CompilationUnit with SyntaxTree.
            var compilationUnit = CSharpCompilation.Create("ArtifactCompilation")
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));

            // Step 4. Generate SemanticModel with CompilationUnit & SyntaxTree.
            var semanticModel = compilationUnit.GetSemanticModel(syntaxTree);

            // Step 5. Use SyntaxTree & SemanticModel to generate NavTokens for the artifact.
            var navTokens = await _navTokenGenerator.GenerateNavTokens(semanticModel, syntaxTree, fileData.Document);

            // Step 6. Generate TokenTags.
            _tokenTagGenerator.GenerateTokenTags(navTokens);

            // Step 7. Generate TokenCharts.
            _tokenChartGenerator.GenerateTokenCharts(navTokens);

            // Step 8. Add TokenCharts highlight indices for highlighting in the code viewer.
            _tokenChartWizard.AddHighlightValuesToNavTokenCharts(navTokens);

            // Step 9. Add TokenCharts definitions & insights.
            _tokenChartWizard.AddFactsAndInsightsToNavTokenCharts(navTokens);

            // Step 10. Add syntax highlighting for all NavTokens (should be last step in workflow).
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(navTokens);

            // Step 11. Stop stopwatch.
            stopwatch.Stop();

            // Step 12. Build & return artifact.
            var artifact = new Artifact(fileData.FileName, stopwatch.Elapsed, navTokens);

            // Bonus: Log artifact (optional)
            if (_config.LogArtifact)
            {
                TokenLogger.LogArtifact(artifact);
            }

            return artifact;
        }
    }
}
