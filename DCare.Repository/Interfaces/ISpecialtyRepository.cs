using DCare.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Repository.Interfaces
{
    public interface ISpecialtyRepository
    {
        Task<Specialty> GetByNameAsync(string name);
        Task<List<Specialty>> GetAllAsync();


    }
}