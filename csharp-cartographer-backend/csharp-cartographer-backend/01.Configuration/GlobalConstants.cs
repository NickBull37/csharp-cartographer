namespace csharp_cartographer_backend._01.Configuration
{
    public class GlobalConstants
    {
        public static readonly List<string> _punctuationChars =
        [
            ".",
            ",",
            ";",
            ":",
            "?"
        ];

        public static readonly List<string> _delimiterChars =
        [
            "(",
            ")",
            "[",
            "]",
            "{",
            "}",
            "<",
            ">"
        ];

        public static readonly List<string> _primitiveTypes =
        [
            "StringKeyword",
            "DecimalKeyword",
            "DoubleKeyword",
            "IntKeyword",
            "CharKeyword",
            "FloatKeyword",
            "BoolKeyword"
        ];

        public static readonly List<string> _predefinedTypes =
        [
            "byte",
            "sbyte",
            "short",
            "ushort",
            "int",
            "uint",
            "long",
            "ulong",
            "char",
            "float",
            "double",
            "decimal",
            "bool",
            "string",
            "object",
            "dynamic",
        ];

        public static readonly List<string> _primitiveIntegralTypes =
        [
            "byte",
            "sbyte",
            "short",
            "ushort",
            "int",
            "uint",
            "long",
            "ulong",
            "char",
        ];

        public static readonly List<string> _primitiveFloatingPointTypes =
        [
            "double",
            "float"
        ];

        public static readonly List<string> _accessModifiers =
        [
            "PublicKeyword",
            "PrivateKeyword",
            "ProtectedKeyword",
            "InternalKeyword"
        ];

        public static readonly List<string> _modifiers =
        [
            "AbstractKeyword",
            "AsyncKeyword",
            "ConstKeyword",
            "OverrideKeyword",
            "ReadOnlyKeyword",
            "SealedKeyword",
            "StaticKeyword",
            "VirtualKeyword",
            "VolatileKeyword"
        ];

        public static readonly List<string> _literalKinds =
        [
            "NumericLiteralToken",
            "StringLiteralToken"
        ];

        public static readonly List<string> _genericCollections =
        [
            "List",
        ];

        public static readonly List<string> _operators =
        [
            // arithmetic
            "+",
            "-",
            "*",
            "/",
            "%",
            // comparison (relational)
            "==",
            "!=",
            ">",
            "<",
            ">=",
            "<=",
            // logical
            "&&",
            "||",
            "!",
            // bitwise
            "&",
            "|",
            "^",
            "~",
            "<<",
            ">>",
            // assignment
            "=",
            "+=",
            "-=",
            "*=",
            "/=",
            "%=",
            "&=",
            "|=",
            "^=",
            "<<=",
            ">>=",
            // unary (single-operand)
            "+",
            "-",
            "++",
            "--",
            "!",
            "~",
            // null-coalescing
            "??",
            "??=",
            // type
            "is",
            "as",
            "sizeof",
            "typeof",
            // lambda
            "=>",
            // index & range
            "[]",
            "..",
            "^",
            // member access
            ".",
            "?.",
            "::",
            // misc
            "new",
            "checked",
            "unchecked",
            "default",
            "nameof",
            "stackalloc"
        ];
    }
}
