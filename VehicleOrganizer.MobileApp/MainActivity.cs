using Java.Sql;
using Microsoft.Extensions.DependencyInjection;
using System;
using VehicleOrganizer.Core.Config;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.MobileApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var con = new StreamReader(Assets.Open("Data/appsettings.Development.json")).ReadToEnd();

            var service = new ServiceCollection();
            DependencyInjection.RegisterModules(service, con);

            using ServiceProvider serviceProvider = service.BuildServiceProvider();
            var vehicleRepository = serviceProvider.GetRequiredService<IVehicleRepository>();

            var t = vehicleRepository.GetAll();

            Toast.MakeText(this, t.Count, ToastLength.Long);
        }
    }
}