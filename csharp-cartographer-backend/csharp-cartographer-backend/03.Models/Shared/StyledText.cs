namespace csharp_cartographer_backend._03.Models.Shared
{
    public sealed class StyledText
    {
        public IReadOnlyList<TextSegment> Segments { get; init; } = [];

        public StyledText(IReadOnlyList<TextSegment> segments)
        {
            Segments = segments;
        }

        public static StyledText NotFound()
        {
            return new([TextSegment.NotFound()]);
        }
    }

    public sealed class TextSegment
    {
        public required string Text { get; init; }

        public IReadOnlyList<string> Classes { get; init; } = [];

        public string? ToolTip { get; init; }

        public string? Link { get; init; }

        public static TextSegment LineBreak()
        {
            return new()
            {
                Text = "\r\n\r\n",
                Classes = ["line-break"]
            };
        }

        public static TextSegment NotFound()
        {
            return new()
            {
                Text = "Could not find definition.",
                Classes = []
            };
        }
    }
}
