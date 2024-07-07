using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public interface IHsPersonContactRepository
    {
        Task<List<HsPersonContact>> GetAllAsync();
        Task<HsPersonContact?> GetByIdAsync(Guid id);

        Task<HsPersonContact> CreateAsync(HsPersonContact Ms);

        Task<HsPersonContact?> UpdateAsync(Guid id, HsPersonContact Ms);
        Task<HsPersonContact?> DeleteAsync(Guid id);
    }
}
