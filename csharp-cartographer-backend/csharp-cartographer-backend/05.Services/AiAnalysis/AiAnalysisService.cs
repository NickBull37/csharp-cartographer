using csharp_cartographer_backend._05.Services.AiAnalysis.Models;
using csharp_cartographer_backend._07.Clients.ChatGpt;

namespace csharp_cartographer_backend._05.Services.AiAnalysis
{
    public class AiAnalysisService : IAiAnalysisService
    {
        private readonly IChatGptClient _chatGptClient;

        public AiAnalysisService(IChatGptClient chatGptClient)
        {
            _chatGptClient = chatGptClient;
        }

        public async Task<string> GetAnalysisResult(string code, CancellationToken cancellationToken)
        {
            CodeAnalysisResult clientResponse = await _chatGptClient.GetCodeAnalysis(code, cancellationToken);
            return clientResponse.Analysis;
        }
    }
}
