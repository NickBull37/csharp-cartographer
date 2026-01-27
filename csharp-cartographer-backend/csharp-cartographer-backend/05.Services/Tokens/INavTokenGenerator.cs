using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public interface INavTokenGenerator
    {
        Task<List<NavToken>> GenerateNavTokens(FileData fileData, CancellationToken cancellationToken);
    }
}
