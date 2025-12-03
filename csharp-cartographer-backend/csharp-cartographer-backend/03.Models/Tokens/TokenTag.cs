namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class TokenTag
    {
        public string Label { get; set; }

        public List<string> Facts { get; set; } = [];

        public List<string> Insights { get; set; } = [];

        public string BorderClass { get; set; }

        public string BgColorClass { get; set; }
    }

    public class KeywordTag : TokenTag
    {
        public KeywordTag()
        {
            Label = $"C# Keyword";
            Facts =
            [
                "Keywords are reserved words that have special meaning to the C# compiler and cannot be used as identifiers.",
            ];
            Insights =
            [
                "Some keywords are contextual — they only act like keywords in certain situations (var, nameof, async).",
            ];
            BorderClass = "tag-border-purple";
            BgColorClass = "tag-bg-purple";
        }
    }

    public class AccessorTag : TokenTag
    {
        public AccessorTag()
        {
            Label = $"Accessor";
            Facts =
            [
                "Accessors are elements of code within a property that allow getting or setting the property's value.",
                "You can provide an access modifier to an accessor to control who can get or set a property's value independently of the property's overall access level.",
            ];
            Insights =
            [
                "You can provide an access modifier to an accessor to control who can get or set a property's value independently of the property's overall access level.",
                "Properties do not require accessors. Remove the \"set\" accessor and that property's value cannot be set outside of initialization."
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
                "Access modifiers in C# control who is allowed to see and use your classes, methods, and variables.",
                "This allows you to hide or expose parts of your code so others interact with it as intended."
            ];
            Insights =
            [
                "Access modifiers harden your code by preventing other parts of your program from misusing internal details.",
                "Choosing the most restrictive modifier by default (like private) is common since the access level can always be updated later."
            ];
            BorderClass = "tag-border-blue";
            BgColorClass = "tag-bg-blue";
        }
    }

    public class AttributeTag : TokenTag
    {
        public AttributeTag()
        {
            Label = $"Attribute";
            Facts =
            [
                "Attributes are declarative tags used to add extra information to code elements such as classes, methods, or properties.",
                "They don’t change your code directly, but they tell the compiler or other tools to treat your code in a certain way."
            ];
            Insights =
            [
                "Attributes let you quickly add powerful behavior without changing your actual code logic, such as data validation or serialization.",
                "Many frameworks (like ASP.NET) use attributes heavily and there are many options available to take advantage of."
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
                "C# allows for single-class inheritance, meaning a class can only inherit from one base class at a time.",
                "C# allows for multiple-interface inheritance, meaning a class or interface can inherit from multiple interfaces at the same time.",
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
                "Classes are blueprints for creating objects. They define the structure and behavior of an object using fields, properties, and methods that operate on the data.",
                "You can create an instance of a class by using the \"new\" keyword."
            ];
            Insights =
            [
                "Classes can be used in many ways such as model definitions which often contain many properties, to services which usually have more methods.",
                "Using classes lets you reuse logic and share behavior through inheritance, reducing repetition in your code."
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
                "You can create multiple constructors with different parameters, giving flexibility in how an object can be created."
            ];
            Insights =
            [
                "If you create a class and don’t create a constructor, C# gives your class a default constructor that does nothing but create the object.",
                "If you do create a constructor that requires input parameters, you will lose access to the default constructor provided by C#."
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
                "Delimiters are characters used to separate, enclose, or structure code elements."
            ];
            Insights =
            [
                "Visual Studio can automatically fix indentation, braces, and spacing around delimiters using the shortcut Ctrl + K, Ctrl + D.",
                "You can also enforce certains rules around delimiters using an .editorconfig file."
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
                "Fields are variables that belong to a class or struct to store data and cannot be accessed outside the scope they are defined.",
                "Fields are typically accessed directly when inside the class or through properties when outside the class for better encapsulation and control."
            ];
            Insights =
            [
                "Fields can have access modifiers (like private or public) to control who can read or change them.",
                "Most fields are kept private so the class controls how its data is accessed, usually through properties."
            ];
            BorderClass = "tag-border-red";
            BgColorClass = "tag-bg-red";
        }
    }

    public class GenericTypeArgumentTag : TokenTag
    {
        public GenericTypeArgumentTag()
        {
            Label = $"Generic Type Argument";
            Facts =
            [
                "A generic type argument is the specific data type you supply when using a generic class or method.",
                "They replace the generic type parameter inside angle brackets (T, TKey, TValue, etc.) defined in the generic declaration."
            ];
            Insights =
            [
                "This reduces duplication because one generic class or method can work with many different types.",
                "If a method requires a generic type constraint, any type that inherits from that constraint will also work.",
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
                "Hover your cursor over an identifier in Visual Studio to show details about the identifier.",
                "You can add information to these details by using XML documentation comments."
            ];
            BorderClass = "tag-border-gray";
            BgColorClass = "tag-bg-gray";
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
                "Breaking your program into small, focused methods that perform a single task makes your code easier to understand and debug.",
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
                "Modifiers are words added to classes, methods, fields, or properties that change how they behave, such as controlling access or inheritance.",
                "Common modifiers in C# include \"abstract\", \"async\", \"const\", \"override\", \"readonly\", \"sealed\", \"static\", \"virtual\", & \"volatile\"."
            ];
            Insights =
            [
                "Modifiers help make your intent obvious to other developers, improving clarity and reducing mistakes."
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
                "Numeric literal are values written directly in the code instead of storing it in a variable.",
            ];
            Insights =
            [
                "They can be integers (e.g., 42), floating-point numbers (e.g., 3.14), or use suffixes to specify types (e.g., 42L for long, 3.14f for float, 2.71m for decimal).",
                "Numeric literals can also be written in different formats like hexadecimal (0x1A) or binary (0b1010)."
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
                "Operators are symbols that performs a specific operation on one or more operands, such as arithmetic, assignment, comparison, or logical operations.",
            ];
            Insights =
            [
                "Learning operators helps you express logic clearly and efficiently while expanding your options for solving problems.",
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
                "Parameters are variables declared in a method or constructor definition that will receive input values when the method or constructor is called.",
                "Parameters are defined inside the parentheses of the method or constructor signature and require a name and a type.",

            ];
            Insights =
            [
                "Parameters can also have modifiers like \"ref\" or \"out\" to modify their behavior.",
                "Parameters only refer to values in the method/constructor where they are defined. The values passed in for parameters are called arguments."
            ];
            BorderClass = "tag-border-cyan";
            BgColorClass = "tag-bg-cyan";
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
            Insights =
            [
                "Choosing the right predefined type for your data helps your program run efficiently and avoids unexpected bugs.",
            ];
            BorderClass = "tag-border-darkjade";
            BgColorClass = "tag-bg-darkjade";
        }
    }

    public class PropertyTag : TokenTag
    {
        public PropertyTag()
        {
            Label = $"Property";
            Facts =
            [
                "Properties are class members that provide a flexible mechanism to access and modify the property's value.",
                "Auto-properties (public int Age { get; set; }) let C# automatically create the hidden field that stores the value."
            ];
            Insights =
            [
                "Properties can help protect data by adding rules or checks before a value is returned or updated.",
                "A property can have a getter, a setter, or both, and you can control access to each one independently."
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
                "Punctuation refers to the special symbols used to structure code and tell the compiler how to read it."
            ];
            Insights =
            [
                "Mastering punctuation helps reduce common beginner errors since small mistakes like missing semicolons can cause major headaches."
            ];
            BorderClass = "tag-border-black";
            BgColorClass = "tag-bg-black";
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
                "String literals can also include escape sequences like \n for a new line or \" for an actual quote character."
            ];
            Insights =
            [
                "You can also use a verbatim string literal (@\"C:\\Path\\Here\") to write text without escaping backslashes.",
                "Interpolated string literals allow you to insert variable values into placeholders ($\"Hello {name}\")."
            ];
            BorderClass = "tag-border-orange";
            BgColorClass = "tag-bg-orange";
        }
    }
}
