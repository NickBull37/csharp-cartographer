using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._02.Utilities.Logging;
using csharp_cartographer_backend._05.Services.AiAnalysis.Models;
using csharp_cartographer_backend._07.Clients.ChatGpt.Dtos;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace csharp_cartographer_backend._07.Clients.ChatGpt
{
    public class ChatGptClient : IChatGptClient
    {
        private const string DefaultErrorMsg = "An error occurred while retrieving data. Please try again.";

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
                var requestJson = JsonSerializer.Serialize(CreateRequestDto(code));

                HttpContent requestContent = new StringContent(
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

                return CodeAnalysisResult.Ok(ExtractAnalysisFromResponse(response));
            }
            catch (HttpRequestException ex)
            {
                CartographerLogger.LogException(ex);
                return CodeAnalysisResult.Fail("An error occurred while retrieving data. Please try again.");
            }
            catch (JsonException ex)
            {
                CartographerLogger.LogException(ex);
                return CodeAnalysisResult.Fail("A deserialization error occurred while processing the response.");
            }
            catch (OperationCanceledException)
            {
                return CodeAnalysisResult.Canceled();
            }
        }

        private CreateChatCompletionDto CreateRequestDto(string code)
        {
            return new CreateChatCompletionDto
            {
                Model = "gpt-4o-mini",
                Temperature = 0.7m,
                Messages =
                [
                    new Message
                    {
                        Role = "user",
                        Content = $"{_config.ChatGptPrompt}\r\n\r\n{code}"
                    }
                ]
            };
        }

        private static string ExtractAnalysisFromResponse(ChatCompletionResponse? response)
        {
            return response is not null
                ? response.Choices.First().Message.Content
                : DefaultErrorMsg;
        }
    }
}
