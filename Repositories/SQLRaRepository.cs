using Microsoft.EntityFrameworkCore;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public class SQLRaRepository : IRaRepository
    {
        private readonly RamsDbContext _dbContext;
        public SQLRaRepository(RamsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public RamsDbContext DbContext { get; }

        public async Task<RA> CreateAsync(RA ra)
        {
            await _dbContext.RA.AddAsync(ra);
            await _dbContext.SaveChangesAsync();
            return ra;
        }

        public async Task<List<RA>> GetAllAsync()
        {
            return await _dbContext.RA.ToListAsync();
        }
    }
}
