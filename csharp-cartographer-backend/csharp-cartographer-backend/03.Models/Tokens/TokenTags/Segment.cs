namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class Segment
    {
        public Guid ID { get; set; }

        public required string Text { get; init; }

        public bool IsKeyword { get; init; }

        public bool IsBold { get; init; }

        public bool IsItalic { get; init; }

        public bool IsCode { get; init; }

        public string? HighlightColor { get; init; }

        public SegmentRef? Ref { get; init; }
    }

    public sealed class SegmentRef
    {
        public Guid ID { get; set; }

        public int? TokenIndex { get; init; }

        public string? Url { get; init; }
    }
}
