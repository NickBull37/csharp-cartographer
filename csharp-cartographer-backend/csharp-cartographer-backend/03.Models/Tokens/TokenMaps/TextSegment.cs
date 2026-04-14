namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    public sealed class TextSegment
    {
        public required string Text { get; init; }

        public IReadOnlyList<string> Classes { get; init; } = [];

        public string? ToolTip { get; set; }

        public string? Link { get; init; }

        public static TextSegment Undefined()
        {
            return new TextSegment { Text = "Could not find definition." };
        }
    }
}
