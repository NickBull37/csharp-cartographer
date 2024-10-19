namespace csharp_cartographer._03.Models.Tokens
{
    public class TokenTag
    {
        public string Label { get; set; } = string.Empty;

        public int Level { get; set; }

        public List<string>? Facts { get; set; }

        public List<string>? Insights { get; set; }
    }
}
