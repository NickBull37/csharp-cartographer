using csharp_cartographer_backend._02.Utilities.Providers;
using Microsoft.AspNetCore.Mvc;

namespace csharp_cartographer_backend._08.Controllers.Demos
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        /// <summary>Retrieves data for Cartographer demo files.</summary>
        [HttpGet]
        [Route("get-demo-files")]
        public IActionResult GetDemos()
        {
            var demoFiles = DemoFileProvider.GetDemoFiles();

            if (demoFiles is null)
            {
                return Problem(
                    type: "Internal Server Error",
                    detail: "An error occured attempting to retreive demo files.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(demoFiles);
        }
    }
}
