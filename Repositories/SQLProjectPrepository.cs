using Microsoft.EntityFrameworkCore;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public class SQLProjectPrepository : IProjectRepository
    {
        private readonly RamsDbContext _dbContext;

        public SQLProjectPrepository(RamsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Project> CreateAsync(Project project)
        {
            await _dbContext.AddAsync(project);
            await _dbContext.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> DeleteAsync(Guid id)
        {
            var existingProject = await _dbContext.Project.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProject == null) 
            {
                return null;
            }
            _dbContext.Project.Remove(existingProject);
            await _dbContext.SaveChangesAsync();
            return existingProject;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Project.ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Project.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Project?> UpdateAsync(Guid id, Project project)
        {
            throw new NotImplementedException();
        }
    }
}
