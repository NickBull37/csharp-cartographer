namespace csharp_cartographer_backend._03.Models.Tokens
{
    public enum ObjectEntity
    {
        Class,
        Keyword,
        Delimiter,
        Operator,
        Punctuator,
        Interface,
        Record,
        Struct,
        LocalVariable,
        Parameter,
        Field,
        Method,
        Property,
        Attribute,
        Enum,
    }

    public struct TokenClassification
    {
        public ObjectEntity EntityType { get; set; }

        public bool IsDataType { get; set; }

        public bool IsPreDefinedType { get; set; }

        public bool IsGeneric { get; set; }

        public bool IsNullable { get; set; }

        public bool IsDeclaration { get; set; }

        public bool IsInvocation { get; set; }

        public bool IsLiteral { get; set; }
    }
}
