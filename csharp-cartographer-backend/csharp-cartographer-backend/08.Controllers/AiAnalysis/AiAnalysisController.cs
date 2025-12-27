using csharp_cartographer_backend._02.Utilities.Logging;
using csharp_cartographer_backend._05.Services.AiAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace csharp_cartographer_backend._08.Controllers.AiAnalysis
{
    [ApiController]
    [Route("[controller]")]
    public class AiAnalysisController : ControllerBase
    {
        private readonly IAiAnalysisService _aiAnalysisService;

        public AiAnalysisController(IAiAnalysisService aiAnalysisService)
        {
            _aiAnalysisService = aiAnalysisService;
        }

        /// <summary>Passes the selected code to an external AI client for analysis.</summary>
        [HttpPost]
        [Route("get-ai-analysis")]
        public async Task<IActionResult> GetAiAnalysis([FromBody] string code, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return Problem(
                    type: "Bad Request",
                    title: "Invalid code segment",
                    detail: "The code segment cannot be null or whitespace.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            try
            {
                var analysis = await _aiAnalysisService.GetAnalysisResult(code, cancellationToken);
                return Ok(analysis);
            }
            catch (OperationCanceledException)
            {
                return Problem(
                    title: "Request canceled",
                    detail: "The request was canceled by the client.",
                    statusCode: StatusCodes.Status499ClientClosedRequest);
            }
            catch (Exception ex)
            {
                CartographerLogger.LogException(ex);

                return Problem(
                    type: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
