using System.ComponentModel.DataAnnotations;

namespace RamsTrackerAPI.Models.DTO
{
    public class FilesUploadRequestDTO
    {
        [Required]
        public IFormFile uploadFile { get; set; }
        [Required]
        public string  FileName { get; set; }
        [Required]
        public string FileExtension { get; set; }
        public string? FileDescription { get; set; }
    }
}
