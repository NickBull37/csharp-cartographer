using csharp_cartographer_backend._05.Services.AiAnalysis.Models;

namespace csharp_cartographer_backend._07.Clients.ChatGpt
{
    public interface IChatGptClient
    {
        Task<CodeAnalysisResult> GetCodeAnalysis(string code, CancellationToken cancellationToken);
    }
}
