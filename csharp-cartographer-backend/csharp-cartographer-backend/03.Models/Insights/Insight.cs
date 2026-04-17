using csharp_cartographer_backend._08.Controllers.Insights.Dtos;

namespace csharp_cartographer_backend._03.Models.Insights
{
    public sealed class Insight
    {
        public Guid ID { get; } = Guid.NewGuid();
        public DateTime CreatedDate { get; } = DateTime.Now;
        public Guid ArtifactID { get; }
        public string Description { get; set; }
        public IEnumerable<int> Highlights { get; set; }
        public IEnumerable<Note> Notes { get; set; }

        public Insight(CreateInsightDto insightDto)
        {
            List<Note> notes = [];
            foreach (var noteDto in insightDto.NoteDtos)
            {
                notes.Add(new Note(ID, noteDto));
            }

            ArtifactID = insightDto.ArtifactID;
            Description = insightDto.Description;
            Highlights = insightDto.Highlights;
            Notes = notes;
        }
    }
}
