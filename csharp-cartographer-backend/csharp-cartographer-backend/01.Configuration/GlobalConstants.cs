namespace csharp_cartographer_backend._01.Configuration
{
    public partial class GlobalConstants
    {
        /// **************************************************
        /// |                   PUNCTUATORS                  |
        /// **************************************************
        public static readonly List<string> Punctuators =
        [
            ".",
            ",",
            ";",
            ":",
            "?",
            "_",
        ];

        /// **************************************************
        /// |                   DELIMITERS                   |
        /// **************************************************
        public static readonly List<string> Delimiters =
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

        /// **************************************************
        /// |                   OPERATORS                    |
        /// **************************************************
        public static readonly List<string> Operators =
        [
            // arithmetic - unary
            "+",
            "-",
            "++",
            "--",

            // arithmetic - binary
            "+",
            "-",
            "*",
            "/",
            "%",

            // comparison (relational)
            "<",
            ">",
            "<=",
            ">=",
            "==",
            "!=",

            // boolean logical - unary logical
            "!",

            // boolean logical - binary logical
            "&",
            "|",
            "^",

            // boolean logical - binary conditional logical
            "&&",
            "||",

            // bitwise and shift - unary
            "~",

            // bitwise and shift - binary
            "<<",
            ">>",
            ">>>",
            "&",
            "|",
            "^",

            // equality (same as comparison?)
            "==",
            "!=",

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
            ">>>=",

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
            "?.", // null-conditional
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
