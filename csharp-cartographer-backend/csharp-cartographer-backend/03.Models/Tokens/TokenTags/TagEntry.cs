namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class TagEntry
    {
        public Guid ID { get; set; }

        public required string TagType { get; init; }

        public required List<Segment> Segments { get; init; }
    }
}
