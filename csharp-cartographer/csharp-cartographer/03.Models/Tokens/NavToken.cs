using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Text.Json.Serialization;

namespace csharp_cartographer._03.Models.Tokens
{
    public class NavToken
    {
        public int ID { get; set; }

        public int Index { get; set; }

        public string Text { get; init; } = string.Empty;

        //public string Label { get; set; } = string.Empty;

        public string RoslynKind { get; set; } = string.Empty;

        public string? HighlightColor { get; set; } = null;

        public List<string> LeadingTrivia { get; set; } = [];

        public List<string> TrailingTrivia { get; set; } = [];

        public List<TokenTag> Tags { get; set; } = [];

        [JsonIgnore]
        public SyntaxToken RoslynToken { get; set; }

        public NavToken(SyntaxToken roslynToken, int index)
        {
            Index = index;
            Text = roslynToken.Text;
            RoslynKind = roslynToken.Kind().ToString();
            //Label = roslynToken.Kind().ToString();
            RoslynToken = roslynToken;
            if (roslynToken.HasLeadingTrivia)
            {
                foreach (var trivia in roslynToken.LeadingTrivia)
                {
                    LeadingTrivia.Add(trivia.ToString());
                }
            }
            if (roslynToken.HasTrailingTrivia)
            {
                foreach (var trivia in roslynToken.TrailingTrivia)
                {
                    TrailingTrivia.Add(trivia.ToString());
                }
            }
        }
    }
}
