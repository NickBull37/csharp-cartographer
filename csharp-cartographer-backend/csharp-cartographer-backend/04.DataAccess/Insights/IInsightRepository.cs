using csharp_cartographer_backend._03.Models.Insights;

namespace csharp_cartographer_backend._04.DataAccess.Insights
{
    public interface IInsightRepository
    {
        Task SaveInsight(Insight insight, CancellationToken cancellationToken);
    }
}
