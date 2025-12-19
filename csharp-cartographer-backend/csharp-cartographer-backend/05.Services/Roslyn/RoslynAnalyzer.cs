using csharp_cartographer_backend._03.Models.Files;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public class RoslynAnalyzer : IRoslynAnalyzer
    {
        public SyntaxTree GetSyntaxTree(FileData fileData)
        {
            return CSharpSyntaxTree.ParseText(fileData.Content);
        }

        public SemanticModel GetSemanticModel(SyntaxTree syntaxTree)
        {
            var compilationUnit = CSharpCompilation.Create("ArtifactCompilation")
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));

            return compilationUnit.GetSemanticModel(syntaxTree);
        }
    }
}
