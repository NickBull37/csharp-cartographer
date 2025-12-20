using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Charts
{
    public class TokenChartGenerator : ITokenChartGenerator
    {
        private static readonly List<SyntaxKind> _KindsToSkip =
        [
            SyntaxKind.CompilationUnit,
            SyntaxKind.QualifiedName,
            //SyntaxKind.Block,
            //SyntaxKind.IdentifierToken,
            //SyntaxKind.PredefinedType,
        ];

        /// <summary>Iterates through NavTokens and adds a chart for itself and each parent node.</summary>
        /// <param name="navTokens">The list of NavTokens.</param>
        public void GenerateTokenCharts(List<NavToken> navTokens)
        {
            foreach (var navToken in navTokens)
            {
                int level = 1;
                AddTokenChart(navToken, navToken.Kind.ToString(), level, navToken.RoslynToken);
                level++;

                foreach (var parentNode in GetParentNodes(navToken.RoslynToken))
                {
                    if (_KindsToSkip.Contains(parentNode.Kind()))
                    {
                        continue;
                    }

                    AddTokenChart(navToken, parentNode.Kind().ToString(), level, parentNode);
                    level++;
                }
            }
        }

        private static void AddTokenChart(NavToken navToken, string label, int level, SyntaxNodeOrToken nodeOrToken)
        {
            List<SyntaxToken> tokens;

            if (nodeOrToken.AsNode() != null && nodeOrToken.IsNode)
            {
                // if it's a SyntaxNode, get its descendant tokens
                tokens = nodeOrToken.AsNode()!.DescendantTokens().ToList();
            }
            else if (nodeOrToken.IsToken)
            {
                // if it's a SyntaxToken, wrap it in a list
                tokens = [nodeOrToken.AsToken()];
            }
            else
            {
                // handle unexpected case (this shouldn't happen in normal Roslyn APIs)
                throw new InvalidOperationException("Unsupported SyntaxNodeOrToken type.");
            }

            var tokenChart = new TokenChart
            {
                Label = label,
                Level = level,
                Tokens = tokens
            };

            navToken.Charts.Add(tokenChart);
        }

        private static IEnumerable<SyntaxNode> GetParentNodes(SyntaxToken token)
        {
            var parentNode = token.Parent;
            while (parentNode != null)
            {
                yield return parentNode;
                parentNode = parentNode.Parent;
            }
        }
    }
}
