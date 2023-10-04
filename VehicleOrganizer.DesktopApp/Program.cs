using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Domain.Abstractions.Config;

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
          
            var service = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .Build();

            DependencyInjection.RegisterModules(service);
            using (ServiceProvider serviceProvider = service.BuildServiceProvider())
            {
                var form = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(form);
            }
        }

    }
}