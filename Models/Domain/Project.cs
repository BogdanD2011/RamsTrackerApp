namespace RamsTrackerAPI.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public int ProjNumber { get; set; }
        public string ProjName { get; set; }
        public DateTime StartDate { get; set; }
        public int? Duration { get; set; }
    }
}
