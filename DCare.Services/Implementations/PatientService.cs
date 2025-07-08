using AutoMapper;
using DCare.API.Helper;
using DCare.Data.Entites;
using DCare.Data.Enums;
using DCare.Repository.Implementations;
using DCare.Repository.Interfaces;
using DCare.Services.DTOs.Patient;
using DCare.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Implementations
{
    public class PatientService:IPatientService
    {
        private readonly IPatientRepository _patientRepo;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepo, IMapper mapper, IPatientRepository patientRepository)
        {
            _patientRepo = patientRepo;
            _mapper = mapper;
            _patientRepository = patientRepository;
        }
        public async Task<string> RegisterAsync(PatientRegisterDto request)
        {  
            var existing = await _patientRepo.GetByEmailAsync(request.Email);
            if (existing != null)
                throw new Exception("This email is already registered");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);


            var patient = new Patient
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = hashedPassword, // 🔐 Hash here
                Height = request.Height,
                Weight = request.Weight,
                BloodType = request.BloodType,
                Allergies = request.Allergies,
                MedicalHistory = request.MedicalHistory,
                CreatedAt = DateTime.Now,
                Role = UserRole.Patient // لو عندك Enum للـ Role
            };

            await _patientRepo.AddAsync(patient);
            return "Patient registered successfully!";
        }

        public async Task<PatientProfileDto> GetProfileAsync(string email)
        {
            var patient = await _patientRepo.GetByEmailAsync(email);
            if (patient == null)
                throw new Exception("The patient is not present");

            var profileDto = _mapper.Map<PatientProfileDto>(patient);
            return profileDto;
        }

        public async Task<string> UpdateProfileAsync(string email, PatientUpdateDto updateDto)
        {
            var patient = await _patientRepo.GetByEmailAsync(email);
            if (patient == null)
                throw new Exception("The patient is not present");

            // استخدام AutoMapper
            _mapper.Map(updateDto, patient);

            await _patientRepo.UpdateAsync(patient);
            return "The profile has been updated successfully";
        }
        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _patientRepo.GetAllAsync();
            return _mapper.Map<List<PatientDto>>(patients);
        }
        public async Task<bool> SoftDeleteAsync(int id)
        {
            return await _patientRepository.SoftDeleteAsync(id);
        }

    }
}
