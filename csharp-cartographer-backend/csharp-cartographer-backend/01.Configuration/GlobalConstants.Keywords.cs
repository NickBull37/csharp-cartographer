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

        /// ------------  Contextual Keywords  ------------ ///
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

        /// Access Modifiers
        public static readonly List<string> AccessModifiers =
        [
            "public",
            "private",
            "protected",
            "internal",
        ];

        public static readonly List<Keyword> AccessModifiersTest =
        [
            new Keyword ("public", true),
            new Keyword ("private", true),
            new Keyword ("protected", true),
            new Keyword ("internal", true),
        ];

        /// Accessors
        public static readonly List<string> AccessorKeywords =
        [
            "get",
            "set",
            "init",

            /*
             *  Covered by EventHandling
             *  
             *  - add
             *  - remove
             */
        ];

        /// Argument modifiers
        public static readonly List<string> ArgumentModifiers =
        [
            "in",
            "out",
            "ref",
        ];

        /// Compilation Scope
        public static readonly List<string> CompilationScopeKeywords =
        [
            "alias",
            "file",
            "global",
            "namespace",
            "using",
        ];

        /// Concurrency
        public static readonly List<string> ConcurrencyKeywords =
        [
            "await",
            "lock",

            /*
             *  Covered by ConcurrencyModifier
             *  
             *  - async
             */
        ];

        /// Conditional Branching
        public static readonly List<string> ConditionalBranchingKeywords =
        [
            "if",
            "else",
        ];

        /// Constraints
        public static readonly List<string> ConstraintKeywords =
        [
            "managed",
            "notnull",
            "unmanaged",

            /*
             *  Covered by manual check since "where"
             *  can fall under multiple roles
             *  
             *  - where
             */
        ];

        /// Control Flow
        public static readonly List<string> ControlFlowKeywords =
        [
            "switch",

            /*
             *  Covered by manual check since "case" & "default
             *  can fall under multiple roles
             *  
             *  - case
             *  - default
             */
        ];

        /// <summary>
        /// case / default / switch
        /// </summary>
        public static readonly List<Keyword> ControlFlowKeywordsTest =
        [
            new Keyword ("switch", true),
            new Keyword ("case", false),
            new Keyword ("default", false),
        ];

        /// Events
        public static readonly List<string> EventKeywords =
        [
            "event",
            "add",
            "remove",
        ];

        /// Exception Handling
        public static readonly List<string> ExceptionHandlingKeywords =
        [
            "try",
            "catch",
            "finally",
            "throw",
        ];

        /// Iterators
        public static readonly List<string> IteratorKeywords =
        [
            "yield",
        ];

        /// Jump Statements
        public static readonly List<string> JumpStatementKeywords =
        [
            "break",
            "continue",
            "goto",
            "return",
        ];

        /// Loop Statements
        public static readonly List<string> LoopStatementKeywords =
        [
            "do",
            "for",
            "foreach",
            "while",
        ];

        /// Member Modifiers
        public static readonly List<string> MemberModifiers =
        [
            "async",
            "const",
            "extern",
            "partial",
            "readonly",
            "required",
            "static",
            "volatile",
            "unsafe",

            /*
             *  Covered by PolymorphismModifier
             *  
             *  - abstract
             *  - sealed
             */

            /*
             *  Covered by manual check since "new" can
             *  also fall under other semantic roles
             *  
             *  - new
             */
        ];

        /// Object Construction
        public static readonly List<string> ObjectConstructionKeywords =
        [
            "base",

            /*
             *  Covered by manual check since "new" and "this" can
             *  also fall under other semantic roles
             *  
             *  - new
             *  - this
             */
        ];

        /// Parameter modifiers
        public static readonly List<string> ParameterModifiers =
        [
            "in",
            "out",
            "params",
            "ref",
            "scoped",
            "this",
        ];

        /// Pattern Matching
        public static readonly List<string> PatternMatchingKeywords =
        [
            "and",
            "as",
            "is",
            "not",
            "or",
            "when",

            /*
             *  Covered by manual check since "case" can
             *  also fall under control flow
             *  
             *  - case
             */
        ];

        /// Polymorphism modifiers
        public static readonly List<string> PolymorphismModifiers =
        [
            "abstract",
            "override",
            "sealed",
            "virtual",
        ];

        /// Pre-defined Types
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

        /// Query Expression
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

            /*
             *  Covered by manual check since "in" can also fall
             *  under loop statements and parameter modifiers
             *  
             *  - in
             */
        ];

        /// Safety Context
        public static readonly List<string> SafetyContextKeywords =
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

        /// Type Declarations
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

        /// Type Modifiers
        public static readonly List<string> TypeModifiers =
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

        /// Type System
        public static readonly List<string> TypeSystemKeywords =
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
