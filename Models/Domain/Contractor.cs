namespace RamsTrackerAPI.Models.Domain
{
    public class Contractor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Activity { get; set; }
        public Guid HsPersId { get; set; }
        public virtual HsPersonContact HsPersonContact { get; set; }
    }
}
