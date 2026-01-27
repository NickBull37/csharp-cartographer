using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._05.Services.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Text;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public class NavTokenGenerator : INavTokenGenerator
    {
        private readonly IRoslynAnalyzer _roslynAnalyzer;

        public NavTokenGenerator(IRoslynAnalyzer roslynAnalyzer)
        {
            _roslynAnalyzer = roslynAnalyzer;
        }

        public async Task<List<NavToken>> GenerateNavTokens(FileData fileData, CancellationToken cancellationToken)
        {
            var syntaxTree = _roslynAnalyzer.GetSyntaxTree(fileData, cancellationToken);
            var semanticModel = _roslynAnalyzer.GetSemanticModel(syntaxTree, cancellationToken);

            SyntaxNode root = syntaxTree.GetRoot(cancellationToken);
            var roslynTokens = root.DescendantTokens();
            var classificationLookup = await BuildClassificationLookup(syntaxTree, fileData.Document, cancellationToken);

            List<NavToken> navTokens = [];
            int index = 0;

            foreach (var token in roslynTokens)
            {
                var newToken = new NavToken(token, index);

                var classification = GetTokenClassification(token, classificationLookup);
                newToken.RoslynClassification = classification;

                _roslynAnalyzer.AddTokenSemanticData(newToken, semanticModel, syntaxTree, cancellationToken);

                navTokens.Add(newToken);

                // each token in list needs data from the ones before & after for token mapping
                if (index > 0)
                {
                    var prevToken = navTokens[index - 1];

                    newToken.PrevToken = prevToken;
                    prevToken.NextToken = newToken;
                }
                index++;
            }

            return navTokens;
        }

        private static async Task<IReadOnlyDictionary<int, List<ClassifiedSpan>>> BuildClassificationLookup(
            SyntaxTree syntaxTree,
            Document document,
            CancellationToken cancellationToken)
        {
            var classifiedSpans = await GetClassifiedSpans(syntaxTree, document, cancellationToken);

            return classifiedSpans
                .GroupBy(c => c.TextSpan.Start)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        private static string? GetTokenClassification(SyntaxToken token, IReadOnlyDictionary<int, List<ClassifiedSpan>> classificationLookup)
        {
            if (!classificationLookup.TryGetValue(token.Span.Start, out var candidates) || candidates is null)
            {
                return null;
            }
            return candidates.FirstOrDefault(c => c.TextSpan.Length == token.Span.Length).ClassificationType;
        }

        private static async Task<IEnumerable<ClassifiedSpan>> GetClassifiedSpans(
            SyntaxTree syntaxTree,
            Document document,
            CancellationToken cancellationToken)
        {
            var text = await syntaxTree.GetTextAsync(cancellationToken);
            var fullSpan = new TextSpan(0, text.Length);

            return await Classifier.GetClassifiedSpansAsync(document, fullSpan, cancellationToken) ?? [];
        }
    }
}
