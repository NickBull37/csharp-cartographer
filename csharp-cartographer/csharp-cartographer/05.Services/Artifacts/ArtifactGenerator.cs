using csharp_cartographer._03.Models.Artifacts;
using csharp_cartographer._03.Models.FileProcessing;
using csharp_cartographer._05.Services.FileProcessing;
using csharp_cartographer._05.Services.Tokens;
using csharp_cartographer._05.Services.TokenTags;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer._05.Services.Artifacts
{
    public class ArtifactGenerator
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ITokenTagGenerator _tokenTagGenerator;

        public ArtifactGenerator(IFileProcessor fileProcessor, ITokenGenerator tokenGenerator, ITokenTagGenerator tokenTagGenerator)
        {
            _fileProcessor = fileProcessor;
            _tokenGenerator = tokenGenerator;
            _tokenTagGenerator = tokenTagGenerator;
        }

        public Artifact GenerateDemoArtifact()
        {
            // Step 1. read in source code
            FileData fileData = _fileProcessor.ReadInTestFileData();

            // Step 2. generate syntax tree
            var syntaxTree = CSharpSyntaxTree.ParseText(fileData.Content);

            // Step 3. use syntax tree to generate nav tokens
            var navTokens = _tokenGenerator.GenerateNavTokens(syntaxTree);

            _tokenTagGenerator.GenerateTokenTags(navTokens);

            _tokenWizard.UpdateTokenTags(navTokens);

            _tokenWizard.AddTokenTagData(navTokens);

            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(navTokens);

            Artifact artifact = new()
            {
                CreatedDate = DateTime.Now,
                ArtifactType = "Model Class",
                Title = fileData.FileName,
                Language = "C#",
                NavTokens = navTokens,
            };

            return artifact;
        }
    }
}
