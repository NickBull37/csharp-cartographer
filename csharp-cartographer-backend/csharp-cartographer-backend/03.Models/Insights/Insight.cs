using csharp_cartographer_backend._02.Utilities.Providers;
using csharp_cartographer_backend._08.Controllers.Insights.Dtos;

namespace csharp_cartographer_backend._03.Models.Insights
{
    /// <summary>
    /// A definition for the Insight domain model. Insights
    /// are displayed in the Artifact Legend side bar.
    /// </summary>
    public sealed class Insight
    {
        public Guid ID { get; } = Guid.NewGuid();
        public DateTime CreatedDate { get; } = DateTime.Now;
        public Guid ArtifactID { get; }
        public string Label { get; }
        public string Description { get; set; }
        public IEnumerable<int> Highlights { get; set; }
        public IEnumerable<Note> Notes { get; set; }

        /// <summary>
        /// A constructor for creating artifact insights
        /// with data pulled from embedded json files.
        /// </summary>
        public Insight(EmbeddedInsight embeddedInsight)
        {
            List<Note> notes = [];
            foreach (var note in embeddedInsight.Notes)
            {
                notes.Add(new Note(ID, note));
            }

            ArtifactID = embeddedInsight.ArtifactID;
            Label = embeddedInsight.Label;
            Description = embeddedInsight.Description;
            Highlights = embeddedInsight.Highlights;
            Notes = notes;
        }

        /// <summary>
        /// A constructor for creating artifact insights
        /// with data from incoming http requests.
        /// </summary>
        public Insight(CreateInsightDto insightDto)
        {
            List<Note> notes = [];
            foreach (var noteDto in insightDto.NoteDtos)
            {
                notes.Add(new Note(ID, noteDto));
            }

            ArtifactID = insightDto.ArtifactID;
            Label = insightDto.Label;
            Description = insightDto.Description;
            Highlights = insightDto.Highlights;
            Notes = notes;
        }
    }
}
