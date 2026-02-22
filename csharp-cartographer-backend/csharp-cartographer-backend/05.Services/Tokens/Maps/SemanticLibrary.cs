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

        private static MapText? GetPrimaryDefinition(NavToken token)
        {
            var role = token.Map.SemanticRole;

            //if (role is SemanticRole.QuotedString
            //    or SemanticRole.VerbatimString
            //    or SemanticRole.NumericLiteral)
            //{
            //    return DefinitionProvider.GetMapText("StringLiteral");
            //    //return null;
            //}
            //if (role is SemanticRole.NumericLiteral)
            //{
            //    return DefinitionProvider.GetMapText("NumericLiteralShort");
            //    //return null;
            //}

            return DefinitionProvider.GetMapText(role.ToString());
        }

        private static MapText? GetPrimaryFocusedDefinition(NavToken token)
        {
            string? key = null;
            var role = token.Map.SemanticRole;

            if (role is SemanticRole.Argument
                or SemanticRole.AssignmentValue
                or SemanticRole.ReturnValue
                or SemanticRole.ConcatenationOperand
                or SemanticRole.ComparisonOperand
                or SemanticRole.BitwiseOperand
                or SemanticRole.ShiftOperand)
            {
                if (token.IsQuotedString())
                {
                    key = "QuotedString";
                }
                if (token.IsVerbatimString())
                {
                    key = "VerbatimString";
                }
                if (token.IsNumericLiteral())
                {
                    key = "NumericLiteral";
                }
                if (token.IsBooleanLiteral())
                {
                    key = "BooleanLiteral";
                }
            }
            //else if (role is SemanticRole.QuotedString
            //    or SemanticRole.VerbatimString
            //    or SemanticRole.NumericLiteral
            //    or SemanticRole.CharacterLiteral)
            //{
            //    key = role.ToString();
            //}
            else if (token.IsInterpolatedString())
            {
                key = "InterpolatedString";
            }
            else if (token.IsKeyword() || token.IsOperator())
            {
                key = token.Text;
            }

            return key is not null
                ? DefinitionProvider.GetMapText(key)
                : null;
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
