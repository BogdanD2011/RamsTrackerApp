namespace RamsTrackerAPI.Repositories.AuthRepository
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML);
    }
}
