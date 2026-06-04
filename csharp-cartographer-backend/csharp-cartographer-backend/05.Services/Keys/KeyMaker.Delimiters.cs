using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /// Delimiter Key: 
        /// [kindabrv]:[extension]:[modifier]
        /// DL:{token.SemanticRole}:Open

        private static string GetDelimiterKey(NavToken token)
        {
            return token.IsOpenDelimiter()
                ? Key(DL, token.SemanticRole.ToString(), "Open")
                : Key(DL, token.SemanticRole.ToString(), "Close");
        }
    }
}
