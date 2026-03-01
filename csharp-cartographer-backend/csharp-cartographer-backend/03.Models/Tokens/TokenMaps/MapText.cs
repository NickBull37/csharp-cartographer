namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    public class MapText
    {
        public Guid ID { get; init; } = Guid.NewGuid();

        public List<TextSegment> Segments { get; set; } = [];
    }
}
