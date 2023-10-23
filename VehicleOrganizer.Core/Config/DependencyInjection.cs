using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using BachorzLibrary.Web.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Utils;
using VehicleOrganizer.Infrastructure;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Services.Email;

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

            var configFile = FileUtils.GetProperFileByEnv(Codes.Files.DevConfig, Codes.Files.ProdConfig);
            var config = JsonConvert.DeserializeObject<EFCCustomConfig>(File.ReadAllText(configFile));
            service.AddObjectMappingConfiguration(AutoMapperFixture.Create());
            service.AddSingleton<IEFCCustomConfig>(config);
            service.AddDbContext<DataBaseContext>();

            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IVehicleRepository, VehicleRepository>();
            service.AddTransient<IOperationalActivityRepository, OperationalActivityRepository>();

            service.AddTransient<EmailSenderService>();
        }
    }
}
