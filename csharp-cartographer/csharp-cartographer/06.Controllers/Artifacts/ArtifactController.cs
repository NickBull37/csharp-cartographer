using csharp_cartographer._05.Services.Artifacts;
using Microsoft.AspNetCore.Mvc;

namespace csharp_cartographer._06.Controllers.Artifacts
{
    [ApiController]
    [Route("[controller]")]
    public class ArtifactController : ControllerBase
    {
        private readonly IArtifactGenerator _artifactGenerator;
        public ArtifactController(IArtifactGenerator artifactGenerator)
        {
            _artifactGenerator = artifactGenerator;
        }

        #region GET Endpoints
        /// <summary>Gets the demo artifact.</summary>
        [HttpGet]
        [Route("get-demo-artifact")]
        public async Task<IActionResult> GetDemoArtifact()
        {
            try
            {
                var mapFile = _artifactGenerator.GenerateDemoArtifact();
                return Ok(mapFile);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: ex.Message);
            }
        }
        #endregion
    }
}
