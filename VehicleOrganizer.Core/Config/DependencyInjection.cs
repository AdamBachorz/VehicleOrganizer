﻿using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using BachorzLibrary.Web.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Utils;
using VehicleOrganizer.Infrastructure;
using VehicleOrganizer.Infrastructure.Extensions;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

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

            service.AddEmailSenderService(config, settings =>
            {
                settings.SmtpClientUrl = "smtp.poczta.onet.pl";
                settings.SenderEmail = "adar_1@op.pl";
                settings.SenderHeader = Codes.AppName;
            });
        }
    }
}
