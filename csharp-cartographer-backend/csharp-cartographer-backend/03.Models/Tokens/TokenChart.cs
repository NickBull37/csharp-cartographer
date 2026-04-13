namespace csharp_cartographer_backend._03.Models.Tokens
{
    public sealed record TokenChart
    {
        public string Label { get; }

        public HighlightRange? HighlightRange { get; }

        public List<string> Facts { get; set; } = [];

        public List<string> Insights { get; set; } = [];

        public TokenChart(string label, HighlightRange? highlightRange)
        {
            Label = label;
            HighlightRange = highlightRange;
        }
    }

    public sealed record HighlightRange
    {
        public int StartIndex { get; }

        public int EndIndex { get; }

        public HighlightRange(int start, int end)
        {
            StartIndex = start;
            EndIndex = end;
        }
    }
}
