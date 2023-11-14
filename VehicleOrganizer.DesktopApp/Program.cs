using Microsoft.Extensions.DependencyInjection;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Core.Config;
using BachorzLibrary.Common.Tools.Email;
using VehicleOrganizer.Core.Services.Interfaces;

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
            RegisterForms(service);

            using (ServiceProvider serviceProvider = service.BuildServiceProvider())
            {
                var vehicleRepository = serviceProvider.GetRequiredService<IVehicleRepository>();
                var backgroundActionInvokeService = serviceProvider.GetRequiredService<IBackgroundActionInvokeService>();

                await backgroundActionInvokeService.InvokeAllAsync();

                var userHasVehicle = vehicleRepository.UserHasVehicle(User.Default);

                var mainForm = serviceProvider.GetRequiredService<MainForm>();
                if (!userHasVehicle)
                {
                    MessageBox.Show("Brak pojazdów. Dodaj nowy teraz!");
                    var addVehicleForm = serviceProvider.GetRequiredService<AddOrEditVehicleForm>();
                    addVehicleForm.Init(mainForm, vehicle: null, isEditMode: false, isFromFirstRun: true);
                    addVehicleForm.ShowDialog();
                }
                else
                {
                    Application.Run(mainForm);
                }
            }
        }

        private static void RegisterForms(IServiceCollection service)
        {
            service.AddScoped<MainForm>();
            service.AddScoped<AddOrEditVehicleForm>();
        }
    }
}