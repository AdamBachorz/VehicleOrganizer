using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Tools.Html;
using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using BachorzLibrary.DAL.DotNetSix.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VehicleOrganizer.Core.Services;
using VehicleOrganizer.Core.Services.Interfaces;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Utils;
using VehicleOrganizer.Infrastructure;
using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Abstractions.Validators.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Services.Email;
using VehicleOrganizer.Infrastructure.Validators;
using VehicleOrganizer.Infrastructure.Extensions;

namespace VehicleOrganizer.Core.Config
{
    public static class DependencyInjection
    {
        public static void RegisterModules(IServiceCollection services, string configFileContent, Action<DbContextOptionsBuilder> options)
        {
            services.AddLogging(config =>
            {
                config.ClearProviders();
            });

            var config = JsonConvert.DeserializeObject<EFCCustomConfig>(configFileContent);
            services.AddInfrastructure(configFileContent, options);

            services.AddObjectMappingConfiguration(AutoMapperFixture.Create());
            services.AddScoped<IBackgroundActionInvokeService, BackgroundActionInvokeService>();
            services.AddScoped<IEmailService, EmailService>();

            

            services.AddTransient<IValidator<Vehicle, VehicleValidationCriteria>, VehicleValidator>();
            services.AddTransient<IValidator<OperationalActivity, OperationalActivityValidationCriteria>, OperationalActivityValidator>();

            services.AddTransient<HtmlHelper>();

            services.AddEmailSender(settings =>
            {
                settings.SenderHeader = Codes.AppName + EnvUtils.GetValueDependingOnEnvironment(" [DEV]",  string.Empty);
                settings.SenderValues = config.ValuesBag["Sender"] as string;
            });
        }
    }
}
