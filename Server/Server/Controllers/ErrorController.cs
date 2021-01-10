using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.DTOs;
using Server.Helpers;

namespace Server.Controllers
{
    /// <summary>
    /// Error handler features
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        protected readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Log error
        /// </summary>
        /// <returns></returns>
        [HttpPost("log")]
        public IActionResult Log(ErrorDto errorData)
        {
            _logger.LogError("{0} {1}", StringHelper.ClientError, errorData.Message);
            return Ok();
        }
    }
}
