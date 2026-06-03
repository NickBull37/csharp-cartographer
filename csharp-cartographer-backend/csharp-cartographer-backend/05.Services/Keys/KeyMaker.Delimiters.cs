using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /*
         * DEFAULT KEY
         *  
         *    DL:{token.SemanticRole}:Open
         *    DL:{token.SemanticRole}:Close
         */

        private static DefinitionKey GetDelimiterKey(NavToken token)
        {
            string? direction = null;

            if (token.IsOpenDelimiter())
                direction = "Open";

            if (token.IsCloseDelimiter())
                direction = "Close";

            if (direction is null)
                throw new InvalidDataException();

            return new DefinitionKey(
                DelimiterKind,
                token.SemanticRole.ToString(),
                [direction]
            );
        }
    }
}
