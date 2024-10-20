using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._03.Models.Artifacts
{
    public class Artifact
    {
        public int ID { get; set; }

        public string Language { get; } = "C#";

        public string ArtifactType { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<NavToken> NavTokens { get; set; } = [];

        public DateTime CreatedDate { get; init; }
    }
}
