using DCare.Services.DTOs.Appointment;
using DCare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("book")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> BookAppointment(BookAppointmentDto dto)
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var sessionId = await _appointmentService.BookAppointmentAsync(dto, patientId);

            return Ok(new { sessionId });
        }
        [HttpGet("admin/all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAppointmentsForAdmin()
        {
            var appointments = await _appointmentService.GetAllAppointmentsForAdminAsync();
            return Ok(appointments);
        }
        [Authorize(Roles = "Doctor")]
        [HttpGet("appointments")]
        public async Task<IActionResult> GetMyAppointments()
        {
            int doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var appointments = await _appointmentService.GetDoctorAppointmentsAsync(doctorId);
            return Ok(appointments);
        }
        [Authorize(Roles = "Patient")]
        [HttpGet("my-appointments")]
        public async Task<IActionResult> GetMyAppointmentsForPatients()
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var appointments = await _appointmentService.GetAppointmentsForPatientAsync(patientId);
            return Ok(appointments);
        }
        [Authorize(Roles = "Patient,Doctor")]
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var result = await _appointmentService.CancelAppointmentAsync(id, userId, role);

            if (!result)
                return BadRequest("Appointment not found or unauthorized access.");

            return Ok("Appointment canceled successfully.");
        }
        [HttpGet("patient-details/{appointmentId}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetPatientDetails(int appointmentId)
        {
            var patientDetails = await _appointmentService.GetPatientDetailsByAppointmentIdAsync(appointmentId);
            return Ok(patientDetails);
        }
        [HttpPost("confirm")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> ConfirmAppointment([FromBody] ConfirmAppointmentRequest request)
        {
            var result = await _appointmentService.ConfirmAppointmentAsync(request.AppointmentId);
            return Ok(result);
        }



    }
}
