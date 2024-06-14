namespace RamsTrackerAPI.Models.DTO
{
    public class RADTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public int revision { get; set; }
        public string? DownloadUrl { get; set; }
    }
}
