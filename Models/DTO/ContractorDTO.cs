using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Models.DTO
{
    public class ContractorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Activity { get; set; }
        public Guid HsPersId { get; set; }
        public virtual HsPersonContact HsPersonContact { get; set; }

    }
}
