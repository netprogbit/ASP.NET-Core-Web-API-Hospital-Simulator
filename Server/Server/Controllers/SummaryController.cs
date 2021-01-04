using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        // <summary>
        /// Get device completely
        /// </summary>
        [HttpGet("summaries")]
        public async Task<List<SummaryDto>> FindAllDevices() => await _summaryService.FindAllAsync();
    }
}
