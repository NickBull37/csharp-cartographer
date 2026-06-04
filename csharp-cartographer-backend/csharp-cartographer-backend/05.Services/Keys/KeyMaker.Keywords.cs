using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /*
         *  Default Key: KW:{token.Text}
         *  Special Key: KW:{token.Text}:{token.SemanticRole}
         * 
         *  There are a handful of special case keywords that can fall
         *  under multiple roles depending on context. For these cases,
         *  append the semantic role to the default key to find the 
         *  context-specific definition.
         */

        /// Keyword Key: 
        /// [kindabrv]:[extension]:[modifier]
        /// KW:{token.Text}:{token.SemanticRole?}

        private static string GetKeywordKey(NavToken token)
        {
            if (token.IsVarPatternKeyword())
                return Key(KW, token.Text, "PatternMatching");

            if (token.IsDefaultLiteral())
                return Key(KW, token.Text, "Literal");

            bool requiresRoleExt = token.Text
                is "case"
                or "in"
                or "new"
                or "static"
                or "using"
                or "where";

            return requiresRoleExt
                ? Key(KW, token.Text, token.SemanticRole.ToString())
                : Key(KW, token.Text);
        }
    }
}
