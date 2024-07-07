using Microsoft.Identity.Client;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public interface IMSRepository
    {
       Task<List<MS>> GetAllAsync();
        Task<MS?> GetByIdAsync(Guid id);

        Task<MS> CreateAsync(MS Ms);

        Task<MS?> UpdateAsync(Guid id, MS Ms);
        Task<MS?> DeleteAsync(Guid id);
        Task<RamsCount> CountRams();

    }
}
