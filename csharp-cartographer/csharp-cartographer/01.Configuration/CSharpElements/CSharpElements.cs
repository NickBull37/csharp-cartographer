namespace csharp_cartographer._01.Configuration.CSharpElements
{
    public class CSharpElement
    {
        public string Type { get; set; }
        public string Label { get; init; } = string.Empty;
        public string HighlightColor { get; set; } = string.Empty;
        public List<string> Facts { get; set; } = [];
        public List<string> Insights { get; set; } = [];
    }

    public class CSharpElements
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

        public static readonly List<CSharpElement> ElementList = new()
        {
            //new CSharpElement
            //{
            //    Type = "Identifier",
            //    Label = "IdentifierToken",
            //    Definition = "",
            //    HighlightColor = _orange,
            //    Facts = [
            //        "A user defined name value."
            //    ],
            //    Insights =
            //    [
            //        "Prefer descriptive names over short names. The next developer should be able to tell exactly what an element is based on it's name.",
            //        "Avoid abbreviations when possible."
            //    ],
            //},
            new CSharpElement
            {
                Type = "Punctuation",
                Label = "DotToken",
                Facts = [
                    "Allows access to properties or fields of an object or class instance.",
                    "Can be used to invoke methods of an object or class instance.",
                    "Can be used to navigate namespaces in using directives."
                ],
                Insights =
                [
                    "IDEs will often show available members after typing a '.' in your code editor. Use the arrow keys to navigate available options or see what members are available.",
                    "Member access operators are often strung together for nested objects."
                ],
            },
            new CSharpElement
            {
                Type = "Punctuation",
                Label = "SemicolonToken",
                Facts = [
                    "Marks the end of a statement or expression.",
                ],
                Insights =
                [
                    "",
                ],
            },
            new CSharpElement
            {
                Type = "Keyword",
                Label = "NamespaceKeyword",
                Facts = [
                    "A reserved C# keyword used to define a location the enclosed code can be accessed from.",
                ],
                Insights = [],
            },
            /// *************************************************
            /// |                EXPRESSIONS                |
            /// *************************************************
            new CSharpElement
            {
                Type = "Literal",
                Label = "StringLiteralToken",
                HighlightColor = _orange,
                Facts =
                [
                    "This represents the actual text value inside the double quotes."
                ],
                Insights =
                [
                    "Literal values are good for harding values such as constants that aren't going to change. String literals are not the best for comparisons. Variable comparisons are better here."
                ],
            },
            new CSharpElement
            {
                Type = "Literal",
                Label = "DecimalLiteralToken",
                HighlightColor = _lightGreen,
                Facts =
                [
                    "Represents the literal decimal value. The 'm' at the end of the literal value sigifies that the value is a decimal."
                ],
                Insights =
                [
                    "Decimals are highly accurate and good for financial calculations or calculations that need to be exact."
                ],
            },
            new CSharpElement
            {
                Type = "Literal",
                Label = "FloatLiteralToken",
                HighlightColor = _lightGreen,
                Facts =
                [
                    "Represents the literal float value. The 'f' at the end of the literal value sigifies that the value is a float."
                ],
                Insights =
                [
                    ""
                ],
            },
            /// *************************************************
            /// |                EXPRESSIONS                |
            /// *************************************************
            new CSharpElement
            {
                Type = "Expression",
                Label = "LiteralExpression",
                HighlightColor = _red,
                Facts =
                [
                    ""
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "BinaryExpression",
                HighlightColor = _red,
                Facts =
                [
                    ""
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "InvocationExpression",
                //HighlightColor = _lightBlue,
                Facts =
                [
                    "An expression that calls or \"invokes\" a method, delegate, or function-like construct (e.g., a lambda expression or local function)."
                ],
                Insights =
                [
                    "In Visual Studio, hovering your cursor over yellow invocation text will give useful details such as return type and paramters."
                ],
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "MemberAccessExpression",
                HighlightColor = _white,
                Facts =
                [
                    "This token represents a member of the class its referencing. The '.' token is how the class member is accessed."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "ConditionalExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "LambdaExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "CastExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "AwaitExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "ObjectCreationExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "ElementAccessExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "ParenthesizedExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "InterpolatedStringExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "ConditionalAccessExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "AssignmentExpression",
                HighlightColor = _red,
            },
            new CSharpElement
            {
                Type = "Expression",
                Label = "CheckedExpression",
                HighlightColor = _red,
            },
            /// *************************************************
            /// |               DECLARATIONS                |
            /// *************************************************
            new CSharpElement
            {
                Type = "Declaration",
                Label = "Parameter Name",
                HighlightColor = _lightBlue,
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "VariableDeclaration",
                HighlightColor = _lightBlue,
                Facts =
                [
                    "This declares a variable."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "LocalFunctionStatement",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "TypeParameter",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "ForStatement",
                HighlightColor = _lightBlue,
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "ForEachStatement",
                HighlightColor = _lightBlue,
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "CatchDeclaration",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "MethodDeclaration",
                HighlightColor = _yellow
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "ConstructorDeclaration",
                HighlightColor = _green
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "DestructorDeclaration",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "IndexerDeclaration",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "OperatorDeclaration",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "PropertyDeclaration",
                HighlightColor = _white,
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "EventDeclaration",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "FieldDeclaration",
                HighlightColor = _white,
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "EnumDeclaration",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "EnumMemberDeclaration",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "DelegateDeclaration",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "ClassDeclaration",
                HighlightColor = _green
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "StructDeclaration",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "InterfaceDeclaration",
                HighlightColor = _lightGreen
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "NamespaceDeclaration",
                HighlightColor = _white,
                Facts = [
                    "A namespace consists of the \"namespace\" keyword and the location needed to reference the code outside of the namespace.",
                ],
                Insights =
                [
                    "Namespaces will be automattically generated based on the file path when creating a new file in Visual Studio.",
                ],
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "UsingKeyword",
                HighlightColor = _blue,
                Facts = [
                    "A reserved C# keyword used to import namespaces & manage resources."
                ],
                Insights =
                [
                    "The .NET framework has built-in garbage collection. Check to make sure the resource actually needs to be disposed of manually.",
                ],
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "UsingDirective",
                HighlightColor = _white,
                Facts = [
                    "A using directive consists of the \"using\" keyword and the location of the namespace being imported.",
                    "Once a namespace is imported, the classes and methods defined within can be referenced without needing to fully qualify their names."
                ],
                Insights =
                [
                    "Visual Studio has a setting that will automattically remove unused using statements & order alphabetically remaining ones.",
                ],
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "BaseType",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "Attribute",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "Argument",
                HighlightColor = _red,
                Facts = [
                    "A value passed to a function.",
                ],
                Insights =
                [
                    "Arguments can be prefixed with the corresponding parameter name to increase readability.",
                ],
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "ArgumentList",
                HighlightColor = _red,
                Facts = [
                    "An argument list is a complete set of values passed to a function.",
                ],
                Insights =
                [
                    "",
                ],
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "AttributeArgument",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "AliasQualifiedName",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "QualifiedName",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "SimpleBaseType",
                HighlightColor = _red
            },
            new CSharpElement
            {
                Type = "Declaration",
                Label = "PredefinedType",
                HighlightColor = _red
            },
            /// ****************************************
            /// |               UPDATED                |
            /// ****************************************
            new CSharpElement
            {
                Type = "",
                Label = "InterfaceReference",
                HighlightColor = _lightGreen,
            },
            new CSharpElement
            {
                Type = "",
                Label = "ClassReference",
                HighlightColor = _green,
            },
            new CSharpElement
            {
                Type = "",
                Label = "ParameterDataType - Interface",
                HighlightColor = _lightGreen,
            },
            new CSharpElement
            {
                Type = "",
                Label = "ParameterDataType - Class",
                HighlightColor = _green,
            },
            new CSharpElement
            {
                Type = "",
                Label = "FieldDataType - Interface",
                HighlightColor = _lightGreen,
            },
            new CSharpElement
            {
                Type = "",
                Label = "FieldDataType - Class",
                HighlightColor = _green,
            },
            new CSharpElement
            {
                Type = "",
                Label = "InheritedInterface",
                HighlightColor = _lightGreen,
            },
            new CSharpElement
            {
                Type = "",
                Label = "InheritedClass",
                HighlightColor = _green,
            },
            new CSharpElement
            {
                Label = "AttributeIdentifier",
                Facts =
                [
                    "Attributes are metadata annotations that provide additional information to classes, methods, properties, and other elements.",
                    "Attributes are placed above the element they apply to, enclosed in square brackets [].",
                ],
                Insights =
                [
                    "An attribute in C# is any class that derives from System.Attribute."
                ],
            },
            new CSharpElement
            {
                Label = "ApiController Attribute",
                Facts =
                [
                    "Marks the controller as an API controller, which enables features like automatic model validation and parameter binding.",
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "Route Attribute",
                Facts =
                [
                    "Used to map requsets to different controller endpoints based on their URL.",
                ],
                Insights =
                [
                    "The Route attribute allows you to define parameters in the URL, which can be passed to the action method as arguments.",
                    "Routes can be customized using placeholders, defaults, and optional parameters."
                ],
            },
            new CSharpElement
            {
                Label = "HttpGet Attribute",
                Facts =
                [
                    "Specifies that the method handles HTTP GET requests, typically for reading or retrieving data.",
                ],
                Insights =
                [
                    "GET endpoints should only ever receive data through query parameters. GET requests should never contain data in the request body."
                ],
            },
            new CSharpElement
            {
                Label = "HttpPost Attribute",
                Facts =
                [
                    "Specifies that the method handles HTTP POST requests, typically for creating or submitting data.",
                ],
                Insights =
                [
                    "POST requests often carry the bulk of their data in the request body as serialized JSON."
                ],
            },
            new CSharpElement
            {
                Label = "FromQuery Attribute",
                Facts =
                [
                    "",
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "FromBody Attribute",
                Facts =
                [
                ],
                Insights =
                [
                ],
            },
        };
    }
}
