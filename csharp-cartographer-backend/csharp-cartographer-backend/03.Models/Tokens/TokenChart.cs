namespace csharp_cartographer_backend._03.Models.Tokens
{
    public sealed record TokenChart
    {
        public string Label { get; }

        public HighlightRange? HighlightRange { get; }

        public List<string> Facts { get; } = [];

        public List<string> Insights { get; } = [];

        public TokenChart(string label, HighlightRange? highlightRange)
        {
            Label = label;
            HighlightRange = highlightRange;
        }
    }

    public readonly struct HighlightRange(int startIndex, int endIndex)
    {
        public int StartIndex { get; } = startIndex;

        public int EndIndex { get; } = endIndex;
    }
}
