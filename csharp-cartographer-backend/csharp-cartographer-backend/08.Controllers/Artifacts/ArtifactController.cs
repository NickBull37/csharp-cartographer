using csharp_cartographer_backend._06.Workflows.Artifacts;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace csharp_cartographer_backend._08.Controllers.Artifacts
{
    [ApiController]
    [Route("[controller]")]
    public class ArtifactController : ControllerBase
    {
        private readonly IGenerateArtifactWorkflow _generateArtifactWorkflow;

        public ArtifactController(IGenerateArtifactWorkflow generateArtifactWorkflow)
        {
            _generateArtifactWorkflow = generateArtifactWorkflow;
        }

        /// <summary>Generates and returns the requested demo artifact.</summary>
        [HttpGet]
        [Route("get-demo-artifact")]
        public async Task<IActionResult> GetDemoArtifact([FromQuery] string fileName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("The file name must match one of the existing demo files available.");
            }

            var response = await _generateArtifactWorkflow.ExecGenerateDemoArtifact(fileName, cancellationToken);

            if (response.IsFailure)
            {
                return Problem(
                    type: "Internal Server Error",
                    detail: response.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(response.Content);
        }

        /// <summary>Generates and returns an artifact for user uploaded source code.</summary>
        [HttpPost]
        [Route("generate-artifact")]
        public async Task<IActionResult> GenerateArtifact([FromBody] GenerateArtifactDto dto, CancellationToken cancellationToken)
        {
            var response = await _generateArtifactWorkflow.ExecGenerateUserArtifact(dto, cancellationToken);

            if (response.IsFailure)
            {
                return Problem(
                    type: "Internal Server Error",
                    detail: response.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(response.Content);
        }
    }
}
