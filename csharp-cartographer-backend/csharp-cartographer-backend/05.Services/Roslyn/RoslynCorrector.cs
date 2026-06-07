using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public static class RoslynCorrector
    {
        public static (string? corrected, string? colorAs) GetClassifications(NavToken token, string? roslynClass)
        {
            var corrected = GetCorrected(token, roslynClass);
            var colorAs = GetColorAs(token, roslynClass);

            return (corrected, colorAs);
        }

        private static string? GetCorrected(NavToken token, string? roslynClass)
        {
            return roslynClass switch
            {
                "keyword" => GetKeywordCorrection(token),
                "identifier" => GetIdentifierCorrection(token),
                "operator" => GetOperatorCorrection(token),
                "punctuation" => GetPunctuationCorrection(token),
                "property name" => GetPropertyNameCorrection(token),
                _ => null,
            };
        }

        private static string? GetKeywordCorrection(NavToken token)
        {
            if (token.IsArgsIdentifierKeyword())
                return "identifier - keyword";

            if (token.IsKeywordOperator())
                return "keyword - operator";

            return null;
        }

        private static string? GetIdentifierCorrection(NavToken token)
        {
            bool isNintKeyword = token.Text is "nint" && token.SemanticData?.SymbolName == "IntPtr";
            bool isNuintKeyword = token.Text is "nuint" && token.SemanticData?.SymbolName == "UIntPtr";

            if (isNintKeyword || isNuintKeyword)
                return "keyword";

            return null;
        }

        private static string? GetOperatorCorrection(NavToken token)
        {
            if (token.IsNullableTypeMarker())
                return "punctuation";

            if (token.IsPointerTypeIndicator())
                return "punctuation";

            if (token.IsQualifiedNameSeparator())
                return "punctuation";

            return null;
        }

        private static string? GetPunctuationCorrection(NavToken token)
        {
            if (token.IsDelimiter())
                return "delimiter";

            if (token.IsRangeOperator() || token.IsSliceOperator())
                return "operator";

            return null;
        }

        private static string? GetPropertyNameCorrection(NavToken token)
        {
            /*
             *  Roslyn will incorrectly classify a type qualifier in a property declaration
             *  as a property name if it shares the same name as a Type. Check each property
             *  name classification with a declaration ancestor for a property declaration parent.
             */

            bool hasPropertyDeclAncestor = token.Ancestors.HasAncestor(SyntaxKind.PropertyDeclaration);
            bool propertyDeclIsParent = token.Ancestors.GetParent() == SyntaxKind.PropertyDeclaration;

            if (hasPropertyDeclAncestor && !propertyDeclIsParent)
                return "identifier";

            return null;
        }

        private static string? GetColorAs(NavToken token, string? roslynClass)
        {
            if (roslynClass == "property name")
            {
                bool hasPropertyDeclAncestor = token.Ancestors.HasAncestor(SyntaxKind.PropertyDeclaration);
                bool propertyDeclIsParent = token.Ancestors.GetParent() == SyntaxKind.PropertyDeclaration;

                if (hasPropertyDeclAncestor && !propertyDeclIsParent)
                    return "identifier";
            }

            if (roslynClass == "identifier")
            {
                bool isNintKeyword = token.Text is "nint" && token.SemanticData?.SymbolName == "IntPtr";
                bool isNuintKeyword = token.Text is "nuint" && token.SemanticData?.SymbolName == "UIntPtr";

                if (isNintKeyword || isNuintKeyword)
                    return "keyword";
            }

            return null;
        }
    }
}
