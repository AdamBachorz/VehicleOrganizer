using AutoMapper;
using BachorzLibrary.Desktop.Extensions;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class PickVehicleForm : Form
    {
        private readonly IMapper _mapper;

        private readonly MainForm _mainForm;
        private readonly VehiclePanel _vehiclePanel;
        private readonly AddOrEditVehicleForm _addOrEditVehicleForm;

        private IList<Vehicle> _vehicles;

        public PickVehicleForm(IMapper mapper, VehiclePanel vehiclePanel, AddOrEditVehicleForm addOrEditVehicleForm, MainForm mainForm)
        {
            InitializeComponent();
            _mapper = mapper;
            _vehiclePanel = vehiclePanel;
            _addOrEditVehicleForm = addOrEditVehicleForm;
            _mainForm = mainForm;
        }

        public void Init(IList<Vehicle> vehicles)
        {
            _vehicles = vehicles;
            comboBoxVehicles.LoadData(vehicles, v => v.Name + (v.IsSold ? " [SPRZEDANY]" : string.Empty));
        }

        private void buttonPickVehicle_Click(object sender, EventArgs e)
        {
            var vehicleView = _mapper.Map<VehicleView>(_vehicles[comboBoxVehicles.SelectedIndex]);
            Close();
            _mainForm.Init(vehicleView);
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
