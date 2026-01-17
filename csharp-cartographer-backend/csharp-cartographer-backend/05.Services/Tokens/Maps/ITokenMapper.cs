using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    public interface ITokenMapper
    {
        void MapNavTokens(List<NavToken> navTokens);
    }
}
