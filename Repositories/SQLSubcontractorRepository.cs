using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public class SQLSubcontractorRepository : ISubcontractorRepository
    {
        private readonly RamsDbContext _dbContext;

        public SQLSubcontractorRepository(RamsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public RamsDbContext DbContext { get; }
        public async Task<List<Subcontractor>> GetAllAsync()
        {
            return await _dbContext.Subcontractor.ToListAsync();
        }
        public async Task<Subcontractor> AddSubcontractorAsync(Subcontractor sub)
        {
            await _dbContext.Subcontractor.AddAsync(sub);
            await _dbContext.SaveChangesAsync();
            return sub;
        }

        public async Task<Subcontractor?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Subcontractor.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
