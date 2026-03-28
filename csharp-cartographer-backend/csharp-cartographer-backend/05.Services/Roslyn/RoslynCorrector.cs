using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public class RoslynCorrector : IRoslynCorrector
    {
        public string? GetCorrectedClassification(NavToken token, string? classification)
        {
            if (string.IsNullOrEmpty(classification))
                return null;

            if (classification == "identifier")
                return GetIdentifierCorrection(token);

            if (classification == "punctuation")
                return GetPunctuationCorrection(token);

            if (classification == "static symbol")
                return GetStaticSymbolCorrection(token);

            return null;
        }

        public string? GetCorrectedColorAs(NavToken token, string? classification)
        {
            if (string.IsNullOrEmpty(classification))
                return null;

            bool isNintKeyword = token.Text is "nint" && token.SemanticData?.SymbolName == "IntPtr";
            bool isNuintKeyword = token.Text is "nuint" && token.SemanticData?.SymbolName == "UIntPtr";

            if (isNintKeyword || isNuintKeyword)
                return "keyword";

            return null;
        }

        // Currently only handles range operator (..)
        private static string? GetPunctuationCorrection(NavToken token)
        {
            if (token.IsRangeOperator())
                return "operator";

            return null;
        }

        // Currently only handles nint & nuint keyword corrections
        private static string? GetIdentifierCorrection(NavToken token)
        {
            bool isNintKeyword = token.Text is "nint" && token.SemanticData?.SymbolName == "IntPtr";
            bool isNuintKeyword = token.Text is "nuint" && token.SemanticData?.SymbolName == "UIntPtr";

            if (isNintKeyword || isNuintKeyword)
                return "keyword";

            return null;
        }

        // Currently only handles constant identifier declarations & field references
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
