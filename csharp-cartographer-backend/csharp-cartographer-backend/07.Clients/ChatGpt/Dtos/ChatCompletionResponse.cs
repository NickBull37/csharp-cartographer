namespace csharp_cartographer_backend._07.Clients.ChatGpt.Dtos
{
    public record ChatCompletionResponse
    {
        public string Id { get; init; } = string.Empty;

        public string Object { get; init; } = string.Empty;

        public long Created { get; init; }

        public string Model { get; init; } = string.Empty;

        public Usage Usage { get; init; } = new();

        public List<Choice> Choices { get; init; } = [];
    }

    public record Usage
    {
        public int PromptTokens { get; init; }

        public int CompletionTokens { get; init; }

        public int TotalTokens { get; init; }

        public CompletionTokensDetails CompletionTokensDetails { get; init; } = new();
    }

    public record CompletionTokensDetails
    {
        public int ReasoningTokens { get; init; }

        public int AcceptedPredictionTokens { get; init; }

        public int RejectedPredictionTokens { get; init; }
    }

    public record Choice
    {
        public Message Message { get; init; } = new();

        public object? LogProbs { get; init; }

        public string FinishReason { get; init; } = string.Empty;

        public int Index { get; init; }
    }
}
