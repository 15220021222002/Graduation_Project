using AutoMapper;
using DCare.Services.DTOs.Doctor;
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
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly IAppointmentService _appointmentService;

        public DoctorsController(IDoctorService doctorService, IMapper mapper, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _appointmentService = appointmentService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] DoctorRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _doctorService.GetByEmailAsync(dto.Email);
            if (exists != null)
                return BadRequest("Email is already registered.");

            var result = await _doctorService.RegisterDoctorAsync(dto);
            if (!result)
                return BadRequest("Specialization not found.");

            return Ok("Doctor registered successfully. And wait for approval.");
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);

                if (string.IsNullOrEmpty(email))
                    return Unauthorized("Email not found in token.");

                var profile = await _doctorService.GetProfileAsync(email);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [Authorize(Roles = "Doctor")]
        [HttpPut("Profile-Update")]
        public async Task<IActionResult> UpdateProfile([FromBody] DoctorProfileUpdateDto dto)
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                if (email == null)
                    return Unauthorized("Email not found in token.");

                await _doctorService.UpdateDoctorProfileAsync(email, dto);
                return Ok(new { message = "Doctor profile updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("approved-doctors")]
        public async Task<IActionResult> GetApprovedDoctors()
        {
            var result = await _doctorService.GetApprovedDoctorsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingDoctors()
        {
            var doctors = await _doctorService.GetPendingDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet("by-specialization/{specialtyName}")]
        public async Task<IActionResult> GetDoctorsBySpecialization(string specialtyName)
        {
            var doctors = await _doctorService.GetDoctorsBySpecialtyAsync(specialtyName);

            if (doctors == null || !doctors.Any())
                return NotFound("No doctors found for this specialty.");

            return Ok(doctors);
        }

        [HttpPut("approve/{doctorId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveDoctor(int doctorId)
        {
            var success = await _doctorService.ApproveDoctorAsync(doctorId);
            if (!success)
                return NotFound("Doctor not found or already deleted.");

            return Ok("Doctor approved successfully.");
        }
        [HttpPut("reject/{doctorId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectDoctor(int doctorId, [FromBody] RejectDoctorDto dto)
        {
            var success = await _doctorService.RejectDoctorAsync(doctorId, dto.RejectionReason);
            if (!success)
                return NotFound("Doctor not found or already deleted.");

            return Ok("Doctor rejected successfully.");
        }

        [HttpGet("{doctorId}/overview")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetDoctorOverview(int doctorId)
        {
            var doctor = await _doctorService.GetDoctorOverviewByIdAsync(doctorId);
            if (doctor == null)
                return NotFound("Doctor not found.");

            return Ok(doctor);
        }
        [HttpGet("earnings")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetEarnings()
        {
            var doctorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(doctorIdClaim))
                return Unauthorized("Doctor ID not found in token.");

            int doctorId = int.Parse(doctorIdClaim);

            var earnings = await _appointmentService.GetDoctorEarningsAsync(doctorId);
            return Ok(earnings);
        }


        //[HttpGet("by-specialization/{specializationName}")]
        //public async Task<IActionResult> GetBySpecialization(string specializationName)
        //{
        //    var doctors = await _doctorService.GetDoctorsBySpecializationNameAsync(specializationName);
        //    var result = _mapper.Map<List<DoctorListDto>>(doctors);
        //    return Ok(result);
        //}
    }
}
