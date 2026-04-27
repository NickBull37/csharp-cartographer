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

        private static DefinitionKey GetKeywordKey(NavToken token)
        {
            if (token.IsVarPatternKeyword())
                return new DefinitionKey(KeywordKind, token.Text, ["PatternMatching"]);

            if (token.IsDefaultLiteral())
                return new DefinitionKey(KeywordKind, token.Text, ["Literal"]);

            bool requiresRoleExt = token.Text
                is "case"
                or "in"
                or "new"
                or "static"
                or "using"
                or "where";

            return requiresRoleExt
                ? new DefinitionKey(KeywordKind, token.Text, [token.SemanticRole.ToString()])
                : new DefinitionKey(KeywordKind, token.Text, []);
        }
    }
}
