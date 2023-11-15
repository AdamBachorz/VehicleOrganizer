using AutoMapper;
using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.Core.Services.Interfaces;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBackgroundActionInvokeService _backgroundActionInvokeService;

        private readonly AddOrEditVehicleForm _addOrEditVehicleForm;

        public MainForm(IMapper mapper, IVehicleRepository vehicleRepository, IBackgroundActionInvokeService backgroundActionInvokeService, 
            AddOrEditVehicleForm addOrEditVehicleForm)
        {
            InitializeComponent();
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _backgroundActionInvokeService = backgroundActionInvokeService;
            _addOrEditVehicleForm = addOrEditVehicleForm;

            toolStripMenuAdmin.Visible = toolStripMenuAdmin.Enabled = User.Default.IsWorthy;
        }

        public void Init(VehicleView vehicleView)
        {
            if (vehicleView is not null)
            {
                PlacePanel(new VehiclePanel(vehicleView)); 
            }
        }

        public void PlacePanel(Control control) => PlacePanel(mainPanel, control);

        private void toolStripMenuItemAddNewVehicle_Click(object sender, EventArgs e)
        {
            _addOrEditVehicleForm.Init(this, vehicle: null, isEditMode: false);
            _addOrEditVehicleForm.ShowDialog();
        }

        private void toolStripMenuItemSelectVehicle_Click(object sender, EventArgs e)
        {

        }

        private async void toolStripMenuItemRunReminders_Click(object sender, EventArgs e)
        {
            await _backgroundActionInvokeService.RunRemindersAsync();
            var errors = _backgroundActionInvokeService.CurrentErrors();
            MessageBox.Show(errors.IsNotNullOrEmpty() ? errors.Select(x => x).Join(Environment.NewLine) : "Wysłano powiadomienia");       
        }

        private void PlacePanel(Control baseControl, Control control)
        {
            baseControl.Controls.Clear();
            baseControl.Controls.Add(control);
        }

    }
}
