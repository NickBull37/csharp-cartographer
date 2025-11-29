namespace csharp_cartographer_backend._07.Clients.ChatGpt.Dtos
{
    public record Message
    {
        public string Role { get; init; } = string.Empty;

        public string Content { get; init; } = string.Empty;
    }
}
