namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    public class TextSegment
    {
        public Guid ID { get; init; } = Guid.NewGuid();

        public required string Text { get; init; }

        public IReadOnlyList<string> Classes { get; init; } = [];
    }

    public sealed class SegmentLink
    {
        public Guid ID { get; set; }

        public string? Url { get; init; }
    }
}
