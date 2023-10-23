using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Infrastructure.Services.Email
{
    public class EmailSenderService
    {
        private SmtpClient _smtpClient;
        private IEFCCustomConfig _customConfig;
        private MailAddress _baseMail;

        public EmailSenderService(IEFCCustomConfig customConfig)
        {
            _customConfig = customConfig;
            var values = ((string)_customConfig.ValuesBag["Sender"]).Split('#');
            _baseMail = new MailAddress(values[1], Codes.AppName);
            _smtpClient = new SmtpClient("smtp.poczta.onet.pl")
            {
                Port = 587,
                Credentials = new NetworkCredential(values[1], values[0]),
                EnableSsl = true,
            };
        }

        public async Task SendEmail(string subject, string body)
        {
            using var message = new MailMessage(_baseMail, new MailAddress(User.Default.Email, User.Default.Name))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
             await _smtpClient.SendMailAsync(message);
            
        }
    }
}
