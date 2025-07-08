using DCare.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Repository.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetByEmailAsync(string email);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task<List<Patient>> GetAllAsync();
        Task<bool> SoftDeleteAsync(int patientId);

    }
}
