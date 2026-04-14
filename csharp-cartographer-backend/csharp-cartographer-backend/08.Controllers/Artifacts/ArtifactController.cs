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

        #region GET Endpoints
        /// <summary>Generates and returns the requested demo artifact.</summary>
        [HttpGet]
        [Route("get-demo-artifact")]
        public async Task<IActionResult> GetDemoArtifact([FromQuery] string fileName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return Problem(
                    type: "Bad Request",
                    title: "Invalid file name",
                    detail: "The file name must match one of the existing demo files available.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            var actionResponse = await _generateArtifactWorkflow.ExecGenerateDemoArtifact(fileName, cancellationToken);

            if (actionResponse.IsFailure)
            {
                return Problem(
                    type: "Internal Server Error",
                    detail: actionResponse.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(actionResponse.Content);
        }
        #endregion

        #region POST Endpoints
        /// <summary>Generates and returns an artifact for user uploaded source code.</summary>
        [HttpPost]
        [Route("generate-artifact")]
        public async Task<IActionResult> GenerateArtifact([FromBody] GenerateArtifactDto dto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(dto.FileName) || string.IsNullOrEmpty(dto.FileContent))
            {
                return Problem(
                    type: "Bad Request",
                    title: "Missing file name/content",
                    detail: "The uploaded file must have a file name and valid C# source code.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            var actionResponse = await _generateArtifactWorkflow.ExecGenerateUserArtifact(dto, cancellationToken);

            if (actionResponse.IsFailure)
            {
                return Problem(
                    type: "Internal Server Error",
                    detail: actionResponse.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(actionResponse.Content);
        }
        #endregion
    }
}
