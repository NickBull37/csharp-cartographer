namespace csharp_cartographer_backend._01.Configuration
{
    /// **************************************************
    /// |               COMMON IDENTIFIERS               |
    /// **************************************************
    public partial class GlobalConstants
    {
        /// ------  Common types to color manually (Roslyn will often have no data)  ------ ///
        public static readonly HashSet<string> CommonIdentifiers =
            new(StringComparer.Ordinal)
            {
                // Enums
                "Accessibility",
                "AttributeTargets",
                "BindingFlags",
                "ConsoleColor",
                "ConsoleKey",
                "ConsoleModifiers",
                "DateTimeKind",
                "DayOfWeek",
                "EnvironmentVariableTarget",
                "FileAccess",
                "FileMode",
                "FileShare",
                "HttpStatusCode",
                "MidpointRounding",
                "PrimaryKind",
                "RegexOptions",
                "SearchOption",
                "SemanticModifiers",
                "SemanticRole",
                "StringComparison",
                "StringSplitOptions",
                "SymbolKind",
                "SyntaxKind",
                "TaskStatus",
                "TypeKind",

                // Structs
                "Boolean",
                "CancellationToken",
                "DateOnly",
                "DateTime",
                "Decimal",
                "Double",
                "Guid",
                "HashCode",
                "Index",
                "Int32",
                "Int64",
                "KeyValuePair",
                "Point",
                "Range",
                "ReadOnlySpan",
                "Rectangle",
                "Span",
                "SyntaxToken",
                "TextSpan",
                "TimeOnly",
                "TimeSpan",
                "ValueTuple",
            };

        /// ------  Common Enums  ------ ///
        public static readonly HashSet<string> CommonEnums =
            new(StringComparer.Ordinal)
            {
                "Accessibility",
                "AttributeTargets",
                "BindingFlags",
                "ConsoleColor",
                "ConsoleKey",
                "ConsoleModifiers",
                "DateTimeKind",
                "DayOfWeek",
                "EnvironmentVariableTarget",
                "FileAccess",
                "FileMode",
                "FileShare",
                "HttpStatusCode",
                "MidpointRounding",
                "PrimaryKind",
                "RegexOptions",
                "SearchOption",
                "SemanticModifiers",
                "SemanticRole",
                "StringComparison",
                "StringSplitOptions",
                "SymbolKind",
                "SyntaxKind",
                "TaskStatus",
                "TypeKind",
            };

        /// ------  Common Structs  ------ ///
        public static readonly HashSet<string> CommonStructs =
            new(StringComparer.Ordinal)
            {
                "Boolean",
                "CancellationToken",
                "DateOnly",
                "DateTime",
                "Decimal",
                "Double",
                "Guid",
                "HashCode",
                "Index",
                "Int32",
                "Int64",
                "KeyValuePair",
                "Point",
                "Range",
                "ReadOnlySpan",
                "Rectangle",
                "Span",
                "SyntaxToken",
                "TextSpan",
                "TimeOnly",
                "TimeSpan",
                "ValueTuple",
            };
    }
}
