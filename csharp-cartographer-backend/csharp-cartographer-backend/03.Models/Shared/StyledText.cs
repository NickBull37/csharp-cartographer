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

        public static TextSegment HoverExtension()
        {
            return new()
            {
                Text = "<break/>Hover your cusor over the {c:color-yellow bold}method{/c} name to see addition details like what the method will return or what types the provided arguments need to be.",
                Classes = []
            };
        }

        public static TextSegment JumpExtension()
        {
            return new()
            {
                Text = "<break/>Put your cursor inside the identifier name in your IDE and hit {c:keyword}F12{/c} to jump to the identifier's definition.",
                Classes = []
            };
        }

        public static TextSegment ReferenceExtension()
        {
            return new()
            {
                Text = "<break/>Look for a {c:underline}references{/c} link above the declaration in your IDE to see everywhere it's currently being used.",
                Classes = []
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
