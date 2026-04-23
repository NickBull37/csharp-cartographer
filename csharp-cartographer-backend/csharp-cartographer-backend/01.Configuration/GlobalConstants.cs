namespace csharp_cartographer_backend._01.Configuration
{
    public partial class GlobalConstants
    {
        /// **************************************************
        /// |                   DELIMITERS                   |
        /// **************************************************
        public static readonly HashSet<string> Delimiters = new(StringComparer.Ordinal)
        {
            "(",
            ")",
            "[",
            "]",
            "{",
            "}",
            "<",
            ">"
        };

        /// **************************************************
        /// |                  PUNCTUATION                   |
        /// **************************************************
        public static readonly HashSet<string> Punctuation = new(StringComparer.Ordinal)
        {
            ".",
            ",",
            ";",
            ":",
            "::",
            "?",
            "_",
        };

        /// **************************************************
        /// |                   OPERATORS                    |
        /// **************************************************
        public static readonly HashSet<string> Operators = new(StringComparer.Ordinal)
        {
            // arithmetic
            "+",
            "-",
            "*",
            "/",
            "%",
            "++",
            "--",

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
            "??=",

            // bitwise
            "&",
            "|",
            "^",
            "~",

            // boolean logical (only when operands are bools)
            "&",
            "^",
            "|",

            // boolean logical conditional (short-circuit)
            "&&",
            "||",
            "!",

            // comparison (relational)
            "<",
            ">",
            "<=",
            ">=",

            // equality
            "==",
            "!=",

            // index & range
            "^",
            "..",

            // indirection
            "&",
            "*",
            "->",

            // lambda
            "=>",

            // member/element access
            ".",
            "x?.y",
            "x?[y]",

            // null-coalescing
            "??",

            // null-forgiving
            "x!",

            // shift
            "<<",
            ">>",
            ">>>",

            // ternary
            "c?t:f",

            // keyword operators
            "as",
            "default",
            "is",
            "nameof",
            "sizeof",
            "typeof",
        };
    }
}
