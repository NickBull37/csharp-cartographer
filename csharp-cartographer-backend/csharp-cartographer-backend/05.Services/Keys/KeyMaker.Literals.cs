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

        private static DefinitionKey? GetLiteralKey(NavToken token)
        {
            if (token.IsCharacterLiteral())
                return new DefinitionKey(LiteralKind, "CharacterLiteral", []);

            if (token.IsQuotedString())
                return new DefinitionKey(LiteralKind, "QuotedString", []);

            if (token.IsVerbatimString())
                return new DefinitionKey(LiteralKind, "VerbatimString", []);

            if (token.IsInterpolatedVerbatimString())
                return new DefinitionKey(LiteralKind, "InterpolatedVerbatimString", []);

            if (token.IsInterpolatedString())
                return new DefinitionKey(LiteralKind, "InterpolatedString", []);

            if (token.IsNumericLiteral())
            {
                if (token.IsDecimalValue())
                    return new DefinitionKey(LiteralKind, "DecimalLiteral", []);

                if (token.IsFloatingPointValue())
                    return new DefinitionKey(LiteralKind, "FloatingPointLiteral", []);

                return new DefinitionKey(LiteralKind, "NumericLiteral", []);
            }

            return null;
        }
    }
}
