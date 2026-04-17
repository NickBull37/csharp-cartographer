using csharp_cartographer_backend._08.Controllers.Insights.Dtos;

namespace csharp_cartographer_backend._03.Models.Insights
{
    public sealed class Note
    {
        public Guid ID { get; } = Guid.NewGuid();
        public DateTime CreatedDate { get; } = DateTime.Now;
        public Guid InsightID { get; }
        public string Text { get; set; }
        public IEnumerable<int> Highlights { get; set; }

        public Note(Guid insightID, CreateNoteDto dto)
        {
            InsightID = insightID;
            Text = dto.Text;
            Highlights = dto.Highlights;
        }
    }
}
