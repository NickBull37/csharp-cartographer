using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._05.Services.Cartography
{
    public interface ICartographer
    {
        void AddNavigationCharts(List<NavToken> navTokens);
    }
}
