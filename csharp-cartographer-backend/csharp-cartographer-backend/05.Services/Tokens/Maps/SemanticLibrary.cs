using csharp_cartographer_backend._02.Utilities.Helpers;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
using csharp_cartographer_backend._05.Services.Keys;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    public class SemanticLibrary : ISemanticLibrary
    {
        public SemanticMap GetSemanticMap(NavToken token)
        {
            var kindLabel = GetLabelOrSpacedString(token.PrimaryKind);
            var roleLabel = GetLabelOrSpacedString(token.SemanticRole);
            var focusedLabel = GetFocusedLabel(token);
            var roleDefinition = GetRoleDefinition(token.SemanticRole);
            var focusedDefinition = GetFocusedDefinition(token);

            return new SemanticMap(
                kindLabel,
                roleLabel,
                focusedLabel,
                roleDefinition,
                focusedDefinition
            );
        }

        private static string GetLabelOrSpacedString<TEnum>(TEnum value)
            where TEnum : Enum
        {
            return value.GetSpacedLabel() ?? value.ToSpacedString();
        }

        private static string GetFocusedLabel(NavToken token)
        {
            return token.PrimaryKind switch
            {
                PrimaryKind.Identifier => token.IsGenericType()
                    ? "GenericType"
                    : token.PrimaryKind.ToString(),
                _ => token.PrimaryKind.ToString(),
            };
        }

        private static MapText? GetRoleDefinition(SemanticRole? role)
        {
            if (role is null)
                return null;

            /*
             * The SemanticRole is used as the definition key by default. 
             * Delimiters are the exception since they have much more overlap
             * than keywords, operators, etc. Use the label on the SemanticRole
             * as the key for Delimiters.
             */

            var key = KeyMaker.GetKey((SemanticRole)role);

            if (key is null)
                return null;

            return DefinitionProvider.GetMapText(key) ?? MapText.Undefined();
        }

        private static MapText? GetFocusedDefinition(NavToken token)
        {
            var key = KeyMaker.GetKey(token);

            if (key is null)
                return null;

            return DefinitionProvider.GetMapText(key) ?? null;
        }
    }
}
