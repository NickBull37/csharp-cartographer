namespace csharp_cartographer_backend._05.Services.AiAnalysis
{
    public interface IAiAnalysisService
    {
        Task<string> GetAnalysisResult(string code, CancellationToken cancellationToken);
    }
}
