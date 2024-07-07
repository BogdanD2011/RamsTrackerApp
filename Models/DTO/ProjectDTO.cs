namespace RamsTrackerAPI.Models.DTO
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public int ProjNumber { get; set; }
        public string ProjName { get; set; }
        public DateTime StartDate { get; set; }
        public int? Duration { get; set; }
    }
}
