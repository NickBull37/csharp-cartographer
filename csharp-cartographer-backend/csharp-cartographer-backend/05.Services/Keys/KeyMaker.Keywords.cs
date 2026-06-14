using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /// Key format
        /// [kindabrv]:[extension]:[modifier]
        /// 
        /// Standard key: 
        /// KW:{token.Text}
        /// 
        /// Extended key
        /// KW:{token.Text}:{token.SemanticRole}
        /// 

        private static readonly IEnumerable<string> ExtensionRequiredTokens =
        [
            "case",
            "in",
            "new",
            "ref",
            "static",
            "using",
            "where",
        ];

        private static string GetKeywordKey(NavToken token)
        {
            if (token.IsVarPatternKeyword())
                return Key(KW, token.Text, "PatternMatching");

            if (token.IsDefaultLiteral())
                return Key(KW, token.Text, "Literal");

            return ExtensionRequiredTokens.Contains(token.Text)
                ? Key(KW, token.Text, token.SemanticRole.ToString())
                : Key(KW, token.Text);
        }
    }
}
