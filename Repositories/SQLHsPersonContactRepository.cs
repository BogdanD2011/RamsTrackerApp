using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public class SQLHsPersonContactRepository : IHsPersonContactRepository
    {
        private readonly RamsDbContext _dbContext;

        public SQLHsPersonContactRepository(RamsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<HsPersonContact> CreateAsync(HsPersonContact HsPers)
        {
            await _dbContext.HsPersonContacts.AddAsync(HsPers);
            await _dbContext.SaveChangesAsync();
            return HsPers;
            
        }

        public async Task<HsPersonContact?> DeleteAsync(Guid id)
        {
            var HsPers = await _dbContext.HsPersonContacts.FirstOrDefaultAsync(x => x.Id == id);
            if (HsPers == null)
            { 
                return null;
            }
             _dbContext.HsPersonContacts.Remove(HsPers);
            await _dbContext.SaveChangesAsync();
            return HsPers;
        }

        public async Task<List<HsPersonContact>> GetAllAsync()
        {
            return await _dbContext.HsPersonContacts.ToListAsync();
        }

        public async Task<HsPersonContact?> GetByIdAsync(Guid id)
        {
            return await _dbContext.HsPersonContacts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<HsPersonContact?> UpdateAsync(Guid id, HsPersonContact HsPers)
        {
            var HsPersToUpdate = await _dbContext.HsPersonContacts.FirstOrDefaultAsync(x => x.Id == id);
            if (HsPersToUpdate == null)
            {
                return null;
            }

            HsPersToUpdate.FirstName = HsPers.FirstName;
            HsPersToUpdate.LastName = HsPers.LastName;
            HsPersToUpdate.Email = HsPers.Email;
            HsPersToUpdate.Phone = HsPers.Phone;
           
            await _dbContext.SaveChangesAsync();
            return HsPersToUpdate;
        }
    }
}
