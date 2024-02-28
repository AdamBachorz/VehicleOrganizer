using Microsoft.Extensions.DependencyInjection;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Core.Config;
using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.DesktopApp.Panels;
using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using VehicleOrganizer.Core;
using AutoMapper;
using VehicleOrganizer.Domain.Abstractions;
using BachorzLibrary.Common;
using VehicleOrganizer.Domain.Abstractions.Utils;

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
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledExceptionHandler;
          
            var service = new ServiceCollection();

            string configFile = EnvUtils.GetValueDependingOnEnvironment(Codes.Files.DevConfig, Codes.Files.ProdConfig);
            DependencyInjection.RegisterModules(service, File.ReadAllText(configFile));
            RegisterFormsAndPanels(service);

            using (ServiceProvider serviceProvider = service.BuildServiceProvider())
            {
                var config = serviceProvider.GetRequiredService<IEFCCustomConfig>();
                var mapper = serviceProvider.GetRequiredService<IMapper>();
                CommonPool.IsDebugMode = Convert.ToBoolean(config.ValuesBag["DebugMode"]);
                var vehicleRepository = serviceProvider.GetRequiredService<IVehicleRepository>();

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

                if (vehiclesForUser.Count == 1)
                {
                    var vehicle = vehiclesForUser.First();
                    mainForm.PlacePanel(new VehiclePanel(vehicle, vehicleRepository, mapper));
                }

                if (vehiclesForUser.Count > 1)
                {
                    var pickVehicleForm = serviceProvider.GetRequiredService<PickVehicleForm>();
                    pickVehicleForm.Init(mainForm, vehiclesForUser);
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
            service.AddScoped<AdminToolsForm>();

            service.AddScoped<OperationalActivityPanel>();
        }


        private static void CurrentDomain_UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;

            var directory = Path.Combine(Codes.MainPath, Codes.Directories.Exceptions, Codes.Directories.EnvSubdirectory);
            var file = Path.Combine(directory, $"Exception_{DateTime.Now.ToString(Consts.DateFormat.Compact)}.txt");

            File.WriteAllText(file, ex.FullMessageWithStackTrace());
        }
    }
}