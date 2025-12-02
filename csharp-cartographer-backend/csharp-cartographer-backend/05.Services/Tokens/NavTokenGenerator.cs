using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Text;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public class NavTokenGenerator : INavTokenGenerator
    {
        public async Task<List<NavToken>> GenerateNavTokens(SemanticModel semanticModel, SyntaxTree syntaxTree, Document document)
        {
            List<NavToken> navTokens = [];

            SyntaxNode root = syntaxTree.GetRoot();
            var roslynTokens = root.DescendantTokens();
            var classifiedSpans = await GetClassifiedSpans(syntaxTree, document);
            var classificationLookup = classifiedSpans
                .GroupBy(c => c.TextSpan.Start)
                .ToDictionary(g => g.Key, g => g.ToList());

            int index = 0;
            foreach (var token in roslynTokens)
            {
                var span = token.Span;
                classificationLookup.TryGetValue(token.Span.Start, out var candidates);
                var classification = candidates?
                    .FirstOrDefault(c => c.TextSpan.Length == token.Span.Length).ClassificationType;

                var newToken = new NavToken(token, semanticModel, syntaxTree, index, classification);
                navTokens.Add(newToken);

                // each token in list needs data from the ones before & after for syntax highlighting
                if (index > 0)
                {
                    var prevToken = navTokens[index - 1];
                    prevToken.NextToken = newToken;

                    if (index > 1)
                    {
                        prevToken.PrevToken = navTokens[index - 2];
                    }
                }

                index++;
            }

            return navTokens;
        }

        private async Task<IEnumerable<ClassifiedSpan>> GetClassifiedSpans(SyntaxTree syntaxTree, Document document)
        {
            var text = await syntaxTree.GetTextAsync();
            var fullSpan = new TextSpan(0, text.Length);
            return await Classifier.GetClassifiedSpansAsync(document, fullSpan) ?? [];
        }
    }
}
