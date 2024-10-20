using csharp_cartographer._03.Models.Artifacts;
using csharp_cartographer._03.Models.FileProcessing;
using csharp_cartographer._05.Services.FileProcessing;
using csharp_cartographer._05.Services.SyntaxHighlighting;
using csharp_cartographer._05.Services.Tokens;
using csharp_cartographer._05.Services.TokenTags;
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

        public ArtifactGenerator(IFileProcessor fileProcessor,
            ISyntaxHighlighter syntaxHighlighter,
            ITokenGenerator tokenGenerator,
            ITokenTagGenerator tokenTagGenerator,
            ITokenTagWizard tokenTagWizard)
        {
            _fileProcessor = fileProcessor;
            _syntaxHighlighter = syntaxHighlighter;
            _tokenGenerator = tokenGenerator;
            _tokenTagGenerator = tokenTagGenerator;
            _tokenTagWizard = tokenTagWizard;

        }

        public Artifact GenerateDemoArtifact(string fileName)
        {
            // Step 1. read in source code
            FileData fileData = _fileProcessor.ReadInTestFileData(fileName);

            // Step 2. generate syntax tree
            var syntaxTree = CSharpSyntaxTree.ParseText(fileData.Content);

            // Step 3. use syntax tree to generate nav tokens
            var navTokens = _tokenGenerator.GenerateNavTokens(syntaxTree);

            _tokenTagGenerator.GenerateTokenTags(navTokens);

            _tokenTagWizard.UpdateNavTokenTags(navTokens);

            _syntaxHighlighter.HighlightNavTokens(navTokens);

            Artifact artifact = new()
            {
                CreatedDate = DateTime.Now,
                ArtifactType = "Model Class",
                Title = fileData.FileName,
                NavTokens = navTokens,
            };

            return artifact;
        }
    }
}
