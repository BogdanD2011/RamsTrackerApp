using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RamsTrackerAPI.CustomActionFilter;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;
using RamsTrackerAPI.Models.DTO;
using RamsTrackerAPI.Repositories;

namespace RamsTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HsPersonContactController : ControllerBase
    {
        private readonly RamsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHsPersonContactRepository _hsPersonContactRepository;

        public HsPersonContactController(RamsDbContext dbContext, IMapper mapper, 
            IHsPersonContactRepository hsPersonContactRepository)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._hsPersonContactRepository = hsPersonContactRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var HsPerson = await _hsPersonContactRepository.GetAllAsync();
            return Ok(_mapper.Map<List<HsPersonContactDTO>>(HsPerson));
        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        //[ValidateModelAtribute]
        public async Task<IActionResult> Create([FromBody] HsPersonContactDTO hsPerson)
        {
            if (ModelState.IsValid)
            {
                // Map  DTO to Domain Model
                var HsPersDomainModel = _mapper.Map<HsPersonContact>(hsPerson);


                // Use Domain Model to create MS
                HsPersDomainModel = await _hsPersonContactRepository.CreateAsync(HsPersDomainModel);

                // Map domain model back to DTO
                var HsPersDto = _mapper.Map<HsPersonContactDTO>(HsPersDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = HsPersDomainModel.Id }, HsPersDto);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var HsPersDomain = await _hsPersonContactRepository.GetByIdAsync(id);
            if(HsPersDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HsPersonContactDTO>(HsPersDomain));
        }

        // PUT: https//localhost:portnumber/api/MS/{id}
        [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        [ValidateModelAtribute]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] HsPersonContactDTO hsPersonContact)
        {
            // Map DTO to domain model

            var HsPersDomainModel = _mapper.Map<HsPersonContact>(hsPersonContact);

            // Check if MS exist
            HsPersDomainModel = await _hsPersonContactRepository.UpdateAsync(id, HsPersDomainModel);

            if (HsPersDomainModel == null)
            {
                return NotFound();
            }

            //Convert and return Domain Model to Dto

            return Ok(_mapper.Map<HsPersonContactDTO>(HsPersDomainModel));
        }

        //Delete Region
        // DELETE: https://localhost:portnumber/api/MS/{id}
        [HttpDelete]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var HsPersDomainModel = await _hsPersonContactRepository.DeleteAsync(id);

            if (HsPersDomainModel == null)
            {
                return NotFound();
            }

            // return deleted MS

            return Ok(_mapper.Map<HsPersonContactDTO>(HsPersDomainModel));
        }
    }

}

