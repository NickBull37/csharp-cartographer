namespace csharp_cartographer_backend._01.Configuration
{
    /// **************************************************
    /// |                    KEYWORDS                    |
    /// **************************************************
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

        /// ------------  Special Case Keywords  ------------ ///
        public static readonly List<string> SpecialCaseKeywords =
        [
            // Keywords that fall into multiple semantic roles

            "case",         // switch section / pattern case
            "default",      // switch label / default literal
            "in",           // foreach loops / query expressions / param modifiers
            "new",          // object creation / member hiding
            "where",        // query expressions / generic constraints
        ];



        /// ------  Modifier Keywords  ------ ///

        /// Access Modifiers
        public static readonly List<string> AccessModifiers =
        [
            "public",
            "private",
            "protected",
            "internal",
        ];

        /// Argument / Parameter Modifiers
        //public static readonly List<string> ArgParamModifiers =
        //[
        //    "in",
        //    "out",
        //    "params",
        //    "ref",
        //];

        /// Argument modifiers
        public static readonly List<string> ArgumentModifiers =
        [
            "in",
            "out",
            "ref",
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

        /// Inheritance modifiers
        //public static readonly List<string> InheritanceModifiers =
        //[
        //    "abstract",
        //    "new",         // for member hiding
        //    "override",
        //    "sealed",
        //    "virtual",
        //];

        /// Polymorphism modifiers
        public static readonly List<string> PolymorphismModifiers =
        [
            "abstract",
            "override",
            "sealed",
            "virtual",
        ];

        /// Member Modifiers
        public static readonly List<string> MemberModifiers =
        [
            // covered by Polymorphism modifiers
            // "abstract",
            // "sealed",

            "async",
            "const",
            "extern",
            "new",
            "partial",
            "readonly",
            "required",
            "static",
            "volatile",
            "unsafe",
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



        /// Accessors
        public static readonly List<string> AccessorKeywords =
        [
            "get",
            "set",
            "init",

            // covered by ImplicitParameter
            //"value",

            // covered by EventHandling
            //"add",
            //"remove",
        ];

        /// Concurrency
        public static readonly List<string> ConcurrencyKeywords =
        [
            "await",
            "lock",

            // covered by ConcurrencyModifier
            // "async",
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
            "where",
        ];

        /// Control Flow
        public static readonly List<string> ControlFlowKeywords =
        [
            "switch",
            "case",
            "default",
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

        /// Compilation Scope
        public static readonly List<string> CompilationScopeKeywords =
        [
            "alias",
            "file",
            "global",
            "namespace",
            "using",
        ];

        /// Object Construction
        public static readonly List<string> ObjectConstructionKeywords =
        [
            "base",
            "new",
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
            "in",
            "into",
            "join",
            "let",
            "on",
            "orderby",
            "select",
            "where",
        ];

        /// Safety Context
        public static readonly List<string> SafetyContextKeywords =
        [
            "checked",
            "fixed",
            "stackalloc",
            "unchecked",
            "unsafe",

            // covered by individual Operator roles
            //"sizeof",
            //"typeof",
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

        /// Type System
        public static readonly List<string> TypeSystemKeywords =
        [
            "dynamic",
            "nameof",
            //"var", covered by 
        ];
    }
}
