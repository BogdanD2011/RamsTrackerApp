using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;
using RamsTrackerAPI.Models.DTO;
using RamsTrackerAPI.Repositories;

namespace RamsTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MSController : ControllerBase
    {
        private readonly RamsDbContext dbContext;
        private readonly IMSRepository _MSRepository;
        private readonly IMapper mapper;

        public MSController(RamsDbContext dbcontext, IMSRepository MSRepository, IMapper mapper)
        {
            this.dbContext = dbcontext;
            this._MSRepository = MSRepository;
            this.mapper = mapper;
        }
        // Get all MS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Databas - Domain models
            var MSs = await _MSRepository.GetAllAsync();

            // Map Domain Models to DTOs
            //var MSsDto = new List<MSDTO>();
            //foreach (var MS in MSs)
            //{
            //    MSsDto.Add(new MSDTO()
            //    {
            //        Id = MS.Id,
            //        MS_Title = MS.MS_Title,
            //        revision = MS.revision,
            //        SubcontractorId = MS.SubcontractorId,
            //        RAid = MS.RAid,
            //        Subcontractor = MS.Subcontractor,
            //        RA = MS.RA,

            //    });
            //}


            //Map domain models to DTOs
            var MSsDto = mapper.Map<List<MSDTO>>(MSs);

            // Return DTOs
            return Ok(MSsDto);

        }

        //Get single MS by id
        // GET: https//localhost:portnumber/api/MS/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get domain model from database
            // var MS = dbContext.MS.Find(id); --as alternative but only work to find by PK
            var MSDomain = await _MSRepository.GetByIdAsync(id);

            if (MSDomain == null)
            {
                return NotFound();
            }

            // Convert and return model domain to DTO

            return Ok(mapper.Map<MSDTO>(MSDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddMSRequestDto addMSRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var MSDomainModel = mapper.Map<MS>(addMSRequestDto);
           

            // Use Domain Model to create MS
            MSDomainModel = await _MSRepository.CreateAsync(MSDomainModel);

            // Map domain model back to DTO
            var MsDto = mapper.Map<MSDTO>(MSDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = MSDomainModel.Id }, MsDto);
        }

        // PUT: https//localhost:portnumber/api/MS/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMSRequestDto updateMSRequestDto)
        {
            // Map DTO to domain model

            var MsDomainModel = mapper.Map<MS>(updateMSRequestDto);
            
            // Check if MS exist
            MsDomainModel = await _MSRepository.UpdateAsync(id, MsDomainModel);

            if (MsDomainModel == null)
            {
                return NotFound();
            }

            //Convert and return Domain Model to Dto

            return Ok(mapper.Map<MSDTO>(MsDomainModel));
        }

        //Delete Region
        // DELETE: https://localhost:portnumber/api/MS/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var MSDomainModel = await _MSRepository.DeleteAsync(id);

            if (MSDomainModel == null)
            {
                return NotFound();
            }

            // return deleted MS

            return Ok(mapper.Map<MSDTO>(MSDomainModel));
        }
    }
}
