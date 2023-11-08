using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Tools.Html;
using BachorzLibrary.Common.Utils;
using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VehicleOrganizer.Core.Services;
using VehicleOrganizer.Core.Services.Interfaces;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Infrastructure;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Services.Email;
using VehicleOrganizer.Infrastructure.Validators;

namespace VehicleOrganizer.Core.Config
{
    public static class DependencyInjection
    {
        public static void RegisterModules(IServiceCollection service)
        {
            service.AddLogging(config =>
            {
                config.ClearProviders();
            });

            var configFile = EnvUtils.GetValueDependingOnEnvironment(Codes.Files.DevConfig, Codes.Files.ProdConfig);
            var config = JsonConvert.DeserializeObject<EFCCustomConfig>(File.ReadAllText(configFile));
            service.AddObjectMappingConfiguration(AutoMapperFixture.Create());
            service.AddSingleton<IEFCCustomConfig>(config);
            service.AddDbContext<DataBaseContext>();

            service.AddScoped<IBackgroundActionInvokeService, BackgroundActionInvokeService>();
            service.AddScoped<IEmailService, EmailService>();

            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IVehicleRepository, VehicleRepository>();
            service.AddTransient<IOperationalActivityRepository, OperationalActivityRepository>();

            service.AddTransient<IValidator<Vehicle>, VehicleValidator>();

            service.AddTransient<HtmlHelper>();

            service.AddEmailSenderService(settings =>
            {
                settings.SenderHeader = Codes.AppName;
                settings.SenderValues = config.ValuesBag["Sender"] as string;
            });
        }
    }
}
