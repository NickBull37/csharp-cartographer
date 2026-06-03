using csharp_cartographer_backend._02.Utilities.Providers;
using Microsoft.AspNetCore.Mvc;

namespace csharp_cartographer_backend._08.Controllers.DemoOptions
{
    [ApiController]
    [Route("[controller]")]
    public class DemoOptionsController : ControllerBase
    {
        /// <summary>Gets the available demo file options.</summary>
        [HttpGet]
        [Route("get-demo-options")]
        public IActionResult GetDemoOptions()
        {
            var options = DemoOptionProvider.GetDemoOptions();

            if (options is null)
            {
                return Problem(
                    type: "Internal Server Error",
                    detail: "An error occured attempting to retreive demo options.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(options);
        }
    }
}
