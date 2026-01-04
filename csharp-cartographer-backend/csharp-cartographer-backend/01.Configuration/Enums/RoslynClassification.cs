namespace csharp_cartographer_backend._01.Configuration.Enums
{
    public class RoslynClassification
    {
        /*
         *  A list of all the different classifications Roslyn can assign a token.
         */
        public static readonly List<string> Classifications = new()
        {
            "class name",
            "constant name",
            "delegate name",
            "enum name",
            "enum member name",
            "event name",
            "field name",
            "identifier",
            "interface name",
            "keyword",
            "keyword - control",
            "local name",
            "method name",
            "namespace name",
            "number",
            "operator",
            "parameter name",
            "property name",
            "punctuation",
            "record class name",
            "record struct name",
            "static symbol",
            "string",
            "string - verbatim",
            "struct name",
            "type parameter name",
        };
    }
}
