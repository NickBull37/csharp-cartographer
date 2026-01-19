namespace csharp_cartographer_backend._01.Configuration
{
    public class GlobalConstants
    {
        public static readonly List<string> Punctuators =
        [
            ".",
            ",",
            ";",
            ":",
            "?",
            "_",
        ];

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

        /// **************************************************
        /// |         KEYWORDS: Reserved & Contextual        |
        /// **************************************************
        public static readonly List<string> Keywords =
        [
            "abstract",
            "add",
            "alias",
            "and",
            "as",
            "ascending",
            "async",
            "await",
            "base",
            "bool",
            "by",
            "byte",
            "break",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "decimal",
            "default",
            "delegate",
            "descending",
            "do",
            "double",
            "dynamic",
            "else",
            "enum",
            "equals",
            "event",
            "explicit",
            "false",
            "file",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "from",
            "get",
            "global",
            "group",
            "goto",
            "if",
            "implicit",
            "in",
            "init",
            "int",
            "interface",
            "internal",
            "into",
            "is",
            "join",
            "let",
            "lock",
            "long",
            "managed",
            "nameof",
            "namespace",
            "new",
            "nint",
            "not",
            "notnull",
            "nuint",
            "null",
            "object",
            "on",
            "operator",
            "or",
            "orderby",
            "out",
            "override",
            "params",
            "partial",
            "private",
            "protected",
            "public",
            "readonly",
            "record",
            "ref",
            "remove",
            "required",
            "return",
            "sbyte",
            "sealed",
            "select",
            "set",
            "short",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unmanaged",
            "ushort",
            "unchecked",
            "unsafe",
            "using",
            "value",
            "var",
            "virtual",
            "void",
            "volatile",
            "when",
            "where",
            "while",
            "with",
            "yield",
        ];

        /// **************************************************
        /// |            KEYWORDS: Contextual only           |
        /// **************************************************
        public static readonly List<string> ContextualKeywords =
        [
            "add",
            "alias",
            "and",
            "ascending",
            "async",
            "await",
            "by",
            "descending",
            "dynamic",
            "equals",
            "file",
            "from",
            "get",
            "global",
            "group",
            "init",
            "into",
            "join",
            "let",
            "managed",
            "nameof",
            "nint",
            "not",
            "notnull",
            "nuint",
            "on",
            "or",
            "orderby",
            "partial",
            "record",
            "remove",
            "required",
            "select",
            "set",
            "unmanaged",
            "value",
            "var",
            "when",
            "where",
            "with",
            "yield"
        ];

        /*
         *  Keywords that fall into multiple buckets
         *  
         *  new         (object creation and member hiding)
         *  where       (LINQ query and generic constraints)
         *  default     (switch label and default literal)
         *  case        (switch section and pattern case)
         *  in          (foreach loops and query expressions)
         * 
         */

        /// **************************************************
        /// |            KEYWORDS: Accessors           |
        /// **************************************************
        public static readonly List<string> AccessorKeywords =
        [
            "get",
            "set",
            "init",
            "value",
        ];

        /// **************************************************
        /// |            KEYWORDS: Access Modifiers           |
        /// **************************************************
        public static readonly List<string> AccessModifiers =
        [
            "public",
            "private",
            "protected",
            "internal",
        ];

        /// **************************************************
        /// |            KEYWORDS: Concurrency           |
        /// **************************************************
        public static readonly List<string> ConcurrencyKeywords =
        [
            "async",
            "await",
            "lock",
        ];

        /// **************************************************
        /// |            KEYWORDS: Conditional Branching           |
        /// **************************************************
        public static readonly List<string> ConditionalBranchingKeywords =
        [
            "if",
            "else",
        ];

        /// **************************************************
        /// |            KEYWORDS: Constraints           |
        /// **************************************************
        public static readonly List<string> ConstraintKeywords =
        [
            "managed",
            "notnull",
            "unmanaged",
            "where",
        ];

        /// **************************************************
        /// |            KEYWORDS: Control Flow           |
        /// **************************************************
        public static readonly List<string> ControlFlowKeywords =
        [
            "switch",
            "case",
            "default",
        ];

        /// **************************************************
        /// |            KEYWORDS: Events           |
        /// **************************************************
        public static readonly List<string> EventKeywords =
        [
            "event",
            "add",
            "remove",
        ];

        /// **************************************************
        /// |            KEYWORDS: Exception Handling           |
        /// **************************************************
        public static readonly List<string> ExceptionHandlingKeywords =
        [
            "try",
            "catch",
            "finally",
            "throw",
        ];

        /// **************************************************
        /// |            KEYWORDS: Inheritance Modifiers           |
        /// **************************************************
        public static readonly List<string> InheritanceModifiers =
        [
            "abstract",
            "new", // for member hiding
            "override",
            "sealed",
            "virtual",
        ];

        /// **************************************************
        /// |            KEYWORDS: Iterators           |
        /// **************************************************
        public static readonly List<string> IteratorKeywords =
        [
            "yield",
        ];

        /// **************************************************
        /// |            KEYWORDS: Jump Statements           |
        /// **************************************************
        public static readonly List<string> JumpStatementKeywords =
        [
            "break",
            "continue",
            "goto",
            "return",
        ];

        /// **************************************************
        /// |            KEYWORDS: Literals           |
        /// **************************************************
        public static readonly List<string> LiteralKeywords =
        [
            "true",
            "false",
            "null",
        ];

        /// **************************************************
        /// |            KEYWORDS: Loop           |
        /// **************************************************
        public static readonly List<string> LoopStatementKeywords =
        [
            "do",
            "for",
            "foreach",
            "while",
        ];

        /// **************************************************
        /// |            KEYWORDS: Member Modifiers           |
        /// **************************************************
        public static readonly List<string> MemberModifiers =
        [
            "const",
            "partial",
            "readonly",
            "required",
            "static",
            "volatile",
        ];

        /// **************************************************
        /// |            KEYWORDS: Source Scope           |
        /// **************************************************
        public static readonly List<string> SourceScopeKeywords =
        [
            "alias",
            "file",
            "global",
            "namespace",
            "using",
        ];

        /// **************************************************
        /// |            KEYWORDS: Object Construction           |
        /// **************************************************
        public static readonly List<string> ObjectConstructionKeywords =
        [
            "base",
            "new",
            "this",
        ];

        /// **************************************************
        /// |            KEYWORDS: Parameter Modifiers           |
        /// **************************************************
        public static readonly List<string> ParameterModifiers =
        [
            "in",
            "out",
            "params",
            "ref",
        ];

        /// **************************************************
        /// |            KEYWORDS: Pattern Matching           |
        /// **************************************************
        public static readonly List<string> PatternMatchingKeywords =
        [
            "and",
            "as",
            "is",
            "not",
            "or",
            "when",
        ];

        /// **************************************************
        /// |            KEYWORDS: Pre-defined Types           |
        /// **************************************************
        public static readonly List<string> PredefinedTypes =
        [
            "bool",
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
            "string",
            "object",
            "nint",
            "nuint",
        ];

        /// **************************************************
        /// |            KEYWORDS: Query Expression           |
        /// **************************************************
        public static readonly List<string> QueryExpressionKeywords =
        [
            "ascending",
            "by",
            "descending",
            "equals",
            "from",
            "group",
            "into",
            "join",
            "let",
            "on",
            "orderby",
            "select",
            "where",
        ];

        /// **************************************************
        /// |            KEYWORDS: Safety Context           |
        /// **************************************************
        public static readonly List<string> SafetyContextKeywords =
        [
            "checked",
            "fixed",
            "sizeof",
            "stackalloc",
            "typeof",
            "unchecked",
            "unsafe",
        ];

        /// **************************************************
        /// |            KEYWORDS: Type Declarations           |
        /// **************************************************
        public static readonly List<string> TypeDeclarationKeywords =
        [
            "class",
            "delegate",
            "enum",
            "interface",
            "operator",
            "record",
            "struct",
        ];

        /// **************************************************
        /// |            KEYWORDS: Type System           |
        /// **************************************************
        public static readonly List<string> TypeReferenceKeywords =
        [
            "bool",
            "byte",
            "char",
            "decimal",
            "double",
            "float",
            "int",
            "long",
            "nint",
            "nuint",
            "object",
            "sbyte",
            "short",
            "string",
            "uint",
            "ulong",
            "ushort",
        ];

        /// **************************************************
        /// |            KEYWORDS: Type System           |
        /// **************************************************
        public static readonly List<string> TypeSystemKeywords =
        [
            "dynamic",
            "nameof",
            //"var", covered by 
        ];
    }
}
