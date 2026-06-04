using csharp_cartographer_backend._02.Utilities.Helpers;
using csharp_cartographer_backend._02.Utilities.Providers;
using csharp_cartographer_backend._03.Models.Shared;
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
            return token.IsIdentifier() && token.IsGenericType()
                ? "Generic Type"
                : token.PrimaryKind.ToString();
        }

        private static StyledText GetRoleDefinition(SemanticRole role)
        {
            if (role is SemanticRole.Unknown)
                return StyledText.NotFound();

            var key = KeyMaker.GetKey(role);

            return DefinitionProvider.GetStyledText(key) ?? StyledText.NotFound();
        }

        private static StyledText? GetFocusedDefinition(NavToken token)
        {
            var key = KeyMaker.GetKey(token);

            if (key is null)
                return null;

            return DefinitionProvider.GetStyledText(key) ?? null;
        }
    }
}
