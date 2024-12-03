using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text.Json.Serialization;

namespace csharp_cartographer._03.Models.TokenMap
{
    public class TokenMap
    {
        // Misc data
        public int ID { get; set; }

        public int Index { get; set; }

        public string? HighlightColor { get; set; }

        // Lexical (token) data
        public string Text { get; }

        public SyntaxKind Kind { get; set; }

        public TextSpan Span { get; set; }

        public List<string> LeadingTrivia { get; set; } = [];

        public List<string> TrailingTrivia { get; set; } = [];

        public object? Value { get; set; }

        [JsonIgnore]
        public SyntaxToken RoslynToken { get; set; }

        public SyntaxNode? Parent { get; set; }

        // Syntax data
        public string? ParentNodeKind { get; set; }

        public string? ParentNodeType { get; set; }

        // Semantic data
        public ISymbol? Symbol { get; set; }

        public string? SymbolName { get; set; }

        public string? SymbolKind { get; set; }

        public string? ContainingType { get; set; }

        public string? ContainingNamespace { get; set; }

        public string? TypeName { get; set; }

        public string? TypeKind { get; set; }

        public bool IsNullable { get; set; }

        // Contextual Data
        public List<string> References { get; set; } = [];

        public TokenMap(SyntaxToken token, SemanticModel semanticModel, SyntaxTree syntaxTree, int index)
        {
            Index = index;

            #region Lexical (token) data
            Text = token.Text;
            Kind = token.Kind();
            Span = token.Span;
            if (token.HasLeadingTrivia)
            {
                foreach (var trivia in token.LeadingTrivia)
                {
                    var triviaString = trivia.ToString();

                    if (trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia))
                    {
                        triviaString = "///" + triviaString;
                    }

                    LeadingTrivia.Add(triviaString);

                    if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia)
                        || trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia)
                        || trivia.IsKind(SyntaxKind.RegionDirectiveTrivia)
                        || trivia.IsKind(SyntaxKind.EndRegionDirectiveTrivia))
                    {
                        LeadingTrivia.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                    }
                }
            }
            if (token.HasTrailingTrivia)
            {
                foreach (var trivia in token.TrailingTrivia)
                {
                    TrailingTrivia.Add(trivia.ToString());
                }
            }
            Value = token.Value;
            RoslynToken = token;
            if (token.Parent is not null)
            {
                Parent = token.Parent;
            }
            #endregion

            #region Syntax data
            var parentNode = token.Parent;

            if (parentNode != null)
            {
                ParentNodeKind = parentNode.Kind().ToString();
                ParentNodeType = parentNode.GetType().Name;
            }
            #endregion

            #region Semantic data
            if (token.Parent is not null)
            {
                var symbolInfo = semanticModel.GetSymbolInfo(token.Parent);
                if (symbolInfo.Symbol != null)
                {
                    Symbol = symbolInfo.Symbol;
                    SymbolName = symbolInfo.Symbol.Name;
                    SymbolKind = symbolInfo.Symbol.Kind.ToString();
                    ContainingType = symbolInfo.Symbol.ContainingType?.ToString() ?? null;
                    ContainingNamespace = symbolInfo.Symbol.ContainingNamespace.ToString();
                }

                var typeInfo = semanticModel.GetTypeInfo(token.Parent);
                if (typeInfo.Type != null)
                {
                    TypeName = typeInfo.Type.Name;
                    TypeKind = typeInfo.Type.Kind.ToString();
                    IsNullable = typeInfo.Nullability.FlowState == NullableFlowState.MaybeNull;
                }
            }
            #endregion

            #region Contextual data
            var root = syntaxTree.GetRoot();
            if (token.Parent is IdentifierNameSyntax identifier)
            {
                var symbol = semanticModel.GetSymbolInfo(identifier).Symbol;
                if (symbol != null)
                {
                    var references = root.DescendantNodes()
                        .OfType<IdentifierNameSyntax>()
                        .Where(id => semanticModel.GetSymbolInfo(id).Symbol?.Equals(symbol) == true);
                }
            }
            #endregion
        }
    }
}
