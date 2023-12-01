using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly AddOrEditVehicleForm _addOrEditVehicleForm;
        private readonly AdminToolsForm _adminToolsForm;
        private readonly OperationalActivityPanel _operationalActivityPanel;

        private Control _currentPanel;

        public MainForm(AddOrEditVehicleForm addOrEditVehicleForm, AdminToolsForm adminToolsForm, OperationalActivityPanel operationActivityPanel)
        {
            InitializeComponent();
            _addOrEditVehicleForm = addOrEditVehicleForm;
            _adminToolsForm = adminToolsForm;
            _operationalActivityPanel = operationActivityPanel;

            UpdateToolStrips();
        }

        public void PlacePanel(Control control) => PlacePanel(mainPanel, control);

        private void toolStripMenuItemAddNewVehicle_Click(object sender, EventArgs e)
        {
            _addOrEditVehicleForm.Init(this, vehicle: null);
            _addOrEditVehicleForm.ShowDialog();
        }

        private void toolStripMenuItemSelectVehicle_Click(object sender, EventArgs e)
        {
            
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
