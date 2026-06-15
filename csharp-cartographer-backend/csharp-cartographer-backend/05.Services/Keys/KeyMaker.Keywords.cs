using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /// Key format
        /// [kindabrv]:[extension1]?:[extension2]
        /// 
        /// Standard key: 
        /// KW:{token.Text}
        /// 
        /// Extended keys:
        /// KW:{token.Text}:{token.SemanticRole}
        /// KW:{token.Text}:{hardcoded extension}

        private static readonly IEnumerable<string> CustomExtensionRequired =
        [
            "default",
            "var",
        ];

        private static readonly IEnumerable<string> RoleExtensionRequired =
        [
            "case",
            "in",
            "new",
            "out",
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

            return RoleExtensionRequired.Contains(token.Text)
                ? Key(KW, token.Text, token.SemanticRole.ToString())
                : Key(KW, token.Text);
        }
    }
}
