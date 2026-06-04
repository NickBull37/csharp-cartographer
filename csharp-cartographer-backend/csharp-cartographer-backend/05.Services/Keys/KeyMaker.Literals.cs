using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /*
         *  Default Key: LT:[literal type]
         * 
         *  Literal definitions depend on the type of literal,
         *  not the SemanricRole the literal has. Add extension
         *  based on the type of literal. Boolean literal definitions
         *  are covered by Keywords.
         */

        /// Literal Key: 
        /// [kindabrv]:[extension]:[modifier]
        /// LT:{string ext}

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

            if (token.IsNumericLiteral())
            {
                if (token.IsDecimalValue())
                    return Key(LT, "DecimalLiteral");

                if (token.IsFloatingPointValue())
                    return Key(LT, "FloatingPointLiteral");

                return Key(LT, "NumericLiteral");
            }

            return null;
        }
    }
}
