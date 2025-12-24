using csharp_cartographer_backend._01.Configuration.Enums;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class TagEntry
    {
        public Guid ID { get; set; }

        public required string TagType { get; init; }

        public TokenTagSection Section { get; set; }

        public bool IsExample { get; set; }

        public bool IsInsight { get; set; }

        public required List<Segment> Segments { get; init; }
    }
}
