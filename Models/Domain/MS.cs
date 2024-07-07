using System.ComponentModel.DataAnnotations.Schema;

namespace RamsTrackerAPI.Models.Domain
{
    public class MS
    {
        public Guid Id { get; set; }
        public Guid ContractorId { get; set; }
        //public virtual Contractor Contractor { get; set; }

        [ForeignKey("RaId")]
        public Guid? RaId { get; set; }
        //public virtual RA RA { get; set; }
        public string MsTitle { get; set; }
        public Guid ProjectId { get; set; }
        public int revision { get; set; }
        public DateTime RevDate { get; set; }
        public string Status { get; set; }
        
        [NotMapped]
        public required IFormFile uploadFile { get; set; }

        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public string? ApprovedSheetPath {  get; set; }


   

        
        
    }
}
