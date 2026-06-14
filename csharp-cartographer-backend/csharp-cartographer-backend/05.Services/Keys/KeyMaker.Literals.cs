using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /// Key format
        /// [kindabrv]:[extension1]:[extension2]
        /// 
        /// Standard key: 
        /// LT:{hardcoded extension}

        private static string? GetLiteralKey(NavToken token)
        {
            if (token.IsCharacterLiteral())
                return Key(LT, "CharacterLiteral");

            if (token.IsQuotedString())
                return Key(LT, "QuotedString");

            if (token.IsVerbatimString())
                return Key(LT, "VerbatimString");

            if (token.IsInterpolatedVerbatimString())
                return Key(LT, "InterpolatedVerbatimString");

            if (token.IsInterpolatedString())
                return Key(LT, "InterpolatedString");

            if (token.IsDecimalValue())
                return Key(LT, "DecimalLiteral");

            if (token.IsFloatingPointValue())
                return Key(LT, "FloatingPointLiteral");

            if (token.IsNumericLiteral())
                return Key(LT, "NumericLiteral");

            return null;
        }
    }
}
