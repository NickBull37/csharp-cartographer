using csharp_cartographer_backend._02.Utilities.Providers;
using csharp_cartographer_backend._08.Controllers.Insights.Dtos;

namespace csharp_cartographer_backend._03.Models.Insights
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Note
    {
        public Guid ID { get; } = Guid.NewGuid();
        public DateTime CreatedDate { get; } = DateTime.Now;
        public Guid InsightID { get; }
        public string Label { get; }
        public string Text { get; }
        public IEnumerable<int> Highlights { get; }

        /// <summary>
        /// A constructor for creating insight notes
        /// with data pulled from embedded json files.
        /// </summary>
        public Note(Guid insightID, EmbeddedNote embeddedNote)
        {
            InsightID = insightID;
            Label = embeddedNote.Label;
            Text = embeddedNote.Text;
            Highlights = embeddedNote.Highlights;
        }

        /// <summary>
        /// A constructor for creating insight notes
        /// with data from incoming http requests.
        /// </summary>
        public Note(Guid insightID, CreateNoteDto dto)
        {
            InsightID = insightID;
            Label = dto.Label;
            Text = dto.Text;
            Highlights = dto.Highlights;
        }
    }
}
