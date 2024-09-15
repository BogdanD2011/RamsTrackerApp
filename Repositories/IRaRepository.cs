using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public interface IRaRepository
    {
        Task<List<RA>> GetAllAsync();
        Task<RA?> GetByIdAsync(Guid id);

        Task<RA> CreateAsync(RA Ra);

        Task<RA?> UpdateAsync(Guid id, RA Ra);
        Task<RA?> DeleteAsync(Guid id);
    }


}
