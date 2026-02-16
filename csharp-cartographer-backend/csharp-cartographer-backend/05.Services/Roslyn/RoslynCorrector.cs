using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public class RoslynCorrector : IRoslynCorrector
    {
        public string? GetCorrectedClassification(NavToken token, string? roslynClassification)
        {
            if (roslynClassification == "identifier")
                return GetIdentifierCorrection(token);

            if (roslynClassification == "static symbol")
                return GetStaticSymbolCorrection(token);

            return null;
        }

        // Currently only handles nint & nuint keyword corrections
        private static string? GetIdentifierCorrection(NavToken token)
        {
            bool isNintKeyword = token.Text is "nint" && token.SemanticData?.SymbolName == "IntPtr";
            bool isNuintKeyword = token.Text is "nuint" && token.SemanticData?.SymbolName == "UIntPtr";
            if (isNintKeyword || isNuintKeyword)
            {
                return "keyword";
            }

            return null;
        }

        // Currently only handles constant identifier declarations
        private static string? GetStaticSymbolCorrection(NavToken token)
        {
            bool isFieldSymbol = token.SemanticData?.IsFieldSymbol ?? false;
            bool IsConstant = token.SemanticData?.IsConst ?? false;
            if (isFieldSymbol && IsConstant)
            {
                return "constant name";
            }

            return null;
        }
    }
}
