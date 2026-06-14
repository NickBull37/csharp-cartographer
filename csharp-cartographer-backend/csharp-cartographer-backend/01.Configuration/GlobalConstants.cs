namespace csharp_cartographer_backend._01.Configuration
{
    public static class GlobalConstants
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
        /// |                   IDENTIFIERS                  |
        /// **************************************************

        public static readonly HashSet<string> CommonEnums = new(StringComparer.Ordinal)
        {
            "Accessibility",
            "AttributeTargets",
            "BindingFlags",
            "ConsoleColor",
            "ConsoleKey",
            "ConsoleModifiers",
            "DateTimeKind",
            "DayOfWeek",
            "EnvironmentVariableTarget",
            "FileAccess",
            "FileMode",
            "FileShare",
            "GroupRole",
            "HttpStatusCode",
            "MidpointRounding",
            "PrimaryKind",
            "RegexOptions",
            "SearchOption",
            "SemanticModifiers",
            "SemanticRole",
            "StringComparison",
            "StringSplitOptions",
            "SymbolKind",
            "SyntaxKind",
            "TaskStatus",
            "TypeKind",
        };

        public static readonly HashSet<string> CommonStructs = new(StringComparer.Ordinal)
        {
            "AncestorNodeKinds",
            "Boolean",
            "CancellationToken",
            "DateOnly",
            "DateTime",
            "Decimal",
            "Double",
            "Guid",
            "HashCode",
            "ImmutableArray",
            "Index",
            "Int32",
            "Int64",
            "IntPtr",
            "KeyValuePair",
            "Point",
            "Range",
            "ReadOnlySpan",
            "Rectangle",
            "Span",
            "SyntaxToken",
            "TextSpan",
            "TimeOnly",
            "TimeSpan",
            "ValueTuple",
        };

        /// **************************************************
        /// |                    KEYWORDS                    |
        /// **************************************************

        /// ------------  All C# Keywords (reserved & contextual)  ------------ ///
        public static readonly HashSet<string> Keywords =
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

        /// ------------  Contextual Keywords  ------------ ///
        public static readonly HashSet<string> ContextualKeywords =
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

        /// <summary>
        /// bool / byte / sbyte / short / ushort / int / uint / long /
        /// ulong / char / float / double / decimal / string / object /
        /// nint / nuint
        /// </summary>
        public static readonly HashSet<string> PredefinedTypes =
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

        /// <summary>
        /// abstract / file / partial / readonly / ref / sealed / static / unsafe
        /// </summary>
        public static readonly HashSet<string> TypeModifiers =
        [
            "abstract",
            "file",
            "partial",
            "readonly",
            "ref",
            "sealed",
            "static",
            "unsafe",
        ];

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
    }
}
