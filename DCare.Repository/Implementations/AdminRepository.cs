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
    public class AdminRepository :IAdminRepository
    {
        private readonly DCareDbContext _context;

        public AdminRepository(DCareDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}

