using csharp_cartographer_backend._02.Utilities.Providers;
using csharp_cartographer_backend._03.Models.Insights;

namespace csharp_cartographer_backend._05.Services.Insights
{
    public class InsightService : IInsightService
    {
        public InsightService()
        {
        }

        public Insight? GetDemoFileInsight(string fileName)
        {
            var embeddedInsight = InsightProvider.GetInsight(fileName);

            if (embeddedInsight is null)
            {
                return null;
            }

            return new Insight(embeddedInsight);
        }
    }
}
