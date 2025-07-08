using DCare.Data.Context;
using DCare.Data.Entites;
using DCare.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCare.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DCare.Data.Enums;

namespace DCare.Repository.Implementations
{
    public class DoctorRepository:IDoctorRepository
    {
        private readonly DCareDbContext _context;

        public DoctorRepository(DCareDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> GetByEmailAsync(string email)
        {
            return await _context.Doctors
                .FirstOrDefaultAsync(d => d.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> ExistsAsync(Expression<Func<Doctor, bool>> predicate)
        {
            return await _context.Doctors.AnyAsync(predicate);
        }

        public async Task AddAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Doctor>> GetDoctorsBySpecialtyIdAsync(int specialtyId)
        {
            return await _context.Doctors
                .Where(d => d.SpecialtyId == specialtyId && !d.IsDeleted)
                .Include(d => d.Specialty)
                .ToListAsync();
        }

        public async Task UpdateDoctorProfileAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<Doctor>> GetApprovedDoctorsAsync()
        {
            return await _context.Doctors
                .Where(d => d.Status == DoctorStatus.Approved && !d.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<Doctor>> GetPendingDoctorsAsync()
        {
            return await _context.Doctors
                .Where(d => d.Status == DoctorStatus.Pending && !d.IsDeleted)
                .Include(d => d.Specialty) // ⬅️ لازم ده
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyNameAsync(string specialtyName)
        {
            return await _context.Doctors
                .Include(d => d.Specialty)
                .Where(d => d.Specialty.Name.ToLower() == specialtyName.ToLower()
                            && d.Status == DoctorStatus.Approved
                            && !d.IsDeleted)
                .ToListAsync();
        }


        public async Task<Doctor> GetByIdAsync(int doctorId)
        {
            return await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId && !d.IsDeleted);
        }

        public async Task<Doctor> GetDoctorOverviewByIdAsync(int doctorId)
        {
            return await _context.Doctors
                .Where(d => d.DoctorId == doctorId && !d.IsDeleted && d.Status == DoctorStatus.Approved)
                .FirstOrDefaultAsync();
        }

    }
}
