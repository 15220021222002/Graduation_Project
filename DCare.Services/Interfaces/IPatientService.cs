using DCare.Data.Entites;
using DCare.Services.DTOs.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Interfaces
{
    public interface IPatientService
    {
        Task<string> RegisterAsync(PatientRegisterDto request);
        Task<PatientProfileDto> GetProfileAsync(string email);
        Task<string> UpdateProfileAsync(string email, PatientUpdateDto updateDto);
        Task<List<PatientDto>> GetAllPatientsAsync();
        Task<bool> SoftDeleteAsync(int id);
    }
}
