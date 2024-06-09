using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public class SQLMSRepository : IMSRepository
    {
        private readonly RamsDbContext _dbContext;
        public SQLMSRepository(RamsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public RamsDbContext DbContext { get; }

        public async Task<MS> CreateAsync(MS Ms)
        {
            await _dbContext.MS.AddAsync(Ms);
            await _dbContext.SaveChangesAsync();
            return Ms;
        }

        public async Task<MS?> DeleteAsync(Guid id)
        {
           var existingMs = await _dbContext.MS.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMs == null)
            {
                return null;
            }
            _dbContext.MS.Remove(existingMs);
            await _dbContext.SaveChangesAsync();
            return existingMs;
        }

        public async Task<List<MS>> GetAllAsync()
        {
            return await _dbContext.MS.ToListAsync();
        }

        public async Task<MS?> GetByIdAsync(Guid id)
        {
            return await  _dbContext.MS.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MS?> UpdateAsync(Guid id, MS Ms)
        {
           var existingMs = await _dbContext.MS.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMs == null)
            {
                return null;
            }
            existingMs.revision = Ms.revision;

            await _dbContext.SaveChangesAsync();   

            return existingMs;
        }
    }
}
