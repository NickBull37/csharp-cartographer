using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public class RoslynCorrector : IRoslynCorrector
    {
        public string? GetCorrectedClassification(NavToken token, string? roslynClassification)
        {
            return roslynClassification switch
            {
                "keyword" => GetKeywordCorrection(token),
                "identifier" => GetIdentifierCorrection(token),
                "operator" => GetOperatorCorrection(token),
                "punctuation" => GetPunctuationCorrection(token),
                "static symbol" => GetStaticSymbolCorrection(token),
                _ => null,
            };
        }

        public string? GetCorrectedColorAs(NavToken token, string? roslynClassification)
        {
            if (string.IsNullOrEmpty(roslynClassification))
                return null;

            bool isNintKeyword = token.Text is "nint" && token.SemanticData?.SymbolName == "IntPtr";
            bool isNuintKeyword = token.Text is "nuint" && token.SemanticData?.SymbolName == "UIntPtr";
            if (isNintKeyword || isNuintKeyword)
                return "keyword";

            return null;
        }

        private static string? GetOperatorCorrection(NavToken token)
        {
            if (token.IsQualifiedNameSeparator())
                return "punctuation";

            if (token.IsNullableTypeMarker())
                return "punctuation";

            return null;
        }

        private static string? GetPunctuationCorrection(NavToken token)
        {
            if (token.IsDelimiter())
                return "delimiter";

            if (token.IsRangeOperator())
                return "operator";

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

        private static string? GetKeywordCorrection(NavToken token)
        {
            bool isArgsKeyword = token.Kind == SyntaxKind.IdentifierToken
                && token.Text == "args";

            if (isArgsKeyword)
                return "identifier";

            return null;
        }

        private static string? GetStaticSymbolCorrection(NavToken token)
        {
            // constant identifiers
            bool isFieldSymbol = token.SemanticData?.IsFieldSymbol ?? false;
            bool IsConstant = token.SemanticData?.IsConst ?? false;

            if (isFieldSymbol && IsConstant)
                return "constant name";

            // field refs
            if (isFieldSymbol)
                return "field name";

            return null;
        }
    }
}
