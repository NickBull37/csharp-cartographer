namespace csharp_cartographer._01.Configuration.CSharpElements
{
    public class CSharpElement
    {
        public string Label { get; init; } = string.Empty;
        public List<string> Facts { get; set; } = [];
        public List<string> Insights { get; set; } = [];
    }

    public class CSharpElements
    {
        public static readonly List<CSharpElement> ElementList = new()
        {
            new CSharpElement
            {
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
                Label = "NamespaceKeyword",
                Facts = [
                    "A reserved C# keyword used to define a location the enclosed code can be accessed from.",
                ],
                Insights =
                [
                    "Namespaces provide structure to your code by organizing classes and interfaces into logical groups.",
                ],
            },
            /// *************************************************
            /// |                EXPRESSIONS                |
            /// *************************************************
            new CSharpElement
            {
                Label = "StringLiteralToken",
                Facts =
                [
                    "Represents a sequence of characters enclosed in double quotes."
                ],
                Insights =
                [
                    "String literals are commonly used for constants, but care should be taken with string comparison due to case sensitivity."
                ],
            },
            new CSharpElement
            {
                Label = "DecimalLiteralToken",
                Facts =
                [
                    "Represents the literal decimal value. The 'm' at the end of the literal value signifies that the value is a decimal."
                ],
                Insights =
                [
                    "Decimals are highly accurate and suitable for financial or high-precision calculations."
                ],
            },
            new CSharpElement
            {
                Label = "FloatLiteralToken",
                Facts =
                [
                    "Represents the literal float value. The 'f' at the end of the literal value signifies that the value is a float."
                ],
                Insights =
                [
                    "Floats are used when precision is less critical but memory efficiency is important."
                ],
            },
            /// *************************************************
            /// |                EXPRESSIONS                |
            /// *************************************************
            new CSharpElement
            {
                Label = "LiteralExpression",
                Facts =
                [
                    "Represents a literal value in the code, such as a number or string."
                ],
                Insights =
                [
                    "Literal expressions represent values that are directly written into the code."
                ],
            },
            new CSharpElement
            {
                Label = "BinaryExpression",
                Facts =
                [
                    "Represents an expression involving two operands and a binary operator (e.g., +, -, *, /).",
                ],
                Insights =
                [
                    "Binary expressions are used for arithmetic and logical operations between two values or variables.",
                ],
            },
            new CSharpElement
            {
                Label = "InvocationExpression",
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
                Label = "MemberAccessExpression",
                Facts =
                [
                    "Represents accessing a member of a class or struct, such as a property or method."
                ],
                Insights =
                [
                    "Member access expressions commonly follow the dot (.) operator."
                ],
            },
            new CSharpElement
            {
                Label = "ConditionalExpression",
                Facts =
                [
                    "Represents an expression that evaluates a condition and returns one of two results depending on whether the condition is true or false.",
                ],
                Insights =
                [
                    "Conditional expressions are often referred to as ternary operators and follow the syntax 'condition ? trueValue : falseValue'.",
                ],
            },
            new CSharpElement
            {
                Label = "LambdaExpression",
                Facts =
                [
                    "Represents an anonymous function that can contain expressions and statements and can be used to create delegates or expression tree types.",
                ],
                Insights =
                [
                    "Lambda expressions are useful for writing concise and functional-style code, especially in LINQ queries.",
                ],
            },
            new CSharpElement
            {
                Label = "CastExpression",
                Facts =
                [
                    "Represents an expression that converts a value from one type to another.",
                ],
                Insights =
                [
                    "Casting is commonly used when you need to convert between compatible types, but be cautious of exceptions with incompatible types.",
                ],
            },
            new CSharpElement
            {
                Label = "AwaitExpression",
                Facts =
                [
                    "Used in asynchronous programming to await the result of an asynchronous method.",
                ],
                Insights =
                [
                    "The 'await' keyword pauses the execution of a method until the awaited task is complete, allowing other code to run in the meantime.",
                ],
            },
            new CSharpElement
            {
                Label = "ObjectCreationExpression",
                Facts =
                [
                    "Represents the creation of a new instance of a class or struct using the 'new' keyword.",
                ],
                Insights =
                [
                    "Object creation expressions are used to instantiate objects and invoke their constructors.",
                ],
            },
            new CSharpElement
            {
                Label = "ElementAccessExpression",
                Facts =
                [
                    "Represents accessing an element of an array or a collection using an index.",
                ],
                Insights =
                [
                    "Element access expressions use square brackets [] to access specific items in collections like arrays and lists.",
                ],
            },
            new CSharpElement
            {
                Label = "ParenthesizedExpression",
                Facts =
                [
                    "Represents an expression enclosed in parentheses to group or clarify operations.",
                ],
                Insights =
                [
                    "Parentheses can be used to control the order of operations in an expression.",
                ],
            },
            new CSharpElement
            {
                Label = "InterpolatedStringExpression",
                Facts =
                [
                    "Represents a string that contains placeholders that are replaced with the values of expressions.",
                ],
                Insights =
                [
                    "Interpolated strings in C# are prefixed with a dollar sign ($) and allow variables and expressions to be embedded inside the string.",
                ],
            },
            new CSharpElement
            {
                Label = "ConditionalAccessExpression",
                Facts =
                [
                    "Represents a safe way to access members of an object that may be null.",
                ],
                Insights =
                [
                    "Conditional access expressions use the ?. operator to avoid null reference exceptions.",
                ],
            },
            new CSharpElement
            {
                Label = "AssignmentExpression",
                Facts =
                [
                    "Represents an expression that assigns a value to a variable.",
                ],
                Insights =
                [
                    "Assignment expressions use the equals sign (=) to assign a value to a variable or field.",
                ],
            },
            new CSharpElement
            {
                Label = "CheckedExpression",
                Facts =
                [
                    "Used to explicitly enable overflow checking for arithmetic operations and conversions.",
                ],
                Insights =
                [
                    "Checked expressions throw an exception when an arithmetic operation results in an overflow.",
                ],
            },
            /// *************************************************
            /// |               DECLARATIONS                |
            /// *************************************************
            new CSharpElement
            {
                Label = "VariableDeclaration",
                Facts =
                [
                    "Declares a variable and optionally assigns it an initial value."
                ],
                Insights =
                [
                    "Variables can be initialized at the time of declaration or assigned later in the code."
                ],
            },
            new CSharpElement
            {
                Label = "LocalFunctionStatement",
                Facts =
                [
                    "Represents a function defined inside another method, allowing local scoping.",
                ],
                Insights =
                [
                    "Local functions are useful when you want to create helper functions that are only relevant to the containing method.",
                ],
            },
            new CSharpElement
            {
                Label = "LocalDeclarationStatement",
                Facts =
                [
                    "Represents a local variable declaration inside a method, constructor, or block in C#.",
                ],
                Insights =
                [
                    "Local declarations are used to define variables with block-level scope. They are only accessible within the block or method they are declared in.",
                ],
            },
            new CSharpElement
            {
                Label = "TypeParameter",
                Facts =
                [
                    "Represents a parameter in a generic type or method, allowing it to be defined with a type specified at runtime.",
                ],
                Insights =
                [
                    "Type parameters are commonly used in collections and methods that need to work with multiple types.",
                ],
            },
            new CSharpElement
            {
                Label = "ForStatement",
                Facts =
                [
                    "Represents a loop that iterates over a sequence of values based on an initializer, condition, and iterator."
                ],
                Insights =
                [
                    "The 'for' loop is commonly used to iterate over arrays and lists, providing control over the loop's index."
                ],
            },
            new CSharpElement
            {
                Label = "ForEachStatement",
                Facts =
                [
                    "Represents a loop that iterates over each element in a collection or array."
                ],
                Insights =
                [
                    "The 'foreach' loop is ideal for iterating over collections where you don't need to manage the index manually."
                ],
            },
            new CSharpElement
            {
                Label = "CatchDeclaration",
                Facts =
                [
                    "Represents a block of code that handles exceptions in a try-catch statement."
                ],
                Insights =
                [
                    "Catch blocks are used to handle exceptions and prevent crashes, allowing you to recover gracefully."
                ],
            },
            new CSharpElement
            {
                Label = "MethodDeclaration",
                Facts =
                [
                    "This declaration creates a new method in the enclosing class or interface.",
                    "The access modifier, return type, method name, and parameter list make up the method signature. The rest of the method is referred to as the method body."
                ],
                Insights =
                [
                    "Methods encapsulate reusable blocks of code that can be called with arguments and return values."
                ],
            },
            new CSharpElement
            {
                Label = "DestructorDeclaration",
                Facts =
                [
                    "Represents a method that is automatically invoked when an object is about to be destroyed by the garbage collector."
                ],
                Insights =
                [
                    "Destructors are used to release unmanaged resources held by an object before it is reclaimed by the garbage collector."
                ],
            },
            new CSharpElement
            {
                Label = "IndexerDeclaration",
                Facts =
                [
                    "Represents a property that allows an object to be indexed in the same way as an array."
                ],
                Insights =
                [
                    "Indexers allow objects to be accessed using array-like syntax, providing a natural way to interact with collections."
                ],
            },
            new CSharpElement
            {
                Label = "OperatorDeclaration",
                Facts =
                [
                    "Defines a custom implementation of a built-in operator (e.g., +, -, *, /) for a class or struct."
                ],
                Insights =
                [
                    "Operator overloading allows classes and structs to behave like built-in types when used with operators."
                ],
            },
            new CSharpElement
            {
                Label = "EventDeclaration",
                Facts =
                [
                    "Represents an event that allows a class to provide notifications to other classes when something happens."
                ],
                Insights =
                [
                    "Events are typically used in the observer pattern to notify subscribers of changes or actions."
                ],
            },
            new CSharpElement
            {
                Label = "EnumDeclaration",
                Facts =
                [
                    "Represents a distinct value type that defines a set of named constants."
                ],
                Insights =
                [
                    "Enums are useful for representing fixed sets of values, such as days of the week or states of an object."
                ],
            },
            new CSharpElement
            {
                Label = "EnumMemberDeclaration",
                Facts =
                [
                    "Represents a single named value in an enumeration."
                ],
                Insights =
                [
                    "Enum members are assigned integer values by default, but can be explicitly set to custom values."
                ],
            },
            new CSharpElement
            {
                Label = "DelegateDeclaration",
                Facts =
                [
                    "Represents a type that defines a method signature and can reference any method with that signature."
                ],
                Insights =
                [
                    "Delegates are used to pass methods as arguments, enabling flexible and reusable code."
                ],
            },
            new CSharpElement
            {
                Label = "StructDeclaration",
                Facts =
                [
                    "Represents a value type that is typically used to encapsulate small, simple objects."
                ],
                Insights =
                [
                    "Structs are value types and are passed by value, which can improve performance for small data structures."
                ],
            },
            new CSharpElement
            {
                Label = "InterfaceDeclaration",
                Facts =
                [
                    "Represents a contract that defines methods, properties, events, or indexers that a class or struct must implement."
                ],
                Insights =
                [
                    "Interfaces define behavior that classes and structs must implement, supporting polymorphism and decoupling."
                ],
            },
            new CSharpElement
            {
                Label = "NamespaceDeclaration",
                Facts =
                [
                    "A namespace consists of the \"namespace\" keyword and the location needed to reference the code outside of the namespace.",
                ],
                Insights =
                [
                    "Namespaces will be automattically generated based on the file path when creating a new file in Visual Studio.",
                ],
            },
            new CSharpElement
            {
                Label = "UsingKeyword",
                Facts = [
                    "A reserved keyword that can be used to import namespaces.",
                    "A reserved keyword that can be used to manage the lifespan of certain resources."
                ],
                Insights =
                [
                    "The .NET framework has built-in garbage collection but some class instances still need to be managed manually.",
                ],
            },
            new CSharpElement
            {
                Label = "UsingDirective",
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
                Label = "UsingDirectiveIdentifier",
                Facts =
                [
                    "Represents a specific part of a using directive that identifies a namespace or alias."
                ],
                Insights =
                [
                    "Using directive identifiers are commonly seen when importing namespaces or defining aliases for namespaces to avoid naming conflicts."
                ],
            },
            new CSharpElement
            {
                Label = "BaseType",
                Facts =
                [
                    "Represents a class or interface that another class or interface inherits or implements."
                ],
                Insights =
                [
                    "Base types allow for the reuse of code and support object-oriented principles like inheritance."
                ],
            },
            new CSharpElement
            {
                Label = "Attribute",
                Facts =
                [
                    "Represents a class that provides metadata about a program element."
                ],
                Insights =
                [
                    "Attributes can be applied to classes, methods, properties, and other elements to provide additional information or behavior."
                ],
            },
            new CSharpElement
            {
                Label = "Argument",
                Facts =
                [
                    "A value passed to a function.",
                ],
                Insights =
                [
                    "Arguments can be prefixed with the corresponding parameter name to increase readability.",
                ],
            },
            new CSharpElement
            {
                Label = "ArgumentList",
                Facts =
                [
                    "An argument list is a complete set of values passed to a method or function."
                ],
                Insights =
                [
                    "Argument lists allow you to pass multiple values to a function in a single call."
                ],
            },
            new CSharpElement
            {
                Label = "AttributeArgument",
                Facts =
                [
                    "Represents a value passed to an attribute to provide additional information."
                ],
                Insights =
                [
                    "Attribute arguments customize the behavior of an attribute for a specific use case."
                ],
            },
            new CSharpElement
            {
                Label = "AliasQualifiedName",
                Facts =
                [
                    "Represents a fully qualified name that uses an alias to reference a namespace or class."
                ],
                Insights =
                [
                    "Alias-qualified names are used to resolve naming conflicts or provide shorthand for long namespace names."
                ],
            },
            new CSharpElement
            {
                Label = "QualifiedName",
                Facts =
                [
                    "Represents a name that is fully qualified by including its namespace."
                ],
                Insights =
                [
                    "Qualified names are necessary when classes or types have the same name but exist in different namespaces."
                ],
            },
            new CSharpElement
            {
                Label = "SimpleBaseType",
                Facts =
                [
                    "Represents a base type that is directly inherited by a class or struct."
                ],
                Insights =
                [
                    "Simple base types provide a single inheritance path for classes and structs."
                ],
            },
            new CSharpElement
            {
                Label = "PredefinedType",
                Facts =
                [
                    "Represents a built-in value type, such as int, float, or bool."
                ],
                Insights =
                [
                    "Predefined types provide the basic building blocks for working with data in C#."
                ],
            },
            /// ****************************************
            /// |               UPDATED                |
            /// ****************************************
            new CSharpElement
            {
                Label = "InterfaceReference",
                Facts =
                [
                    "Represents a reference to an interface type."
                ],
                Insights =
                [
                    "Interface references enable polymorphism by allowing classes to implement multiple behaviors."
                ],
            },
            new CSharpElement
            {
                Label = "ClassReference",
                Facts =
                [
                    "Represents a reference to a class type."
                ],
                Insights =
                [
                    "Class references enable inheritance and method overriding in derived classes."
                ],
            },
            new CSharpElement
            {
                Label = "ParameterDataType - Interface",
                Facts =
                [
                    "",
                ],
                Insights =
                [
                    "",
                ],
            },
            new CSharpElement
            {
                Label = "ParameterDataType - Class",
                Facts =
                [
                    "",
                ],
                Insights =
                [
                    "",
                ],
            },
            new CSharpElement
            {
                Label = "FieldDataType - Interface",
                Facts =
                [
                    "",
                ],
                Insights =
                [
                    "",
                ],
            },
            new CSharpElement
            {
                Label = "FieldDataType - Class",
                Facts =
                [
                    "",
                ],
                Insights =
                [
                    "",
                ],
            },
            new CSharpElement
            {
                Label = "InheritedInterface",
                Facts =
                [
                    "Represents an interface that is inherited by another interface or class."
                ],
                Insights =
                [
                    "Inherited interfaces allow a class or interface to gain additional functionality by implementing multiple interfaces."
                ],
            },
            new CSharpElement
            {
                Label = "InheritedClass",
                Facts =
                [
                    "Represents a class that is inherited by another class."
                ],
                Insights =
                [
                    "Inherited classes allow a derived class to reuse and extend the functionality of its base class."
                ],
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
                    "Binds a parameter in an action method to a query string parameter from the URL."
                ],
                Insights =
                [
                    "FromQuery is useful for retrieving data that is sent via HTTP GET requests."
                ],
            },
            new CSharpElement
            {
                Label = "FromBody Attribute",
                Facts =
                [
                    "Binds a parameter in an action method to the data in the body of an HTTP request."
                ],
                Insights =
                [
                    "FromBody is typically used to parse JSON or XML data sent in HTTP POST requests."
                ],
            },
            new CSharpElement
            {
                Label = "MethodReturnType",
                Facts =
                [
                    "The following method will return this data type."
                ],
                Insights =
                [
                    "A method return type insight."
                ],
            },
            new CSharpElement
            {
                Label = "NamespaceIdentifier",
                Facts =
                [
                    "This token makes up one piece of the namespace this using directive is referencing."
                ],
                Insights =
                [
                    "A namespace identifier insight."
                ],
            },
            new CSharpElement
            {
                Label = "PublicKeyword",
                Facts =
                [
                    "The public keyword indicates that the member is accessible from any other code in the same assembly or another assembly that references it."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "PrivateKeyword",
                Facts =
                [
                    "The private keyword indicates that the member is only accessible from inside the object its defined in."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "ReadOnlyKeyword",
                Facts =
                [
                    "Readonly fields can only be assigned values when they are declared or in a constructor of the class in which they were defined.",
                    "Once a value is assigned, a readonly field cannot be modified."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "ClassKeyword",
                Facts =
                [
                    "Defines a blueprint for objects, encapsulating data and behavior through fields, properties, methods, and events."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "ConstKeyword",
                Facts =
                [
                    "The const keyword declares a constant field or local, which must be assigned a value at the time of its declaration and cannot be modified thereafter."
                ],
                Insights =
                [
                    "constants are implicitly static and readonly, their values must be known at compile-time.",
                    "if no access modifer is specified, the default access level will be private."
                ],
            },
            new CSharpElement
            {
                Label = "StringKeyword",
                Facts =
                [
                    "The string keyword represents a sequence of characters."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "DecimalKeyword",
                Facts =
                [
                    "The decimal keyword represents a 128-bit precise decimal value with 28-29 significant digits."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "DoubleKeyword",
                Facts =
                [
                    "The double keyword represents a double-precision floating-point number."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "FloatKeyword",
                Facts =
                [
                    "The float keyword represents a xxx-bit precise float value with xx-xx significant digits."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "IntKeyword",
                Facts =
                [
                    "The int keyword represents a 32-bit signed integer."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "CharKeyword",
                Facts =
                [
                    "The char keyword represents a single 16-bit Unicode character."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "BoolKeyword",
                Facts =
                [
                    "The bool keyword represents a Boolean value: true or false."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "ObjectKeyword",
                Facts =
                [
                    "The object keyword represents the base type from which all types in C# are derived."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "VarKeyword",
                Facts =
                [
                    "The var keyword allows the compiler to infer the type of the variable at compile time."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "VoidKeyword",
                Facts =
                [
                    "The void keyword specifies that a method does not return a value."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "ClassDeclaration",
                Facts =
                [
                    "A class declaration is the syntax used to define a new class.",
                    "Typically consists of an access modifier, a class name, a list of inherited base types, and a class body."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "ConstructorDeclaration",
                Facts =
                [
                    "A constructor declaration is used to create a new instance of a class.",
                    "Multiple constructors can be defined for single class to fit your needs."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "PropertyDeclaration",
                Facts =
                [
                    "This declaration creates a new property in the enclosing class.",
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "FieldDeclaration",
                Facts =
                [
                    "This declaration creates a new field in the enclosing class.",
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "MethodDeclaration",
                Facts =
                [
                    "This declaration creates a new method in the enclosing class or interface.",
                    "The access modifier, return type, method name, and parameter list make up the method signature. The rest of the method is refered to as the method body."
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "GetKeyword",
                Facts =
                [
                    "The get keyword defines an accessor for getting the value of a property. This accessor is utilized with the DotToken (.) when accessing properties of a class instance.",
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "SetKeyword",
                Facts =
                [
                    "The set keyword defines an accessor for setting the value of a property. This accessor is utilized whenever you assign a property a value.",
                ],
                Insights =
                [
                    ""
                ],
            },
            new CSharpElement
            {
                Label = "ReturnKeyword",
                Facts =
                [
                    "The return keyword is used to exit a method and optionally pass a value back to the calling code.",
                ],
                Insights =
                [
                    "If a method's return type is void, a return statement is not required but can be used to exit the method early."
                ],
            },
            new CSharpElement
            {
                Label = "OpenBraceToken",
                Facts =
                [
                    "Represents the '{' symbol in C#."
                ],
                Insights =
                [
                    "Open braces are used to begin the scope of a block, such as in classes, methods, or control structures."
                ],
            },
            new CSharpElement
            {
                Label = "CloseBraceToken",
                Facts =
                [
                    "Represents the '}' symbol in C#."
                ],
                Insights =
                [
                    "Close braces are used to end the scope of a block that was opened with an open brace."
                ],
            },
            new CSharpElement
            {
                Label = "OpenParenToken",
                Facts =
                [
                    "Represents the '(' symbol in C#."
                ],
                Insights =
                [
                    "Open parentheses are used to group expressions or enclose parameters in method calls or declarations."
                ],
            },
            new CSharpElement
            {
                Label = "CloseParenToken",
                Facts =
                [
                    "Represents the ')' symbol in C#."
                ],
                Insights =
                [
                    "Close parentheses are used to complete expressions or parameter lists started with an open parenthesis."
                ],
            },
            new CSharpElement
            {
                Label = "OpenBracketToken",
                Facts =
                [
                    "Represents the '[' symbol in C#."
                ],
                Insights =
                [
                    "Open brackets are used for array indexing or to begin an attribute declaration."
                ],
            },
            new CSharpElement
            {
                Label = "CloseBracketToken",
                Facts =
                [
                    "Represents the ']' symbol in C#."
                ],
                Insights =
                [
                    "Close brackets are used to complete array indexing or attribute declarations started with an open bracket."
                ],
            },

            new CSharpElement
            {
                Label = "InterfaceKeyword",
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
                Label = "BaseList",
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
                Label = "EqualsToken",
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
                Label = "EqualsValueClause",
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
                Label = "VariableDeclarator",
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
                Label = "StringLiteralExpression",
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
                Label = "CollectionExpression",
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
                Label = "CommaToken",
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
                Label = "EqualsGreaterThanToken",
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
                Label = "ArrowExpressionClause",
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
                Label = "QuestionToken",
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
                Label = "NullableType",
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
                Label = "Parameter",
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
                Label = "SimpleMemberAccessExpression",
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
                Label = "MethodIdentifier",
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
                Label = "ReturnStatement",
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
                Label = "IfKeyword",
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
                Label = "IfStatement",
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
                Label = "TypeArgumentList",
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
                Label = "NewKeyword",
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
                Label = "ImplicitObjectCreationExpression",
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
                Label = "SimpleAssignmentExpression",
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
                Label = "ObjectInitializerExpression",
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
                Label = "StaticKeyword",
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
                Label = "ExclamationToken",
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
                Label = "LogicalNotExpression",
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
                Label = "QuestionQuestionToken",
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
                Label = "CoalesceExpression",
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
                Label = "ExpressionStatement",
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
                Label = "BreakKeyword",
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
                Label = "BreakStatement",
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
                Label = "SwitchStatement",
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
                Label = "SwitchKeyword",
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
                Label = "SwitchSection",
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
                Label = "CaseKeyword",
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
                Label = "CaseSwitchLabel",
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
                Label = "DefaultSwitchLabel",
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
                Label = "DefaultKeyword",
                Facts =
                [
                    "",
                ],
                Insights =
                [
                    ""
                ],
            },
        };
    }
}
