namespace csharp_cartographer_backend._01.Configuration
{
    /// **************************************************
    /// |                   OPERATORS                    |
    /// **************************************************
    public partial class GlobalConstants
    {
        /// ------  All C# Operators  ------ ///
        public static readonly HashSet<string> Operators =
        [
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

            // bitwise
            "&",
            "|",
            "^",
            "~",

            // boolean logical
            "&&",
            "||",
            "!",

            // comparison (relational)
            "<",
            ">",
            "<=",
            ">=",
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

            // member access
            ".",
            "?.",
            "::",

            // null-coalescing
            "??",
            "??=",

            // null
            "!",
            "?[",

            // shift
            "<<",
            ">>",
            ">>>",

            // ternary (split)
            "?",
            ":",

            // keyword - operators
            //"sizeof",
            //"typeof",
        ];

        /// Arithmetic
        public static readonly HashSet<string> ArithmeticOperators =
        [
            "+",
            "-",
            "*",
            "/",
            "%",
            "++",
            "--",
        ];

        /// Assignment
        public static readonly HashSet<string> AssignmentOperators =
        [
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
        ];

        /// Bitwise
        public static readonly HashSet<string> BitwiseOperators =
        [
            "&",
            "|",
            "^",
            "~",
        ];

        /// Boolean Logical
        public static readonly HashSet<string> BooleanLogicalOperators =
        [
            // boolean logical
            "&&",
            "||",
            "!",

            // only logical operators when operands are bools (can't distinguish)
            //"&",
            //"|",
            //"^",
        ];

        /// Comparison
        public static readonly HashSet<string> ComparisonOperators =
        [
            "==",
            "!=",
            "<",
            ">",
            "<=",
            ">=",
        ];

        /// Indirection
        public static readonly HashSet<string> IndirectionOperators =
        [
            "&",
            "*",
            "->",
        ];

        /// Shift
        public static readonly HashSet<string> ShiftOperators =
        [
            "<<",
            ">>",
            ">>>",
        ];

        /// Member Access
        //public static readonly List<string> MemberAccessOperators =
        //[
        //    ".",
        //    "?.",
        //    "::",
        //];

        ///// Pointers
        //public static readonly List<string> PointerOperators =
        //[
        //    "&",
        //    "*",
        //    "->",
        //];
    }
}
