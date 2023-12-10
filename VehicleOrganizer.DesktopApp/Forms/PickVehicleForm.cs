using AutoMapper;
using BachorzLibrary.Desktop.Extensions;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class PickVehicleForm : Form
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;

        private readonly AddOrEditVehicleForm _addOrEditVehicleForm;

        private MainForm _mainForm;
        private IList<Vehicle> _vehicles;

        public PickVehicleForm(IMapper mapper, AddOrEditVehicleForm addOrEditVehicleForm, IVehicleRepository vehicleRepository)
        {
            InitializeComponent();
            _mapper = mapper;
            _addOrEditVehicleForm = addOrEditVehicleForm;
            _vehicleRepository = vehicleRepository;
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
            _mainForm.PlacePanel(new VehiclePanel(selectedVehicle, _vehicleRepository, _mapper));
            Dispose();
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
