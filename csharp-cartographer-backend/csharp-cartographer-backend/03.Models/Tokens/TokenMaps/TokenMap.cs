namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    public enum TokenType
    {
        Keyword,
        Delimiter,
        Operator,
        Punctuator,
        Identifier,
        Literal
    }

    public enum IdentifierType
    {
        Attribute,
        Class,
        Struct,
        Interface,
        Record,
        RecordStruct,
        Enum,
        Method,
        LocalVariable,
        Parameter,
        Property,
        Field,
        Namespace
    }

    public enum EntityType
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
        RecordStruct
    }

    public class TokenMap
    {
        public TokenType TokenType { get; set; }

        public IdentifierType? IdentifierDataType { get; set; }






        public string Label { get; set; }

        public bool HasExternalDefinition { get; init; }

        public bool HasInternalDefinition { get; init; }

        public EntityType EntityType { get; set; }

        public bool IsKeyword { get; set; }

        public bool IsDataType { get; set; }

        public bool IsPreDefinedType { get; set; }

        public bool IsGeneric { get; set; }

        public bool IsConstant { get; set; }

        public bool IsNullable { get; set; }

        public bool IsIdentifier { get; set; }

        public bool IsDeclaration { get; set; }

        public bool IsReference { get; set; }

        public bool IsInvocation { get; set; }

        public bool IsLiteral { get; set; }

    }
}
