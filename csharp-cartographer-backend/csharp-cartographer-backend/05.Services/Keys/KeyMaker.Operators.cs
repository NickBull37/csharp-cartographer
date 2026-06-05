using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /// key structure: kindabrv:extension:modifier?
        /// operator key:  OP:{token.Text}

        private static string? GetOperatorKey(NavToken token)
        {
            // (*)(.) look like operators but aren't
            if (token.IsPointerTypeIndicator() || token.IsQualifiedNameSeparator())
                return null;

            // (?.) split into multiple tokens, requires full operator syntax
            if (token.IsNullConditionalOperator())
                return Key(OP, "?.");

            // (c?t:f) split into multiple tokens, requires full operator syntax
            if (token.IsTernaryOperator())
                return Key(OP, "c?t:f");

            // (+)(-)(!)(^)(*)(&)(..)(=>) overlaps with other operators, requires full name
            if (TryGetOperatorNameExtension(token, out var operatorName))
                return Key(OP, token.Text, operatorName);

            // default key
            return Key(OP, token.Text);
        }

        private static bool TryGetOperatorNameExtension(NavToken token, out string extension)
        {
            extension = string.Empty;

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
    }
}
