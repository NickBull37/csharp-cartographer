using csharp_cartographer_backend._03.Models.Insights;

namespace csharp_cartographer_backend._05.Services.Insights
{
    public interface IInsightService
    {
        Insight? GetDemoFileInsight(string fileName);
    }
}
