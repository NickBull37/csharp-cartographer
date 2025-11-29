namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class TokenTag
    {
        public string Label { get; set; }

        public List<string> Facts { get; set; } = [];

        public List<string> Insights { get; set; } = [];

        public string BorderClass { get; set; }

        public string BgColorClass { get; set; }

        public string OptionsColorClass { get; set; }

        public string OptionsLabel { get; set; }

        public List<AvailableOption>? Options { get; set; }
    }

    public class AvailableOption
    {
        public string? Label { get; set; }

        public string? OptionsText { get; set; }

        public List<string>? OptionsList { get; set; }
    }

    public class AccessorTag : TokenTag
    {
        public AccessorTag()
        {
            Label = $"Accessor";
            Facts =
            [
                "Accessors are elements of code within a property or indexer that allow getting or setting the property's value.",
                "By default, their implementation is implicitly provided by the compiler when using the get or set keywords but can be explicitly defined or customized if needed.",
                "You can also provide an access modifier to an accessor to control their visibility independently of the property's overall access level.",
                "The accessors available in C# are \"get\", \"set\", and \"init\""
            ];
            Insights =
            [
                ""
            ];
            BorderClass = "tag-border-blue";
            BgColorClass = "tag-bg-blue";
        }
    }

    public class AccessModifierTag : TokenTag
    {
        public AccessModifierTag()
        {
            Label = $"Access Modifier";
            Facts =
            [
                "Access modifiers are keywords used to define the visibility and accessibility of classes, methods, variables, and other members."
            ];
            Insights =
            [
                ""
            ];
            BorderClass = "tag-border-blue";
            BgColorClass = "tag-bg-blue";
            OptionsColorClass = "tag-options-blue";
            OptionsLabel = "The available access modifiers in C# are";
            Options =
            [
                new AvailableOption()
                {
                    OptionsText = "public  private  protected  internal  protected\u2011internal  private\u2011protected  file",
                    OptionsList =
                    [
                        "public",
                        "private",
                        "protected",
                        "internal",
                        "protected internal",
                        "private protected",
                    ]
                }
            ];
        }
    }

    public class AttributeTag : TokenTag
    {
        public AttributeTag()
        {
            Label = $"Attribute";
            Facts =
            [
                "Attributes are declarative tags used to add metadata to code elements such as classes, methods, properties, or assemblies."
            ];
            Insights =
            [
                ""
            ];
            BorderClass = "tag-border-green";
            BgColorClass = "tag-bg-green";
        }
    }

    public class BaseTypeTag : TokenTag
    {
        public BaseTypeTag()
        {
            Label = $"BaseType";
            Facts =
            [
                "Base types are classes or interfaces that another class or interface can inherit from.",
                "Base types provide members (fields, methods, properties, etc.) that the derived type can use, override, or extend.",
            ];
            Insights =
            [
                "C# allows for single-class inheritance, meaning a class can inherit from only one base class at a time.",
                "C# allows for multiple-interface inheritance, meaning a class or interface can implement or inherit from multiple interfaces at the same time.",
                "A class can inherit from a single base class while also implementing multiple interfaces."
            ];
            BorderClass = "tag-border-green";
            BgColorClass = "tag-bg-green";
        }
    }

    public class ClassTag : TokenTag
    {
        public ClassTag()
        {
            Label = $"Class";
            Facts =
            [
                "Classes are blueprints for creating objects. They define the structure and behavior of an object using fields (data) and methods (functions) that operate on the data.",
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-green";
            BgColorClass = "tag-bg-green";
        }
    }

    public class ConstructorTag : TokenTag
    {
        public ConstructorTag()
        {
            Label = $"Constructor";
            Facts =
            [
                "Constructors are special methods called automatically called when an instance of a class or struct is created.",
                "Their purpose is to initialize the object, often by setting initial values for its fields or properties."
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-green";
            BgColorClass = "tag-bg-green";
        }
    }

    public class DelimiterTag : TokenTag
    {
        public DelimiterTag()
        {
            Label = $"Delimiter";
            Facts =
            [
                "Delimiters are characters or sequences of characters used to separate, enclose, or structure code elements."
            ];
            Insights =
            [
                ""
            ];
            BorderClass = "tag-border-black";
            BgColorClass = "tag-bg-black";
        }
    }

    public class FieldTag : TokenTag
    {
        public FieldTag()
        {
            Label = $"Field";
            Facts =
            [
                "Fields are variables declared directly within a class or struct to store data or state.",
                "Fields are typically accessed directly or through properties for better encapsulation and control."
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-red";
            BgColorClass = "tag-bg-red";
        }
    }

    public class GenericCollectionTag : TokenTag
    {
        public GenericCollectionTag()
        {
            Label = $"Generic Collection Type";
            Facts =
            [
                "Generic collections are type-safe collections that allows you to define the type of elements they can store at compile-time.",
                "Generic collections are part of the System.Collections.Generic namespace and provide better performance and type safety compared to non-generic collections."
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-green";
            BgColorClass = "tag-bg-green";
        }
    }

    public class GenericTypeArgumentTag : TokenTag
    {
        public GenericTypeArgumentTag()
        {
            Label = $"Generic Type Argument";
            Facts =
            [
                "Generic type arguments are the specific type you provide when instantiating a generic class, struct, interface, method, or delegate.",
                "They replace the generic type parameter (T, TKey, TValue, etc.) defined in the generic declaration."
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-green";
            BgColorClass = "tag-bg-green";
        }
    }

    public class IdentifierTag : TokenTag
    {
        public IdentifierTag()
        {
            Label = $"Identifier";
            Facts =
            [
                "An identifier refers to a named entity in C# code."
            ];
            Insights =
            [
                //"Hover your mouse over an identifier in Visual Studio to show details about the identifier."
            ];
            BorderClass = "tag-border-gray";
            BgColorClass = "tag-bg-gray";
        }
    }

    public class KeywordTag : TokenTag
    {
        public KeywordTag()
        {
            Label = $"C# Keyword";
            Facts =
            [
                "Keywords are reserved words with a special meaning in the C# language syntax and cannot be used as identifiers.",
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-purple";
            BgColorClass = "tag-bg-purple";
        }
    }

    public class MethodTag : TokenTag
    {
        public MethodTag()
        {
            Label = $"Method";
            Facts =
            [
                "Methods are blocks of code that performs a specific task and can be called to execute that task.",
                "Methods can take inputs (parameters), perform actions, and return a result or void (no result)."
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-yellow";
            BgColorClass = "tag-bg-yellow";
        }
    }

    public class ModifierTag : TokenTag
    {
        public ModifierTag()
        {
            Label = $"Modifier";
            Facts =
            [
                "Modifiers (non-access modifiers) define additional behaviors or characteristics of a member (field, method, property, etc.).",
                "Non-access modifiers in C# include \"abstract\", \"async\", \"const\", \"override\", \"readonly\", \"sealed\", \"static\", \"virtual\", & \"volatile\"."
            ];
            Insights =
            [
                ""
            ];
            BorderClass = "tag-border-blue";
            BgColorClass = "tag-bg-blue";
        }
    }

    public class NumericLiteralTag : TokenTag
    {
        public NumericLiteralTag()
        {
            Label = $"Numeric Literal";
            Facts =
            [
                "Numeric literal are constant values written directly in the code to represent a number.",
                "It can be an integer (e.g., 42), a floating-point number (e.g., 3.14), or use suffixes to specify types (e.g., 42L for long, 3.14f for float, 2.71m for decimal).",
                "Numeric literals can also be written in different formats like hexadecimal (0x1A) or binary (0b1010)."
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-lightgreen";
            BgColorClass = "tag-bg-lightgreen";
        }
    }

    public class OperatorTag : TokenTag
    {
        public OperatorTag()
        {
            Label = $"Operator";
            Facts =
            [
                "Operators are symbols or keyword that performs a specific operation on one or more operands, such as arithmetic, assignment, comparison, or logical operations.",
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-darkpurple";
            BgColorClass = "tag-bg-darkpurple";
        }
    }

    public class ParameterTag : TokenTag
    {
        public ParameterTag()
        {
            Label = $"Parameter";
            Facts =
            [
                "Parameters are variables declared in a method, constructor, or function definition that will receive input values (called arguments) when the method is called.",
                "Parameters are defined inside the parentheses of the method signature, with a type and a name, such as int number in public void PrintNumber(int number).",
                "Parameters can also have modifiers like ref, out, or params to modify their behavior.",
            ];
            Insights =
            [
                ""
            ];
            BorderClass = "tag-border-cyan";
            BgColorClass = "tag-bg-cyan";
        }
    }

    public class ParameterTypeTag : TokenTag
    {
        public ParameterTypeTag()
        {
            Label = $"Parameter Type";
            Facts =
            [
                "Parameter types are the data types specified for a parameter in a method, constructor, or function definition and define the type of value that the parameter can accept."
            ];
            Insights =
            [
                ""
            ];
            BorderClass = "tag-border-lightblue";
            BgColorClass = "tag-bg-lightblue";
        }
    }

    public class PredefinedTypeTag : TokenTag
    {
        public PredefinedTypeTag()
        {
            Label = $"Predefined Type";
            Facts =
            [
                "Predefined types refer to all data types that are natively supported by the C# language.",
                "These types are aliases for corresponding .NET Framework types (e.g., int is an alias for System.Int32)."
            ];
            Insights = [];
            BorderClass = "tag-border-darkjade";
            BgColorClass = "tag-bg-darkjade";
        }
    }

    public class PrimitiveTypeTag : TokenTag
    {
        public PrimitiveTypeTag()
        {
            Label = $"Primitive Type";
            Facts =
            [
                "Primitive types are a subset of predefined types that are directly mapped to low-level data types in the Common Language Runtime (CLR).",
                "These are the most basic data types the C# language has to offer and are typically optimized for performance."
            ];
            Insights = [];
            BorderClass = "tag-border-jade";
            BgColorClass = "tag-bg-jade";
            //OptionsColorClass = "tag-option-jade";
            //OptionsLabel = "The available primitive types in C# are";
            //Options =
            //[
            //    new AvailableOption()
            //    {
            //        Label = "Integral Types",
            //        OptionsText = "byte  sbyte  short  ushort  int  uint  long  ulong  char"
            //    },
            //    new AvailableOption()
            //    {
            //        Label = "Floating-point Types",
            //        OptionsText = "float  double  decimal"
            //    },
            //    new AvailableOption()
            //    {
            //        Label = "Other Types",
            //        OptionsText = "bool  string  object  void"
            //    }
            //];
        }
    }

    public class PrimitiveIntegralTypeTag : TokenTag
    {
        public PrimitiveIntegralTypeTag()
        {
            Label = $"Primitive Type: Integral";
            Facts =
            [
                "Primitive types are a subset of predefined types that are directly mapped to low-level data types in the Common Language Runtime (CLR).",
                "These are the most basic data types the C# language has to offer and are typically optimized for performance.",
                "Integral types are value types that represent whole numbers without fractional parts."
            ];
            Insights = [];
            BorderClass = "tag-border-jade";
            BgColorClass = "tag-bg-jade";
            OptionsColorClass = "tag-option-jade";
            OptionsLabel = "The predefined integral types in C# are";
            Options =
            [
                new AvailableOption()
                {
                    OptionsText = "byte  sbyte  short  ushort  int  uint  long  ulong  char"
                },
            ];
        }
    }

    public class PrimitiveFloatingPointTypeTag : TokenTag
    {
        public PrimitiveFloatingPointTypeTag()
        {
            Label = $"Primitive Type: Floating-point";
            Facts =
            [
                "Primitive types are a subset of predefined types that are directly mapped to low-level data types in the Common Language Runtime (CLR).",
                "These are the most basic data types the C# language has to offer and are typically optimized for performance.",
                "Floating-point types are value types used to represent numbers with fractional parts."
            ];
            Insights = [];
            BorderClass = "tag-border-jade";
            BgColorClass = "tag-bg-jade";
            OptionsColorClass = "tag-option-jade";
            OptionsLabel = "The predefined floating-point types in C# are";
            Options =
            [
                new AvailableOption()
                {
                    OptionsText = "float  double  decimal"
                },
            ];
        }
    }

    public class PropertyTag : TokenTag
    {
        public PropertyTag()
        {
            Label = $"Property";
            Facts =
            [
                "Properties are class members that provide a flexible mechanism to access and modify the value of a class's fields or data, often encapsulating logic for getting and setting values.",
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-indigo";
            BgColorClass = "tag-bg-indigo";
        }
    }

    public class PunctuationTag : TokenTag
    {
        public PunctuationTag()
        {
            Label = $"Punctuation";
            Facts =
            [
                "Punctuation refers to the special symbols used in the syntax to structure code and separate elements."
            ];
            Insights =
            [
                ""
            ];
            BorderClass = "tag-border-black";
            BgColorClass = "tag-bg-black";
        }
    }

    public class ReturnTypeTag : TokenTag
    {
        public ReturnTypeTag()
        {
            Label = $"Return Type";
            Facts =
            [
                "The return type specifies the type of value that a method returns to its caller and is defined before the method name in the method declaration..",
            ];
            Insights =
            [
                //"To make code more dynamic, return interfaces when possible. This allows implementation to be more adaptable because the code can return any object that implements the interface.",
            ];
            BorderClass = "tag-border-purple";
            BgColorClass = "tag-bg-purple";
        }
    }

    public class StringLiteralTag : TokenTag
    {
        public StringLiteralTag()
        {
            Label = $"String Literal";
            Facts =
            [
                "String literals are a sequence of characters enclosed in double quotes (\" \"), representing a constant text value.",
            ];
            Insights =
            [
                "",
            ];
            BorderClass = "tag-border-orange";
            BgColorClass = "tag-bg-orange";
        }
    }
}
