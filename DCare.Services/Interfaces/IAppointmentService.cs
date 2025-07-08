using DCare.Services.DTOs.Appointment;
using DCare.Services.DTOs.Doctor;
using DCare.Services.DTOs.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<string> BookAppointmentAsync(BookAppointmentDto dto, int patientId);
        Task<IEnumerable<AdminAppointmentDto>> GetAllAppointmentsForAdminAsync();
        Task<List<DoctorAppointmentDto>> GetDoctorAppointmentsAsync(int doctorId);
        Task<IEnumerable<PatientAppointmentDto>> GetAppointmentsForPatientAsync(int patientId);
        Task<bool> CancelAppointmentAsync(int appointmentId, int userId, string role);
        Task<PatientDetailsDto> GetPatientDetailsByAppointmentIdAsync(int appointmentId);
        Task<ConfirmAppointmentResponseDto> ConfirmAppointmentAsync(int appointmentId);
        Task<DoctorEarningsDto> GetDoctorEarningsAsync(int doctorId);

    }
}
