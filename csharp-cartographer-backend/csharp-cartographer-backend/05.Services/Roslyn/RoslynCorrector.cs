using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public static class RoslynCorrector
    {
        public static (string? corrected, string? colorAs) GetCorrectedClassifications(NavToken token, string? roslyn)
        {
            var corrected = GetCorrected(token, roslyn);
            var colorAs = GetColorAs(token, corrected);

            return (corrected, colorAs);
        }

        private static string? GetCorrected(NavToken token, string? roslynClass)
        {
            var corrected = roslynClass switch
            {
                "keyword" => GetKeywordCorrected(token),
                "identifier" => GetIdentifierCorrected(token),
                "operator" => GetOperatorCorrected(token),
                "punctuation" => GetPunctuationCorrected(token),
                "property name" => GetPropertyNameCorrected(token),
                _ => null,
            };

            return corrected;
        }

        private static string? GetColorAs(NavToken token, string? corrected)
        {
            var colorAs = corrected switch
            {
                "delimiter" => "delimiter",
                "keyword" => "keyword",
                "identifier" => "identifier",
                "punctuation" => GetPunctuationColorAs(token),
                _ => null,
            };

            return colorAs;
        }

        private static string? GetKeywordCorrected(NavToken token)
        {
            if (token.IsArgsIdentifierKeyword())
                return "identifier - keyword";

            if (token.IsKeywordOperator())
                return "keyword - operator";

            return null;
        }

        private static string? GetIdentifierCorrected(NavToken token)
        {
            bool isNintKeyword = token.Text is "nint" && token.SemanticData?.SymbolName == "IntPtr";
            bool isNuintKeyword = token.Text is "nuint" && token.SemanticData?.SymbolName == "UIntPtr";

            if (isNintKeyword || isNuintKeyword)
                return "keyword";

            return null;
        }

        private static string? GetOperatorCorrected(NavToken token)
        {
            if (token.IsNullableTypeMarker())
                return "punctuation";

            if (token.IsPointerTypeIndicator())
                return "punctuation";

            if (token.IsQualifiedNameSeparator())
                return "punctuation";

            return null;
        }

        private static string? GetPunctuationCorrected(NavToken token)
        {
            if (token.IsDelimiter())
                return "delimiter";

            if (token.IsRangeOperator() || token.IsSliceOperator())
                return "operator";

            return null;
        }

        private static string? GetPropertyNameCorrected(NavToken token)
        {
            /*
             *  Roslyn will incorrectly classify a type qualifier in a property declaration
             *  as a property name if it shares the same name as a Type. Check each property
             *  name classification with a declaration ancestor for a property declaration parent.
             */

            bool hasPropertyDeclAncestor = token.AncestorKinds.HasAncestor(SyntaxKind.PropertyDeclaration);
            bool propertyDeclIsParent = token.AncestorKinds.GetParent() == SyntaxKind.PropertyDeclaration;

            if (hasPropertyDeclAncestor && !propertyDeclIsParent)
                return "identifier";

            return null;
        }

        private static string? GetPunctuationColorAs(NavToken token)
        {
            if (token.IsNullableTypeMarker())
                return "operator";

            return null;
        }
    }
}
