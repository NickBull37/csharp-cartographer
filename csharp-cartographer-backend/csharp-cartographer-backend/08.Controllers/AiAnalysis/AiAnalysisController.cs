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
                return BadRequest();

            var analysis = await _aiAnalysisService.GetAnalysisResult(code, cancellationToken);
            return Ok(analysis);
        }
    }
}
