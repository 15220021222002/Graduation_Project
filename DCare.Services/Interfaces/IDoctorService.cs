using DCare.Data.Entites;
using DCare.Services.DTOs.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> GetByEmailAsync(string email);
        Task<bool> RegisterDoctorAsync(DoctorRegisterDto dto);
        Task<DoctorProfileDto> GetProfileAsync(string email);
        //Task<bool> UpdateProfileAsync(string email, DoctorProfileUpdateDto dto);
        Task UpdateDoctorProfileAsync(string email, DoctorProfileUpdateDto dto);
        Task<IEnumerable<DoctorAdminListDto>> GetApprovedDoctorsAsync();
        Task<IEnumerable<DoctorPendingListDto>> GetPendingDoctorsAsync();
        Task<IEnumerable<DoctorBySpecializationDto>> GetDoctorsBySpecialtyAsync(string specialtyName);
        Task<bool> ApproveDoctorAsync(int doctorId);
        Task<bool> RejectDoctorAsync(int doctorId, string reason);
        Task<DoctorOverviewDto> GetDoctorOverviewByIdAsync(int doctorId);

        //Task<List<DoctorListDto>> GetDoctorsBySpecializationNameAsync(string specializationName);


    }
}
