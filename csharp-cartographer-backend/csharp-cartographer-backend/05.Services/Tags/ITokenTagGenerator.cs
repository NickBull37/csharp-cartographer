using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Tags
{
    public interface ITokenTagGenerator
    {
        void GenerateTokenTags(List<NavToken> navTokens);
    }
}
