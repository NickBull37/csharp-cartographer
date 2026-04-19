using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace csharp_cartographer_backend._08.Controllers.Insights.Dtos
{
    public sealed class CreateInsightDto
    {
        [Required]
        [JsonPropertyName("artifactId")]
        public Guid ArtifactID { get; init; }

        [Required]
        [JsonPropertyName("label")]
        public string Label { get; init; } = default!;

        [Required]
        [JsonPropertyName("description")]
        public string Description { get; init; } = default!;

        [Required]
        [JsonPropertyName("highlights")]
        public IEnumerable<int> Highlights { get; init; } = [];

        [JsonPropertyName("noteDtos")]
        public IEnumerable<CreateNoteDto> NoteDtos { get; init; } = [];
    }

    public sealed class CreateNoteDto
    {
        [Required]
        [JsonPropertyName("label")]
        public string Label { get; init; } = default!;

        [Required]
        [JsonPropertyName("text")]
        public string Text { get; init; } = default!;

        [Required]
        [JsonPropertyName("highlights")]
        public IEnumerable<int> Highlights { get; init; } = [];
    }
}
