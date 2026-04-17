using csharp_cartographer_backend._02.Utilities.ActionResponse;
using csharp_cartographer_backend._08.Controllers.Insights.Dtos;

namespace csharp_cartographer_backend._06.Workflows.Insights
{
    public interface ICreateInsightWorkflow
    {
        Task<ActionResponse> CreateInsight(CreateInsightDto dto, CancellationToken cancellationToken);
    }
}
