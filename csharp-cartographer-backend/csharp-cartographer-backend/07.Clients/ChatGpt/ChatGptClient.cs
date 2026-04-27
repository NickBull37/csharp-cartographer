using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._02.Utilities.Logging;
using csharp_cartographer_backend._05.Services.AiAnalysis.Models;
using csharp_cartographer_backend._07.Clients.ChatGpt.Dtos;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace csharp_cartographer_backend._07.Clients.ChatGpt
{
    public class ChatGptClient : IChatGptClient
    {
        private const string DefaultErrorMsg = "An error occurred while retrieving code analysis. Please try again.";

        private readonly CartographerConfig _config;
        private readonly HttpClient _httpClient;

        public ChatGptClient(IOptions<CartographerConfig> config, HttpClient httpClient)
        {
            _config = config.Value;
            _httpClient = httpClient;
        }

        public async Task<CodeAnalysisResult> GetCodeAnalysis(string code, CancellationToken cancellationToken)
        {
            try
            {
                var dto = new CreateChatCompletionDto(_config.ChatGptPrompt, code);
                var requestJson = JsonSerializer.Serialize(dto);

                using HttpContent requestContent = new StringContent(
                    requestJson,
                    Encoding.UTF8,
                    "application/json");

                var httpResponse = await _httpClient.PostAsync(
                    _config.ChatGptUrl,
                    requestContent,
                    cancellationToken);

                httpResponse.EnsureSuccessStatusCode();

                var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                var response = JsonSerializer.Deserialize<ChatCompletionResponse>(responseContent);

                var analysis = response?.Choices.First().Message.Content ?? DefaultErrorMsg;

                var testanalysis = response.Choices.First().Message.Content ?? DefaultErrorMsg;

                var testtestanalysis = response!.Choices.First().Message.Content ?? DefaultErrorMsg;

                return analysis is not null
                    ? CodeAnalysisResult.Ok(analysis)
                    : CodeAnalysisResult.Fail(); ;
            }
            catch (HttpRequestException ex)
            {
                CartographerLogger.LogException(ex);
                return CodeAnalysisResult.Fail();
            }
            catch (JsonException ex)
            {
                CartographerLogger.LogException(ex);
                return CodeAnalysisResult.Fail();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }
    }
}
