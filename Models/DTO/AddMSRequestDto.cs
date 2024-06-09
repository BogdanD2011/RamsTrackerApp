using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Models.DTO
{
    // DTO for adding Ms
    public class AddMSRequestDto
    {
        public string MS_Title { get; set; }
        public int revision { get; set; }
        public Guid SubcontractorId { get; set; }
        public Guid RAid { get; set; }
        public Subcontractor Subcontractor { get; set; }
        public RA RA { get; set; }
    }
}
