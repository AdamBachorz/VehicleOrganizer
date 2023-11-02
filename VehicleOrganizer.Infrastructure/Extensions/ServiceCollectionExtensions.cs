using BachorzLibrary.DAL;
using Microsoft.Extensions.DependencyInjection;
using VehicleOrganizer.Infrastructure.Services.Email;

namespace VehicleOrganizer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEmailSenderService(this IServiceCollection services, ICustomConfig config, Action<EmailSenderSettings> settingsInvoker)
        {
            var senderValues = config.ValuesBag["Sender"] as string;
            var settings = new EmailSenderSettings();
            settingsInvoker(settings);
            settings.SenderValues = senderValues;
            var emailSenderService = new EmailSender(settings);

            services.AddSingleton(emailSenderService);
        }
    }
}
