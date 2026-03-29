using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace csharp_cartographer_backend._05.Services.Charts
{
    public class TokenChartGenerator : ITokenChartGenerator
    {
        private static readonly HashSet<SyntaxKind> _KindsToSkip =
        [
            SyntaxKind.CompilationUnit,
            SyntaxKind.EndOfFileToken,
            //SyntaxKind.Block,
        ];

        /// <summary>Iterates through NavTokens and adds a chart for itself and each ancestor node.</summary>
        /// <param name="navTokens">The list of NavTokens.</param>
        public void GenerateTokenCharts(List<NavToken> navTokens)
        {
            var tokenIndexBySpan = navTokens.ToDictionary(
                t => t.RoslynToken.FullSpan,
                t => t.Index);

            foreach (var token in navTokens)
            {
                var tokenChart = CreateSingleTokenChart(token);
                token.Charts.Add(tokenChart);

                foreach (var ancestor in GetAncestorNodes(token.RoslynToken))
                {
                    if (_KindsToSkip.Contains(ancestor.Kind()))
                        continue;

                    var ancestorChart = CreateAncestorNodeChart(
                        ancestor,
                        tokenIndexBySpan);

                    token.Charts.Add(ancestorChart);
                }
            }
        }

        private static TokenChart CreateSingleTokenChart(NavToken token)
        {
            return new TokenChart
            {
                Label = token.Kind.ToString(),
                HighlightRange = new HighlightRange
                {
                    StartIndex = token.Index,
                    EndIndex = token.Index,
                }
            };
        }

        private static TokenChart CreateAncestorNodeChart(SyntaxNode node, Dictionary<TextSpan, int> tokenIndexBySpan)
        {
            return new TokenChart
            {
                Label = node.Kind().ToString(),
                HighlightRange = TryCreateNodeHighlightRange(node, tokenIndexBySpan),
            };
        }

        private static HighlightRange? TryCreateNodeHighlightRange(SyntaxNode node, Dictionary<TextSpan, int> tokenIndexBySpan)
        {
            using var enumerator = node.DescendantTokens().GetEnumerator();

            if (!enumerator.MoveNext())
                return null;

            var firstToken = enumerator.Current;
            var lastToken = firstToken;

            while (enumerator.MoveNext())
            {
                lastToken = enumerator.Current;
            }

            if (!tokenIndexBySpan.TryGetValue(firstToken.FullSpan, out var startIndex))
                return null;

            if (!tokenIndexBySpan.TryGetValue(lastToken.FullSpan, out var endIndex))
                return null;

            return new HighlightRange
            {
                StartIndex = startIndex,
                EndIndex = endIndex,
            };
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
