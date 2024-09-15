using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace RamsTrackerAPI.Controllers
{
    [Route("api/[controler]")]
    [ApiController]
    public class DownloadFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DownloadFileController(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet, DisableRequestSizeLimit]
        [Route("DocumentsDownload")]
        public async Task<IActionResult> DocumentsDownload([FromQuery] string fileUrl)
        {
            var urlStrip = fileUrl.TrimStart(_webHostEnvironment.EnvironmentName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), urlStrip);
            if (!System.IO.File.Exists(filePath)) return NotFound();
            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), filePath);
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
