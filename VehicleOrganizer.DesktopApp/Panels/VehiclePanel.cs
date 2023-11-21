using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Domain.Abstractions.Views;

namespace VehicleOrganizer.DesktopApp.Panels
{
    public partial class VehiclePanel : UserControl
    {
        private VehicleView _vehicleView;
        private MainForm _mainForm;

        public VehiclePanel(MainForm mainForm, VehicleView vehicleView)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _vehicleView = vehicleView;

            if (!vehicleView.IsOilBased)
            {
                buttonUpdateMileage.Enabled = false;
            }
            FillUpControls();
        }

        public int GetVehicleReference() => _vehicleView?.Reference ?? 0;

        private void FillUpControls()
        {
            labelVehicleName.Text = _vehicleView.Name;
            labelYearOfProduction.Text = _vehicleView.YearOfProduction;
            labelOilType.Text = _vehicleView.OilType;
            labelLatestMileage.Text = _vehicleView.LatestMileage;
            labelPruchaseDate.Text = _vehicleView.PurchaseDate;
            labelRegistrationDate.Text = _vehicleView.RegistrationDate;

            labelInsuranceConclusion.Text = _vehicleView.InsuranceConclusion;
            labelInsuranceTermination.Text = _vehicleView.InsuranceTermination;
        }

        private void buttonUpdateMileage_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdateInsurance_Click(object sender, EventArgs e)
        {

        }

    }
}
