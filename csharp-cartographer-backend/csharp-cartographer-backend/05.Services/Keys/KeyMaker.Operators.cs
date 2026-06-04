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

        /// Operator Key: 
        /// [kindabrv]:[extension]:(modifier)
        /// OP:{token.Text}

        private static string? GetOperatorKey(NavToken token)
        {
            // (*)(.) look like operators but aren't
            if (token.IsPointerTypeIndicator() || token.IsQualifiedNameSeparator())
                return null;

            // (?.) split into multiple tokens, requires full operator syntax
            if (IsNullConditional(token))
                return Key(OP, "?.");

            // (c?t:f) split into multiple tokens, requires full operator syntax
            if (IsTernary(token))
                return Key(OP, "c?t:f");

            // (+)(-)(!)(^)(*)(&)(..)(=>) overlaps with other operators, requires full name
            if (TryGetOperatorNameExtension(token, out var operatorName))
                return Key(OP, token.Text, operatorName);

            // default key
            return Key(OP, token.Text);
        }

        private static bool TryGetOperatorNameExtension(NavToken token, out string? extension)
        {
            extension = null;

            // (+)(-) unary plus / unary minus
            if (token.IsUnaryPlusOperator())
                extension = "UnaryPlus";
            if (token.IsUnaryMinusOperator())
                extension = "UnaryMinus";

            // (!) logical NOT / null forgiving
            if (token.IsLogicalNotOperator())
                extension = "LogicalNot";
            if (token.IsNullForgivingOperator())
                extension = "NullForgiving";

            // (^) index / bitwise xor
            if (token.IsIndexFromEndOperator())
                extension = "Index";
            if (token.IsBitwiseXorOperator())
                extension = "BitwiseXor";

            // (*) multiplication / dereference
            if (token.IsMultiplicationOperator())
                extension = "Multiplication";
            if (token.IsDereferenceOperator())
                extension = "Dereference";

            // (&) address-of / bitwise and
            if (token.IsAddressOfOperator())
                extension = "AddressOf";
            if (token.IsBitwiseAndOperator())
                extension = "BitwiseAnd";

            // (..) range / slice
            if (token.IsRangeOperator())
                extension = "Range";
            if (token.IsSliceOperator())
                extension = "Slice";

            // (=>) lambda / expression body arrow
            if (token.IsLambdaOperator())
                extension = "Lambda";

            return !string.IsNullOrEmpty(extension);
        }

        private static bool IsNullConditional(NavToken token)
            => token.SemanticRole
                is SemanticRole.NullConditionalDot
                or SemanticRole.NullConditionalQuestion;

        private static bool IsTernary(NavToken token)
            => token.SemanticRole
                is SemanticRole.TernaryQuestion
                or SemanticRole.TernaryColon;
    }
}
