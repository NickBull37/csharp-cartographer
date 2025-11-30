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
                var newToken = new NavToken(token, semanticModel, syntaxTree, index);
                navTokens.Add(newToken);

                // each token in list needs data from the next token for syntax highlighting
                if (index > 0)
                {
                    var prevToken = navTokens[index - 1];
                    prevToken.NextToken = newToken;
                }

                index++;
            }

            return navTokens;
        }
    }
}
