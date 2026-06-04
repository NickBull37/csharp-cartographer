using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /*
         *  Default Key: KWOP:{token.Text}
         */

        /// Keyword Operator Key: 
        /// [kindabrv]:[extension]:[modifier]
        /// KW:{token.Text}

        private static string GetKeywordOperatorKey(NavToken token)
        {
            return Key(KWOP, token.Text);
        }
    }
}
