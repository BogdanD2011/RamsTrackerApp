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
using RamsTrackerAPI.CustomActionFilter;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.IO;
using System;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Identity;
using RamsTrackerAPI.Data.Account;

namespace RamsTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MSController : ControllerBase
    {
        private readonly RamsDbContext dbContext;
        private readonly IMSRepository _MSRepository;
        private readonly IMapper mapper;
        private readonly ILogger<MSController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _usermanager;
        private readonly IContractorRepository _contractorRepository;

        public MSController(RamsDbContext dbcontext, IMSRepository MSRepository, IMapper mapper,
            ILogger<MSController> logger, IHttpContextAccessor httpContextAccessor, UserManager<User> usermanager,
            IContractorRepository contractorRepository)
        {
            this.dbContext = dbcontext;
            this._MSRepository = MSRepository;
            this.mapper = mapper;
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessor;
            this._usermanager = usermanager;
            this._contractorRepository = contractorRepository;
        }
        // Get all MS
        [HttpGet]
        [Route("custom")]
        //[Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> GetAll ([FromQuery] Guid id, [FromQuery] string role)
        {
            Guid id_check = Guid.Empty;
            if (role == "Subcontractor")
                id_check = id;
            // Get Data From Databas - Domain models
            var MSs = await _MSRepository.GetAllAsync(id_check);

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
            var ContractorName = await _contractorRepository.GetByIdAsync(id_check);
            //Map domain models to DTOs
            var MSsDto = mapper.Map<List<MSDTO>>(MSs, opt => opt.Items["Subcontractor"] = ContractorName.Name); 

            // Return DTOs
            return Ok(MSsDto);

        }
        [HttpGet]
        [Route("countrams")]

        public async Task<IActionResult> countRams()
        {
            var countTotal = await  _MSRepository.CountRams();

            return Ok(countTotal);
        }
        //Get single MS by id
        // GET: https//localhost:portnumber/api/MS/{id}
        [HttpGet]
        //[Authorize(Roles ="")]
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

            var ContractorId = MSDomain.ContractorId;
            var ContractorName = await _contractorRepository.GetByIdAsync(ContractorId);
           
            // Convert and return model domain to DTO

            return Ok(mapper.Map<MSDTO>(MSDomain, opt => opt.Items["Subcontractor"] = ContractorName.Name));
        }

        // POST: https//localhost:portnumber/api/MS
        [HttpPost]
        [Consumes("multipart/form-data")]
        //[Authorize(Roles = "Writer")]
        //[ValidateModelAtribute]
        public async Task<IActionResult> Create([FromForm] AddMSRequestDto addMSRequestDto)
        {
            ValidateFileUpload(addMSRequestDto);


            if (ModelState.IsValid) 
            {
                // Map  DTO to Domain Model
            var MSDomainModel = mapper.Map<MS>(addMSRequestDto);
                      

            // Use Domain Model to create MS
            MSDomainModel = await _MSRepository.CreateAsync(MSDomainModel);

            // Map domain model back to DTO
            var MsDto = mapper.Map<MSDTO>(MSDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = MSDomainModel.Id }, MsDto);
        } 
        return BadRequest(ModelState);
        }

        private void ValidateFileUpload(AddMSRequestDto request)
        {
            var allowedExtension = new string[] { ".pdf", ".doc", ".jpg", "jpeg" };

            if (!allowedExtension.Contains(Path.GetExtension(request.uploadFile.FileName)))
            {
                ModelState.AddModelError("file", "Unsuported file extension");

            }
            if (request.uploadFile.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10Mb");
            }
        }

            // PUT: https//localhost:portnumber/api/MS/{id}
            [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        [ValidateModelAtribute]
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
        [Authorize(Roles = "Writer")]
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
