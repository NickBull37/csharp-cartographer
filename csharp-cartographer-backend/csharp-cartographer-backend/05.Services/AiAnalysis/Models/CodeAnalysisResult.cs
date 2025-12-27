namespace csharp_cartographer_backend._05.Services.AiAnalysis.Models
{
    public record CodeAnalysisResult
    {
        public bool IsSuccess { get; init; }

        public string Analysis { get; init; } = string.Empty;

        public string? ErrorMessage { get; init; }

        private CodeAnalysisResult() { }

        public static CodeAnalysisResult Ok(string analysis) =>
            new()
            {
                IsSuccess = true,
                Analysis = analysis
            };

        public static CodeAnalysisResult Fail(string errorMessage) =>
            new()
            {
                IsSuccess = false,
                Analysis = "An analysis could not be provided at this time.",
                ErrorMessage = errorMessage
            };

        public static CodeAnalysisResult Canceled() =>
            new()
            {
                IsSuccess = false,
                Analysis = string.Empty,
                ErrorMessage = "The operation was canceled."
            };
    }
}
