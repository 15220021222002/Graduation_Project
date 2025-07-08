using DCare.Data.Context;
using DCare.Data.Entites;
using DCare.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DCare.Repository.Implementations
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly DCareDbContext _context;

        public SpecialtyRepository(DCareDbContext context)
        {
            _context = context;
        }

        public async Task<Specialty> GetByNameAsync(string name)
        {
            return await _context.Specialties
                .FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
        }
        public async Task<List<Specialty>> GetAllAsync()
        {
            return await _context.Specialties.ToListAsync();
        }

    }
}
