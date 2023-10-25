using System.Net;
using System.Net.Mail;
using VehicleOrganizer.Domain.Abstractions;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class EmailSenderService
    {
        private EmailSenderServiceSettings _settings;
        private SmtpClient _smtpClient;
        private MailAddress _baseMail;

        public EmailSenderService(EmailSenderServiceSettings settings)
        {
            _settings = settings;
            var values = _settings.SenderValues.Split('#');
            _baseMail = new MailAddress(values[1], Codes.AppName);
            _smtpClient = new SmtpClient(_settings.SmtpClientUrl)
            {
                Port = 587,
                Credentials = new NetworkCredential(values[1], values[0]),
                EnableSsl = true,
            };
        }

        public async Task SendEmailAsync(string subject, string body, string destinationEmail, string destinationVisibleName)
        {
            using var message = new MailMessage(_baseMail, new MailAddress(destinationEmail, destinationVisibleName))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
             await _smtpClient.SendMailAsync(message);
            
        }
    }
}
