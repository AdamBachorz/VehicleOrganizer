using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using BachorzLibrary.DAL.DotNetSix.Utils;
using Java.Sql;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using VehicleOrganizer.Core.Config;
using VehicleOrganizer.Infrastructure.Entities;
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

            var services = new ServiceCollection();
            RegisterModules(services);

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            var vehicleRepository = serviceProvider.GetRequiredService<IVehicleRepository>();

            var t = vehicleRepository.GetAll();

            Toast.MakeText(this, t.FirstOrDefault()?.Name ?? "brak", ToastLength.Long).Show();
        }

        private void RegisterModules(ServiceCollection service)
        {
            User.Default = G.U(this);
            DependencyInjection.RegisterModules(service, G.Cc(this), options => DbContextUtils.ExplicitConfig(options, G.C(this)));
        }
    }
}