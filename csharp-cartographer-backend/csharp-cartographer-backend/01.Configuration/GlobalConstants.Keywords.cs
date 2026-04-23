namespace csharp_cartographer_backend._01.Configuration
{
    /// **************************************************
    /// |                    KEYWORDS                    |
    /// **************************************************

    public class Keyword(string keywordText, bool isRoleExclusive)
    {
        public string Text { get; init; } = keywordText;

        public bool IsRoleExclusive { get; init; } = isRoleExclusive;
    }

    public partial class GlobalConstants
    {
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
        /// alias / file / global / namespace / using
        /// </summary>
        public static readonly HashSet<string> CompilationScopeKeywords =
        [
            "alias",
            "file",
            "global",
            "namespace",
            "using",
        ];

        /// <summary>
        /// await / lock
        /// </summary>
        public static readonly HashSet<string> ConcurrencyKeywords =
        [
            "await",
            "lock",
        ];

        /// <summary>
        /// event / add / remove
        /// </summary>
        public static readonly HashSet<string> EventKeywords =
        [
            "event",
            "add",
            "remove",
        ];

        /// <summary>
        /// yield
        /// </summary>
        public static readonly HashSet<string> IteratorKeywords =
        [
            "yield",
        ];

        /// <summary>
        /// abstract / async / const / extern / new / override / partial /
        /// readonly / required / sealed / static / virtual / volatile / unsafe
        /// </summary>
        public static readonly HashSet<string> MemberModifiers =
        [
            "abstract",
            "async",
            "const",
            "extern",
            "new",
            "override",
            "partial",
            "readonly",
            "required",
            "sealed",
            "static",
            "virtual",
            "volatile",
            "unsafe",
        ];

        /// <summary>
        /// base / new
        /// </summary>
        public static readonly HashSet<string> ObjectConstructionKeywords =
        [
            "base",
            "new",

            /*
             *  Covered by manual check since "new" and "this" can
             *  also fall under other semantic roles
             *  
             *  - this
             */
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
        /// ascending / by / descending / equals / from / group /
        /// into / join / let / on / orderby / select / where
        /// </summary>
        public static readonly HashSet<string> QueryExpressionKeywords =
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

            /*
             *  Covered by manual check since "in" can also fall
             *  under loop statements and parameter modifiers
             *  
             *  - in
             */
        ];

        /// <summary>
        /// checked / fixed / stackalloc / unchecked / unsafe
        /// </summary>
        public static readonly HashSet<string> SafetyContextKeywords =
        [
            "checked",
            "fixed",
            "stackalloc",
            "unchecked",
            "unsafe",

            /*
             *  Covered by individual Operator roles
             *  
             *  - sizeof
             *  - typeof
             */
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

        /// <summary>
        /// dynamic / nameof
        /// </summary>
        public static readonly HashSet<string> TypeSystemKeywords =
        [
            "dynamic",
            "nameof",

            /*
             *  Covered by IdentifierType roles
             *  
             *  - var
             */
        ];
    }
}
