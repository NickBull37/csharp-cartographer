using csharp_cartographer_backend._02.Utilities.Providers;
using csharp_cartographer_backend._03.Models.Insights;

namespace csharp_cartographer_backend._05.Services.Insights
{
    public class InsightService : IInsightService
    {
        public Insight? GetDemoFileInsight(string fileName)
        {
            var embInsight = InsightProvider.GetEmbeddedInsight(fileName);

            return embInsight is not null
                ? new Insight(embInsight)
                : null;
        }
    }
}
