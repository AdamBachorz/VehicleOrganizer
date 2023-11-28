using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.DesktopApp.Panels
{
    public partial class VehiclePanel : UserControl
    {
        private VehicleView _vehicleView;
        private MainForm _mainForm;

        public Vehicle VehicleReference { get; private set; }

        public VehiclePanel(MainForm mainForm, VehicleView vehicleView, Vehicle vehicleReference)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _vehicleView = vehicleView;
            VehicleReference = vehicleReference;

            if (!vehicleView.IsOilBased)
            {
                buttonUpdateMileage.Enabled = false;
            }
            FillUpControls();
        }

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
