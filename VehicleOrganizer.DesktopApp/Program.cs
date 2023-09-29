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
        /// 


        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
          
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .Build();

            services.AddLogging(config =>
            {
                config.ClearProviders();
            });

            services.AddDbContext<DataBaseContext>();
            services.AddScoped<MainForm>();

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(form);
            }
        }

    }
}