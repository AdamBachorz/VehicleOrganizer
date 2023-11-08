using AutoMapper;
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

        public MainForm(IMapper mapper, IVehicleRepository vehicleRepository)
        {
            InitializeComponent();
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;

            toolStripMenuAdmin.Visible = toolStripMenuAdmin.Enabled = User.Default.IsWorthy;
        }

        public void InitAndOpen(VehicleView vehicleView)
        {
            PlacePanel(mainPanel, new VehiclePanel(vehicleView));
            ShowDialog();
        }

        private void toolStripMenuItemAddNewVehicle_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItemSelectVehicle_Click(object sender, EventArgs e)
        {

        }

        private async void toolStripMenuItemRunReminders_Click(object sender, EventArgs e)
        {

        }

        private void PlacePanel(Control baseControl, Control control)
        {
            baseControl.Controls.Clear();
            baseControl.Controls.Add(control);
        }
    }
}
