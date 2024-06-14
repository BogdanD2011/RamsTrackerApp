using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public interface ISubcontractorRepository
    {
        Task <List<Subcontractor>> GetAllAsync();
        Task<Subcontractor> GetByIdAsync(Guid id);
        Task<Subcontractor> AddSubcontractorAsync(Subcontractor sub);
    }
}
