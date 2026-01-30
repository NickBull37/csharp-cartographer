namespace csharp_cartographer_backend._01.Configuration
{
    /// **************************************************
    /// |                    KEYWORDS                    |
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

            // bitwise-shift
            "<<",
            ">>",
            ">>>",

            // boolean logical
            "!",
            "&",
            "|",
            "^",

            // boolean logical - conditional
            "&&",
            "||",

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
            //"??",
            //"??=",
            "?[",

            // pointer
            "&",
            "*",
            "->",
        ];

        /// ------  Special Case Operators  ------ ///
        public static readonly List<string> SpecialCaseOperators =
        [
            // Operators that fall into multiple semantic roles

            "!",    // BooleanLogical : !flag
                    // Null            : obj!.Member   (null-forgiving)

            "&",    // BitwiseShift   : x & y
                    // BooleanLogical : a & b          (when a and b are bool)
                    // Pointer        : int* p = &x;

            "|",    // BitwiseShift   : x | y
                    // BooleanLogical : a | b          (when a and b are bool)

            "^",    // BitwiseShift   : x ^ y
                    // BooleanLogical : a ^ b          (when a and b are bool)
                    // IndexRange     : arr[^1]

            "*",    // Arithmetic     : x * y
                    // Pointer        : int* p;   or   *p
        ];

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

        /// Bitwise & Shift
        public static readonly List<string> BitwiseShiftOperators =
        [
            // bitwise
            "&",
            "|",
            "^",
            "~",

            // shift
            "<<",
            ">>",
            ">>>",
        ];

        /// Boolean Logical
        public static readonly List<string> BooleanLogicalOperators =
        [
            // boolean logical
            "!",
            "&",
            "|",
            "^",

            // boolean logical - conditional
            "&&",
            "||",
        ];

        /// Comparison
        public static readonly List<string> ComparisonOperators =
        [
            // comparison (relational)
            "<",
            ">",
            "<=",
            ">=",
            "==",
            "!=",
        ];

        /// Index & Range
        public static readonly List<string> IndexRangeOperators =
        [
            "^", // index from end
            "..",
        ];

        /// Lambda
        public static readonly List<string> LambdaOperators =
        [
            "=>",
        ];

        /// Member Access
        public static readonly List<string> MemberAccessOperators =
        [
            ".",
            "?.",
            "::",
        ];

        /// Null
        public static readonly List<string> NullOperators =
        [
            "!",
            "??",
            "??=",
            "?[",
        ];

        /// Pointers
        public static readonly List<string> PointerOperators =
        [
            "&",
            "*",
            "->",
        ];
    }
}
