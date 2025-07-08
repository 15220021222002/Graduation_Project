using DCare.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Repository.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin> GetByEmailAsync(string email);
    }
}
