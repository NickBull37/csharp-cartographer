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

        public static readonly List<string> Accessors =
        [
            "get",
            "set",
            "init",
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

        public static readonly List<string> ConditionalKeywords =
        [
            "if",
            "else",
        ];

        public static readonly List<string> LoopKeywords =
        [
            "do",
            "for",
            "foreach",
            "while",
        ];

        public static readonly List<string> JumpKeywords =
        [
            "return",
            "break",
            "continue",
        ];

        public static readonly List<string> PatternMatchingKeywords =
        [
            "and",
            "as",
            "case",
            "is",
            "not",
            "or",
            "switch",
            "when",
        ];

        public static readonly List<string> _genericCollections =
        [
            "List",
        ];
    }
}
