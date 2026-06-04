using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /// Punctuation Key: 
        /// [kindabrv]:[extension]:(modifier)
        /// PN:{token.Text}

        private static string GetPunctuationKey(NavToken token)
        {
            return Key(PN, token.Text);
        }
    }
}
