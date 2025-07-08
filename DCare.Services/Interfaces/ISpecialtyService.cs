using DCare.Services.DTOs.Specialty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Interfaces
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync();

    }
}
