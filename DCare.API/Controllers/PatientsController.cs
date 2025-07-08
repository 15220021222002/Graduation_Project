using DCare.Services.DTOs.Patient;
using DCare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PatientRegisterDto request)
        {
            try
            {
                var result = await _patientService.RegisterAsync(request);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                
                return BadRequest(new { error = ex.InnerException?.Message ?? ex.Message });
            }
        }
 
        [Authorize(Roles = "Patient")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);

                if (string.IsNullOrWhiteSpace(email))
                    return Unauthorized(new { error = "Email claim missing in token" });

                var profile = await _patientService.GetProfileAsync(email);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPut("profile-Update")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateProfile([FromBody] PatientUpdateDto dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var result = await _patientService.UpdateProfileAsync(email, dto);
            return Ok(new { message = result });
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GET-Patients")]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var result = await _patientService.SoftDeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Patient not found." });

            return Ok(new { message = "Patient deleted successfully (soft delete)." });
        }

    }
}
