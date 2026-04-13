using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace csharp_cartographer_backend._05.Services.Charts
{
    public class TokenChartGenerator : ITokenChartGenerator
    {
        private static readonly HashSet<SyntaxKind> KindsToSkip =
        [
            SyntaxKind.CompilationUnit,
            SyntaxKind.EndOfFileToken,
            //SyntaxKind.Block,
        ];

        /// <summary>
        /// Generates a chart for each token and its ancestor nodes.
        /// A cache of ancestor nodes and their charts is maintained so charts
        /// can be reused when consecutive tokens share the same ancestors.
        /// </summary>
        /// <param name="navTokens">The list of NavTokens to generate charts for.</param>
        public void GenerateTokenCharts(IReadOnlyList<NavToken> navTokens)
        {
            var cachedAncestors = new Dictionary<SyntaxNode, TokenChart>();

            var tokenIndexBySpan = navTokens.ToDictionary(
                token => token.RoslynToken.FullSpan,
                token => token.Index);

            foreach (var token in navTokens)
            {
                token.Charts.Add(CreateTokenChart(token));

                var ancestorNodes = GetAncestorNodes(token.RoslynToken)
                    .Where(node => !KindsToSkip.Contains(node.Kind()))
                    .ToList();

                foreach (var ancestorNode in ancestorNodes)
                {
                    if (KindsToSkip.Contains(ancestorNode.Kind()))
                        continue;

                    if (cachedAncestors.TryGetValue(ancestorNode, out var cachedChart))
                    {
                        token.Charts.Add(cachedChart);
                    }
                    else
                    {
                        token.Charts.Add(CreateAncestorNodeChart(ancestorNode, tokenIndexBySpan));
                    }
                }

                UpdateAncestorCache(ancestorNodes, token.Charts, cachedAncestors);
            }

            // TODO: trim single token ancestors
        }

        private static TokenChart CreateTokenChart(NavToken token)
        {
            return new TokenChart(
                token.Kind.ToString(),
                new HighlightRange(token.Index, token.Index));
        }

        private static TokenChart CreateAncestorNodeChart(
            SyntaxNode node,
            Dictionary<TextSpan, int> tokenIndexBySpan)
        {
            return new TokenChart(
                node.Kind().ToString(),
                TryGetNodeHighlightRange(node, tokenIndexBySpan));
        }

        private static HighlightRange? TryGetNodeHighlightRange(
            SyntaxNode node,
            Dictionary<TextSpan, int> tokenIndexBySpan)
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

            return new HighlightRange(startIndex, endIndex);
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

        private static void UpdateAncestorCache(
            IEnumerable<SyntaxNode> ancestors,
            IEnumerable<TokenChart> charts,
            Dictionary<SyntaxNode, TokenChart> cachedAncestors)
        {
            cachedAncestors.Clear();

            // the first chart is always for the token itself, skip caching this one
            foreach (var (ancestor, chart) in ancestors.Zip(charts.Skip(1)))
            {
                cachedAncestors[ancestor] = chart;
            }
        }
    }
}
