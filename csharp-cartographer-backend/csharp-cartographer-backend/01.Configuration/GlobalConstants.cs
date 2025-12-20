namespace csharp_cartographer_backend._01.Configuration
{
    public class GlobalConstants
    {
        public static readonly List<string> _punctuations =
        [
            ".",
            ",",
            ";",
            ":",
            "?"
        ];

        public static readonly List<string> _delimiters =
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

        public static readonly List<string> PredefinedTypes =
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

        public static readonly List<string> AccessModifiers =
        [
            "public",
            "protected",
            "internal",
            "private",
        ];

        public static readonly List<string> Modifiers =
        [
            "abstract",
            "async",
            "const",
            "override",
            "partial",
            "readonly",
            "required",
            "sealed",
            "static",
            "virtual",
            "volatile"
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

        public static readonly List<string> _genericCollections =
        [
            "List",
        ];
    }
}
