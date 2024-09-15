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
    // /api/RA
    [Route("api/[controller]")]
    [ApiController]
    public class RAController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRaRepository _raRepository;

        public RAController(IMapper mapper, IRaRepository raRepository)
        {
            this._mapper = mapper;
            this._raRepository = raRepository;
        }
        // create RA
        //POST: /api/RA

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRaDTO addRaRequestDto)
        {
            // Map DTO to Domain Model
           var RaDomainModel = _mapper.Map<RA>(addRaRequestDto);

            await _raRepository.CreateAsync(RaDomainModel);

            // Map Domain model to DTO
            return Ok(_mapper.Map<RADTO>(RaDomainModel));
        }
        // Get all MS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Databas - Domain models
            var RAs = await _raRepository.GetAllAsync();


            //Map domain models to DTOs
            var MSsDto = _mapper.Map<List<MSDTO>>(RAs);

            // Return DTOs
            return Ok(MSsDto);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var RaDomain = await _raRepository.GetByIdAsync(id);
            if (RaDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HsPersonContactDTO>(RaDomain));
        }

        [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        [ValidateModelAtribute]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RADTO RaDTO )
        {
            // Map DTO to domain model

            var RaDomainModel = _mapper.Map<RA>(RaDTO);

            // Check if MS exist
            RaDomainModel = await _raRepository.UpdateAsync(id, RaDomainModel);

            if (RaDomainModel == null)
            {
                return NotFound();
            }

            //Convert and return Domain Model to Dto

            return Ok(_mapper.Map<RADTO>(RaDomainModel));
        }

        //Delete Region
        // DELETE: https://localhost:portnumber/api/MS/{id}
        [HttpDelete]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var RaDomainModel = await _raRepository.DeleteAsync(id);

            if (RaDomainModel == null)
            {
                return NotFound();
            }

            // return deleted MS as DTO

            return Ok(_mapper.Map<RADTO>(RaDomainModel));
        }

    }
}
