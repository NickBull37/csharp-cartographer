using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Charts
{
    public interface ITokenChartWizard
    {
        void AddFactsAndInsightsToNavTokenCharts(List<NavToken> navTokens);

        void AddHighlightRangeToNavTokenCharts(List<NavToken> navTokens);
    }
}
