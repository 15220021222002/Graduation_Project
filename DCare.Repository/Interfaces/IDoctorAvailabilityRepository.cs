using DCare.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Repository.Interfaces
{
    public interface IDoctorAvailabilityRepository
    {
        Task AddAsync(DoctorAvailability availability);
        Task SaveChangesAsync();
        Task<IEnumerable<DoctorAvailability>> GetAvailableAppointmentsByDoctorIdAsync(int doctorId);

        Task<DoctorAvailability> GetByIdAsync(int id);
        Task DeleteAsync(DoctorAvailability availability);
        Task<IEnumerable<DoctorAvailability>> GetAllByDoctorIdAsync(int doctorId);


    }
}
