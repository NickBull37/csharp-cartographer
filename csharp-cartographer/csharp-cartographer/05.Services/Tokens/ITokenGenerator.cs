using csharp_cartographer._03.Models.Tokens;
using Microsoft.CodeAnalysis;

namespace csharp_cartographer._05.Services.Tokens
{
    public interface ITokenGenerator
    {
        List<NavToken> GenerateNavTokens(SemanticModel semanticModel, SyntaxTree syntaxTree);
    }
}
