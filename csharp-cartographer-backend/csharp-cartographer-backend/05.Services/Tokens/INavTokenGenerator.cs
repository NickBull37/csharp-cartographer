using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public interface INavTokenGenerator
    {
        List<NavToken> GenerateNavTokens(SemanticModel semanticModel, SyntaxTree syntaxTree);
    }
}
