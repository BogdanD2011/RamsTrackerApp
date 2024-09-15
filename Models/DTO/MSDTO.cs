using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Models.DTO
{
    // DTO for full MS model
    public class MSDTO
    {
        public Guid Id { get; set; }
        public Guid? RaId { get; set; }
        public string? MsTitle { get; set; }
        public int revision { get; set; }

        public DateTime RevDate { get; set; }

        public string? Subcontractor { get; set; }

        public string? Status { get; set; }
        public string? FilePath { get; set; }
        public string? ApprovedSheetPath { get; set; }


    }
}
