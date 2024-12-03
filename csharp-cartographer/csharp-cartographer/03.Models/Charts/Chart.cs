namespace csharp_cartographer._03.Models.Charts
{
    public class Chart
    {
        public string Label { get; set; }

        public List<string> Facts { get; set; } = [];

        public List<string> Insights { get; set; } = [];

        public string BorderClass { get; set; }

        public string BgColorClass { get; set; }
    }

    public class AccessModifierChart : Chart
    {
        public AccessModifierChart()
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
            BorderClass = "chart-border-blue";
            BgColorClass = "chart-bg-blue";
        }
    }

    public class AttributeChart : Chart
    {
        public AttributeChart()
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
            BorderClass = "chart-border-green";
            BgColorClass = "chart-bg-green";
        }
    }

    public class BaseTypeChart : Chart
    {
        public BaseTypeChart()
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
            BorderClass = "chart-border-green";
            BgColorClass = "chart-bg-green";
        }
    }

    public class DelimiterChart : Chart
    {
        public DelimiterChart()
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
            BorderClass = "chart-border-black";
            BgColorClass = "chart-bg-black";
        }
    }

    public class FieldChart : Chart
    {
        public FieldChart()
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
            BorderClass = "chart-border-red";
            BgColorClass = "chart-bg-red";
        }
    }

    public class GenericCollectionChart : Chart
    {
        public GenericCollectionChart()
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
            BorderClass = "chart-border-green";
            BgColorClass = "chart-bg-green";
        }
    }

    public class GenericTypeArgumentChart : Chart
    {
        public GenericTypeArgumentChart()
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
            BorderClass = "chart-border-green";
            BgColorClass = "chart-bg-green";
        }
    }

    public class IdentifierChart : Chart
    {
        public IdentifierChart()
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
            BorderClass = "chart-border-gray";
            BgColorClass = "chart-bg-gray";
        }
    }

    public class KeywordChart : Chart
    {
        public KeywordChart()
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
            BorderClass = "chart-border-purple";
            BgColorClass = "chart-bg-purple";
        }
    }

    public class PropertyChart : Chart
    {
        public PropertyChart()
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
            BorderClass = "chart-border-indigo";
            BgColorClass = "chart-bg-indigo";
        }
    }

    public class StringLiteralChart : Chart
    {
        public StringLiteralChart()
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
            BorderClass = "chart-border-orange";
            BgColorClass = "chart-bg-orange";
        }
    }

    public class NumericLiteralChart : Chart
    {
        public NumericLiteralChart()
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
            BorderClass = "chart-border-lightgreen";
            BgColorClass = "chart-bg-lightgreen";
        }
    }

    public class ReturnTypeChart : Chart
    {
        public ReturnTypeChart()
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
            BorderClass = "chart-border-purple";
            BgColorClass = "chart-bg-purple";
        }
    }

    public class PrimitiveTypeChart : Chart
    {
        public PrimitiveTypeChart()
        {
            Label = $"Primitive Type";
            Facts =
            [
                "Primitive types are basic data types provided by the language to represent simple values.",
            ];
            Insights = [];
            BorderClass = "chart-border-jade";
            BgColorClass = "chart-bg-jade";
        }
    }

    public class ParameterChart : Chart
    {
        public ParameterChart()
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
            BorderClass = "chart-border-cyan";
            BgColorClass = "chart-bg-cyan";
        }
    }

    public class ParameterTypeChart : Chart
    {
        public ParameterTypeChart()
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
            BorderClass = "chart-border-lightblue";
            BgColorClass = "chart-bg-lightblue";
        }
    }

    public class PunctuationChart : Chart
    {
        public PunctuationChart()
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
            BorderClass = "chart-border-black";
            BgColorClass = "chart-bg-black";
        }
    }

    public class ModifierChart : Chart
    {
        public ModifierChart()
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
            BorderClass = "chart-border-blue";
            BgColorClass = "chart-bg-blue";
        }
    }
}
