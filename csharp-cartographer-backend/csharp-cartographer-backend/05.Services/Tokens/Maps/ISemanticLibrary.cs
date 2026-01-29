using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    public interface ISemanticLibrary
    {
        void AddSemanticInfo(List<NavToken> navTokens);
    }
}
