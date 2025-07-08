using AutoMapper;
using DCare.Repository.Implementations;
using DCare.Repository.Interfaces;
using DCare.Services.DTOs.Specialty;
using DCare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCare.Services.Implementations
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _repo;
        private readonly IMapper _mapper;

        public SpecialtyService(ISpecialtyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
       
        }

        public async Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync()
        {
            var specialties = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<SpecialtyDto>>(specialties);
        }

    }
}
