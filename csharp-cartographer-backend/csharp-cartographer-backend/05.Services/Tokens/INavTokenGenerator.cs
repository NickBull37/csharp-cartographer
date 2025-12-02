using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public interface INavTokenGenerator
    {
        Task<List<NavToken>> GenerateNavTokens(SemanticModel semanticModel, SyntaxTree syntaxTree, Document document);
    }
}
