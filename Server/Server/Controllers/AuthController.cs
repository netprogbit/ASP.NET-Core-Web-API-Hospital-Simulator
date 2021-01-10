using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Helpers;
using Server.DTOs;
using Server.Services;
using AutoMapper;

namespace Server.Controllers
{
    /// <summary>
    /// Authentication actions
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }
        
        /// <summary>
        /// Registration in the system
        /// </summary>
        /// <returns>Success message</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            bool isRegistered = await _authService.RegisterAsync(patient);

            if (!isRegistered)
                return BadRequest(new { message = StringHelper.EmailExists });

            return Ok(new { message = StringHelper.RegistrationSuccefully });
        }

        /// <summary>
        /// Logging in
        /// </summary>
        /// <returns>Token data</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(PatientDto patientDto)
        {
            AuthDto authDto = await _authService.LoginAsync(patientDto.Email, patientDto.Password);

            if (authDto == null)
                return NotFound(new { message = StringHelper.UserNotFound });

            return Ok(authDto);
        }        
    }
}
