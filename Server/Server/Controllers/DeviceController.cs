using AutoMapper;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Helpers;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    /// <summary>
    /// Device actions
    /// </summary>
    /// [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDeviceService _deviceService;

        public DeviceController(IMapper mapper, IDeviceService deviceService)
        {
            _mapper = mapper;
            _deviceService = deviceService;
        }

        // <summary>
        /// Get device completely
        /// </summary>
        [HttpGet("devices")]
        public async Task<IEnumerable<DeviceDto>> FindAllDevices()
        {
            var devices = await _deviceService.FindAllAsync();
            return _mapper.Map<List<DeviceDto>>(devices);
        }            

        /// <summary>
        /// Create device
        /// </summary>
        /// <returns>OK message</returns>        
        [HttpPost("")]
        public async Task<IActionResult> Add(DeviceDto deviceDto)
        {
            var device = _mapper.Map<Device>(deviceDto);
            device.PatientId = Convert.ToInt32(HttpContext.Session.GetString("patientId"));
            bool isExisted = await _deviceService.AddAsync(device);

            if (!isExisted)
                return BadRequest(new { message = StringHelper.DeviceIdExists });

            return Ok(new { message = StringHelper.AdditionSuccefully });
        }

        /// <summary>
        /// Update device
        /// </summary>
        /// <returns>OK message</returns>        
        [HttpPut("")]
        public async Task<IActionResult> Update(DeviceDto deviceDto)
        {
            var device = _mapper.Map<Device>(deviceDto);
            await _deviceService.UpdateAsync(device);
            return Ok(new { message = StringHelper.UpdationSuccefully });
        }

        /// <summary>
        /// Remove device by Id
        /// </summary>
        /// <returns>Succcess message</returns>        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _deviceService.RemoveAsync(id);
            return Ok(new { message = StringHelper.DeleteSuccefully });
        }
    }
}
