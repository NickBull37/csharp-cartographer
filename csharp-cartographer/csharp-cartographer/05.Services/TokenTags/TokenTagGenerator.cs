using csharp_cartographer._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer._05.Services.TokenTags
{
    public class TokenTagGenerator : ITokenTagGenerator
    {
        private static readonly List<SyntaxKind> _KindsToSkip =
        [
            SyntaxKind.Block,
            SyntaxKind.CompilationUnit,
            SyntaxKind.QualifiedName,
            SyntaxKind.PredefinedType,
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
                // add tag for the current node
                AddCurrentNodeTokenTag(navToken, navToken.RoslynKind, 1);

                var currentNode = navToken.RoslynToken.Parent;
                int level = 2;

                // add tags for all parent nodes
                while (currentNode != null)
                {
                    if (_KindsToSkip.Contains(currentNode.Kind()))
                    {
                        currentNode = currentNode.Parent;
                        continue;
                    }

                    //if (currentNode.IsKind(SyntaxKind.CompilationUnit)
                    //    || currentNode.IsKind(SyntaxKind.QualifiedName)
                    //    || currentNode.IsKind(SyntaxKind.PredefinedType))
                    //{
                    //    currentNode = currentNode.Parent;
                    //    continue;
                    //}

                    AddParentNodeTokenTag(navToken, currentNode, level);

                    currentNode = currentNode.Parent;
                    level++;
                }
            }
        }

        private static void AddCurrentNodeTokenTag(NavToken navToken, string roslynKind, int level)
        {
            var tokenTag = new TokenTag
            {
                Label = roslynKind,
                Level = level,
                Tokens = []
            };
            navToken.Tags.Add(tokenTag);
        }

        private static void AddParentNodeTokenTag(NavToken navToken, SyntaxNode currentNode, int level)
        {
            var textSpan = currentNode.ToString();
            var syntaxTree = CSharpSyntaxTree.ParseText(textSpan);
            SyntaxNode root = syntaxTree.GetRoot();
            var roslynTokens = root.DescendantTokens();

            var tokenTag = new TokenTag
            {
                Label = currentNode.Kind().ToString(),
                Level = level,
                Tokens = roslynTokens.ToList()
            };
            navToken.Tags.Add(tokenTag);
        }
    }
}
