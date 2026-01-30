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

                token.Map.PrimaryLabel = GetPrimaryLabel(token);
                token.Map.SecondaryLabel = GetSecondaryLabel(token);
                token.Map.PrimaryDefinition = GetPrimaryDefinition(token);
                token.Map.PrimaryFocusedDefinition = GetPrimaryFocusedDefinition(token);
                token.Map.SecondaryDefinition = GetSecondaryDefinition(token);
            }
        }

        private static string GetPrimaryLabel(NavToken token)
        {
            if (token.Map is null)
                return string.Empty;

            var semanticRole = token.Map.SemanticRole;
            var primaryKind = token.Map.PrimaryKind;

            // Special Case: drop Identifier PK for Qualifiers
            if (semanticRole.ToString().Contains("Qualifier"))
                return semanticRole.ToSpacedString();

            // Special Case: drop Keyword/Identifier PK for DataTypes
            bool isDataType = semanticRole.ToString().Contains("DataType") || semanticRole.ToString().Contains("ReturnType");
            if (primaryKind == PrimaryKind.Keyword && isDataType)
                return semanticRole.ToSpacedString();

            // Special Case: drop Punctuation PK for all punctuation
            if (primaryKind == PrimaryKind.Punctuation)
                return semanticRole.ToSpacedString();

            // Special Case: drop Delimiter PK for all delimiters
            if (primaryKind == PrimaryKind.Delimiter)
                return semanticRole.ToSpacedString();

            // Special Case: drop Literal PK for all literals
            if (primaryKind == PrimaryKind.Literal)
                return semanticRole.ToSpacedString();

            // Special Case: 
            if (semanticRole == SemanticRole.NullConditionalGuard)
                return semanticRole.ToSpacedString();

            // Normal Case is SR + PK
            return semanticRole.ToSpacedString() + " " + primaryKind.ToSpacedString();
        }

        private static string? GetSecondaryLabel(NavToken token)
        {
            if (token.Index == 117)
            {

            }

            if (!token.Map.ModifierStrings.Any())
                return null;

            return token.Map.ModifierStrings.First();
        }

        private static MapText GetPrimaryDefinition(NavToken token)
        {
            var key = token.Map.SemanticRole.ToString();

            var definition = key is not null
                ? DefinitionProvider.GetDefinition(key)
                : null;

            if (string.IsNullOrWhiteSpace(definition))
                return new MapText();

            return ToPlainMapText(definition);
        }

        private static MapText GetPrimaryFocusedDefinition(NavToken token)
        {
            var key = token.Text;

            var definition = key is not null
                ? DefinitionProvider.GetDefinition(key)
                : null;

            if (string.IsNullOrWhiteSpace(definition))
                return new MapText();

            return ToPlainMapText(definition);
        }

        private static MapText GetSecondaryDefinition(NavToken token)
        {
            var key = token.Map.ModifierStrings.FirstOrDefault();

            var definition = key is not null
                ? DefinitionProvider.GetDefinition(key)
                : null;

            if (string.IsNullOrWhiteSpace(definition))
                return new MapText();

            return ToPlainMapText(definition);
        }

        private static MapText ToPlainMapText(string definition)
        {
            return new MapText
            {
                ID = Guid.NewGuid(),
                Segments =
                [
                    new TextSegment
                    {
                        ID = Guid.NewGuid(),
                        Text = definition,
                        Classes = []
                    }
                ]
            };
        }
    }
}
