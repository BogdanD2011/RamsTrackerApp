using System.ComponentModel.DataAnnotations.Schema;

namespace RamsTrackerAPI.Models.Domain
{
    public class RA
    {
        public Guid Id { get; set; }
        public Guid ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; } 
        public string Title { get; set; }
        public int Revision { get; set; }
        public DateTime RevDate { get; set; }
        public string Status { get; set; }
    
        [NotMapped]
        public required IFormFile uploadFile { get; set; }

        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public string? ApprovedSheetPAth { get; set; }
        
    }
}
