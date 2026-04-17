using csharp_cartographer_backend._02.Utilities.ActionResponse;
using csharp_cartographer_backend._02.Utilities.Logging;
using csharp_cartographer_backend._03.Models.Insights;
using csharp_cartographer_backend._04.DataAccess.Insights;
using csharp_cartographer_backend._08.Controllers.Insights.Dtos;

namespace csharp_cartographer_backend._06.Workflows.Insights
{
    public class CreateInsightWorkflow : ICreateInsightWorkflow
    {
        private readonly IInsightRepository _insightRepository;

        public CreateInsightWorkflow(IInsightRepository insightRepository)
        {
            _insightRepository = insightRepository;
        }

        public async Task<ActionResponse> CreateInsight(CreateInsightDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _insightRepository.SaveInsight(new Insight(dto), cancellationToken);
                return ActionResponse.Success();
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                CartographerLogger.LogException(ex);
                return ActionResponse.Failure("An exception occurred while attempting to save insight.");
            }
        }
    }
}
