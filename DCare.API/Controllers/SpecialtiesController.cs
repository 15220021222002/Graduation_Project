using AutoMapper;
using DCare.Services.DTOs.Specialty;
using DCare.Services.Implementations;
using DCare.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly ISpecialtyService _service;
        private readonly IMapper _mapper;

        public SpecialtiesController(ISpecialtyService service, IMapper mapper, ISpecialtyService specialtyService)
        {
            _service = service;
            _mapper = mapper;
            _specialtyService = specialtyService;
        }


        [HttpGet("GetAllSpecialties")]
        public async Task<IActionResult> GetAll()
        {
            var specialties = await _service.GetAllSpecialtiesAsync(); // GetAll returns List<Specialty>
            var result = _mapper.Map<List<SpecialtyDto>>(specialties);
            return Ok(result);
        }
    }
}
