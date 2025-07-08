using DCare.Repository.Interfaces;
using DCare.Services.DTOs.Auth;
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
    public class AuthService : IAuthService
    {
        readonly IPatientRepository _patientRepo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IAdminRepository _adminRepo;
        private readonly IConfiguration _config;

        public AuthService(IPatientRepository patientRepo, IDoctorRepository doctorRepo, IAdminRepository adminRepo, IConfiguration config)
        {
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
            _adminRepo = adminRepo;
            _config = config;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            // Check Patient
            var patient = await _patientRepo.GetByEmailAsync(loginDto.Email);
            if (patient != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, patient.PasswordHash))
            {
                return GenerateToken(patient.PatientId.ToString(), patient.Email, "Patient");
            }

         
            // Check Doctor
            var doctor = await _doctorRepo.GetByEmailAsync(loginDto.Email);
            if (doctor != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, doctor.PasswordHash))
            {
                return GenerateToken(doctor.DoctorId.ToString(), doctor.Email, "Doctor", doctor.Status.ToString());
            }


            // Check Admin
            var admin = await _adminRepo.GetByEmailAsync(loginDto.Email);
            if (admin != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.PasswordHash))
            {
                return GenerateToken(admin.AdminId.ToString(), admin.Email, "Admin");
            }

            throw new Exception("Login data is incorrect");
        }

        private AuthResponseDto GenerateToken(string userId, string email, string role, string? status = null)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };
            if (role == "Doctor" && !string.IsNullOrEmpty(status))
            {
                claims.Add(new Claim("Status", status));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = role
            };
        }

    }
}


