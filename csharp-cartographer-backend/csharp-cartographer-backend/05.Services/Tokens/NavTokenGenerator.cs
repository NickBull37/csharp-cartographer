using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public class NavTokenGenerator : INavTokenGenerator
    {
        public List<NavToken> GenerateNavTokens(SemanticModel semanticModel, SyntaxTree syntaxTree)
        {
            List<NavToken> navTokens = [];

            SyntaxNode root = syntaxTree.GetRoot();
            var roslynTokens = root.DescendantTokens();

            int index = 0;
            foreach (var token in roslynTokens)
            {
                navTokens.Add(new NavToken(token, semanticModel, syntaxTree, index));
                index++;
            }

            return navTokens;
        }
    }
}
