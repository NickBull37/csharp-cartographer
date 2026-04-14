using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /*
         *  Default Key: PN:{token.Text}
         */

        private static DefinitionKey? GetPunctuationKey(NavToken token)
        {
            return new DefinitionKey(KeywordKind, token.Text, []);
        }
    }
}
