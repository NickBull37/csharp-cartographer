using Microsoft.CodeAnalysis;
using System.Text.Json.Serialization;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class TokenChart
    {
        public string Label { get; set; } = string.Empty;

        public string? Alias { get; set; }

        public List<string> Facts { get; set; } = [];

        public List<string> Insights { get; set; } = [];

        public HighlightRange HighlightRange { get; set; }

        [JsonIgnore]
        public List<SyntaxToken> Tokens { get; set; } = [];
    }

    public class HighlightRange
    {
        public int StartIndex { get; set; }

        public int EndIndex { get; set; }
    }
}
