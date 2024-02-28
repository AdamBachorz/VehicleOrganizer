using Android.Views;
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


        private Button _buttonUpdateMileage;
        private PopupWindow _popupWindow;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            _buttonUpdateMileage = FindViewById<Button>(Resource.Id.buttonUpdateMileage);
            _buttonUpdateMileage.Click += ButtonUpdateMileage_Click;

            var services = new ServiceCollection();
            RegisterModules(services);

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            var vehicleRepository = serviceProvider.GetRequiredService<IVehicleRepository>();

            var t = vehicleRepository.GetAll();

            Toast.MakeText(this, t.FirstOrDefault()?.Name ?? "brak", ToastLength.Long).Show();
        }

        private async void ButtonUpdateMileage_Click(object? sender, EventArgs e)
        {
            LayoutInflater inflater = LayoutInflater.From(this);
            var popupView = inflater.Inflate(Resource.Layout.popup_update_mileage, null);
            _popupWindow = new PopupWindow(popupView, 500, 500, true);

            _popupWindow.ShowAtLocation(_buttonUpdateMileage, GravityFlags.Center, 0, 0);

        }

        private void RegisterModules(ServiceCollection service)
        {
            User.Default = G.U(this);
            DependencyInjection.RegisterModules(service, G.Cc(this), options => DbContextUtils.ExplicitConfig(options, G.C(this)));
        }
    }
}