using csharp_cartographer_backend._06.Workflows.Insights;
using csharp_cartographer_backend._08.Controllers.Insights.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace csharp_cartographer_backend._08.Controllers.Insights
{
    [ApiController]
    [Route("[controller]")]
    public class InsightController : ControllerBase
    {
        private readonly ICreateInsightWorkflow _workflow;

        public InsightController(ICreateInsightWorkflow workflow)
        {
            _workflow = workflow;
        }

        /// <summary>Gets the insights for an artifact.</summary>
        [HttpGet]
        [Route("get-insights")]
        public async Task<IActionResult> GetInsights([FromQuery] Guid artifactID, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>Creates a new artifact insight and saves it to the database.</summary>
        [HttpPost]
        [Route("save-insight")]
        public async Task<IActionResult> CreateInsight([FromBody] CreateInsightDto dto, CancellationToken cancellationToken)
        {
            var response = await _workflow.CreateInsight(dto, cancellationToken);

            if (response.IsFailure)
            {
                return Problem(
                    type: "Internal Server Error",
                    detail: response.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        /// <summary>Updates an existing artifact insight and saves it to the database.</summary>
        [HttpPost]
        [Route("update-insight")]
        public async Task<IActionResult> UpdateInsight(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>Deletes an insight off an artifact.</summary>
        [HttpPost]
        [Route("delete-insight")]
        public async Task<IActionResult> DeleteInsight([FromQuery] Guid insightID, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
