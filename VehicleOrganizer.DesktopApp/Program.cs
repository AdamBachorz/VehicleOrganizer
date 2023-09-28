using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Reflection;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Infrastructure;
using VehicleOrganizer.Infrastructure.Entities;
using NHibernate.Cfg;
using Xceed.Document.NET;

namespace VehicleOrganizer.DesktopApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            string environment;
#if DEBUG
            environment = "Development";
#else
            environment = "Production";           
#endif
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

            ConfigureServices(services, configuration);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form = serviceProvider.GetRequiredService<MainForm>();
                form.SetLabel(environment);
                Application.Run(form);
            }
        }

        static void ConfigureServices(ServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddLogging(config =>
            {
                config.ClearProviders();
            });

            AddDbContextWithConfig(services, configuration);

            services.AddScoped<MainForm>();
        }

        static void AddDbContextWithConfig(ServiceCollection services, IConfigurationRoot configuration)
        {
            var config = configuration.GetSection(nameof(EFCCustomConfig)).Get<EFCCustomConfig>();

            services.AddDbContext<DataBaseContext>(options =>
            {
                if (config.IsProduction)
                {
                    options.UseNpgsql(config.ConnectionString);
                }
                else
                {
                    options.UseSqlite(config.ConnectionString);
                }
            });

            services.AddSingleton<IEFCCustomConfig>(config);
        }
    }
}