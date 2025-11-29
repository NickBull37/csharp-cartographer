namespace csharp_cartographer_backend._07.Clients.ChatGpt.Dtos
{
    public record CreateChatCompletionDto
    {
        // TODO: Move model & temp to Chat GPT config
        public string Model { get; init; } = "gpt-4o-mini";

        public decimal Temperature { get; init; } = 0.7m;

        public List<Message> Messages { get; init; } = [];
    }
}
