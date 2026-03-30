using csharp_cartographer_backend._02.Utilities.Helpers;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    public class SemanticLibrary : ISemanticLibrary
    {
        public SemanticMap GetSemanticMap(NavToken token)
        {
            var roleDefinition = GetRoleDefinition(token.SemanticRole);
            var focusedDefinition = GetFocusedDefinition(token);

            return new SemanticMap(
                token.PrimaryKind,
                token.SemanticRole,
                roleDefinition,
                focusedDefinition
            );
        }

        private static MapText GetRoleDefinition(SemanticRole role)
        {
            /*
             * The SemanticRole is used as the definition key by default. 
             * Delimiters are the exception since they have much more overlap
             * than keywords, operators, etc. Use the label on the SemanticRole
             * as the key for Delimiters.
             */

            var key = role.GetLabel() ?? role.ToString();
            return DefinitionProvider.GetMapText(key) ?? MapText.Undefined();
        }

        private static MapText? GetFocusedDefinition(NavToken token)
        {
            string? key = null;

            switch (token.PrimaryKind)
            {
                case PrimaryKind.Delimiter:
                    key = GetDelimiterKey(token);
                    break;
                case PrimaryKind.Operator:
                    key = GetOperatorKey(token);
                    break;
                case PrimaryKind.Identifier:
                    key = GetIdentifierKey(token);
                    break;
                case PrimaryKind.Literal:
                    key = GetLiteralKey(token);
                    break;
                case PrimaryKind.Keyword:
                    key = GetKeywordKey(token);
                    break;
                default:
                    break;
            }

            if (key is null)
                return null;

            return DefinitionProvider.GetMapText(key) ?? null;
        }

        private static string GetDelimiterKey(NavToken token)
        {
            /*
             *  Delimiter default key = SemanticRole:Open/Close
             */

            var key = $"{token.SemanticRole}:";

            key += token.Text switch
            {
                "(" or "{" or "[" or "<" => "Open",
                ")" or "}" or "]" or ">" => "Close",
                _ => throw new InvalidOperationException(),
            };

            return key;
        }

        private static string? GetOperatorKey(NavToken token)
        {
            /*
             *  Operator default key = Operator:TokenText
             * 
             *  Add specific operator name for operators
             *  that fall into multiple categories.
             *  
             *  Use full operator for scenarios where roslyn
             *  splits an operator into multiple tokens.
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

            // (?) conditional-member access: ? -> ?.
            if (token.IsNullConditionalGuard())
                return key + ".";

            // (=>) lambda / expression body arrow
            if (token.IsLambdaOperator())
                return key + ":Lambda";

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

            var role = token.SemanticRole.ToString();
            var key = "Identifier:";

            // identifier declarations can be defined by semantic role
            bool isDeclaration = token.SemanticRole.ToString().Contains("Declaration")
                || token.SemanticRole == SemanticRole.Parameter
                || token.SemanticRole == SemanticRole.LambdaParameter;
            if (isDeclaration)
                return key + role;

            // identifier references get "special keys"
            if (token.Classification == "parameter name")
            {
                if (token.IsLambdaParameterReference())
                    return key + "LambdaParameterReference";

                return key + "ParameterReference";
            }

            if (token.Classification == "local name")
                return key + "LocalVariableReference";

            if (token.Classification == "field name")
                return key + "FieldReference";

            // default
            return key + role;
        }

        private static string? GetLiteralKey(NavToken token)
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

        private static string GetKeywordKey(NavToken token)
        {
            /*
             *  Keyword default key = token.Text
             * 
             *  There are a handful of special case keywords that can fall
             *  into multiple roles depending on the context. For these cases,
             *  append the role to the key to get the context-specific definition.
             */

            var role = token.SemanticRole.ToString();

            if (token.Text is "case" or "default" or "in" or "new" or "where")
                return $"{token.Text}:{role}";

            return $"{token.Text}";
        }
    }
}
