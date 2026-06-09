using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._01.Configuration.Enums
{
    /// <summary>
    /// Describes the general syntax category the token falls under.
    /// </summary>
    public enum PrimaryKind
    {
        Unknown,
        Delimiter,
        Identifier,
        Keyword,
        Literal,
        Operator,
        Punctuation,

        [Label("Keyword / Literal")]
        KeywordLiteral,
        [Label("Keyword / Operator")]
        KeywordOperator,
        [Label("Identifier / Keyword")]
        IdentifierKeyword
    }
}
