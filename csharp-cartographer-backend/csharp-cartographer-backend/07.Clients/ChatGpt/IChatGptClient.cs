namespace csharp_cartographer_backend._07.Clients.ChatGpt
{
    public interface IChatGptClient
    {
        Task<string> GetCodeAnalysis(string code, CancellationToken cancellationToken);
    }
}
