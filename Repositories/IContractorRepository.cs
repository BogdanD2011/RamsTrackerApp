using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public interface IContractorRepository
    {
        Task <List<Contractor>> GetAllAsync();
        Task<Contractor> GetByIdAsync(Guid id);
        Task<Contractor> AddSubcontractorAsync(Contractor constractor);
        Task<Contractor> Delete(Guid id);
        Task <Contractor> Update(Guid id, Contractor contractor);
    }
}
