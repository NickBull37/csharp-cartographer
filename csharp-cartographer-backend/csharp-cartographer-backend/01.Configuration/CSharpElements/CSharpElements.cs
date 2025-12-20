namespace csharp_cartographer_backend._01.Configuration.CSharpElements
{
    public class CSharpElement
    {
        public string Label { get; init; } = string.Empty;
        public string? Alias { get; init; }
        public List<string> Facts { get; set; } = [];
        public List<string> Insights { get; set; } = [];
    }

    public class CSharpElements
    {
        public static readonly List<CSharpElement> Elements = new()
        {
            new CSharpElement
            {
                Label = "DotToken",
                Facts =
                [
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
                Facts =
                [
                    "Marks the end of a statement or expression.",
                ],
                Insights =
                [
                ],
            },
            new CSharpElement
            {
                Label = "NamespaceKeyword",
                Facts =
                [
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
                Label = "ForEachKeyword",
                Facts =
                [
                    "The 'foreach' keyword is used to iterate through elements in a collection.",
                    "It automatically retrieves elements in the order they appear in the collection.",
                    "The 'foreach' loop simplifies traversal by eliminating the need for an explicit index or enumerator."
                ],
                Insights =
                [
                    "Use 'foreach' for concise and readable iteration through collections.",
                    "It is particularly useful for iterating through collections that implement IEnumerable or arrays.",
                    "If you need access to elements other than the current element in the loop, consider a 'for' loop instead."
                ],
            },
            new CSharpElement
            {
                Label = "ParameterType",
                Facts =
                [
                    "The data type that the following parameter can accept during method invocation.",
                    "ParameterType is required for all non-dynamic method parameters in C#."
                ],
                Insights =
                [
                    "Use flexible types like interfaces or base classes when accepting various implementations.",
                    "For optional parameters, consider combining ParameterType with nullable types or default values for greater flexibility."
                ],
            },
            new CSharpElement
            {
                Label = "ParameterList",
                Facts =
                [
                    "A parameter list refers to the collection of parameters defined within the parentheses of a method, constructor, indexer, or delegate declaration.",
                    "These parameters specify the inputs that the method or function can accept when it is called.",
                ],
                Insights =
                [
                    "Highlight the parameter list to see all the parameters available in the implementation below."
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
                Facts =
                [
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
                Label = "UnderscoreToken",
                Facts =
                [
                    "Represents the '_' symbol in C#."
                ],
                Insights =
                [
                    "Also known as a discard value, the underscore is used here when you don't need to save the value into a variable."
                ],
            },

            new CSharpElement
            {
                Label = "InterfaceKeyword",
                Facts =
                [
                    "The 'interface' keyword is used to declare an interface in C#.",
                    "Interfaces define a contract that classes or structs can implement.",
                    "An interface can contain methods, properties, events, or indexer signatures but no implementations."
                ],
                Insights =
                [
                    "Use interfaces to achieve polymorphism and define reusable abstractions in your code.",
                    "They are especially useful for dependency injection and unit testing by decoupling implementations from contracts.",
                    "Explicit interface implementation can hide methods from public access."
                ],
            },

            new CSharpElement
            {
                Label = "BaseList",
                Facts =
                [
                    "A BaseList specifies the base class and implemented interfaces for a class or struct.",
                    "It appears after a colon (:) in a class or struct declaration.",
                    "The first item in the BaseList is the base class, followed by interfaces."
                ],
                Insights =
                [
                    "Using BaseList improves code reusability by allowing inheritance and interface implementation.",
                    "A class can inherit from only one base class but can implement multiple interfaces.",
                    "Ensure meaningful class hierarchies to avoid the pitfalls of deep inheritance."
                ],
            },

            new CSharpElement
            {
                Label = "EqualsToken",
                Facts =
                [
                    "The '=' token is used for assignment in C#.",
                    "It can also appear in named arguments to assign values to specific parameters.",
                    "The token is part of many constructs, such as variable initialization and property setters."
                ],
                Insights =
                [
                    "Using '=' for assignments is fundamental to imperative programming in C#.",
                    "Be cautious of unintentional assignments in conditional expressions.",
                    "Named arguments with '=' improve code readability in method calls."
                ],
            },

            new CSharpElement
            {
                Label = "EqualsValueClause",
                Facts =
                [
                    "An EqualsValueClause represents an assignment of a value to a variable or property.",
                    "It appears in variable initializations and default parameter values.",
                    "The clause follows the '=' token and specifies the assigned value."
                ],
                Insights =
                [
                    "EqualsValueClause ensures variables are initialized with a meaningful value at the point of declaration.",
                    "Using default parameter values with EqualsValueClause simplifies method signatures.",
                    "Avoid excessive use of default values to maintain method clarity."
                ],
            },

            new CSharpElement
            {
                Label = "VariableDeclarator",
                Facts =
                [
                    "A VariableDeclarator represents a declared variable in C#.",
                    "It contains the variable's name and an optional initializer.",
                    "VariableDeclarators are part of a VariableDeclaration."
                ],
                Insights =
                [
                    "VariableDeclarators are the foundation of variable declarations in methods, classes, and structs.",
                    "Initialize variables during declaration to prevent uninitialized value errors.",
                    "Group related VariableDeclarators for cleaner code organization."
                ],
            },

            new CSharpElement
            {
                Label = "StringLiteralExpression",
                Facts =
                [
                    "A StringLiteralExpression represents a string value enclosed in double quotes.",
                    "String literals support escape sequences like '\\n' and '\\t'.",
                    "Verbatim string literals start with '@' and can span multiple lines."
                ],
                Insights =
                [
                    "Use StringLiteralExpression to represent textual data in your code.",
                    "Verbatim string literals are useful for file paths and multiline text.",
                    "Consider string interpolation or formatting for more dynamic string construction."
                ],
            },

            new CSharpElement
            {
                Label = "CollectionExpression",
                Facts =
                [
                    "A CollectionExpression represents a collection initializer in C#.",
                    "It uses curly braces to add elements directly to a collection.",
                    "It works with arrays, lists, and other collection types that implement IEnumerable."
                ],
                Insights =
                [
                    "CollectionExpression simplifies collection initialization with inline elements.",
                    "Use CollectionExpression with object initializers for clean and concise code.",
                    "Ensure the collection type supports the elements being initialized."
                ],
            },

            new CSharpElement
            {
                Label = "CommaToken",
                Facts =
                [
                    "The ',' token separates elements in lists, such as method arguments and array initializers.",
                    "It is used in loops, like 'for', to separate multiple expressions.",
                    "The ',' token is also used in tuple declarations and deconstruction."
                ],
                Insights =
                [
                    "CommaToken improves readability and organization in grouped expressions.",
                    "Be cautious of trailing commas, which can cause syntax errors in some contexts.",
                    "Tuples with CommaToken simplify multiple return values from methods."
                ],
            },

            new CSharpElement
            {
                Label = "EqualsGreaterThanToken",
                Facts =
                [
                    "The '=>' token is used in lambda expressions and switch expressions.",
                    "In lambdas, it separates the parameter list from the expression or statement block.",
                    "In switch expressions, it separates patterns from their result expressions."
                ],
                Insights =
                [
                    "EqualsGreaterThanToken simplifies function expressions with concise syntax.",
                    "It is a key component of functional programming constructs in C#.",
                    "Switch expressions using '=>' improve pattern matching readability."
                ],
            },

            new CSharpElement
            {
                Label = "ArrowExpressionClause",
                Facts =
                [
                    "An ArrowExpressionClause represents a concise body for a member using '=>'.",
                    "It is commonly used in properties, methods, and operators for single-line definitions.",
                    "ArrowExpressionClause simplifies syntax by omitting braces and 'return' keywords."
                ],
                Insights =
                [
                    "Use ArrowExpressionClause for short and readable member definitions.",
                    "Avoid overusing ArrowExpressionClause for complex logic to maintain clarity.",
                    "They are especially useful for calculated properties and lightweight methods."
                ],
            },

            new CSharpElement
            {
                Label = "QuestionToken",
                Facts =
                [
                    "The '?' token is used in nullable types, the null-coalescing operator, and conditional expressions.",
                    "As a nullable type marker, it allows value types to accept 'null'.",
                    "In conditional expressions, it separates the condition from the true branch."
                ],
                Insights =
                [
                    "QuestionToken is versatile in expressing nullability and conditions.",
                    "Use nullable types ('T?') to handle optional values in a type-safe manner.",
                    "Conditional expressions with '?' and ':' improve inline decision-making readability."
                ],
            },

            new CSharpElement
            {
                Label = "NullableType",
                Facts =
                [
                    "A NullableType allows value types to represent 'null'.",
                    "It is declared using the '?' token after a value type.",
                    "NullableType is often used in databases and optional configurations."
                ],
                Insights =
                [
                    "NullableType improves flexibility by allowing value types to represent missing values.",
                    "Avoid excessive nullables; prefer well-defined default values when possible.",
                    "NullableType works seamlessly with nullable reference type annotations."
                ],
            },

            new CSharpElement
            {
                Label = "Parameter",
                Facts =
                [
                    "A Parameter represents an input to a method, constructor, or delegate.",
                    "Parameters can have default values and modifiers like 'ref', 'out', or 'params'.",
                    "They are declared in parentheses after the method or function name."
                ],
                Insights =
                [
                    "Well-named parameters improve method readability and usage.",
                    "Default parameter values simplify method overloads for common cases.",
                    "Modifiers like 'ref' and 'out' provide more control over data flow in methods."
                ],
            },

            new CSharpElement
            {
                Label = "SimpleMemberAccessExpression",
                Facts =
                [
                    "A SimpleMemberAccessExpression represents member access using the '.' operator.",
                    "It is used to access fields, properties, methods, and nested types.",
                    "The left side is the object or type, and the right side is the member being accessed."
                ],
                Insights =
                [
                    "SimpleMemberAccessExpression is fundamental to object-oriented programming in C#.",
                    "Use it for accessing members while respecting encapsulation principles.",
                    "Chained member access can lead to null reference exceptions; consider using null-conditional operators."
                ],
            },

            new CSharpElement
            {
                Label = "MethodIdentifier",
                Facts =
                [
                    "Represents the name of a method.",
                    "It can be used as part of a method declaration or invocation.",
                ],
                Insights =
                [
                    "Choose descriptive and consistent names for better code understanding.",
                    "Overloaded methods can share the same method name while having different parameter lists.",
                ],
            },

            new CSharpElement
            {
                Label = "ReturnStatement",
                Facts =
                [
                    "A ReturnStatement is used to exit a method and optionally return a value.",
                    "It is declared using the 'return' keyword followed by a value or nothing (void methods).",
                    "Every non-void method must include a ReturnStatement for all execution paths."
                ],
                Insights =
                [
                    "Use ReturnStatement to ensure method results are clear and intentional.",
                    "Avoid overusing multiple return statements in methods to maintain readability.",
                    "Returning early in methods can improve clarity when checking preconditions."
                ],
            },
            new CSharpElement
            {
                Label = "IfKeyword",
                Facts =
                [
                    "The 'if' keyword is used to start a conditional statement in C#.",
                    "It allows execution of a block of code if a specified condition evaluates to true.",
                    "The condition inside the 'if' statement must evaluate to a boolean value."
                ],
                Insights =
                [
                    "The 'if' keyword is essential for controlling the flow of execution in your programs.",
                    "Avoid complex conditions in a single 'if' statement to maintain readability.",
                    "Use 'if' statements with 'else' or 'else if' for multi-branch decision-making."
                ],
            },
            new CSharpElement
            {
                Label = "IfStatement",
                Facts =
                [
                    "An 'if' statement in C# is used to execute code conditionally based on a boolean expression.",
                    "The 'if' statement can optionally include an 'else' or 'else if' block for additional conditions.",
                    "The condition in the 'if' statement is evaluated only once."
                ],
                Insights =
                [
                    "Use 'if' statements to handle decision logic in your code.",
                    "For complex conditional logic, consider breaking it into multiple 'if' statements for clarity.",
                    "Be cautious of deeply nested 'if' statements, as they can reduce readability."
                ],
            },
            new CSharpElement
            {
                Label = "TypeArgumentList",
                Facts =
                [
                    "A TypeArgumentList is used in C# to specify the types of a generic class or method.",
                    "It appears within angle brackets '<>' and contains one or more type arguments.",
                    "The TypeArgumentList is used when defining or invoking generic types or methods."
                ],
                Insights =
                [
                    "TypeArgumentList is essential for working with generics in C#.",
                    "Be mindful of type constraints to ensure type safety when using generics.",
                    "Generics with TypeArgumentList improve code reusability and type safety."
                ],
            },
            new CSharpElement
            {
                Label = "NewKeyword",
                Facts =
                [
                    "The 'new' keyword is used to create instances of types (e.g., objects, collections, arrays).",
                    "It can also be used to hide members in a base class when declaring a member in a derived class.",
                    "In C#, 'new' is used for object creation or to hide a member in inheritance."
                ],
                Insights =
                [
                    "Use 'new' to instantiate objects or allocate memory for data types in C#.",
                    "When hiding base class members, consider using 'new' carefully to avoid confusion with polymorphism.",
                    "For clear object creation, prefer constructors over directly using 'new' for complex types."
                ],
            },
            new CSharpElement
            {
                Label = "ImplicitObjectCreationExpression",
                Facts =
                [
                    "An ImplicitObjectCreationExpression creates an object without explicitly specifying the type.",
                    "It typically occurs when using 'new' to instantiate a type, and the type can be inferred by the compiler.",
                    "This expression is often seen in anonymous types and var declarations."
                ],
                Insights =
                [
                    "Use ImplicitObjectCreationExpression when the type can be inferred by the compiler for cleaner code.",
                    "In complex scenarios, avoid excessive use of implicit object creation to maintain clarity.",
                    "Implicit types can be beneficial for quick prototyping but should be used cautiously in production code."
                ],
            },
            new CSharpElement
            {
                Label = "SimpleAssignmentExpression",
                Facts =
                [
                    "A SimpleAssignmentExpression assigns a value to a variable using the '=' operator.",
                    "The left side of the assignment must be a variable, field, or property.",
                    "The right side can be any expression that evaluates to a value compatible with the left-hand side."
                ],
                Insights =
                [
                    "Use SimpleAssignmentExpression to update variable values during the program's execution.",
                    "Ensure the value type matches the variable type to avoid runtime errors.",
                    "Avoid complex expressions on the right-hand side to keep assignments clear."
                ],
            },
            new CSharpElement
            {
                Label = "ObjectInitializerExpression",
                Facts =
                [
                    "An ObjectInitializerExpression allows initializing properties or fields of an object during its creation.",
                    "It is written using curly braces '{}' and is commonly used with object and collection initializers.",
                    "This expression helps make the initialization of objects more concise and readable."
                ],
                Insights =
                [
                    "Use ObjectInitializerExpression to streamline object creation and initialization.",
                    "Object initialization syntax promotes immutability and reduces boilerplate code.",
                    "Avoid overusing object initialization when the logic for setting properties is complex."
                ],
            },
            new CSharpElement
            {
                Label = "StaticKeyword",
                Facts =
                [
                    "The 'static' keyword in C# indicates that a member belongs to the type itself rather than an instance.",
                    "It is used for fields, methods, properties, and classes that are shared among all instances of the type.",
                    "Static members are accessed using the type name, not through an instance."
                ],
                Insights =
                [
                    "Use 'static' for utility methods, constants, or singleton patterns where you don't need an instance.",
                    "Static members are shared across all instances, which can lead to state issues in multithreaded environments.",
                    "Be careful with static constructors, as they execute only once when the type is accessed."
                ],
            },
            new CSharpElement
            {
                Label = "ExclamationToken",
                Facts =
                [
                    "The '!' token is used for logical negation in C#.",
                    "It inverts the boolean value of an expression, turning true to false and vice versa.",
                    "The '!' token is often used in conditional expressions and loops."
                ],
                Insights =
                [
                    "Use the '!' token to perform boolean negation for condition checks.",
                    "ExclamationToken is essential in controlling the flow of conditional logic.",
                    "Avoid double negation, as it can reduce the clarity of your conditions."
                ],
            },
            new CSharpElement
            {
                Label = "LogicalNotExpression",
                Facts =
                [
                    "A LogicalNotExpression represents a logical negation operation in C#.",
                    "It is represented using the '!' token and inverts the truth value of an expression.",
                    "LogicalNotExpression is typically used in boolean conditions or expressions."
                ],
                Insights =
                [
                    "LogicalNotExpression is an essential tool for inverting boolean logic.",
                    "Be cautious of overuse, as it can make conditions harder to understand.",
                    "Double negation can often be simplified for better readability."
                ],
            },
            new CSharpElement
            {
                Label = "QuestionQuestionToken",
                Alias = "Null-coalescing operator",
                Facts =
                [
                    "When the left-hand side expression evaluates to null, the null-coalescing operator provides a default value on the right.",
                ],
                Insights =
                [
                    "The null-coalescing operator is commonly used to simplify null checks.",
                    "Chaining '??' with other expressions can help streamline error handling and fallback logic."
                ],
            },
            new CSharpElement
            {
                Label = "AmpersandAmpersandToken",
                Alias = "Logical AND operator",
                Facts =
                [
                    "The \'&&\' operator is used to evaluate two expressions at one time.",
                    "The expressions evaluated must return type bool."
                ],
                Insights =
                [
                ],
            },
            new CSharpElement
            {
                Label = "LogicalAndExpression",
                Facts =
                [
                    "The logical AND expression evaluates if the results of two expressions both evaluate to true.",
                    "If both the expression to the left of the operator and the expression to the right of the operator evaluate to true, the condition passes.",
                    "If either the expression to the left of the operator or the expression to the right of the operator evaluate to false, the condition fails."
                ],
                Insights =
                [
                    "Avoid adding too many expressions to one conditional statement. Too many evaluations in one condition can make code harder to read."
                ],
            },
            new CSharpElement
            {
                Label = "CoalesceExpression",
                Facts =
                [
                    "A CoalesceExpression uses the '??' operator to evaluate and return the first non-null value.",
                    "It is commonly used to set default values for nullable types or reference types.",
                    "The CoalesceExpression is a concise alternative to explicit null checks in many scenarios."
                ],
                Insights =
                [
                    "Use CoalesceExpression to simplify null handling by providing fallback values.",
                    "The '??' operator is particularly useful in dealing with optional parameters or nullable types.",
                    "Avoid overusing the '??' operator in cases where explicit null checks improve clarity."
                ],
            },
            new CSharpElement
            {
                Label = "ExpressionStatement",
                Facts =
                [
                    "An ExpressionStatement represents a single expression followed by a semicolon.",
                    "It is used to execute expressions that produce side effects, such as method calls or assignments.",
                    "ExpressionStatements are one of the most common types of statements in C#."
                ],
                Insights =
                [
                    "Use ExpressionStatements for concise and clear logic execution.",
                    "Avoid embedding too much logic in a single ExpressionStatement to maintain readability.",
                    "ExpressionStatements are often used for invoking methods, assigning values, or incrementing variables."
                ],
            },
            new CSharpElement
            {
                Label = "BreakKeyword",
                Facts =
                [
                    "The 'break' keyword is used to exit a loop or a switch statement prematurely.",
                    "It can appear in loops (for, while, do-while) or within switch sections.",
                    "When executed, it transfers control to the statement immediately following the enclosing construct."
                ],
                Insights =
                [
                    "Use 'break' to improve control flow and exit loops or switches when a condition is met.",
                    "Overusing 'break' in loops can make logic harder to follow; consider refactoring in such cases.",
                    "Combine 'break' with conditions in loops to terminate iterations effectively."
                ],
            },
            new CSharpElement
            {
                Label = "BreakStatement",
                Facts =
                [
                    "A BreakStatement is a statement that uses the 'break' keyword to terminate a loop or switch section.",
                    "It is followed by a semicolon and does not take any additional expressions or parameters.",
                    "BreakStatements improve control flow by exiting constructs prematurely."
                ],
                Insights =
                [
                    "BreakStatements are useful in scenarios where continuing the loop or switch is unnecessary.",
                    "They improve readability by explicitly showing when a construct ends early.",
                    "Overuse of BreakStatements may indicate opportunities to simplify control structures."
                ],
            },
            new CSharpElement
            {
                Label = "SwitchStatement",
                Facts =
                [
                    "A SwitchStatement allows multi-way branching based on the value of an expression.",
                    "It contains one or more switch sections, each with a case label or a default label.",
                    "The expression evaluated in a SwitchStatement must result in a compatible type for comparison."
                ],
                Insights =
                [
                    "SwitchStatements are ideal for replacing long chains of 'if-else' conditions with clearer logic.",
                    "The addition of switch expressions in modern C# enhances flexibility and conciseness.",
                    "Always include a 'default' section in a SwitchStatement to handle unexpected cases."
                ],
            },
            new CSharpElement
            {
                Label = "SwitchKeyword",
                Facts =
                [
                    "The 'switch' keyword is used to declare a switch statement or expression.",
                    "It evaluates an expression and directs control flow based on its value.",
                    "The 'switch' keyword is followed by a block containing case or default labels."
                ],
                Insights =
                [
                    "Use the 'switch' keyword to implement readable multi-way branching logic.",
                    "Switch constructs simplify decision-making compared to long 'if-else' chains.",
                    "In performance-critical scenarios, prefer switch statements over multiple conditional checks."
                ],
            },
            new CSharpElement
            {
                Label = "SwitchSection",
                Facts =
                [
                    "A SwitchSection represents a block of code associated with one or more case labels in a SwitchStatement.",
                    "Each SwitchSection can contain one or more statements, which execute if the case matches.",
                    "A SwitchSection ends with a 'break', 'goto', or 'return' statement to terminate execution."
                ],
                Insights =
                [
                    "SwitchSections improve readability by grouping logic for each case.",
                    "Use clear and concise logic within SwitchSections to avoid clutter.",
                    "Always ensure a SwitchSection ends properly to prevent unintended fall-through."
                ],
            },
            new CSharpElement
            {
                Label = "CaseKeyword",
                Facts =
                [
                    "The 'case' keyword is used to define a match condition in a switch statement.",
                    "Each 'case' is followed by a value that the switch expression is compared against.",
                    "The code block under a 'case' executes if the value matches the switch expression."
                ],
                Insights =
                [
                    "Use 'case' to define specific conditions in a switch statement.",
                    "Ensure each 'case' ends with a terminating statement like 'break' to avoid fall-through.",
                    "Consider combining 'case' labels with the same logic to avoid duplication."
                ],
            },
            new CSharpElement
            {
                Label = "CaseSwitchLabel",
                Facts =
                [
                    "A CaseSwitchLabel represents a specific label in a switch section defined by the 'case' keyword.",
                    "It specifies the value to be compared with the switch expression.",
                    "Multiple CaseSwitchLabels can direct execution to the same SwitchSection."
                ],
                Insights =
                [
                    "CaseSwitchLabels improve clarity by defining exact match conditions.",
                    "Use consistent formatting for CaseSwitchLabels to enhance readability.",
                    "Combine related CaseSwitchLabels to reduce repetitive logic."
                ],
            },
            new CSharpElement
            {
                Label = "DefaultSwitchLabel",
                Facts =
                [
                    "A DefaultSwitchLabel is used to define the default case in a switch statement.",
                    "It executes when none of the 'case' labels match the switch expression.",
                    "The default case is optional but recommended to handle unexpected values."
                ],
                Insights =
                [
                    "Use DefaultSwitchLabel as a fallback for handling unmatched cases.",
                    "Including a default label makes your switch statements robust and future-proof.",
                    "Ensure meaningful logic in the default case to handle edge cases appropriately."
                ],
            },
            new CSharpElement
            {
                Label = "DefaultKeyword",
                Facts =
                [
                    "The 'default' keyword has multiple uses in C#: as a default case in a switch statement or as the default literal.",
                    "In generics, 'default' initializes a type parameter to its default value.",
                    "The 'default' keyword can also be used to specify a default value for nullable or reference types."
                ],
                Insights =
                [
                    "Use 'default' to initialize variables with type-safe defaults in generics or nullable contexts.",
                    "The 'default' case in switch statements ensures all possible inputs are handled.",
                    "Leverage the 'default' literal for cleaner and safer default initializations."
                ],
            },
            new CSharpElement
            {
                Label = "PlusPlusToken",
                Alias = "Increment operator",
                Facts =
                [
                    "The \'++\' operator is used to increase the value of a numeric variable by 1.",
                ],
                Insights =
                [
                ],
            },
            new CSharpElement
            {
                Label = "MinusMinusToken",
                Alias = "Decrement operator",
                Facts =
                [
                    "The \'--\' operator is used to decrease the value of a numeric variable by 1.",
                ],
                Insights =
                [
                ],
            },
            new CSharpElement
            {
                Label = "BarBarToken",
                Alias = "Logical OR operator",
                Facts =
                [
                    "The \'||\' operator is used to evaluate two expressions at one time.",
                    "The expressions evaluated must return type bool."
                ],
                Insights =
                [
                ],
            },
            new CSharpElement
            {
                Label = "ExclamationToken",
                Alias = "Logical NOT operator",
                Facts =
                [
                    "The \'!\' operator is used to negate a boolean expression.",
                ],
                Insights =
                [
                ],
            },
            new CSharpElement
            {
                Label = "ExclamationEqualsToken",
                Alias = "Inequality operator",
                Facts =
                [
                    "The \'!=\' operator is used to .",
                ],
                Insights =
                [
                ],
            },
            new CSharpElement
            {
                Label = "EqualsEqualsToken",
                Alias = "Equality operator",
                Facts =
                [
                    "The \'==\' operator is used to .",
                ],
                Insights =
                [
                ],
            },
        };
    }
}
