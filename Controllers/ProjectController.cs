using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;
using RamsTrackerAPI.Models.DTO;
using RamsTrackerAPI.Repositories;

namespace RamsTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly RamsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectRepository _projectRepository;

        public ProjectController(RamsDbContext dbContext, IMapper mapper, 
            ILogger<ProjectController> logger, IProjectRepository projectRepository)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._logger = logger;
            this._projectRepository = projectRepository;
        }

        [HttpGet]
        //[Authorize(Roles = " Writer, Reader")]

        public async Task<IActionResult> GetAll()
        {
            //Fetch data from DB in to Domain model
            var Projects = await _projectRepository.GetAllAsync();

            // Map domains to DTO's
            var ProjectsDTO = _mapper.Map<List<Project>>(Projects);

            return Ok(ProjectsDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Fetch by id 
            var ProjectDomainModel = await _projectRepository.GetByIdAsync(id);

            if (ProjectDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProjectDTO>(ProjectDomainModel));
        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        //[ValidateModelAtribute]
        
        public async Task<IActionResult> Create([FromBody] ProjectDTO projectDTO)
        {
            if (ModelState.IsValid)
            {
                // From DTO to model
                var ProjectDomainModel = _mapper.Map<Project>(projectDTO);

                ProjectDomainModel = await _projectRepository.CreateAsync(ProjectDomainModel);

                // Reverse map to DTO
                var ProjectDto = _mapper.Map<Project>(projectDTO);

                return CreatedAtAction(nameof(GetById), new { id = ProjectDomainModel.Id }, ProjectDto);
            }
            return BadRequest(ModelState);
        }

    }
}
