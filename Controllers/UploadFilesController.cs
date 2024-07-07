using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RamsTrackerAPI.Models.Domain;
using RamsTrackerAPI.Models.DTO;
using RamsTrackerAPI.Repositories;

namespace RamsTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFilesController : ControllerBase
    {
        private readonly IFilesRepository _filesRepository;

        public UploadFilesController(IFilesRepository filesRepository)
        {
            this._filesRepository = filesRepository;
        }
        //// POST: /api/UploadFiles/Upload
        ////[HttpPost]
        ////[Route("Upload")]
        ////public async Task<IActionResult> Upload([FromForm] FilesUploadRequestDTO request)
        ////{
        ////    ValidateFileUpload(request);

        ////    if (ModelState.IsValid)
        ////    {
        ////        //convert DTO to Domain model
        ////        var fileDomainModel = new Files
        ////        {
        ////            uploadFile = request.uploadFile,
        ////            FileName = request.FileName,
        ////            FileExtension = request.FileExtension,
        ////            FileDescription = request.FileDescription
        ////        };
        ////        // User repository to upload file
        ////        await _filesRepository.Upload(fileDomainModel);
        ////        return Ok(fileDomainModel);
        ////    }

        ////    return BadRequest(ModelState);
        ////}

        private void ValidateFileUpload(FilesUploadRequestDTO request)
        {
            var allowedExtension = new string[] { ".pdf", ".doc", ".jpg", "jpeg" };

            if (!allowedExtension.Contains(Path.GetExtension(request.uploadFile.FileName))) 
            {
                ModelState.AddModelError("file", "Unsuported file extension");
                
            }
            if (request.uploadFile.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10Mb");

            }
        }
    }
}
