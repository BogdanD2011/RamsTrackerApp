using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper mapper;
        private readonly IRaRepository raRepository;

        public RAController(IMapper mapper, IRaRepository raRepository)
        {
            this.mapper = mapper;
            this.raRepository = raRepository;
        }
        // create RA
        //POST: /api/RA

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRaDTO addRaRequestDto)
        {
            // Map DTO to Domain Model
           var RaDomainModel = mapper.Map<RA>(addRaRequestDto);

            await raRepository.CreateAsync(RaDomainModel);

            // Map Domain model to DTO
            return Ok(mapper.Map<RADTO>(RaDomainModel));
        }
        // Get all MS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Databas - Domain models
            var RAs = await raRepository.GetAllAsync();


            //Map domain models to DTOs
            var MSsDto = mapper.Map<List<MSDTO>>(RAs);

            // Return DTOs
            return Ok(MSsDto);

        }

        //
    }
}
