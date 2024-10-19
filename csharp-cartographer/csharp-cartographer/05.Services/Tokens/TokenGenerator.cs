using csharp_cartographer._03.Models.Tokens;
using Microsoft.CodeAnalysis;

namespace csharp_cartographer._05.Services.Tokens
{
    public class TokenGenerator : ITokenGenerator
    {
        public TokenGenerator()
        {
        }

        public List<NavToken> GenerateNavTokens(SyntaxTree syntaxTree)
        {
            SyntaxNode root = syntaxTree.GetRoot();
            var roslynTokens = root.DescendantTokens();
            return ConvertRoslynTokensToNavTokens(roslynTokens);
        }

        private static List<NavToken> ConvertRoslynTokensToNavTokens(IEnumerable<SyntaxToken> roslynTokens)
        {
            int index = 0;
            List<NavToken> navTokens = [];
            foreach (var token in roslynTokens)
            {
                navTokens.Add(new NavToken(token, index));
                index++;
            }
            return navTokens;
        }
    }
}
