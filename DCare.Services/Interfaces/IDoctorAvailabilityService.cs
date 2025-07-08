using DCare.Data.Entites;
using DCare.Services.DTOs.Availability;
using DCare.Services.DTOs.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Interfaces
{
    public interface IDoctorAvailabilityService
    {
        Task AddAvailabilityAsync(CreateAvailabilityDto dto);
        Task<IEnumerable<DoctorAvailability>> GetAvailableAppointmentsByDoctorIdAsync(int doctorId);

        Task<bool> DeleteAvailabilityAsync(int availabilityId, int doctorId);
        Task<IEnumerable<DoctorAvailabilityDetailsDto>> GetAllForDoctorAsync(int doctorId);


    }
}
