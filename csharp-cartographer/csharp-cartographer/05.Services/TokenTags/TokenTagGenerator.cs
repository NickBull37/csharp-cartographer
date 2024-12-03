using csharp_cartographer._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer._05.Services.TokenTags
{
    public class TokenTagGenerator : ITokenTagGenerator
    {
        private static readonly List<SyntaxKind> _KindsToSkip =
        [
            //SyntaxKind.Block,
            SyntaxKind.CompilationUnit,
            //SyntaxKind.QualifiedName,
            //SyntaxKind.IdentifierToken,
            //SyntaxKind.PredefinedType,
        ];

        public TokenTagGenerator()
        {
        }

        public void AddTokenTag(NavToken token, string label, int level)
        {
            var tokenTag = new TokenTag
            {
                Label = label,
                Level = level,
                Tokens = []
            };

            token.Tags.Insert(1, tokenTag);

            foreach (var tag in token.Tags)
            {
                int count = 0;
                if (tag.Level != count)
                {
                    tag.Level = count;
                }
            }
        }

        public void GenerateTokenTags(List<NavToken> navTokens)
        {
            foreach (var navToken in navTokens)
            {
                AddTokenTag(navToken, navToken.Kind.ToString(), 1, navToken.RoslynToken);

                int level = 2;
                foreach (var parentNode in GetParentNodes(navToken.RoslynToken))
                {
                    if (_KindsToSkip.Contains(parentNode.Kind()))
                    {
                        continue;
                    }

                    AddTokenTag(navToken, parentNode.Kind().ToString(), level, parentNode);
                    level++;
                }
            }
        }

        private static IEnumerable<SyntaxNode> GetParentNodes(SyntaxToken token)
        {
            var currentNode = token.Parent;
            while (currentNode != null)
            {
                yield return currentNode;
                currentNode = currentNode.Parent;
            }
        }

        private static void AddTokenTag(NavToken navToken, string label, int level, SyntaxNodeOrToken nodeOrToken)
        {
            List<SyntaxToken> tokens;

            if (nodeOrToken.AsNode() != null && nodeOrToken.IsNode)
            {
                // If it's a SyntaxNode, get its descendant tokens
                tokens = nodeOrToken.AsNode()!.DescendantTokens().ToList();
            }
            else if (nodeOrToken.IsToken)
            {
                // If it's a SyntaxToken, wrap it in a list
                tokens = [nodeOrToken.AsToken()];
            }
            else
            {
                // Handle unexpected case (this shouldn't happen in normal Roslyn APIs)
                throw new InvalidOperationException("Unsupported SyntaxNodeOrToken type.");
            }

            var tokenTag = new TokenTag
            {
                Label = label,
                Level = level,
                Tokens = tokens
            };

            navToken.Tags.Add(tokenTag);
        }

        //private static void CleanUpTokenTags(List<NavToken> navTokens)
        //{
        //    foreach (var token in navTokens)
        //    {
        //        if (token.Tags.Count <= 0)
        //        {
        //            continue;
        //        }

        //        if (token.Tags[0].Label == "IdentifierToken"
        //            && token.Tags[1].Label == "IdentifierName")
        //        {
        //            token.Tags.RemoveAt(1);
        //            token.Tags[0].Label = "IdentifierName";
        //        }
        //    }

        //    foreach (var token in navTokens)
        //    {
        //        foreach (var tag in token.Tags)
        //        {
        //            if (tag.Label == "IdentifierToken")
        //            {
        //                tag.Label = "IdentifierName";
        //            }
        //        }
        //    }
        //}
    }
}
