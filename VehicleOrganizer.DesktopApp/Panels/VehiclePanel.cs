using VehicleOrganizer.Domain.Abstractions.Views;

namespace VehicleOrganizer.DesktopApp.Panels
{
    public partial class VehiclePanel : UserControl
    {
        private VehicleView _vehicleView;

        public VehiclePanel(VehicleView vehicleView)
        {
            InitializeComponent();
            _vehicleView = vehicleView;

            FillUpControls();
        }

        private void FillUpControls()
        {
            labelVehicleName.Text = _vehicleView.Name;
            labelOilType.Text = _vehicleView.OilType;
            labelLatestMileage.Text = _vehicleView.LatestMileage;
        }

        private void buttonUpdateMileage_Click(object sender, EventArgs e)
        {

        }
    }
}
