using Microsoft.Extensions.DependencyInjection;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Entities;
using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.Core.Config;

namespace VehicleOrganizer.DesktopApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 


        [STAThread]
        static async Task Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
          
            var service = new ServiceCollection();
            //var configuration = new ConfigurationBuilder()
            //    .Build();

            DependencyInjection.RegisterModules(service);
            RegisterForms(service);

            using (ServiceProvider serviceProvider = service.BuildServiceProvider())
            {
                var vehicleRepository = serviceProvider.GetRequiredService<IVehicleRepository>();

                var vehicles = await vehicleRepository.GetVehiclesForUser(User.Default);

                //if (vehicles.IsNullOrEmpty())
                //{
                //    MessageBox.Show("Brak pojazdów");
                //}

                var form = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(form);
            }
        }

        private static void RegisterForms(IServiceCollection service)
        {
            service.AddScoped<MainForm>();
        }
    }
}