namespace csharp_cartographer_backend._01.Configuration
{
    /// **************************************************
    /// |                   OPERATORS                    |
    /// **************************************************
    public partial class GlobalConstants
    {
        /// ------  All C# Operators  ------ ///
        public static readonly List<string> Operators =
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

        /// ------  Special Case Operators  ------ ///
        //public static readonly List<string> SpecialCaseOperators =
        //[
        //    // Operators that fall into multiple semantic roles

        //    "!",    // BooleanLogical : !flag
        //            // Null            : obj!.Member   (null-forgiving)

        //    "&",    // BitwiseShift   : x & y
        //            // BooleanLogical : a & b          (when a and b are bool)
        //            // Pointer        : int* p = &x;

        //    "|",    // BitwiseShift   : x | y
        //            // BooleanLogical : a | b          (when a and b are bool)

        //    "^",    // BitwiseShift   : x ^ y
        //            // BooleanLogical : a ^ b          (when a and b are bool)
        //            // IndexRange     : arr[^1]

        //    //"*",    // Arithmetic     : x * y
        //            // Pointer        : int* p;   or   *p
        //];

        /// Arithmetic
        public static readonly List<string> ArithmeticOperators =
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
        public static readonly List<string> AssignmentOperators =
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
        public static readonly List<string> BitwiseOperators =
        [
            "&",
            "|",
            "^",
            "~",
        ];

        /// Boolean Logical
        public static readonly List<string> BooleanLogicalOperators =
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
        public static readonly List<string> ComparisonOperators =
        [
            "==",
            "!=",
            "<",
            ">",
            "<=",
            ">=",
        ];

        /// Indirection
        public static readonly List<string> IndirectionOperators =
        [
            "&",
            "*",
            "->",
        ];

        /// Shift
        public static readonly List<string> ShiftOperators =
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
