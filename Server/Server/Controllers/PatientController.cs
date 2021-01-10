using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Services;

namespace Server.Controllers
{
    /// <summary>
    /// Summary actions
    /// </summary>
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatientService _patientService;

        public PatientController(IMapper mapper, IPatientService patientService)
        {
            _mapper = mapper;
            _patientService = patientService;
        }

        // <summary>
        /// Get patients completely
        /// </summary>        
        [HttpGet("patients")]
        public async Task<IEnumerable<PatientDto>> FindAll()
        {
            var devices = await _patientService.FindAllAsync();
            return _mapper.Map<IEnumerable<PatientDto>>(devices);
        }

        // <summary>
        /// Get summaries completely
        /// </summary>        
        [HttpGet("summaries")]
        public async Task<IEnumerable<SummaryDto>> GetSummaries()
        {
            var summaries = await _patientService.GetSummariesAsync();
            return _mapper.Map<IEnumerable<SummaryDto>>(summaries);
        }
    }
}
