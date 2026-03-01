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
                var role = token.Map.SemanticRole;
                var category = token.Map.SyntaxCategory;

                token.Map.RoleLabel = role.GetLabel() ?? role.ToSpacedString();
                token.Map.RoleDefinition = GetRoleDefinition(token);
                token.Map.CategoryLabel = category.ToString();
                token.Map.FocusedDefinition = GetFocusedDefinition(token);

                //token.Map.SecondaryLabel = GetSecondaryLabel(token);
                //token.Map.SecondaryDefinition = GetSecondaryDefinition(token);
            }
        }

        private static MapText? GetRoleDefinition(NavToken token)
        {
            var role = token.Map.SemanticRole;
            return DefinitionProvider.GetMapText(role.ToString());
        }

        private static string? GetSecondaryLabel(NavToken token)
        {
            if (!token.Map.ModifierStrings.Any())
                return null;

            var label = token.Map.ModifierStrings.First();

            return StringHelpers.AddSpaces(label);
        }

        private static MapText? GetFocusedDefinition(NavToken token)
        {
            string? key = null;
            var role = token.Map.SemanticRole;

            if (token.IsIdentifier())
            {
                key = GetIdentifierKey(token);
            }
            else if (token.IsLiteral())
            {
                key = GetLiteralKey(token);
            }
            else if (token.IsKeyword() || token.IsOperator())
            {
                key = token.Text;
            }

            return key is not null
                ? DefinitionProvider.GetMapText(key)
                : null;
        }

        private static string? GetIdentifierKey(NavToken token)
        {
            // locally defined non-declaration identifiers
            if (!token.Map.SemanticRole.ToString().Contains("Declaration")
                && token.Map.SemanticRole is not SemanticRole.Parameter)
            {
                if (token.RoslynClassification == "parameter name")
                    return "Identifier:ParameterReference";

                if (token.RoslynClassification == "local name")
                    return "Identifier:LocalVariableReference";
            }

            // default case
            var category = token.Map.SyntaxCategory.ToString();
            var role = token.Map.SemanticRole.ToString();

            return category + ":" + role;
        }

        private static string GetLiteralKey(NavToken token)
        {
            // Boolean literals covered by Keywords

            var key = "Literal:";

            if (token.IsCharacterLiteral())
                return key + "CharacterLiteral";

            if (token.IsQuotedString())
                return key + "QuotedString";

            if (token.IsVerbatimString())
                return key + "VerbatimString";

            if (token.IsInterpolatedString() && !token.IsInterpolatedVerbatimString())
                return key + "InterpolatedString";

            if (token.IsInterpolatedVerbatimString())
                return key + "InterpolatedVerbatimString";

            if (token.IsNumericLiteral())
            {
                if (token.IsDecimalValue())
                    return key + "DecimalLiteral";

                if (token.IsFloatingPointValue())
                    return key + "FloatingPointLiteral";

                return key + "NumericLiteral";
            }

            return null;
        }

        private static MapText? GetSecondaryDefinition(NavToken token)
        {
            var key = token.Map.ModifierStrings.FirstOrDefault();

            return key is not null
                ? DefinitionProvider.GetMapText(key)
                : null;
        }
    }
}
