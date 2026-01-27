using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public interface IRoslynAnalyzer
    {
        SyntaxTree GetSyntaxTree(FileData fileData, CancellationToken cancellationToken);

        SemanticModel GetSemanticModel(SyntaxTree syntaxTree, CancellationToken cancellationToken);

        void AddTokenSemanticData(
            NavToken token,
            SemanticModel semanticModel,
            SyntaxTree syntaxTree,
            CancellationToken cancellationToken);
    }
}
