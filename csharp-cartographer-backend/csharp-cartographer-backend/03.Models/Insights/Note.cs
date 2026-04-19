using csharp_cartographer_backend._08.Controllers.Insights.Dtos;

namespace csharp_cartographer_backend._03.Models.Insights
{
    public sealed class Note
    {
        public Guid ID { get; } = Guid.NewGuid();
        public DateTime CreatedDate { get; } = DateTime.Now;
        public Guid InsightID { get; }
        public string Label { get; }
        public string Text { get; }
        public IEnumerable<int> Highlights { get; }

        public Note(Guid insightID, CreateNoteDto dto)
        {
            InsightID = insightID;
            Label = dto.Label;
            Text = dto.Text;
            Highlights = dto.Highlights;
        }
    }
}
