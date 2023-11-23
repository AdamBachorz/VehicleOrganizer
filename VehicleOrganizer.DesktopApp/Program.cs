using Microsoft.Extensions.DependencyInjection;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Core.Config;
using VehicleOrganizer.Core.Services.Interfaces;
using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.DesktopApp.Panels;

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
            ApplicationConfiguration.Initialize();
          
            var service = new ServiceCollection();

            DependencyInjection.RegisterModules(service);
            RegisterFormsAndPanels(service);

            using (ServiceProvider serviceProvider = service.BuildServiceProvider())
            {
                var vehicleRepository = serviceProvider.GetRequiredService<IVehicleRepository>();
                var backgroundActionInvokeService = serviceProvider.GetRequiredService<IBackgroundActionInvokeService>();

                await backgroundActionInvokeService.InvokeAllAsync();

                var vehiclesForUser = await vehicleRepository.GetVehiclesForUserAsync(User.Default, includeSold: true);
                var userHasVehicle = vehiclesForUser.IsNotNullOrEmpty();

                var mainForm = serviceProvider.GetRequiredService<MainForm>();
                
                if (!userHasVehicle)
                {
                    MessageBox.Show("Brak pojazdów. Dodaj nowy teraz!");
                    var addVehicleForm = serviceProvider.GetRequiredService<AddOrEditVehicleForm>();
                    addVehicleForm.Init(mainForm, vehicle: null);
                    addVehicleForm.ShowDialog();
                }
                else
                {
                    var pickVehicleForm = serviceProvider.GetRequiredService<PickVehicleForm>();
                    pickVehicleForm.Init(vehiclesForUser);
                    pickVehicleForm.ShowDialog();
                }

                Application.Run(mainForm);
            }
        }

        private static void RegisterFormsAndPanels(IServiceCollection service)
        {
            service.AddScoped<MainForm>();
            service.AddScoped<AddOrEditVehicleForm>();
            service.AddScoped<AddOrEditOperationalActivityForm>();
            service.AddScoped<PickVehicleForm>();

            service.AddScoped<OperationalActivityPanel>();
        }
    }
}