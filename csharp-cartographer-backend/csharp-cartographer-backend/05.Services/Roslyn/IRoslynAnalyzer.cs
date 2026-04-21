using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public interface IRoslynAnalyzer
    {
        SyntaxTree GetSyntaxTree(FileData fileData, CancellationToken cancellationToken);

        SemanticModel GetSemanticModel(SyntaxTree syntaxTree, CancellationToken cancellationToken);

        void AddSemanticData(
            NavToken token,
            SemanticModel semanticModel,
            SyntaxTree syntaxTree,
            CancellationToken cancellationToken);

        void AddClassificationData(
            NavToken navToken,
            SyntaxToken syntaxToken,
            IReadOnlyDictionary<int, List<ClassifiedSpan>> classificationLookup);
    }
}
