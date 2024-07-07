using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RamsTrackerAPI.CustomActionFilter;
using RamsTrackerAPI.Models.Domain;
using RamsTrackerAPI.Models.DTO;
using RamsTrackerAPI.Repositories;

namespace RamsTrackerAPI.Controllers
{
    // /api/Subcontractor
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContractorRepository _subcontractorRepository;
        public ContractorController(IMapper mapper, IContractorRepository subcontractorRepository)
        {
            this._mapper = mapper;
            this._subcontractorRepository = subcontractorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var SubcontractorDomain = await _subcontractorRepository.GetAllAsync();

            // map and return Subcontractor DTO

            return Ok(_mapper.Map<List<ContractorDTO>>(SubcontractorDomain));

        }
        // POST: https://localhost:portnumber/api/Subcontractor
        [HttpPost]
        public async Task<IActionResult> AddSubcontractor([FromBody] AddContractorDTO addSubcontractorDTO)
        {
            // Mapp DTO to domain model
            var SubcontractorDomain = _mapper.Map<Contractor>(addSubcontractorDTO);


            // Use domain model to create new subcontractor 
            SubcontractorDomain = await _subcontractorRepository.AddSubcontractorAsync(SubcontractorDomain);

            // mapp domain to DTO
            var SubconDTO = _mapper.Map<ContractorDTO>(SubcontractorDomain);

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

            return Ok(_mapper.Map<ContractorDTO>(SubconDomain));
        }

        // PUT: https//localhost:portnumber/api/MS/{id}
        [HttpPut]
        //[Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        [ValidateModelAtribute]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ContractorDTO contractor)
        {
            // Map DTO to domain model

            var ContractorDomainModel = _mapper.Map<Contractor>(contractor);

            // Check if MS exist
            ContractorDomainModel = await _subcontractorRepository.Update(id, ContractorDomainModel);

            if (ContractorDomainModel == null)
            {
                return NotFound();
            }

            //Convert and return Domain Model to Dto

            return Ok(_mapper.Map<ContractorDTO>(ContractorDomainModel));
        }

        //Delete Region
        // DELETE: https://localhost:portnumber/api/Contractor/{id}
        [HttpDelete]
        //[Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var ContractorDomainModel = await _subcontractorRepository.Delete(id);

            if (ContractorDomainModel == null)
            {
                return NotFound();
            }

            // return deleted MS

            return Ok(_mapper.Map<ContractorDTO>(ContractorDomainModel));
        }

    }
}
