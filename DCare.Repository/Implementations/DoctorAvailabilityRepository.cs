using DCare.Data.Context;
using DCare.Data.Entites;
using DCare.Data.Enums;
using DCare.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DCare.Repository.Implementations
{
    public class DoctorAvailabilityRepository : IDoctorAvailabilityRepository
    {
        private readonly DCareDbContext _context;

        public DoctorAvailabilityRepository(DCareDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DoctorAvailability availability)
        {
            await _context.DoctorAvailabilities.AddAsync(availability);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<DoctorAvailability>> GetAvailableAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _context.DoctorAvailabilities
                .Where(a => a.DoctorId == doctorId && a.Status == AvailabilityStatus.Available)
                .ToListAsync();
        }
        public async Task<DoctorAvailability> GetByIdAsync(int id)
        {
            return await _context.DoctorAvailabilities.FindAsync(id);
        }

        public async Task DeleteAsync(DoctorAvailability availability)
        {
            _context.DoctorAvailabilities.Remove(availability);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<DoctorAvailability>> GetAllByDoctorIdAsync(int doctorId)
        {
            return await _context.DoctorAvailabilities
                .Where(a => a.DoctorId == doctorId &&
                       (a.Status == AvailabilityStatus.Available || a.Status == AvailabilityStatus.Booked))
                .ToListAsync();
        }


    }
}
