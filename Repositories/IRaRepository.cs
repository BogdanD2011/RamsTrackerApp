using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public interface IRaRepository
    {
        Task<RA> CreateAsync(RA ra);
        Task<List<RA>> GetAllAsync();
    }


}
