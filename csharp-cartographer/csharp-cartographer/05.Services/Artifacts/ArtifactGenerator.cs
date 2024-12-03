using csharp_cartographer._02.Utilities.Logging;
using csharp_cartographer._03.Models.Artifacts;
using csharp_cartographer._03.Models.FileProcessing;
using csharp_cartographer._05.Services.Cartography;
using csharp_cartographer._05.Services.FileProcessing;
using csharp_cartographer._05.Services.SyntaxHighlighting;
using csharp_cartographer._05.Services.Tokens;
using csharp_cartographer._05.Services.TokenTags;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer._05.Services.Artifacts
{
    public class ArtifactGenerator : IArtifactGenerator
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly ISyntaxHighlighter _syntaxHighlighter;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ITokenTagGenerator _tokenTagGenerator;
        private readonly ITokenTagWizard _tokenTagWizard;
        private readonly ICartographer _cartographer;
        private readonly ITagIndexer _tagIndexer;

        public ArtifactGenerator(IFileProcessor fileProcessor,
            ISyntaxHighlighter syntaxHighlighter,
            ITokenGenerator tokenGenerator,
            ITokenTagGenerator tokenTagGenerator,
            ITokenTagWizard tokenTagWizard,
            ICartographer cartographer,
            ITagIndexer tagIndexer)
        {
            _fileProcessor = fileProcessor;
            _syntaxHighlighter = syntaxHighlighter;
            _tokenGenerator = tokenGenerator;
            _tokenTagGenerator = tokenTagGenerator;
            _tokenTagWizard = tokenTagWizard;
            _cartographer = cartographer;
            _tagIndexer = tagIndexer;
        }

        public Artifact GenerateDemoArtifact(string fileName)
        {
            /*
             * Steps.
             *  1. removal of any data from tokens or tags should be the last step in the process
             */

            // Step 1. read in source code
            FileData fileData = _fileProcessor.ReadInTestFileData(fileName);

            // Step 2. generate syntax tree
            var syntaxTree = CSharpSyntaxTree.ParseText(fileData.Content);

            // Step 3. generate compilation unit
            var compilationUnit = CSharpCompilation.Create("ArtifactCompilation")
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));

            // Step 4. generate semantic model
            var semanticModel = compilationUnit.GetSemanticModel(syntaxTree);

            // Step 5. use syntax tree to generate nav tokens
            var navTokens = _tokenGenerator.GenerateNavTokens(semanticModel, syntaxTree);

            _cartographer.AddNavigationCharts(navTokens);

            // Step 6. generate token tags
            _tokenTagGenerator.GenerateTokenTags(navTokens);

            // Step 7. add token tags highlight indices
            _tagIndexer.AddHighlightIndicesToTags(navTokens);

            // Step 8. add token tags definitions & insights
            _tokenTagWizard.AddFactsAndInsightsToTags(navTokens);

            // Step 8. add syntax highlighting
            _syntaxHighlighter.HighlightNavTokens(navTokens);

            TokenLogger.LogTokenList(navTokens);

            //_tokenTagWizard.CleanUpTokenTags(navTokens);

            // Step 9. build artifact
            Artifact artifact = new()
            {
                CreatedDate = DateTime.Now,
                ArtifactType = "Model Class",
                Title = fileData.FileName,
                NavTokens = navTokens,
            };

            // Step 10. return artifact
            return artifact;
        }
    }
}
