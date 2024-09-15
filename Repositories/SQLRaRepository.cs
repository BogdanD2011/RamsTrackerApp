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

        public async Task<RA> CreateAsync(RA ra)
        {
            await _dbContext.RA.AddAsync(ra);
            await _dbContext.SaveChangesAsync();
            return ra;
        }

        public async Task<RA?> DeleteAsync(Guid id)
        {
            var CurentRa = await _dbContext.RA.FirstOrDefaultAsync(x => x.Id == id);
            if (CurentRa == null)
            {
                return null;
            }
            _dbContext.Remove(CurentRa);
            await _dbContext.SaveChangesAsync();
            return CurentRa;
        }

        public async Task<List<RA>> GetAllAsync()
        {
            return await _dbContext.RA.ToListAsync();
        }

        public async Task<RA?> GetByIdAsync(Guid id)
        {
            return await _dbContext.RA.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RA?> UpdateAsync(Guid id, RA Ra)
        {
            var CurentRa = await _dbContext.RA.FirstOrDefaultAsync(x => x.Id == id);
            if (CurentRa == null)
            {
                return null;
            }
            CurentRa.Revision = Ra.Revision;
            CurentRa.RevDate = Ra.RevDate;
            await _dbContext.SaveChangesAsync();
            return CurentRa;
        }
    }
}
