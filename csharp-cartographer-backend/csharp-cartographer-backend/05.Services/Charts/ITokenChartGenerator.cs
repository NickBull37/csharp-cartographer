using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Charts
{
    public interface ITokenChartGenerator
    {
        void GenerateTokenCharts(List<NavToken> navTokens);
    }
}
