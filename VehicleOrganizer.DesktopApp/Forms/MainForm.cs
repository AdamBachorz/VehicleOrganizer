using VehicleOrganizer.Core.Services.Interfaces;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBackgroundActionInvokeService _backgroundActionInvokeService;

        private readonly AddOrEditVehicleForm _addOrEditVehicleForm;
        private readonly AdminToolsForm _adminToolsForm;
        private readonly OperationalActivityPanel _operationalActivityPanel;
        private readonly PickVehicleForm _pickVehicleForm;

        private Control _currentPanel;

        public MainForm(IVehicleRepository vehicleRepository, AddOrEditVehicleForm addOrEditVehicleForm, AdminToolsForm adminToolsForm,
            OperationalActivityPanel operationActivityPanel, PickVehicleForm pickVehicleForm, IBackgroundActionInvokeService backgroundActionInvokeService)
        {
            InitializeComponent();
            _vehicleRepository = vehicleRepository;
            _addOrEditVehicleForm = addOrEditVehicleForm;
            _adminToolsForm = adminToolsForm;
            _pickVehicleForm = pickVehicleForm;
            _operationalActivityPanel = operationActivityPanel;

            UpdateToolStrips();
            _backgroundActionInvokeService = backgroundActionInvokeService;
        }
        private async void MainForm_Load(object sender, EventArgs e)
        {
            await _backgroundActionInvokeService.InvokeAllAsync();
        }

        public void PlacePanel(Control control) => PlacePanel(mainPanel, control);

        private void toolStripMenuItemAddNewVehicle_Click(object sender, EventArgs e)
        {
            _addOrEditVehicleForm.Init(this, vehicle: null);
            _addOrEditVehicleForm.ShowDialog();
        }

        private async void toolStripMenuItemSelectVehicle_Click(object sender, EventArgs e)
        {
            var vehiclesForUser = await _vehicleRepository.GetVehiclesForUserAsync(User.Default, includeSold: true);

            if (vehiclesForUser.Count > 1)
            {
                _pickVehicleForm.Init(this, vehiclesForUser);
                _pickVehicleForm.ShowDialog();
            }
            else
            {
                //Vehicle currentReference = null;
                //if (_currentPanel is VehiclePanel vp)
                //{
                //    currentReference = vp.VehicleReference;
                //}

                //var innerText = vehiclesForUser.First().Id
                //var dialogResult = MessageBox.Show("Aktualnie nie posiadasz żadnego innego", "Dodawanie nowego pojazdu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }

        private void PlacePanel(Control baseControl, Control control)
        {
            baseControl.Controls.Clear();
            baseControl.Controls.Add(control);
            _currentPanel = control;
            UpdateToolStrips();
        }

        private void UpdateToolStrips()
        {
            toolStripMenuAdmin.Visible = toolStripMenuAdmin.Enabled = User.Default.IsWorthy;

            toolStripMenuItemOpenActivities.Enabled = _currentPanel is not OperationalActivityPanel;
        }

        private async void toolStripMenuItemOpenActivities_Click(object sender, EventArgs e)
        {
            if (_currentPanel is not null)
            {
                await _operationalActivityPanel.Init(this, _currentPanel as VehiclePanel);
                PlacePanel(_operationalActivityPanel);
            }
        }

        private void toolStripMenuItemOpenAdminTools_Click(object sender, EventArgs e)
        {
            _adminToolsForm.ShowDialog();
        }

    }
}
