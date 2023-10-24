using Microsoft.Extensions.DependencyInjection;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Entities;
using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.Core.Config;
using VehicleOrganizer.Infrastructure.Services.Email;

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
                var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
                var vehicleRepository = serviceProvider.GetRequiredService<IVehicleRepository>();
                var emailSenderService = serviceProvider.GetRequiredService<EmailSenderService>();

                await userRepository.AuthorizeUser(User.Default);
                var userHasVehicle = vehicleRepository.UserHasVehicle(User.Default);

                if (!userHasVehicle)
                {
                    MessageBox.Show("Brak pojazdów");
                }

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