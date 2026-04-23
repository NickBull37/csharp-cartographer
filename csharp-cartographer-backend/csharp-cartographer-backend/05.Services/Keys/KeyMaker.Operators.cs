using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /*
         *  DEFAULT KEY: OP:{token.Text}
         * 
         *  SPECIAL KEYS:
         *  Add specific operator name for operators that fall into multiple categories.
         *      OP:{token.Text}:[operator name]
         *  
         *  Use full operator syntax when roslyn splits an operator into multiple tokens.
         *      OP:[full syntax string]
         */

        private static DefinitionKey? GetOperatorKey(NavToken token)
        {
            // (*)(.) look like operators but aren't
            if (token.IsPointerTypeIndicator() || token.IsQualifiedNameSeparator())
                return null;

            // (?.) split into multiple tokens, requires full operator syntax
            if (token.SemanticRole
                is SemanticRole.NullConditionalDot
                or SemanticRole.NullConditionalQuestion)
            {
                return new DefinitionKey(OperatorKind, "?.", []);
            }

            // (c?t:f) split into multiple tokens, requires full operator syntax
            if (token.SemanticRole
                is SemanticRole.TernaryQuestion
                or SemanticRole.TernaryColon)
            {
                return new DefinitionKey(OperatorKind, "c?t:f", []);
            }

            // (+)(-)(!)(^)(*)(&)(=>) overlaps with other operators, requires full name
            string? operatorName = GetOperatorNameExtension(token);
            if (operatorName is not null)
            {
                return new DefinitionKey(OperatorKind, token.Text, [operatorName]);
            }

            // default key
            return new DefinitionKey(OperatorKind, token.Text, []);
        }

        private static string? GetOperatorNameExtension(NavToken token)
        {
            // (+)(-) unary plus / unary minus
            if (token.IsUnaryPlusOperator())
                return "UnaryPlus";
            if (token.IsUnaryMinusOperator())
                return "UnaryMinus";

            // (!) logical NOT / null forgiving
            if (token.IsLogicalNotOperator())
                return "LogicalNot";
            if (token.IsNullForgivingOperator())
                return "NullForgiving";

            // (^) index / bitwise xor
            if (token.IsIndexFromEndOperator())
                return "Index";
            if (token.IsBitwiseXorOperator())
                return "BitwiseXor";

            // (*) multiplication / dereference
            if (token.IsMultiplicationOperator())
                return "Multiplication";
            if (token.IsDereferenceOperator())
                return "Dereference";

            // (&) address-of / bitwise and
            if (token.IsAddressOfOperator())
                return "AddressOf";
            if (token.IsBitwiseAndOperator())
                return "BitwiseAnd";

            // (=>) lambda / expression body arrow
            if (token.IsLambdaOperator())
                return "Lambda";

            return null;
        }
    }
}
