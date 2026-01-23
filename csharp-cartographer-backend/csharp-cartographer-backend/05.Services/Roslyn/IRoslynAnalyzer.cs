using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public interface IRoslynAnalyzer
    {
        SyntaxTree GetSyntaxTree(FileData fileData);

        SemanticModel GetSemanticModel(SyntaxTree syntaxTree);

        void AddTokenSemanticData(
            NavToken token,
            SemanticModel semanticModel,
            SyntaxTree syntaxTree);
    }
}
