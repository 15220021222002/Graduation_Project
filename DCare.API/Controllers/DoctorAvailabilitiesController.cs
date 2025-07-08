using AutoMapper;
using DCare.Services.DTOs.Availability;
using DCare.Services.Implementations;
using DCare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailabilitiesController : ControllerBase
    {
        private readonly IDoctorAvailabilityService _availabilityService;
        private readonly IMapper _mapper;

        public DoctorAvailabilitiesController(IDoctorAvailabilityService availabilityService, IMapper mapper)
        {
            _availabilityService = availabilityService;
            _mapper = mapper;

        }
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<IActionResult> AddAvailability([FromBody] CreateAvailabilityDto dto)
        {
            await _availabilityService.AddAvailabilityAsync(dto);
            return Ok(new { message = "Availability slot added successfully." });
        }
        [HttpGet("by-doctor/{doctorId}")]
        public async Task<IActionResult> GetAvailableAppointmentsByDoctorId(int doctorId)
        {
            var availabilities = await _availabilityService.GetAvailableAppointmentsByDoctorIdAsync(doctorId);

            var result = _mapper.Map<IEnumerable<DoctorAvailabilityDetailsDto>>(availabilities);

            return Ok(result);
        }
        [HttpDelete("{availabilityId}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteAvailability(int availabilityId)
        {
            var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _availabilityService.DeleteAvailabilityAsync(availabilityId, doctorId);

            if (!result)
                return NotFound("Appointment not found or not authorized");

            return Ok();
            // 200
        }
        [HttpGet("my-available")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetMyAppointments()
        {
            var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _availabilityService.GetAllForDoctorAsync(doctorId);
            return Ok(result);
        }


    }
}
