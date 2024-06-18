using RamsTrackerAPI.Models.Domain;
using System.Net;

namespace RamsTrackerAPI.Repositories
{
    public interface IFilesRepository
    {
        Task<Files> Upload(Files files);
    }
}
