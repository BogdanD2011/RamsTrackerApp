using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public interface IHsPersonContactRepository
    {
        Task<List<HsPersonContact>> GetAllAsync();
        Task<HsPersonContact?> GetByIdAsync(Guid id);

        Task<HsPersonContact> CreateAsync(HsPersonContact Hs);

        Task<HsPersonContact?> UpdateAsync(Guid id, HsPersonContact Hs);
        Task<HsPersonContact?> DeleteAsync(Guid id);
    }
}
