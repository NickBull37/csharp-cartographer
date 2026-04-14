namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    public class MapText
    {
        public List<TextSegment> Segments { get; set; } = [];

        public static MapText Undefined()
        {
            return new MapText
            {
                Segments = [TextSegment.Undefined()]
            };
        }
    }
}
