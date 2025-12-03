namespace csharp_cartographer_backend._03.Models.Tokens
{
    public enum TokenClassType
    {
        Delimiter,
        Identifier,
        Keyword,
        NumericLiteral,
        Operator,
        Punctuation,
        StringLiteral,
        Other
    }

    public enum TokenSubClassType
    {
        AccessModifier,
        ClassDeclarationIdentifier,
        PredefinedType,
        GenericCollectionType,
        Modifier,
    }

    public class TokenClass
    {
        public string Label { get; set; }

        public TokenClassType Classification { get; set; }

        public TokenSubClassType? SubClassification { get; set; }

        public string Definition { get; set; }

        public string SubClassDefinition { get; set; }

        public string BorderClass { get; set; }

        public string BgColorClass { get; set; }
    }

    public class KeywordClass : TokenClass
    {
        public KeywordClass(TokenSubClassType? subClassType)
        {
            Label = $"C# Keyword";
            Classification = TokenClassType.Keyword;
            SubClassification = subClassType;
            Definition = "Keywords are reserved words with a special meaning in the C# language syntax and cannot be used as identifiers.";
            Definition = "Define private";
            BorderClass = "tag-border-purple";
            BgColorClass = "tag-bg-purple";
        }
    }
}
