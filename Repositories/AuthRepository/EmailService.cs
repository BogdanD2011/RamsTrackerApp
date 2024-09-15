
using System.Net.Mail;
using System.Net;

namespace RamsTrackerAPI.Repositories.AuthRepository
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            this._config = config;
        }
        public Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML)
        {
            string? MailServer = _config["EmailSettings:MailServer"];
            string? FromEmail = _config["EmailSettings:FromEmail"];
            string? Password = _config["EmailSettings:Password"];
            int Port = int.Parse(_config["EmailSettings:MailPort"]);
            var client = new SmtpClient(MailServer, Port)
            {
                Credentials = new NetworkCredential("7ae641001@smtp-brevo.com", "FVaNmvpD5TGMLAb9"),
                EnableSsl = true,
            };
            MailMessage mailMessage = new MailMessage("info@bogdansprojects.in", toEmail, subject, body)
            {
                IsBodyHtml = isBodyHTML
            };
            return client.SendMailAsync(mailMessage);

        }
    }
}
