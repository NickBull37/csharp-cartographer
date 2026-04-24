using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /*
         *  Default Key: KWOP:{token.Text}
         */

        private static DefinitionKey GetKeywordOperatorKey(NavToken token)
        {
            return new DefinitionKey(
                KeywordOperatorKind,
                token.Text,
                []
            );
        }
    }
}
