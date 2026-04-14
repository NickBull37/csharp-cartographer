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

        private static DefinitionKey? GetDelimiterKey(NavToken token)
        {
            var direction = token.Text switch
            {
                "(" or "{" or "[" or "<" => "Open",
                ")" or "}" or "]" or ">" => "Close",
                _ => null
            };

            return direction is not null
                ? new DefinitionKey(DelimiterKind, token.SemanticRole.ToString(), [direction])
                : null;
        }
    }
}
