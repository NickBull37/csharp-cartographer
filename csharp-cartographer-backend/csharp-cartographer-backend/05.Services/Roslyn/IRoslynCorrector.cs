using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Roslyn
{
    public interface IRoslynCorrector
    {
        /// <summary>
        /// Roslyn's classification value is primarily for syntax highlighting. But the Cartographer
        /// relies on it for other things as well. So a handful of edge cases are corrected before
        /// moving onto individual token mapping.
        /// </summary>
        string? GetCorrectedClassification(
            NavToken token,
            string? roslynClassification);
    }
}
