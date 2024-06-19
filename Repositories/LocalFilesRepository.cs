using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public class LocalFilesRepository : IFilesRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RamsDbContext _dbContext;

        public LocalFilesRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            RamsDbContext dbContext)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            this._dbContext = dbContext;
        }
        public async Task<Files> Upload(Files files)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles",
               files.uploadFile.FileName);

            // Upload File to Lopcal Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await files.uploadFile.CopyToAsync(stream);

            // https://localhost:portnumber/uploadedFiles/fname.pdf

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/UploadedFiles/{files.FileName}{files.FileExtension}";

            files.FilePath = urlFilePath;

            // Add files to files table.
            await _dbContext.Files.AddAsync(files);
            await _dbContext.SaveChangesAsync();

            return files;
        }
    }
}
