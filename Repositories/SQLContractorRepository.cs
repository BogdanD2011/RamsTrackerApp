using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public class SQLContractorRepository : IContractorRepository
    {
        private readonly RamsDbContext _dbContext;

        public SQLContractorRepository(RamsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public RamsDbContext DbContext { get; }
        public async Task<List<Contractor>> GetAllAsync()
        {
            return await _dbContext.Contractor.ToListAsync();
        }
        public async Task<Contractor> AddSubcontractorAsync(Contractor sub)
        {
            await _dbContext.Contractor.AddAsync(sub);
            await _dbContext.SaveChangesAsync();
            return sub;
        }

        public async Task<Contractor?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Contractor.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Contractor> Delete(Guid id)
        {
            var ContractorDomainModel = await _dbContext.Contractor.FirstOrDefaultAsync(x => x.Id == id);
            if (ContractorDomainModel == null)
            {
                return null;
            }
            _dbContext.Remove(ContractorDomainModel);
            await _dbContext.SaveChangesAsync();
            return ContractorDomainModel;
        }

        public async Task<Contractor> Update(Guid id, Contractor contractor)
        {
            var ContractorDomainModel = await _dbContext.Contractor.FirstOrDefaultAsync(x => x.Id == id);
            if (ContractorDomainModel == null)
            {
                return null;
            }
            ContractorDomainModel.Name = contractor.Name;
            ContractorDomainModel.Activity = contractor.Activity;
            ContractorDomainModel.HsPersId = contractor.HsPersId;
            ContractorDomainModel.HsPersonContact = contractor.HsPersonContact;

            await _dbContext.SaveChangesAsync();
            return ContractorDomainModel;


        }
    }
}
