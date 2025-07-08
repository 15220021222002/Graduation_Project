using AutoMapper;
using DCare.Data.Entites;
using DCare.Data.Enums;
using DCare.Repository.Interfaces;
using DCare.Services.DTOs.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DCare.Services.Interfaces;
using DCare.Repository.Implementations;
using DCare.Services.Helper;

namespace DCare.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly ISpecialtyRepository _specialtyRepo;
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly FileHelper _fileHelper;


        public DoctorService(IDoctorRepository doctorRepo, ISpecialtyRepository specialtyRepo, IMapper mapper, ISpecialtyRepository specialtyRepository, IDoctorRepository doctorRepository, FileHelper fileHelper)
        {
            _doctorRepo = doctorRepo;
            _specialtyRepo = specialtyRepo;
            _mapper = mapper;
            _specialtyRepository = specialtyRepository;
            _doctorRepository = doctorRepository;
            _fileHelper = fileHelper;
        }

        public async Task<Doctor> GetByEmailAsync(string email)
        {
            return await _doctorRepo.GetByEmailAsync(email);
        }

        public async Task<bool> RegisterDoctorAsync(DoctorRegisterDto dto)
        {
            var specialty = await _specialtyRepo.GetByNameAsync(dto.Specialization);
            if (specialty == null) return false;

            // 1. Save Images
            var licenseImgPath = await _fileHelper.SaveFileAsync(dto.LicenseImg, "images/licenses");
            var qualificationImgPath = await _fileHelper.SaveFileAsync(dto.QualificationImg, "images/qualifications");
            var doctorImgPath = await _fileHelper.SaveFileAsync(dto.DoctorImg, "images/doctors");

            // 2. Map DTO to Entity
            var doctor = _mapper.Map<Doctor>(dto);
            doctor.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            doctor.SpecialtyId = specialty.SpecialtyId;

            // 3. Set Image Paths
            doctor.LicenseImgUrl = licenseImgPath;
            doctor.QualificationImgUrl = qualificationImgPath;
            doctor.DoctorImgUrl = doctorImgPath;

            // 4. Save to DB
            await _doctorRepo.AddAsync(doctor);
            await _doctorRepo.SaveAsync();

            return true;
        }

        public async Task<DoctorProfileDto> GetProfileAsync(string email)
        {
            var doctor = await _doctorRepository.GetByEmailAsync(email);
            if (doctor == null)
                throw new Exception("Doctor not found.");

            return _mapper.Map<DoctorProfileDto>(doctor);
        }

        public async Task UpdateDoctorProfileAsync(string email, DoctorProfileUpdateDto dto)
        {
            var doctor = await _doctorRepository.GetByEmailAsync(email);
            if (doctor == null)
                throw new Exception("Doctor not found.");

            _mapper.Map(dto, doctor);
            await _doctorRepository.UpdateDoctorProfileAsync(doctor);
        }

        public async Task<IEnumerable<DoctorAdminListDto>> GetApprovedDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetApprovedDoctorsAsync();
            return _mapper.Map<IEnumerable<DoctorAdminListDto>>(doctors);
        }
        public async Task<IEnumerable<DoctorPendingListDto>> GetPendingDoctorsAsync()
        {
            var pendingDoctors = await _doctorRepository.GetPendingDoctorsAsync();
            return _mapper.Map<IEnumerable<DoctorPendingListDto>>(pendingDoctors);
        }

        public async Task<IEnumerable<DoctorBySpecializationDto>> GetDoctorsBySpecialtyAsync(string specialtyName)
        {
            var specialty = await _specialtyRepo.GetByNameAsync(specialtyName.ToLower());

            if (specialty == null)
                return Enumerable.Empty<DoctorBySpecializationDto>();

            // 2. Get only approved doctors using the SpecialtyId
            var doctors = await _doctorRepo.GetDoctorsBySpecialtyIdAsync(specialty.SpecialtyId);

            var approvedDoctors = doctors
                .Where(d => d.Status == DoctorStatus.Approved && !d.IsDeleted);

            // 3. Map and return the result
            return _mapper.Map<IEnumerable<DoctorBySpecializationDto>>(approvedDoctors);
        }
        public async Task<bool> ApproveDoctorAsync(int doctorId)
        {
            var doctor = await _doctorRepo.GetByIdAsync(doctorId);
            if (doctor == null || doctor.IsDeleted)
                return false;

            doctor.Status = DoctorStatus.Approved;

            await _doctorRepo.SaveAsync();
            return true;
        }
        public async Task<bool> RejectDoctorAsync(int doctorId, string reason)
        {
            var doctor = await _doctorRepo.GetByIdAsync(doctorId);
            if (doctor == null || doctor.IsDeleted)
                return false;

            doctor.Status = DoctorStatus.Rejected;
            doctor.REjectionReason = reason;

            await _doctorRepo.SaveAsync();
            return true;
        }
        public async Task<DoctorOverviewDto> GetDoctorOverviewByIdAsync(int doctorId)
        {
            var doctor = await _doctorRepo.GetDoctorOverviewByIdAsync(doctorId);

            if (doctor == null)
                throw new Exception("Doctor not found or not approved.");

            return _mapper.Map<DoctorOverviewDto>(doctor);
        }

        //public async Task<List<DoctorListDto>> GetDoctorsBySpecializationNameAsync(string specializationName)
        //{
        //    var specialty = await _specialtyRepository.GetByNameAsync(specializationName.ToLower());

        //    if (specialty == null)
        //        return new List<DoctorListDto>(); // أو return NotFound إذا في الكونترولر

        //    var doctors = await _doctorRepository.GetDoctorsBySpecialtyIdAsync(specialty.SpecialtyId);

        //    return _mapper.Map<List<DoctorListDto>>(doctors);
        //}


    }
}
