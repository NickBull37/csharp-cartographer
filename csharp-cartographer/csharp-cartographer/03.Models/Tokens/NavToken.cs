using csharp_cartographer._02.Utilities.Helpers;
using csharp_cartographer._03.Models.Charts;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text.Json.Serialization;

namespace csharp_cartographer._03.Models.Tokens
{
    public class NavToken
    {
        // Misc data
        public int ID { get; set; }

        public int Index { get; set; }

        public List<TokenTag> Tags { get; set; } = [];

        // Lexical (token) data
        public string Text { get; }

        [JsonIgnore]
        public SyntaxKind Kind { get; set; }

        public string RoslynKind { get; set; }

        [JsonIgnore]
        public TextSpan Span { get; set; }

        public List<string> LeadingTrivia { get; set; } = [];

        public List<string> TrailingTrivia { get; set; } = [];

        [JsonIgnore]
        public object? Value { get; set; }

        [JsonIgnore]
        public SyntaxToken RoslynToken { get; set; }

        [JsonIgnore]
        public SyntaxNode? Parent { get; set; }

        [JsonIgnore]
        public SyntaxNode? GrandParent { get; set; }

        [JsonIgnore]
        public SyntaxNode? GreatGrandParent { get; set; }

        // Syntax data
        public string? ParentNodeKind { get; set; }

        public string? GrandParentNodeKind { get; set; }

        public string? GreatGrandParentNodeKind { get; set; }

        public string? ParentNodeType { get; set; }

        public string? GrandParentNodeType { get; set; }

        public string? GreatGrandParentNodeType { get; set; }

        // Semantic data
        [JsonIgnore]
        public ISymbol? Symbol { get; set; }

        public string? SymbolName { get; set; }

        public string? SymbolKind { get; set; }

        public string? ContainingType { get; set; }

        public string? ContainingNamespace { get; set; }

        public string? TypeName { get; set; }

        public string? TypeKind { get; set; }

        public bool IsNullable { get; set; }

        // Contextual data
        public List<string> References { get; set; } = [];

        // UI data
        public string? HighlightColor { get; set; }

        public List<Chart> Charts { get; set; } = [];

        public NavToken(SyntaxToken roslynToken, SemanticModel semanticModel, SyntaxTree syntaxTree, int index)
        {
            Index = index;

            #region Lexical (token) data
            Text = roslynToken.Text;
            Kind = roslynToken.Kind();
            RoslynKind = roslynToken.Kind().ToString();
            Span = roslynToken.Span;
            if (roslynToken.HasLeadingTrivia)
            {
                //LeadingTrivia = GetLeadingTrivia(roslynToken);

                foreach (var trivia in roslynToken.LeadingTrivia)
                {
                    var triviaString = trivia.ToString();

                    if (!trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia)
                        && !trivia.IsKind(SyntaxKind.MultiLineCommentTrivia))
                    {
                        LeadingTrivia.Add(triviaString);
                    }

                    if (trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia))
                    {
                        triviaString = "///" + triviaString;

                        if (StringHelpers.CountOccurrences(triviaString, "///") == 1)
                        {
                            // single-line comment
                            LeadingTrivia.Add(triviaString);
                            LeadingTrivia.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                        }
                        if (StringHelpers.CountOccurrences(triviaString, "///") > 1)
                        {
                            // multi-line comment
                            var newStrings = triviaString.Split("\r\n");

                            var count = 1;
                            var numOfStrings = newStrings.Length;
                            foreach (var newString in newStrings)
                            {
                                // check if string has sequential spaces
                                // if so, cut them and create a new space trivia with them
                                if (StringHelpers.HasSequentialSpaces(newString))
                                {
                                    var spacesString = StringHelpers.PullSequentialSpaces(newString);
                                    LeadingTrivia.Add(spacesString);
                                }

                                // add new trivia strings
                                LeadingTrivia.Add(newString.Trim());
                                if (count < numOfStrings)
                                {
                                    LeadingTrivia.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                                }
                                count++;
                            }
                        }
                    }

                    if (trivia.IsKind(SyntaxKind.MultiLineCommentTrivia))
                    {
                        // multi-line comment
                        var newStrings = triviaString.Split("\r\n");

                        var count = 1;
                        var numOfStrings = newStrings.Length;
                        foreach (var newString in newStrings)
                        {
                            // check if string has sequential spaces
                            // if so, cut them and create a new space trivia with them
                            if (StringHelpers.HasSequentialSpaces(newString))
                            {
                                var spacesString = StringHelpers.PullSequentialSpaces(newString);
                                LeadingTrivia.Add(spacesString);
                            }

                            // add new trivia strings
                            LeadingTrivia.Add(newString.Trim());
                            if (count < numOfStrings)
                            {
                                LeadingTrivia.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                            }
                            count++;
                        }
                    }

                    if (trivia.IsKind(SyntaxKind.RegionDirectiveTrivia)
                        || trivia.IsKind(SyntaxKind.EndRegionDirectiveTrivia))
                    {
                        LeadingTrivia.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                    }
                }
            }
            if (roslynToken.HasTrailingTrivia)
            {
                foreach (var trivia in roslynToken.TrailingTrivia)
                {
                    TrailingTrivia.Add(trivia.ToString());
                }
            }
            Value = roslynToken.Value;
            RoslynToken = roslynToken;
            if (roslynToken.Parent is not null)
            {
                Parent = roslynToken.Parent;
            }
            #endregion

            #region Syntax data
            var parentNode = roslynToken.Parent;

            if (parentNode != null)
            {
                ParentNodeKind = parentNode.Kind().ToString();
                ParentNodeType = parentNode.GetType().Name;

                if (parentNode.Parent is not null)
                {
                    GrandParent = parentNode.Parent;
                    GrandParentNodeKind = GrandParent.Kind().ToString();
                    GrandParentNodeType = GrandParent.GetType().Name;

                    if (GrandParent.Parent is not null)
                    {
                        GreatGrandParent = GrandParent.Parent;
                        GreatGrandParentNodeKind = GreatGrandParent.Kind().ToString();
                        GreatGrandParentNodeType = GreatGrandParent.GetType().Name;
                    }
                }
            }
            #endregion

            #region Semantic data
            if (parentNode != null)
            {
                var symbolInfo = semanticModel.GetSymbolInfo(parentNode);
                if (symbolInfo.Symbol != null)
                {
                    Symbol = symbolInfo.Symbol;
                    SymbolName = symbolInfo.Symbol.Name;
                    SymbolKind = symbolInfo.Symbol.Kind.ToString();
                    ContainingType = symbolInfo.Symbol.ContainingType?.ToString() ?? null;
                    ContainingNamespace = symbolInfo.Symbol.ContainingNamespace.ToString();
                }

                var typeInfo = semanticModel.GetTypeInfo(parentNode);
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
            if (parentNode is IdentifierNameSyntax identifier)
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

        public static void Test()
        {

        }
    }
}
