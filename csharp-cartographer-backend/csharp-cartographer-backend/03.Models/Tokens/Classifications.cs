namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class Classifications
    {
        public IEnumerable<string> OriginalSet { get; set; } = [];
        public string Original { get; set; } = string.Empty;
        public string ColorAs { get; set; } = string.Empty;
        public string Corrected { get; set; } = string.Empty;

        public Classifications(
            IEnumerable<string> originalSet,
            string original,
            string? colorAs,
            string? corrected)
        {
            OriginalSet = originalSet;
            Original = original;
            ColorAs = colorAs ?? original;
            Corrected = corrected ?? original;
        }
    }
}
