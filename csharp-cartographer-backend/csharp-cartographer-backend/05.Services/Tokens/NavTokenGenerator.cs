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
            var syntaxTokens = root.DescendantTokens().SkipLast(1); // skips EndOfFile token
            var classificationLookup = await BuildClassificationLookup(syntaxTree, fileData.Document, cancellationToken);

            List<NavToken> navTokens = [];
            int index = 0;

            foreach (var syntaxToken in syntaxTokens)
            {
                var navToken = new NavToken(syntaxToken, index);
                navTokens.Add(navToken);

                // save prev & next tokens for easy lookups
                if (index > 0)
                {
                    var prevToken = navTokens[index - 1];

                    navToken.PrevToken = prevToken;
                    prevToken.NextToken = navToken;
                }

                _roslynAnalyzer.AddSemanticData(navToken, semanticModel, syntaxTree, cancellationToken);
                _roslynAnalyzer.AddClassificationData(navToken, syntaxToken, classificationLookup);

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
                .GroupBy(span => span.TextSpan.Start)
                .ToDictionary(group => group.Key, group => group.ToList());
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
