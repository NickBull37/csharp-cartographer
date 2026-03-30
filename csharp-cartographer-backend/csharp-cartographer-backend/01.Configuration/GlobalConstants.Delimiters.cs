namespace csharp_cartographer_backend._01.Configuration
{
    /// **************************************************
    /// |                   DELIMITERS                   |
    /// **************************************************
    public partial class GlobalConstants
    {
        public static readonly HashSet<string> Delimiters = new(StringComparer.Ordinal)
        {
            "(",
            ")",
            "[",
            "]",
            "{",
            "}",
            "<",
            ">"
        };
    }
}
