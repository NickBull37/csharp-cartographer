namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class Classifications
    {
        public IEnumerable<string> OriginalSet { get; set; } = [];
        public string Original { get; set; } = string.Empty;
        public string ColorAs { get; set; } = string.Empty;
        public string Corrected { get; set; } = string.Empty;
        public string Final => Corrected;

        public Classifications(
            IEnumerable<string> originalSet,
            string roslyn,
            string? colorAs,
            string? corrected)
        {
            OriginalSet = originalSet;
            Original = roslyn;
            ColorAs = colorAs ?? roslyn;
            Corrected = corrected ?? roslyn;
        }
    }
}
