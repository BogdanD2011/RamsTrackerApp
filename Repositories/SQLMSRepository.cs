using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public class SQLMSRepository : IMSRepository
    {
        private readonly RamsDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SQLMSRepository(RamsDbContext dbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
        }

        public RamsDbContext DbContext { get; }

        public async Task<RamsCount> CountRams()
        {
           var total = await _dbContext.MS.CountAsync();
           var total_ap = await _dbContext.MS.CountAsync(t => t.MsTitle == "Pouring concrete");

            var countedRams = new RamsCount
            {
                total = total,
                total_ap = total_ap,
            };

            return countedRams;
  
        }

        public async Task<MS> CreateAsync(MS Ms)
        {
            // Create local path for MS - in UploadedFiles/MS
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles/MS",
                   Ms.uploadFile.FileName);

            // Upload File to Lopcal Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await Ms.uploadFile.CopyToAsync(stream);

            // https://localhost:portnumber/uploadedFiles/fname.pdf

            Ms.Status = "Pending Review";
            Ms.FileName = Ms.uploadFile.FileName;
            Ms.FileExtension = Path.GetExtension(Ms.uploadFile.FileName);
            Ms.FileSizeInBytes = Ms.uploadFile.Length;
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/UploadedFiles/MS/{Ms.uploadFile.FileName}";

            Ms.FilePath = urlFilePath;

            // Add MS to Ms table.

            await _dbContext.MS.AddAsync(Ms);
            await _dbContext.SaveChangesAsync();
            return Ms;
        }
        public async Task<MS?> DeleteAsync(Guid id)
        {
           var existingMs = await _dbContext.MS.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMs == null)
            {
                return null;
            }
            _dbContext.MS.Remove(existingMs);
            await _dbContext.SaveChangesAsync();
            return existingMs;
        }

        public async Task<List<MS>> GetAllAsync(Guid id)
        {
            //return full list if user is Contractor
            if (id == Guid.Empty)
                return await _dbContext.MS.ToListAsync();
            else
                return await _dbContext.MS.Where(m => m.ContractorId == id).ToListAsync();
        }

        public async Task<MS?> GetByIdAsync(Guid id)
        {
            return await  _dbContext.MS.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MS?> UpdateAsync(Guid id, MS Ms)
        {
           var existingMs = await _dbContext.MS.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMs == null)
            {
                return null;
            }
            existingMs.revision = Ms.revision;

            await _dbContext.SaveChangesAsync();   

            return existingMs;
        }

    }
}
