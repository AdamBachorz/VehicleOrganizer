using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Infrastructure.Entities;

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

            var services = new ServiceCollection();
            ConfigureServices(services);

            var ut = User.Default;
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {

                var form = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(form);
            }
        }

        static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.ClearProviders();
            });


        }
    }
}