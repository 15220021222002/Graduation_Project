using DCare.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Repository.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetByEmailAsync(string email);
        Task<bool> ExistsAsync(Expression<Func<Doctor, bool>> predicate);
        Task AddAsync(Doctor doctor);
        Task SaveAsync();
        Task<List<Doctor>> GetDoctorsBySpecialtyIdAsync(int specialtyId);
        Task UpdateDoctorProfileAsync(Doctor doctor);
        Task<IEnumerable<Doctor>> GetApprovedDoctorsAsync();
        Task<IEnumerable<Doctor>> GetPendingDoctorsAsync();
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyNameAsync(string specialtyName);
        Task<Doctor> GetByIdAsync(int doctorId);
        Task<Doctor> GetDoctorOverviewByIdAsync(int doctorId);


    }
}
