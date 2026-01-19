namespace csharp_cartographer_backend._01.Configuration.ReservedText
{
    public class ReservedTextColor
    {
        public required string Text { get; init; }

        public required string HighlightColor { get; init; }
    }

    public class ReservedTextColors
    {
        private const string _white = "color-white";
        private const string _gray = "color-gray";
        private const string _blue = "color-blue";
        private const string _lightBlue = "color-light-blue";
        private const string _darkBlue = "color-dark-blue";
        private const string _green = "color-green";
        private const string _lightGreen = "color-light-green";
        private const string _darkGreen = "color-dark-green";
        private const string _purple = "color-purple";
        private const string _orange = "color-orange";
        private const string _yellow = "color-yellow";
        private const string _red = "color-red";
        private const string _jade = "color-jade";
        private const string _olive = "color-olive";

        public static readonly List<ReservedTextColor> Keywords = new()
        {
            /// **************************************************
            /// |             KEYWORDS - Alphabetical            |
            /// **************************************************

            new ReservedTextColor
            {
                Text = "_",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "abstract",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "add",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "alias",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "and",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "as",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "ascending",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "async",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "await",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "base",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "bool",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "by",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "byte",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "break",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "case",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "catch",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "char",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "checked",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "class",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "const",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "continue",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "decimal",
                HighlightColor = _blue,
            },
            //new ReservedTextColor
            //{
            //    Text = "default",
            //    HighlightColor = _blue/_purple,
            //},
            new ReservedTextColor
            {
                Text = "delegate",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "descending",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "do",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "double",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "dynamic",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "else",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "enum",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "equals",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "event",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "explicit",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "false",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "file",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "finally",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "fixed",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "float",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "for",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "foreach",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "from",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "get",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "global",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "group",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "goto",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "if",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "implicit",
                HighlightColor = _blue,
            },
            //new ReservedTextColor
            //{
            //    Text = "in",
            //    HighlightColor = _purple/_blue,
            //},
            new ReservedTextColor
            {
                Text = "init",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "int",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "interface",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "internal",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "into",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "is",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "join",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "let",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "lock",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "long",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "managed",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "nameof",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "namespace",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "new",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "nint",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "not",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "notnull",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "nuint",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "null",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "object",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "on",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "operator",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "or",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "orderby",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "out",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "override",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "params",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "partial",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "private",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "protected",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "public",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "readonly",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "record",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "ref",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "remove",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "required",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "return",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "sbyte",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "sealed",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "select",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "set",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "short",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "sizeof",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "stackalloc",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "static",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "string",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "struct",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "switch",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "this",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "throw",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "true",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "try",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "typeof",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "uint",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "ulong",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "unmanaged",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "ushort",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "unchecked",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "unsafe",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "using",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "value",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "var",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "virtual",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "void",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "volatile",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "when",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "where",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "while",
                HighlightColor = _purple,
            },
            new ReservedTextColor
            {
                Text = "with",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "yield",
                HighlightColor = _purple,
            },
        };

        public static readonly List<ReservedTextColor> Delimiters = new()
        {
            new ReservedTextColor
            {
                Text = "(",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = ")",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "{",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "}",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "[",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "]",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = ">",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "<",
                HighlightColor = _white,
            },
        };

        public static readonly List<ReservedTextColor> Operators = new()
        {
            // Arithmetic operators
            new ReservedTextColor
            {
                Text = "+",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "-",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "*",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "/",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "%",
                HighlightColor = _gray,
            },

            // Comparison (relational) operators
            new ReservedTextColor()
            {
                Text = "==",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "!=",
                HighlightColor = _gray,
            },
            // TODO: fix aligator clips are getting marked as delimiters and operators
            new ReservedTextColor
            {
                Text = ">",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "<",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = ">=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "<=",
                HighlightColor = _gray,
            },

            // Logical operators
            new ReservedTextColor()
            {
                Text = "&&",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "||",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "!",
                HighlightColor = _gray,
            },

            // Bitwise operators
            new ReservedTextColor()
            {
                Text = "&",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "|",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "^",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "~",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "<<",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = ">>",
                HighlightColor = _gray,
            },

            // Assignment operators
            new ReservedTextColor
            {
                Text = "=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "+=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "-=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "*=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "/=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "%=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "&=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "|=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "^=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "<<=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = ">>=",
                HighlightColor = _gray,
            },
            
            // Unary (single-operand) operators
            new ReservedTextColor
            {
                Text = "+",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "-",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "++",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "--",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "!",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "~",
                HighlightColor = _gray,
            },

            // Null/Null-coalescing operators
            new ReservedTextColor
            {
                Text = "?",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "??",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "??=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = ":",
                HighlightColor = _gray,
            },

            // Type operators
            new ReservedTextColor
            {
                Text = "is",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "as",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "sizeof",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "typeof",
                HighlightColor = _blue,
            },

            // Lambda operators
            new ReservedTextColor
            {
                Text = "=>",
                HighlightColor = _gray,
            },

            // Index & Range operators
            new ReservedTextColor
            {
                Text = "..",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "^",
                HighlightColor = _gray,
            },

            // Member access operators
            new ReservedTextColor
            {
                Text = ".",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "?.",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "::",
                HighlightColor = _gray,
            },

            // Misc operators
            new ReservedTextColor
            {
                Text = "new",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "checked",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "unchecked",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "default",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "nameof",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "stackalloc",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "_",
                HighlightColor = _blue,
            },
        };

        public static readonly List<ReservedTextColor> OperatorsFromMicrosoftDocs = new()
        {
            // Arithmetic: unary (single-operand) operators
            new ReservedTextColor
            {
                Text = "+",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "-",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "++",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "--",
                HighlightColor = _gray,
            },

            // Arithmetic: binary operators
            new ReservedTextColor
            {
                Text = "+",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "-",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "*",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "/",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "%",
                HighlightColor = _gray,
            },
            //TODO: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#compound-assignment

            // Comparison (relational) operators
            new ReservedTextColor
            {
                Text = "<",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = ">",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "<=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = ">=",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "==",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "!=",
                HighlightColor = _gray,
            },

            // Boolean Logical operators
            new ReservedTextColor()
            {
                Text = "!",
                HighlightColor = _gray,
            },
            // Boolean Logical operators: binary logical
            new ReservedTextColor()
            {
                Text = "&",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "|",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "^",
                HighlightColor = _gray,
            },
            // Boolean Logical operators: binary conditional logical
            new ReservedTextColor()
            {
                Text = "&&",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "||",
                HighlightColor = _gray,
            },
            
            // Bitwise & Shift operators: unary
            new ReservedTextColor()
            {
                Text = "~",
                HighlightColor = _gray,
            },
            // Bitwise & Shift operators: binary
            new ReservedTextColor()
            {
                Text = "<<",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = ">>",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = ">>>",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "&",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "|",
                HighlightColor = _gray,
            },
            new ReservedTextColor()
            {
                Text = "^",
                HighlightColor = _gray,
            },

            // Equality operators (duplicates of comparison operators)
            new ReservedTextColor
            {
                Text = "==",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "!=",
                HighlightColor = _gray,
            },
            
            // Assignment operators
            new ReservedTextColor
            {
                Text = "=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "+=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "-=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "*=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "/=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "%=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "&=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "|=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "^=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "<<=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = ">>=",
                HighlightColor = _gray,
            },

            // Null/Null-coalescing operators
            new ReservedTextColor
            {
                Text = "?",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "??",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "??=",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = ":",
                HighlightColor = _gray,
            },

            // Type operators
            new ReservedTextColor
            {
                Text = "is",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "as",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "sizeof",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "typeof",
                HighlightColor = _blue,
            },

            // Lambda operators
            new ReservedTextColor
            {
                Text = "=>",
                HighlightColor = _gray,
            },

            // Index & Range operators
            new ReservedTextColor
            {
                Text = "..",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "^",
                HighlightColor = _gray,
            },

            // Member access operators
            new ReservedTextColor
            {
                Text = ".",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "?.",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "::",
                HighlightColor = _gray,
            },

            // Misc operators
            new ReservedTextColor
            {
                Text = "new",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "checked",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "unchecked",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "default",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "nameof",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "stackalloc",
                HighlightColor = _blue,
            },
            new ReservedTextColor
            {
                Text = "_",
                HighlightColor = _blue,
            },
        };

        public static readonly List<ReservedTextColor> Punctuators = new()
        {
            new ReservedTextColor
            {
                Text = ":",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = ";",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = ",",
                HighlightColor = _white,
            },
            new ReservedTextColor
            {
                Text = "?",
                HighlightColor = _gray,
            },
            new ReservedTextColor
            {
                Text = "_",
                HighlightColor = _blue,
            },
        };


        // currently unused
        public static readonly List<ReservedTextColor> SystemClassList = new()
        {
            #region System Library
            new ReservedTextColor
            {
                Text = "Console",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Environment",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Math",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Nullable",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Object",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Random",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "String",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections Library
            new ReservedTextColor
            {
                Text = "ArrayList",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "BitArray",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Hashtable",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Queue",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "SortedList",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Stack",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections.Specialized Library
            new ReservedTextColor
            {
                Text = "HybridDictionary",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "ListDictionary",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "NameValueCollection",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "OrderedDictionary",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "StringCollection",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "StringDictionary",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections.Generics Library
            new ReservedTextColor
            {
                Text = "Dictionary",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "HashSet",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "LinkedList",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "List",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Queue",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "SortedDictionary",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "SortedList",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "SortedSet",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "Stack",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections.ObjectModel Library
            new ReservedTextColor
            {
                Text = "Collection",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "MyKeyedCollection",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "ObservableCollection",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "ReadOnlyCollection",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "ReadOnlyDictionary",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections.Concurrent Library
            new ReservedTextColor
            {
                Text = "BlockingCollection",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "ConcurrentBag",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "ConcurrentDictionary",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "ConcurrentQueue",
                HighlightColor = _green,
            },
            new ReservedTextColor
            {
                Text = "ConcurrentStack",
                HighlightColor = _green,
            },
            #endregion

            #region System.Threading.Tasks Library
            new ReservedTextColor
            {
                Text = "Task",
                HighlightColor = _green,
            },
            #endregion
        };

        // currently unused
        public static readonly List<ReservedTextColor> SystemInterfaceList = new()
        {
            #region System.Collections Library
            new ReservedTextColor
            {
                Text = "ICollection",
                HighlightColor = _lightGreen,
            },
            new ReservedTextColor
            {
                Text = "IDictionary",
                HighlightColor = _lightGreen,
            },
            new ReservedTextColor
            {
                Text = "IEnumerable",
                HighlightColor = _lightGreen,
            },
            new ReservedTextColor
            {
                Text = "IEnumerator",
                HighlightColor = _lightGreen,
            },
            new ReservedTextColor
            {
                Text = "IList",
                HighlightColor = _lightGreen,
            },
            new ReservedTextColor
            {
                Text = "IReadOnlyCollection",
                HighlightColor = _lightGreen,
            },
            new ReservedTextColor
            {
                Text = "IReadOnlyList",
                HighlightColor = _lightGreen,
            },
            new ReservedTextColor
            {
                Text = "ISet",
                HighlightColor = _lightGreen,
            },
            #endregion
        };

        // currently unused
        public static readonly List<ReservedTextColor> SystemStructList = new()
        {
            #region System Library
            new ReservedTextColor
            {
                Text = "Boolean",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "Byte",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "Char",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "DateOnly",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "DateTime",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "Decimal",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "Double",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "Guid",
                HighlightColor = _jade,
            },
            // TODO: Index is also common property name & should stay white in that case
            //new ReservedTextElement
            //{
            //    Text = "Index",
            //    HighlightColor = _jade,
            //},
            new ReservedTextColor
            {
                Text = "Int32",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "Int64",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "Range",
                HighlightColor = _jade,
            },
            new ReservedTextColor
            {
                Text = "Single",
                HighlightColor = _jade,
            },
            // TODO: Span is also common property name & should stay white in that case
            //new ReservedTextElement
            //{
            //    Text = "Span",
            //    HighlightColor = _jade,
            //},
            new ReservedTextColor
            {
                Text = "TimeSpan",
                HighlightColor = _jade,
            },
            #endregion

            #region System.Threading Library
            new ReservedTextColor
            {
                Text = "CancellationToken",
                HighlightColor = _jade,
            },
            #endregion
        };
    }
}
