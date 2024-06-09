namespace RamsTrackerAPI.Models.Domain
{
    public class RA
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public int revision { get; set; }
        public string? DownloadUrl { get; set; }

        
    }
}
