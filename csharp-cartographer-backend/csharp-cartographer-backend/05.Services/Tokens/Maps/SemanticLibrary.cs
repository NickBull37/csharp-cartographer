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
                token.Map.RoleDefinition = GetRoleDefinition(role);
                token.Map.CategoryLabel = category.ToString();
                token.Map.FocusedDefinition = GetFocusedDefinition(token);

                //token.Map.SecondaryLabel = GetSecondaryLabel(token);
                //token.Map.SecondaryDefinition = GetSecondaryDefinition(token);
            }
        }

        private static MapText? GetRoleDefinition(SemanticRole role)
        {
            var definition = DefinitionProvider.GetMapText(role.ToString());

            if (definition is null)
            {
                // log error
            }

            return definition;
        }

        private static MapText? GetFocusedDefinition(NavToken token)
        {
            string? key = null;

            if (token.IsIdentifier())
                key = GetIdentifierKey(token);

            else if (token.IsOperator())
                key = GetOperatorKey(token);

            else if (token.IsLiteral())
                key = GetLiteralKey(token);

            else if (token.IsKeyword())
                key = token.Text;

            return key is not null
                ? DefinitionProvider.GetMapText(key)
                : null;
        }

        private static string? GetOperatorKey(NavToken token)
        {
            /*
             *  Operator default key = Operator:TokenText
             * 
             *  Add specific operator name for operators
             *  that fall into multiple categories.
             */

            var key = $"Operator:{token.Text}";

            // (.) member access / qualified-name separator
            if (token.IsMemberAccessOperator())
                return key;
            if (token.IsQualifiedNameSeparator())
                return null;

            // (!) logical NOT / null forgiving
            if (token.IsLogicalNotOperator())
                return key + ":LogicalNot";
            if (token.IsNullForgivingOperator())
                return key + ":NullForgiving";

            // (^) index / bitwise xor
            if (token.IsIndexFromEndOperator())
                return key + ":Index";
            if (token.IsBitwiseXorOperator())
                return key + ":BitwiseXor";

            // (*) multiplication / dereference / pointer
            if (token.IsMultiplicationOperator())
                return key + ":Multiplication";
            if (token.IsDereferenceOperator())
                return key + ":Dereference";
            if (token.IsPointerTypeIndicator())
                return null;

            // (&) address-of / bitwise and
            if (token.IsAddressOfOperator())
                return key + ":AddressOf";
            if (token.IsBitwiseAndOperator())
                return key + ":BitwiseAnd";

            return key;
        }

        private static string? GetIdentifierKey(NavToken token)
        {
            /*
             *  Identifier default key = Identifier:SemanticRole
             * 
             *  Identifiers don't have specific definitions like
             *  keywords or operators. Add an extension for all
             *  identifiers defined in the uploaded file to add
             *  a little more information when possible.
             */

            var key = "Identifier:";

            // locally defined non-declaration identifiers
            if (!token.Map.SemanticRole.ToString().Contains("Declaration")
                && token.Map.SemanticRole is not SemanticRole.Parameter)
            {
                if (token.RoslynClassification == "parameter name")
                    return key + "ParameterReference";

                if (token.RoslynClassification == "local name")
                    return key + "LocalVariableReference";

                if (token.RoslynClassification == "field name")
                    return key + "FieldReference";
            }

            var role = token.Map.SemanticRole.ToString();
            return key + role;
        }

        private static string GetLiteralKey(NavToken token)
        {
            /*
             *  Literal default key = Literal:
             * 
             *  Literal definitions depend on the type of literal,
             *  not the SemanricRole the literal has. Add extension
             *  based on the type of literal. Boolean literal definitions
             *  are covered by Keywords.
             */

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

        private static string? GetSecondaryLabel(NavToken token)
        {
            if (!token.Map.ModifierStrings.Any())
                return null;

            var label = token.Map.ModifierStrings.First();

            return StringHelpers.AddSpaces(label);
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
