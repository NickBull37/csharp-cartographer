using csharp_cartographer_backend._02.Utilities.Helpers;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    public class SemanticLibrary : ISemanticLibrary
    {
        public void AddSemanticInfo(List<NavToken> navTokens)
        {
            for (int i = 0; i < navTokens.Count; i++)
            {
                var token = navTokens[i];

                token.Map.PrimaryLabel = token.Map.SemanticRole.ToSpacedString();
                token.Map.PrimaryDefinition = GetPrimaryDefinition(token);
                token.Map.PrimaryFocusedDefinition = GetPrimaryFocusedDefinition(token);
                token.Map.SecondaryLabel = GetSecondaryLabel(token);
                token.Map.SecondaryDefinition = GetSecondaryDefinition(token);
            }
        }

        private static string? GetSecondaryLabel(NavToken token)
        {
            if (!token.Map.ModifierStrings.Any())
                return null;

            var label = token.Map.ModifierStrings.First();

            return StringHelpers.AddSpaces(label);
        }

        private static MapText GetPrimaryDefinition(NavToken token)
        {
            var key = token.Map.SemanticRole.ToString();

            return DefinitionProvider.GetMapText(key);
        }

        private static MapText GetPrimaryFocusedDefinition(NavToken token)
        {
            var key = token.Text;

            return key is not null
                ? DefinitionProvider.GetMapText(key)
                : null;
        }

        private static MapText GetSecondaryDefinition(NavToken token)
        {
            var key = token.Map.ModifierStrings.FirstOrDefault();

            return key is not null
                ? DefinitionProvider.GetMapText(key)
                : null;
        }
    }
}
