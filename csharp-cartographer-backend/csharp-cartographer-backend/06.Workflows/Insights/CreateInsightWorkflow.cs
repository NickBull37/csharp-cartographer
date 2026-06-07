using csharp_cartographer_backend._02.Utilities.ActionResponse;
using csharp_cartographer_backend._03.Models.Insights;
using csharp_cartographer_backend._04.DataAccess.Insights;
using csharp_cartographer_backend._08.Controllers.Insights.Dtos;

namespace csharp_cartographer_backend._06.Workflows.Insights
{
    public class CreateInsightWorkflow : ICreateInsightWorkflow
    {
        private readonly IInsightRepository _insightRepository;
        private readonly ILogger<CreateInsightWorkflow> _logger;

        public CreateInsightWorkflow(IInsightRepository insightRepository, ILogger<CreateInsightWorkflow> logger)
        {
            _insightRepository = insightRepository;
            _logger = logger;
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
                _logger.LogError(ex, "An exception occurred while attempting to save a new insight.");
                return ActionResponse.Failure("An exception occurred while attempting to save a new insight.");
            }
        }
    }
}
