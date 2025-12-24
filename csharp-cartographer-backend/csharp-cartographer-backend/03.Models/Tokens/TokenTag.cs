using csharp_cartographer_backend._01.Configuration.Enums;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class TokenTag
    {
        public string Label { get; init; }

        public List<TagEntry> TheBasicsEntries { get; init; } = [];

        public List<TagEntry> KeyPointsEntries { get; init; } = [];

        public List<TagEntry> UseForEntries { get; init; } = [];

        public List<TagEntry> ExploreEntries { get; init; } = [];

        public string? BorderClass { get; init; }

        public string BgColorClass { get; init; }

        public string? KeywordClass { get; init; }
    }

    public class KeywordTag : TokenTag
    {
        public KeywordTag()
        {
            Label = $"C# Keyword";
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "C# Keyword",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Keywords are predefined, reserved identifiers that have special meanings to the compiler."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "C# Keyword",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Keywords can't be used as identifiers in your program unless they include @ as a prefix."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "C# Keyword",
                    IsExample = true,
                    Segments =
                    [
                        new Segment
                        {
                            Text = "int @class = 10;",
                            IsCode = true,
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "C# Keyword",
                    IsInsight = true,
                    Segments =
                    [
                        new Segment
                        {
                            Text = "As new keywords are added to the C# language, they're added as "
                        },
                        new Segment
                        {
                            Text = "contextual",
                            IsItalic = true,
                        },
                        new Segment
                        {
                            Text = " keywords to avoid breaking older programs."
                        },
                    ]
                }
            ];
            ExploreEntries =
            [
                new TagEntry
                {
                    TagType = "C# Keyword",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Contextual Keywords",
                            Ref = new SegmentRef
                            {
                                Url = "https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/#contextual-keywords"
                            }
                        },
                    ]
                },
            ];
            BorderClass = "tag-border-purple";
            BgColorClass = "tag-bg-purple";
        }
    }

    public class AccessorTag : TokenTag
    {
        public AccessorTag(string text)
        {
            Label = $"Accessor - {text}";
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Accessor",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Accessors are special methods that implement properties, enabling easy access while promoting data safety and flexibility."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Accessor",
                    Segments = GetAccessorDefinitionSegments(text),
                },
            ];
            BorderClass = "tag-border-blue";
            BgColorClass = "tag-bg-blue";
            KeywordClass = "tag-keyword-blue";

            static List<Segment> GetAccessorDefinitionSegments(string text)
            {
                switch (text)
                {
                    case "get":
                        return
                            [
                                new Segment
                                {
                                    Text = "A ",
                                },
                                new Segment
                                {
                                    Text = "get",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " accessor defines a method within a property or indexer that returns the property value or the indexer element.",
                                },
                            ];
                    case "set":
                        return
                            [
                                new Segment
                                {
                                    Text = "A ",
                                },
                                new Segment
                                {
                                    Text = "set",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " accessor defines a method within a property or indexer that assigns a value to the property or the indexer element.",
                                },
                            ];
                    case "init":
                        return
                            [
                                new Segment
                                {
                                    Text = "An ",
                                },
                                new Segment
                                {
                                    Text = "init",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " accessor defines a method within a property or indexer that assigns a value to the property or the indexer element ",
                                },
                                new Segment
                                {
                                    Text = "only during object construction",
                                    IsBold = true,
                                },
                                new Segment
                                {
                                    Text = ".",
                                },
                            ];
                    default:
                        return [];
                }
            }
        }
    }

    public class AccessModifierTag : TokenTag
    {
        public AccessModifierTag(string text)
        {
            var tagEntries = GetGeneralTagEntries();
            tagEntries.AddRange(GetIndividualTagEntries(text));

            Label = $"Access Modifier - {text}";
            TheBasicsEntries = tagEntries.Where(entry => entry.Section == TokenTagSection.TheBasics).ToList();
            KeyPointsEntries = tagEntries.Where(entry => entry.Section == TokenTagSection.KeyPoints).ToList();
            UseForEntries = tagEntries.Where(entry => entry.Section == TokenTagSection.UseFor).ToList();
            ExploreEntries = tagEntries.Where(entry => entry.Section == TokenTagSection.Explore).ToList();
            BorderClass = "tag-border-blue";
            BgColorClass = "tag-bg-blue";
            KeywordClass = "tag-keyword-blue";

            // applicable to all access modifiers
            static List<TagEntry> GetGeneralTagEntries()
            {
                return
                [
                    new TagEntry
                    {
                        TagType = "AccessModifier",
                        Segments =
                        [
                            new Segment
                            {
                                Text = "All types and type members have an accessibility level that determines where they can be accessed from. This level is specified using an access modifier."
                            },
                        ]
                    },
                    new TagEntry
                    {
                        TagType = "AccessModifier",
                        Segments =
                        [
                            new Segment
                            {
                                Text = "Not all access modifiers are valid for all types or members in all contexts."
                            },
                        ]
                    },
                    new TagEntry
                    {
                        TagType = "AccessModifier",
                        Section = TokenTagSection.Explore,
                        Segments =
                        [
                            new Segment
                            {
                                Text = "Access Modifiers (C# programming guide)",
                                Ref = new SegmentRef
                                {
                                    Url = "https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers",
                                }
                            },
                        ]
                    },
                ];
            }

            // applicable to individual access modifiers
            static List<TagEntry> GetIndividualTagEntries(string text)
            {
                return text switch
                {
                    "public" =>
                    [
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.TheBasics,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "public",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " elements are accessible from any assembly.",
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            IsInsight = true,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "An assembly is a "
                                },
                                new Segment
                                {
                                    Text = ".dll",
                                    IsCode = true
                                },
                                new Segment
                                {
                                    Text = " or "
                                },
                                new Segment
                                {
                                    Text = ".exe",
                                    IsCode = true
                                },
                                new Segment
                                {
                                    Text = " created by compiling one or more "
                                },
                                new Segment
                                {
                                    Text = ".cs",
                                    IsCode = true
                                },
                                new Segment
                                {
                                    Text = " files into a single compilation."
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.KeyPoints,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "this entity can be accessed from anywhere"
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.UseFor,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "class methods that need to be called from other classes"
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.UseFor,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "class properties that can be modified by other classes"
                                },
                            ]
                        },
                    ],
                    "private" =>
                    [
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.TheBasics,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "private",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " elements are accessible only within the containing class or struct.",
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.KeyPoints,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "this entity can only be accessed from in the containing scope"
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.UseFor,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "dependencies using the Dependancy Injection (DI) design pattern"
                                },
                            ]
                        },
                    ],
                    "protected" =>
                    [
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.TheBasics,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "protected",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " elements are accessible from within the containing class or any derived class.",
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.KeyPoints,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "find a key point to add later"
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.UseFor,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "find a use case later"
                                },
                            ]
                        },
                    ],
                    "internal" =>
                    [
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.TheBasics,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "internal",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " elements are accessible from anywhere within the same assembly.",
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            IsInsight = true,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "An assembly is a "
                                },
                                new Segment
                                {
                                    Text = ".dll",
                                    IsCode = true
                                },
                                new Segment
                                {
                                    Text = " or "
                                },
                                new Segment
                                {
                                    Text = ".exe",
                                    IsCode = true
                                },
                                new Segment
                                {
                                    Text = " created by compiling one or more "
                                },
                                new Segment
                                {
                                    Text = ".cs",
                                    IsCode = true
                                },
                                new Segment
                                {
                                    Text = " files into a single compilation."
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.KeyPoints,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "find a key point to add later"
                                },
                            ]
                        },
                        new TagEntry
                        {
                            TagType = "AccessModifier",
                            Section = TokenTagSection.UseFor,
                            Segments =
                            [
                                new Segment
                                {
                                    Text = "find a use case later"
                                },
                            ]
                        },
                    ],
                    _ => [],
                };
            }
        }
    }

    public class AttributeTag : TokenTag
    {
        public AttributeTag()
        {
            Label = $"Attribute";
            //Facts =
            //[
            //    "Attributes are declarative tags used to add extra information to code elements such as classes, methods, or properties.",
            //    "They don’t change your code directly, but they tell the compiler or other tools to treat your code in a certain way."
            //];
            //Insights =
            //[
            //    "Attributes let you quickly add powerful behavior without changing your actual code logic, such as data validation or serialization.",
            //    "Many frameworks (like ASP.NET) use attributes heavily and there are many options available to take advantage of."
            //];
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Attribute",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Attributes provide a powerful way to associate metadata, or declarative information, with code (assemblies, types, methods, properties, etc.)."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Attribute",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Attributes can be applied to entire assemblies, modules, or smaller program elements, such as classes and properties."
                        },
                    ]
                }
            ];
            KeyPointsEntries =
            [
                new TagEntry
                {
                    TagType = "Attribute",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "After you associate an attribute with a program entity, you can query the attribute at run time by using a technique called "
                        },
                        new Segment
                        {
                            Text = "reflection",
                            IsItalic = true,
                            Ref = new SegmentRef
                            {
                                Url = "https://learn.microsoft.com/en-us/dotnet/fundamentals/reflection/reflection",
                            }
                        },
                        new Segment
                        {
                            Text = " that enables you to obtain information about loaded assemblies and the types defined within them."
                        },
                    ]
                },
            ];
            BorderClass = "tag-border-green";
            BgColorClass = "tag-bg-green";
        }
    }

    public class ClassBaseTypeTag : TokenTag
    {
        public ClassBaseTypeTag()
        {
            Label = $"BaseType - class";
            //Facts =
            //[
            //    "Base types are classes or interfaces that another class or interface can inherit from.",
            //    "Base types provide members (fields, methods, properties, etc.) that the derived type can use, override, or extend.",
            //];
            //Insights =
            //[
            //    "C# allows for single-class inheritance, meaning a class can only inherit from one base class at a time.",
            //    "C# allows for multiple-interface inheritance, meaning a class or interface can inherit from multiple interfaces at the same time.",
            //    "A class can inherit from a single base class while also implementing multiple interfaces."
            //];
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "BaseType - class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Class base types are classes that another class can inherit from. A derived class is a specialization of the base class."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "BaseType - class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "When you define a class to derive from another class, the derived class implicitly gains all the members of the base class, except for its constructors and finalizers."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "BaseType - class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "You can add additional members in the derived class that do not exist in the base class."
                        },
                    ]
                },
            ];
            KeyPointsEntries =
            [
                new TagEntry
                {
                    TagType = "BaseType - class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "C# allows for single-class inheritance, meaning a class can only inherit from one base class at a time."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "BaseType - class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "C# allows for multiple-interface inheritance, meaning a class or interface can inherit from multiple interfaces at the same time."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "BaseType - class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A class can inherit from a single base class while also implementing multiple interfaces."
                        },
                    ]
                },
            ];
            ExploreEntries =
            [
                new TagEntry
                {
                    TagType = "BaseType - class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = ""
                        },
                    ]
                },
            ];
            BorderClass = "tag-border-green";
            BgColorClass = "tag-bg-green";
        }
    }

    public class InterfaceBaseTypeTag : TokenTag
    {
        public InterfaceBaseTypeTag()
        {
            Label = $"BaseType - interface";
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "BaseType - interface",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Interface base types provide definitions for a group of related functionalities that a non-abstract class or a struct must implement."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "BaseType - interface",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "An interface can't declare instance data such as fields, automatically implemented properties, or property-like events."
                        },
                    ]
                },
            ];
            KeyPointsEntries =
            [
                new TagEntry
                {
                    TagType = "BaseType - interface",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "By convention, interface names begin with a capital "
                        },
                        new Segment
                        {
                            Text = "I",
                            IsCode = true,
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = "."
                        },
                    ]
                },
            ];
            ExploreEntries =
            [
                new TagEntry
                {
                    TagType = "BaseType - interface",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Use an interface when you have multiple classes that perform the same task but in different ways. Define methods for these shared tasks and any class that implements the interface will have to provide an implementation."
                        },
                    ]
                },
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
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Classes are blueprints for creating objects. They define the structure and behavior of an object using fields, properties, and methods."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Classes are declared by using the "
                        },
                        new Segment
                        {
                            Text = "class",
                            IsCode = true,
                            IsKeyword = true,
                            IsBold = true,
                            HighlightColor = "tag-keyword-blue",
                        },
                        new Segment
                        {
                            Text = " keyword followed by a unique identifier. "
                        },
                        new Segment
                        {
                            Text = "An optional access modifier precedes the class keyword."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Instances of a class can be created by using the "
                        },
                        new Segment
                        {
                            Text = "new",
                            IsCode = true,
                            IsKeyword = true,
                            IsBold = true,
                            HighlightColor = "tag-keyword-blue",
                        },
                        new Segment
                        {
                            Text = " keyword followed by the name of the class."
                        },
                    ]
                },
            ];
            KeyPointsEntries =
            [
                new TagEntry
                {
                    TagType = "Class",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Classes can be used in many ways such as model definitions which often contain many properties, to services which usually have more methods."
                        },
                    ]
                },
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
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Constructor",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Constructors are special methods called automatically called when an instance of a class is created. If a class does not have a constructor defined, a default constructor is called that will initialize fields and properties to default values."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Constructor",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A single class can define multiple constructors with different input parameters, giving flexibility in how an object can be created."
                        },
                    ]
                },
            ];
            KeyPointsEntries =
            [
                new TagEntry
                {
                    TagType = "Constructor",
                    Segments =
                    [
                        new Segment
                        {
                            Text = ""
                        },
                    ]
                },
            ];
            ExploreEntries =
            [
                new TagEntry
                {
                    TagType = "Constructor",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "The default constructor is only available if no constructors are defined in the class. Writing your own constructor will cause you to lose access to the default constructor provided by C#."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Constructor",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Use the "
                        },
                        new Segment
                        {
                            Text = "required",
                            IsCode = true,
                            IsKeyword = true,
                            IsBold = true,
                            HighlightColor = "tag-keyword-blue",
                        },
                        new Segment
                        {
                            Text = " keyword on properties of a class to force callers of that class constructor to provide values for them."
                        },
                    ]
                },
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
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Delimiter",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Delimiters are characters used to separate, enclose, or structure code elements."
                        },
                    ]
                },
            ];
            ExploreEntries =
            [
                new TagEntry
                {
                    TagType = "Delimiter",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Visual Studio can automatically fix indentation, braces, and spacing around delimiters using the shortcut "
                        },
                        new Segment
                        {
                            Text = "Ctrl + K, Ctrl + D",
                            IsCode = true,
                        },
                        new Segment
                        {
                            Text = "."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Delimiter",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "You can also enforce certains rules around delimiters using a "
                        },
                        new Segment
                        {
                            Text = ".editorconfig",
                            IsCode = true,
                        },
                        new Segment
                        {
                            Text = " file."
                        },
                    ]
                },
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
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Field",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A "
                        },
                        new Segment
                        {
                            Text = "field",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " is a variable of any type that is declared directly in a class or struct."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Field",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "There are "
                        },
                        new Segment
                        {
                            Text = "instance",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " fields that are specific to an instance of the type they're defined in. And there are "
                        },
                        new Segment
                        {
                            Text = "static",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " fields which belong to the type itself and are shared among all instances of that type."
                        },
                    ]
                },
            ];
            //Insights =
            //[
            //    new TagEntry
            //    {
            //        TagType = "Field",
            //        Segments =
            //        [
            //            new Segment
            //            {
            //                Text = ""
            //            },
            //        ]
            //    },
            //];
            ExploreEntries =
            [
                new TagEntry
                {
                    TagType = "Field",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Generally, you should declare "
                        },
                        new Segment
                        {
                            Text = "private",
                            IsCode = true,
                            IsKeyword = true,
                            IsBold = true,
                            HighlightColor = "tag-keyword-blue",
                        },
                        new Segment
                        {
                            Text = " or "
                        },
                        new Segment
                        {
                            Text = "protected",
                            IsCode = true,
                            IsKeyword = true,
                            IsBold = true,
                            HighlightColor = "tag-keyword-blue",
                        },
                        new Segment
                        {
                            Text = " accessibility for fields."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Field",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Data that your type exposes to client code should be provided through methods, properties, and indexers."
                        },
                    ]
                },
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
            //Facts =
            //[
            //    "A generic type argument is the specific data type you supply when using a generic class or method.",
            //    "They replace the generic type parameter inside angle brackets (T, TKey, TValue, etc.) defined in the generic declaration."
            //];
            //Insights =
            //[
            //    "This reduces duplication because one generic class or method can work with many different types.",
            //    "If a method requires a generic type constraint, any type that inherits from that constraint will also work.",
            //];
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Generic Type Argument",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A generic type argument is the specific data type you supply when using a generic type or method."
                        },
                        new Segment
                        {
                            Text = "generic type argument"
                        },
                        new Segment
                        {
                            Text = " is the specific data type that must be supplied when using a generic class or method."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Generic Type Argument",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A generic type argument replaces the  "
                        },
                        new Segment
                        {
                            Text = "generic type parameter",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " inside the angle brackets (T, TKey, TValue, etc.) defined in the generic type or method definition."
                        },
                    ]
                },
            ];
            //Insights =
            //[
            //    new TagEntry
            //    {
            //        TagType = "Generic Type Argument",
            //        Segments =
            //        [
            //            new Segment
            //            {
            //                Text = ""
            //            },
            //        ]
            //    },
            //];
            //Tips =
            //[
            //    new TagEntry
            //    {
            //        TagType = "Generic Type Argument",
            //        Segments =
            //        [
            //            new Segment
            //            {
            //                Text = ""
            //            },
            //        ]
            //    },
            //];
            BorderClass = "tag-border-green";
            BgColorClass = "tag-bg-green";
        }
    }

    public class IdentifierTag : TokenTag
    {
        public IdentifierTag()
        {
            Label = $"Identifier";
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Identifier",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "An identifier is the name you assign to a type (class, interface, struct, delegate, or enum), member, variable, or namespace."
                        },
                    ]
                },
            ];
            ExploreEntries =
            [
                new TagEntry
                {
                    TagType = "Identifier",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Hover your cursor over an identifier in Visual Studio to show additional details like the type and the namepsace where it's defined."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Identifier",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "You can add your own details to this information using ",
                        },
                        new Segment
                        {
                            Text = "XML documentation comments",
                            Ref = new SegmentRef
                            {
                                Url = "https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/"
                            }
                        },
                        new Segment
                        {
                            Text = ".",
                        },
                    ]
                }
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
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Method",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A method is a code block that contains a series of statements."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Method",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Method declarations consist of an access modifier, one or more optional modifiers, a return type, a method name, and any method parameters. These parts together make up the "
                        },
                        new Segment
                        {
                            Text = "method signature",
                            IsItalic = true,
                        },
                        new Segment
                        {
                            Text = "."
                        },
                    ]
                },
            ];
            BorderClass = "tag-border-yellow";
            BgColorClass = "tag-bg-yellow";
        }
    }

    public class ModifierTag : TokenTag
    {
        public ModifierTag(string text)
        {
            Label = $"Modifier - {text}";
            //DefinitionSegments = GetModifierDefinitionSegments(text);
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Modifier",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Modifiers are keywords added to types and members that change how they behave."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Modifier",
                    Segments = GetModifierDefinitionSegments(text),
                },
            ];
            KeyPointsEntries =
            [
                new TagEntry
                {
                    TagType = "Modifier",
                    Segments = GetModifierHowToReadSegments(text),
                },
            ];
            UseForEntries = GetModifierHowToUseEntries(text);
            BorderClass = "tag-border-blue";
            BgColorClass = "tag-bg-blue";

            static void GetModifierEntrySegments(string text)
            {

            }

            static List<Segment> GetModifierDefinitionSegments(string text)
            {
                switch (text)
                {
                    case "abstract":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "abstract",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier means the class and class members are incomplete and must be implemented in a derived class.",
                                },
                            ];
                    case "async":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "async",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier is used in the ",
                                },
                                new Segment
                                {
                                    Text = "TAP model",
                                    Ref = new SegmentRef
                                    {
                                        Url = "https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/task-asynchronous-programming-model"
                                    }
                                },
                                new Segment
                                {
                                    Text = " which enables code that reads like a sequence of statements, but executes in a more complicated order to avoid performance bottlenecks and enhance overall responsiveness.",
                                },
                            ];
                    case "const":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "const",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier can be applied to fields whose values are set at compile time and can never be changed.",
                                },
                            ];
                    case "override":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "override",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier is used when the method of a derived class is providing a different implementation that will be used instead of the base class implementation.",
                                },
                            ];
                    case "partial":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "partial",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier indicates that other parts of the class, struct, or interface can be defined in the namespace as long as all parts have the .",
                                },
                                new Segment
                                {
                                    Text = "partial",
                                },
                                new Segment
                                {
                                    Text = "modifier. This can be used to split the definition of a class, a struct, an interface, or a member over two or more source files.",
                                },
                            ];
                    case "readonly":
                        return
                            [
                                //new Segment
                                //{
                                //    Text = "The ",
                                //},
                                //new Segment
                                //{
                                //    Text = "readonly",
                                //    IsCode = true,
                                //    IsKeyword = true,
                                //    IsBold = true,
                                //    HighlightColor = "tag-keyword-blue",
                                //},
                                //new Segment
                                //{
                                //    Text = " modifier means that a field can only be assigned a value during initialization or in a constructor.",
                                //},
                                new Segment
                                {
                                    Text = "readonly",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " fields can only be assigned a value during initialization or in a constructor.",
                                },
                            ];
                    case "required":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "required",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier means that a field must be initialized by the constructor, or by an object initializers when an object is created.",
                                },
                            ];
                    case "sealed":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "sealed",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier prevents the inheritance of a class or certain class members that were previously marked ",
                                },
                                new Segment
                                {
                                    Text = "virtual",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = ".",
                                },
                            ];
                    case "static":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "static",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier means the element is accessible from anywhere within the same assembly.",
                                },
                            ];
                    case "virtual":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "virtual",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier is applied to methods of a base class to indicate that it can be overridden in a derived class.",
                                },
                            ];
                    case "volatile":
                        return
                            [
                                new Segment
                                {
                                    Text = "The ",
                                },
                                new Segment
                                {
                                    Text = "volatile",
                                    IsCode = true,
                                    IsKeyword = true,
                                    IsBold = true,
                                    HighlightColor = "tag-keyword-blue",
                                },
                                new Segment
                                {
                                    Text = " modifier indicates that a field might be modified by multiple threads that are executing at the same time.",
                                },
                            ];
                    default:
                        return [];
                }
            }

            static List<Segment> GetModifierHowToReadSegments(string text)
            {
                switch (text)
                {
                    case "abstract":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    case "async":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    case "const":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    case "override":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    case "partial":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    case "readonly":
                        return
                            [
                                new Segment
                                {
                                    Text = "this element's value will never change",
                                },
                            ];
                    case "required":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    case "sealed":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    case "static":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    case "virtual":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    case "volatile":
                        return
                            [
                                new Segment
                                {
                                    Text = "",
                                },
                            ];
                    default:
                        return [];
                }
            }

            static List<TagEntry> GetModifierHowToUseEntries(string text)
            {
                return text switch
                {
                    "abstract" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    "async" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    "const" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    "override" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    "partial" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    "readonly" =>
                        [
                            //new TagEntry
                            //{
                            //    TagType = "Modifier",
                            //    Segments =
                            //    [
                            //        new Segment
                            //        {
                            //            Text = "Modifiers help make your intent obvious to other developers."
                            //        },
                            //    ]
                            //},
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "dependencies using the ",
                                    },
                                    new Segment
                                    {
                                        Text = "Dependancy Injection",
                                        Ref = new SegmentRef
                                        {
                                            Url = "https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection"
                                        }
                                    },
                                    new Segment
                                    {
                                        Text = " (DI) design pattern",
                                    },
                                ]
                            },
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "hardcoded file paths",
                                    },
                                ]
                            }
                        ],
                    "required" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    "sealed" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    "static" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    "virtual" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    "volatile" =>
                        [
                            new TagEntry
                            {
                                TagType = "Modifier",
                                Segments =
                                [
                                    new Segment
                                    {
                                        Text = "",
                                    },
                                ]
                            },
                        ],
                    _ => [],
                };
            }
        }
    }

    public class NumericLiteralTag : TokenTag
    {
        public NumericLiteralTag()
        {
            Label = $"Numeric Literal";
            //Facts =
            //[
            //    "Numeric literal are values written directly in the code instead of storing it in a variable.",
            //];
            //Insights =
            //[
            //    "They can be integers (e.g., 42), floating-point numbers (e.g., 3.14), or use suffixes to specify types (e.g., 42L for long, 3.14f for float, 2.71m for decimal).",
            //    "Numeric literals can also be written in different formats like hexadecimal (0x1A) or binary (0b1010)."
            //];
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Numeric Literal",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A numeric literal represents a fixed numeric value and are written directly in the code instead of being stord in a variable."
                        },
                    ]
                },
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
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Operator",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Operators are symbols or keywords that performs a specific operation on one or more operands."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Operator",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A user-defined type can "
                        },
                        new Segment
                        {
                            Text = "overload",
                            IsBold = true
                        },
                        new Segment
                        {
                            Text = " a predefined C# operator by providing a custom implementation of an operation in case one or both of the operands are of that type."
                        },
                    ]
                },
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
            //Facts =
            //[
            //    "Parameters are variables declared in a method or constructor definition that will receive input values when the method or constructor is called.",
            //    "Parameters are defined inside the parentheses of the method or constructor signature and require a name and a type.",
            //];
            //Insights =
            //[
            //    "Parameters can also have modifiers like \"ref\" or \"out\" to modify their behavior.",
            //    "Parameters only refer to values in the method/constructor where they are defined. The values passed in for parameters are called arguments."
            //];
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Parameter",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A "
                        },
                        new Segment
                        {
                            Text = "parameter",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " is made up of both a type and an identifier. They define what kind of arguments need to be passed when the method is called."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Parameter",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "By default, arguments in C# are passed to functions by "
                        },
                        new Segment
                        {
                            Text = "value",
                            IsItalic = true,
                        },
                        new Segment
                        {
                            Text = "."
                        },
                    ]
                },
            ];
            KeyPointsEntries =
            [
                new TagEntry
                {
                    TagType = "Parameter",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "The parameter modifier "
                        },
                        new Segment
                        {
                            Text = "ref",
                            IsCode = true,
                            IsKeyword = true,
                            IsBold = true,
                            HighlightColor = "tag-keyword-blue",
                        },
                        new Segment
                        {
                            Text = " enables you to pass arguments by "
                        },
                        new Segment
                        {
                            Text = "reference",
                            IsItalic = true,
                        },
                        new Segment
                        {
                            Text = ".",
                        },
                    ]
                },
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
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Predefined Type",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Predefined types",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " are built-in data types provided by the C# language."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Predefined Type",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "These come as both ",
                        },
                        new Segment
                        {
                            Text = "value",
                            IsItalic = true,
                        },
                        new Segment
                        {
                            Text = " and "
                        },
                        new Segment
                        {
                            Text = "reference",
                            IsItalic = true,
                        },
                        new Segment
                        {
                            Text = " types."
                        },
                    ]
                },
            ];
            KeyPointsEntries =
            [
                new TagEntry
                {
                    TagType = "Predefined Type",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "All types in C# (predefined & reference types) have ",
                        },
                        new Segment
                        {
                            Text = "built-in default values",
                            Ref = new SegmentRef
                            {
                                Url = "https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values",
                            }
                        },
                        new Segment
                        {
                            Text = "."
                        },
                    ]
                },
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
            //Facts =
            //[
            //    "Properties are class members that provide a flexible mechanism to access and modify the property's value.",
            //    "Auto-properties (public int Age { get; set; }) let C# automatically create the hidden field that stores the value."
            //];
            //Insights =
            //[
            //    "Properties can help protect data by adding rules or checks before a value is returned or updated.",
            //    "A property can have a getter, a setter, or both, and you can control access to each one independently."
            //];
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Property",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A "
                        },
                        new Segment
                        {
                            Text = "property",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " is a member that provides a flexible mechanism to read, write, or compute the value of a data field."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Property",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "A property definition contains declarations for a "
                        },
                        new Segment
                        {
                            Text = "get",
                            IsCode = true,
                            IsKeyword = true,
                            IsBold = true,
                            HighlightColor = "tag-keyword-blue",
                        },
                        new Segment
                        {
                            Text = " and ",
                        },
                        new Segment
                        {
                            Text = "set",
                            IsCode = true,
                            IsKeyword = true,
                            IsBold = true,
                            HighlightColor = "tag-keyword-blue",
                        },
                        new Segment
                        {
                            Text = " accessor that retrieves and assigns the value of that property."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "Property",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "You can initialize a property to a value other than the default by setting a value after the closing brace for the property."
                        },
                        new Segment
                        {
                            Text = "property",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " is a member that provides a flexible mechanism to read, write, or compute the value of a data field."
                        },
                    ]
                },
            ];
            KeyPointsEntries =
            [
                new TagEntry
                {
                    TagType = "Property",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Properties can have a getter, a setter, or both, and you can control the level of access to each independently."
                        },
                    ]
                },
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
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "Punctuation",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Punctuation refers to the special symbols used to structure code and tell the compiler how to read it."
                        },
                    ]
                },
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
            TheBasicsEntries =
            [
                new TagEntry
                {
                    TagType = "String Literal",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Quoted string literals",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " start and end with a single double quote character (\") on the same line and are best for strings that fit on a single line and don't include any escape sequences."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "String Literal",
                    IsExample = true,
                    Segments =
                    [
                        new Segment
                        {
                            Text = "string message = \"Hello, world!\";",
                            IsCode = true,
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "String Literal",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Verbatim string literals",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " are prefixed with the @ symbol and are convenient for multi-line strings, strings that contain backslash characters, or strings that contain embedded double quotes."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "String Literal",
                    IsExample = true,
                    Segments =
                    [
                        new Segment
                        {
                            Text = "string path = @\"C:\\Projects\\Demo\";",
                            IsCode = true,
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "String Literal",
                    Segments =
                    [
                        new Segment
                        {
                            Text = "Interpolated strings",
                            IsBold = true,
                        },
                        new Segment
                        {
                            Text = " are prefixed with the "
                        },
                        new Segment
                        {
                            Text = "$",
                            IsCode = true,
                        },
                        new Segment
                        {
                            Text = " character and allow you to insert dynamic values inside braces."
                        },
                    ]
                },
                new TagEntry
                {
                    TagType = "String Literal",
                    IsExample = true,
                    Segments =
                    [
                        new Segment
                        {
                            Text = "string text = $\"You have {count} messages.\";",
                            IsCode = true,
                        },
                    ]
                },
            ];
            BorderClass = "tag-border-orange";
            BgColorClass = "tag-bg-orange";
        }
    }
}
