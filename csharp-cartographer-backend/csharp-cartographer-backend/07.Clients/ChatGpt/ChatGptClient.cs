using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._05.Services.AiAnalysis.Models;
using csharp_cartographer_backend._07.Clients.ChatGpt.Dtos;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace csharp_cartographer_backend._07.Clients.ChatGpt
{
    public class ChatGptClient : IChatGptClient
    {
        private readonly CartographerConfig _config;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ChatGptClient> _logger;

        public ChatGptClient(
            IOptions<CartographerConfig> config,
            HttpClient httpClient,
            ILogger<ChatGptClient> logger)
        {
            _config = config.Value;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<CodeAnalysisResult> GetCodeAnalysis(string code, CancellationToken cancellationToken)
        {
            try
            {
                var dto = new CreateChatCompletionDto(_config.ChatGptPrompt, code);
                var requestContent = JsonContent.Create(dto);

                var httpResponse = await _httpClient.PostAsync(
                    _config.ChatGptUrl,
                    requestContent,
                    cancellationToken);

                httpResponse.EnsureSuccessStatusCode();

                var chatCompletion = await httpResponse.Content
                    .ReadFromJsonAsync<ChatCompletionResponse>(cancellationToken);

                var analysis = chatCompletion?.Choices.FirstOrDefault()?.Message.Content;
                if (analysis is null)
                {
                    _logger.LogError("An error occurred while attempting to retrieve AI analysis. No analysis response was found.");
                    return CodeAnalysisResult.Fail();
                }

                return CodeAnalysisResult.Ok(analysis);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "An HttpRequest exception occurred while attempting to retrieve AI analysis.");
                return CodeAnalysisResult.Fail();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "An exception occurred while attempting to deserialize AI analysis response.");
                return CodeAnalysisResult.Fail();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred while attempting to retrieve AI analysis.");
                return CodeAnalysisResult.Fail();
            }
        }
    }
}
