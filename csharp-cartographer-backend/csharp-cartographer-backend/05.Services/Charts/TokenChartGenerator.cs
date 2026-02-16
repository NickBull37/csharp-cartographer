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
            SyntaxKind.EndOfFileToken,
            //SyntaxKind.QualifiedName,
            //SyntaxKind.Block,
            //SyntaxKind.IdentifierToken,
            //SyntaxKind.PredefinedType,
        ];

        /// <summary>Iterates through NavTokens and adds a chart for itself and each parent node.</summary>
        /// <param name="navTokens">The list of NavTokens.</param>
        public void GenerateTokenCharts(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                // add chart for the token itself
                CreateAndAddChart(token, token.Kind.ToString(), token.RoslynToken);

                // add chart for each ancestor
                foreach (var ancestor in GetAncestorNodes(token.RoslynToken))
                {
                    if (_KindsToSkip.Contains(ancestor.Kind()))
                        continue;

                    CreateAndAddChart(token, ancestor.Kind().ToString(), ancestor);
                }
            }
        }

        private static void CreateAndAddChart(NavToken navToken, string label, SyntaxNodeOrToken nodeOrToken)
        {
            List<SyntaxToken> tokens = [];

            if (nodeOrToken.AsNode() != null && nodeOrToken.IsNode)
            {
                // if it's a SyntaxNode, get its descendant tokens
                tokens = nodeOrToken.AsNode()!.DescendantTokens().ToList();
            }
            else if (nodeOrToken.IsToken)
            {
                // if it's a singular SyntaxToken there's no need for descendants
                tokens = [nodeOrToken.AsToken()];
            }

            var tokenChart = new TokenChart
            {
                Label = label,
                Tokens = tokens
            };

            navToken.Charts.Add(tokenChart);
        }

        private static IEnumerable<SyntaxNode> GetAncestorNodes(SyntaxToken token)
        {
            var parent = token.Parent;
            while (parent != null)
            {
                yield return parent;
                parent = parent.Parent;
            }
        }
    }
}
