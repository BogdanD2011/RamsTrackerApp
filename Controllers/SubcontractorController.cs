using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RamsTrackerAPI.Models.Domain;
using RamsTrackerAPI.Models.DTO;
using RamsTrackerAPI.Repositories;

namespace RamsTrackerAPI.Controllers
{
    // /api/Subcontractor
    [Route("api/[controller]")]
    [ApiController]
    public class SubcontractorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubcontractorRepository _subcontractorRepository;
        public SubcontractorController(IMapper mapper, ISubcontractorRepository subcontractorRepository)
        {
            this._mapper = mapper;
            this._subcontractorRepository = subcontractorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var SubcontractorDomain = await _subcontractorRepository.GetAllAsync();

            // map and return Subcontractor DTO

            return Ok(_mapper.Map<List<SubcontractorDTO>>(SubcontractorDomain));

        }
        // POST: https://localhost:portnumber/api/Subcontractor
        [HttpPost]
        public async Task<IActionResult> AddSubcontractor([FromBody] AddSubcontractorDTO addSubcontractorDTO)
        {
            // Mapp DTO to domain model
            var SubcontractorDomain = _mapper.Map<Subcontractor>(addSubcontractorDTO);


            // Use domain model to create new subcontractor 
            SubcontractorDomain = await _subcontractorRepository.AddSubcontractorAsync(SubcontractorDomain);

            // mapp domain to DTO
            var SubconDTO = _mapper.Map<SubcontractorDTO>(SubcontractorDomain);

            return Ok();

        }

        // GET: https://localhost:portnumber/api/Subcontractor/{id}
        [HttpGet]
        [Route ("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var SubconDomain = await _subcontractorRepository.GetByIdAsync(id);

            if (SubconDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SubcontractorDTO>(SubconDomain));
        }

    }
}
