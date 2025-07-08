using DCare.Data.Context;
using DCare.Data.Entites;
using DCare.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Repository.Implementations
{
    public class PatientRepository: IPatientRepository
    {
    private readonly DCareDbContext _context;

        public PatientRepository(DCareDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> GetByEmailAsync(string email)
        {
            return await _context.Patients
              .FirstOrDefaultAsync(p => p.Email.ToLower() == email.ToLower());
        }

        public async Task AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Where(p => !p.IsDelet)
                .ToListAsync();
        }
        public async Task<bool> SoftDeleteAsync(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null)
                return false;

            patient.IsDelet = true;
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}

