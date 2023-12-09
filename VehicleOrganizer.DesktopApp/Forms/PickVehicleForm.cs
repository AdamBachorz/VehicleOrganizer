﻿using AutoMapper;
using BachorzLibrary.Desktop.Extensions;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class PickVehicleForm : Form
    {
        private readonly IMapper _mapper;
      
        private readonly AddOrEditVehicleForm _addOrEditVehicleForm;

        private MainForm _mainForm;
        private IList<Vehicle> _vehicles;

        public PickVehicleForm(IMapper mapper, AddOrEditVehicleForm addOrEditVehicleForm)
        {
            InitializeComponent();
            _mapper = mapper;
            _addOrEditVehicleForm = addOrEditVehicleForm;
        }

        public void Init(MainForm mainForm, IList<Vehicle> vehicles)
        {
            _mainForm = mainForm;
            _vehicles = vehicles;
            comboBoxVehicles.LoadData(vehicles, v => v.Name + (v.IsSold ? $" {Codes.VehicleSoldIndicator}" : string.Empty));
        }

        private void buttonPickVehicle_Click(object sender, EventArgs e)
        {
            var selectedVehicle = _vehicles[comboBoxVehicles.SelectedIndex];
            var vehicleView = _mapper.Map<VehicleView>(selectedVehicle);
            Close();
            _mainForm.PlacePanel(new VehiclePanel(vehicleView, selectedVehicle));
        }

        private void buttonAddNewVehicle_Click(object sender, EventArgs e)
        {
            _addOrEditVehicleForm.Init(_mainForm, vehicle: null);
            _addOrEditVehicleForm.ShowDialog();
        }

        private void comboBoxVehicles_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonPickVehicle.Enabled = comboBoxVehicles.SelectedIndex >= 0;
        }
    }
}
