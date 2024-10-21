using csharp_cartographer._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer._05.Services.TokenTags
{
    public class TokenTagGenerator : ITokenTagGenerator
    {
        public TokenTagGenerator()
        {
        }

        public void GenerateTokenTags(List<NavToken> navTokens)
        {
            foreach (var navToken in navTokens)
            {
                // add tag for the current node
                AddTokenTag(navToken, navToken.RoslynKind, 0);

                var currentNode = navToken.RoslynToken.Parent;
                int level = 1;

                // add tags for all parent nodes
                while (currentNode != null)
                {
                    //if (currentNode.IsKind(SyntaxKind.IdentifierName)
                    //    || currentNode.IsKind(SyntaxKind.QualifiedName)
                    //    || currentNode.IsKind(SyntaxKind.AliasQualifiedName)
                    //    || currentNode.IsKind(SyntaxKind.IdentifierToken)
                    //    || currentNode.IsKind(SyntaxKind.VariableDeclarator)
                    //    || currentNode.IsKind(SyntaxKind.LocalDeclarationStatement)
                    //    || currentNode.IsKind(SyntaxKind.Block)
                    //    || currentNode.IsKind(SyntaxKind.CompilationUnit))
                    //{
                    //    currentNode = currentNode.Parent;
                    //    continue;
                    //}

                    if (currentNode.IsKind(SyntaxKind.CompilationUnit)
                        || currentNode.IsKind(SyntaxKind.QualifiedName)
                        || currentNode.IsKind(SyntaxKind.PredefinedType))
                    {
                        currentNode = currentNode.Parent;
                        continue;
                    }

                    AddTokenTag(navToken, currentNode.Kind().ToString(), level);

                    currentNode = currentNode.Parent;
                    level++;
                }
            }
        }

        private static void AddTokenTag(NavToken navToken, string label, int level)
        {
            var tokenTag = new TokenTag
            {
                Label = label,
                Level = level
            };
            navToken.Tags.Add(tokenTag);
        }
    }
}
