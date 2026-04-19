namespace csharp_cartographer_backend._05.Services.AiAnalysis.Models
{
    public record CodeAnalysisResult
    {
        public bool IsSuccess { get; init; }

        public string Analysis { get; init; } = default!;

        /// <summary>
        /// Overwrite auto-generated default constructor to make 
        /// private so it can't be used outside this class
        /// </summary>
        private CodeAnalysisResult()
        {
        }

        public static CodeAnalysisResult Ok(string analysis) =>
            new()
            {
                IsSuccess = true,
                Analysis = analysis
            };

        public static CodeAnalysisResult Fail() =>
            new()
            {
                IsSuccess = false,
                Analysis = "An analysis could not be provided at this time.",
            };
    }
}
