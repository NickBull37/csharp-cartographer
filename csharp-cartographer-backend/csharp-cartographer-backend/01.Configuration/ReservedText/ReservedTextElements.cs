namespace csharp_cartographer_backend._01.Configuration.ReservedText
{
    public class ReservedTextElement
    {
        public required string Text { get; init; }
        public required string HighlightColor { get; init; }
    }

    public class ReservedTextElements
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

        public static readonly List<ReservedTextElement> KeywordList = new()
        {
            /// **************************************************
            /// |             KEYWORDS - Alphabetical            |
            /// **************************************************

            new ReservedTextElement
            {
                Text = "abstract",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "async",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "await",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "base",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "bool",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "break",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "case",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "catch",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "char",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "checked",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "class",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "const",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "continue",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "decimal",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "do",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "double",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "else",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "enum",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "Equals",
                HighlightColor = _yellow,
            },
            new ReservedTextElement
            {
                Text = "Exception",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "explicit",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "false",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "finally",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "fixed",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "float",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "for",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "foreach",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "get",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "global",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "goto",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "if",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "implicit",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "in",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "int",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "interface",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "internal",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "lock",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "namespace",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "new",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "not",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "null",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "object",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "operator",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "or",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "out",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "override",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "private",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "protected",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "public",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "readonly",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "record",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "ref",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "return",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "sealed",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "set",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "sizeof",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "static",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "string",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "struct",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "switch",
                HighlightColor = _purple,
            },
            // TODO: Not System library elements, shouldn't be hardcoded
            //new ReservedTextElement
            //{
            //    Text = "SyntaxKind",
            //    HighlightColor = _olive,
            //},
            //new ReservedTextElement
            //{
            //    Text = "SyntaxToken",
            //    HighlightColor = _jade,
            //},
            //new ReservedTextElement
            //{
            //    Text = "TextSpan",
            //    HighlightColor = _jade,
            //},
            new ReservedTextElement
            {
                Text = "this",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "throw",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "true",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "try",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "typeof",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "unchecked",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "unsafe",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "using",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "var",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "virtual",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "void",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "volatile",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "when",
                HighlightColor = _purple,
            },
            new ReservedTextElement
            {
                Text = "while",
                HighlightColor = _purple,
            },
        };

        public static readonly List<ReservedTextElement> DelimiterList = new()
        {
            new ReservedTextElement
            {
                Text = "(",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = ")",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = "{",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = "}",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = "[",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = "]",
                HighlightColor = _white,
            },
        };

        public static readonly List<ReservedTextElement> OperatorList = new()
        {
            // Arithmetic operators
            new ReservedTextElement
            {
                Text = "+",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "-",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "*",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "/",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "%",
                HighlightColor = _gray,
            },

            // Comparison (relational) operators
            new ReservedTextElement()
            {
                Text = "==",
                HighlightColor = _gray,
            },
            new ReservedTextElement()
            {
                Text = "!=",
                HighlightColor = _gray,
            },
            // TODO: fix aligator clips are getting marked as delimiters and operators
            new ReservedTextElement
            {
                Text = ">",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "<",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = ">=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "<=",
                HighlightColor = _gray,
            },

            // Logical operators
            new ReservedTextElement()
            {
                Text = "&&",
                HighlightColor = _gray,
            },
            new ReservedTextElement()
            {
                Text = "||",
                HighlightColor = _gray,
            },
            new ReservedTextElement()
            {
                Text = "!",
                HighlightColor = _gray,
            },

            // Bitwise operators
            new ReservedTextElement()
            {
                Text = "&",
                HighlightColor = _gray,
            },
            new ReservedTextElement()
            {
                Text = "|",
                HighlightColor = _gray,
            },
            new ReservedTextElement()
            {
                Text = "^",
                HighlightColor = _gray,
            },
            new ReservedTextElement()
            {
                Text = "~",
                HighlightColor = _gray,
            },
            new ReservedTextElement()
            {
                Text = "<<",
                HighlightColor = _gray,
            },
            new ReservedTextElement()
            {
                Text = ">>",
                HighlightColor = _gray,
            },

            // Assignment operators
            new ReservedTextElement
            {
                Text = "=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "+=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "-=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "*=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "/=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "%=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "&=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "|=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "^=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "<<=",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = ">>=",
                HighlightColor = _gray,
            },
            
            // Unary (single-operand) operators
            new ReservedTextElement
            {
                Text = "+",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "-",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "++",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "--",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "!",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "~",
                HighlightColor = _gray,
            },

            // Null-coalescing operators
            new ReservedTextElement
            {
                Text = "??",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "??=",
                HighlightColor = _gray,
            },

            // Type operators
            new ReservedTextElement
            {
                Text = "is",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "as",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "sizeof",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "typeof",
                HighlightColor = _blue,
            },

            // Lambda operators
            new ReservedTextElement
            {
                Text = "=>",
                HighlightColor = _gray,
            },

            // Index & Range operators
            new ReservedTextElement
            {
                Text = "..",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = "^",
                HighlightColor = _gray,
            },

            // Member access operators
            new ReservedTextElement
            {
                Text = ".",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = "?.",
                HighlightColor = _gray,
            },
            new ReservedTextElement
            {
                Text = "::",
                HighlightColor = _gray,
            },

            // Misc operators
            new ReservedTextElement
            {
                Text = "new",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "checked",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "unchecked",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "default",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "nameof",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "stackalloc",
                HighlightColor = _blue,
            },
            new ReservedTextElement
            {
                Text = "_",
                HighlightColor = _blue,
            },
        };

        public static readonly List<ReservedTextElement> PunctuatorList = new()
        {
            new ReservedTextElement
            {
                Text = ":",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = ";",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = ",",
                HighlightColor = _white,
            },
            new ReservedTextElement
            {
                Text = "?",
                HighlightColor = _gray,
            },
        };

        public static readonly List<ReservedTextElement> SystemClassList = new()
        {
            #region System Library
            new ReservedTextElement
            {
                Text = "Console",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Environment",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Math",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Nullable",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Object",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Random",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "String",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections Library
            new ReservedTextElement
            {
                Text = "ArrayList",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "BitArray",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Hashtable",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Queue",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "SortedList",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Stack",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections.Specialized Library
            new ReservedTextElement
            {
                Text = "HybridDictionary",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "ListDictionary",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "NameValueCollection",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "OrderedDictionary",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "StringCollection",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "StringDictionary",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections.Generics Library
            new ReservedTextElement
            {
                Text = "Dictionary",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "HashSet",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "LinkedList",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "List",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Queue",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "SortedDictionary",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "SortedList",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "SortedSet",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "Stack",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections.ObjectModel Library
            new ReservedTextElement
            {
                Text = "Collection",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "MyKeyedCollection",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "ObservableCollection",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "ReadOnlyCollection",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "ReadOnlyDictionary",
                HighlightColor = _green,
            },
            #endregion

            #region System.Collections.Concurrent Library
            new ReservedTextElement
            {
                Text = "BlockingCollection",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "ConcurrentBag",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "ConcurrentDictionary",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "ConcurrentQueue",
                HighlightColor = _green,
            },
            new ReservedTextElement
            {
                Text = "ConcurrentStack",
                HighlightColor = _green,
            },
            #endregion

            #region System.Threading.Tasks Library
            new ReservedTextElement
            {
                Text = "Task",
                HighlightColor = _green,
            },
            #endregion
        };

        public static readonly List<ReservedTextElement> SystemInterfaceList = new()
        {
            #region System.Collections Library
            new ReservedTextElement
            {
                Text = "ICollection",
                HighlightColor = _lightGreen,
            },
            new ReservedTextElement
            {
                Text = "IDictionary",
                HighlightColor = _lightGreen,
            },
            new ReservedTextElement
            {
                Text = "IEnumerable",
                HighlightColor = _lightGreen,
            },
            new ReservedTextElement
            {
                Text = "IEnumerator",
                HighlightColor = _lightGreen,
            },
            new ReservedTextElement
            {
                Text = "IList",
                HighlightColor = _lightGreen,
            },
            new ReservedTextElement
            {
                Text = "IReadOnlyCollection",
                HighlightColor = _lightGreen,
            },
            new ReservedTextElement
            {
                Text = "IReadOnlyList",
                HighlightColor = _lightGreen,
            },
            new ReservedTextElement
            {
                Text = "ISet",
                HighlightColor = _lightGreen,
            },
            #endregion
        };

        public static readonly List<ReservedTextElement> SystemStructList = new()
        {
            #region System Library
            new ReservedTextElement
            {
                Text = "Boolean",
                HighlightColor = _jade,
            },
            new ReservedTextElement
            {
                Text = "Byte",
                HighlightColor = _jade,
            },
            new ReservedTextElement
            {
                Text = "Char",
                HighlightColor = _jade,
            },
            new ReservedTextElement
            {
                Text = "DateOnly",
                HighlightColor = _jade,
            },
            new ReservedTextElement
            {
                Text = "DateTime",
                HighlightColor = _jade,
            },
            new ReservedTextElement
            {
                Text = "Decimal",
                HighlightColor = _jade,
            },
            new ReservedTextElement
            {
                Text = "Double",
                HighlightColor = _jade,
            },
            new ReservedTextElement
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
            new ReservedTextElement
            {
                Text = "Int32",
                HighlightColor = _jade,
            },
            new ReservedTextElement
            {
                Text = "Int64",
                HighlightColor = _jade,
            },
            new ReservedTextElement
            {
                Text = "Range",
                HighlightColor = _jade,
            },
            new ReservedTextElement
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
            new ReservedTextElement
            {
                Text = "TimeSpan",
                HighlightColor = _jade,
            },
            #endregion

            #region System.Threading Library
            new ReservedTextElement
            {
                Text = "CancellationToken",
                HighlightColor = _jade,
            },
            #endregion
        };
    }
}
