namespace csharp_cartographer_backend._03.Models.Insights
{
    public sealed class Insight
    {
        public Guid ID { get; } = Guid.NewGuid();

        public DateTime CreatedDate { get; } = DateTime.Now;

        public Guid ArtifactID { get; }

        public string Text { get; }

        public IEnumerable<int> Highlights { get; }

        public Insight(Guid artifactId, string text, IEnumerable<int> highlights)
        {
            ArtifactID = artifactId;
            Text = text;
            Highlights = highlights;
        }
    }
}
