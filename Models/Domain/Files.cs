using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RamsTrackerAPI.Models.Domain
{
    public class Files
    {
        public Guid Id { get; set; }

        [NotMapped]
        public required IFormFile uploadFile { get; set; }

        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }

    }
}
