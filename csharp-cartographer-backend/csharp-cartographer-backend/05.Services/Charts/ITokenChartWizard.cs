using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Charts
{
    public interface ITokenChartWizard
    {
        void AddFactsAndInsightsToNavTokenCharts(List<NavToken> navTokens);

        void AddHighlightValuesToNavTokenCharts(List<NavToken> navTokens);

        void RemoveExcessChartsFromNavTokens(List<NavToken> navTokens);
    }
}
