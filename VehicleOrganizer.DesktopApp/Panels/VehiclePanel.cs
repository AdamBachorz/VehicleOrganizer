using AutoMapper;
using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.Domain.Abstractions.Exceptions;
using VehicleOrganizer.Domain.Abstractions.Extensions;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Panels
{
    public partial class VehiclePanel : UserControl
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;

        private VehicleView _vehicleView;

        public Vehicle VehicleReference { get; private set; }

        public VehiclePanel(Vehicle vehicleReference, IVehicleRepository vehicleRepository, IMapper mapper)
        {
            InitializeComponent();
            VehicleReference = vehicleReference;

            if (!VehicleReference.VehicleType.IsOilBased())
            {
                buttonUpdateMileage.Enabled = false;
            }
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;

            FillUpControls();
        }

        private void FillUpControls()
        {
            _vehicleView = _mapper.Map<VehicleView>(VehicleReference);
            labelVehicleName.Text = _vehicleView.Name;
            labelYearOfProduction.Text = _vehicleView.YearOfProduction;
            labelOilType.Text = _vehicleView.OilType;
            labelLatestMileage.Text = _vehicleView.LatestMileage;
            labelPruchaseDate.Text = _vehicleView.PurchaseDate;
            labelRegistrationDate.Text = _vehicleView.RegistrationDate;

            labelInsuranceConclusion.Text = _vehicleView.InsuranceConclusion;
            labelInsuranceTermination.Text = _vehicleView.InsuranceTermination;

            labelLastTechnicalReview.Text = _vehicleView.LastTechnicalReview;
            labelNextTechnicalReview.Text = _vehicleView.NextTechnicalReview;
        }

        private void buttonUpdateMileage_Click(object sender, EventArgs e)
        {
            new ValuePickForm("Aktualizacja przebiegu", "Podaj aktualny przebieg:", async pickedValue => {
                
                if (pickedValue.IsNotDigit())
                {
                    MessageBox.Show("Podana wartość musi być liczbą");
                    return;
                }

                try
                {
                    await _vehicleRepository.UpdateMileageAsync(VehicleReference, pickedValue.ToInt());
                    FillUpControls();
                    MessageBox.Show("Pomyślnie zaktualizowano przebieg pojazdu");
                }
                catch (CustomArgumentException caex)
                {
                    MessageBox.Show(caex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.FullMessageWithStackTrace());
                }

            }).ShowDialog();
        }

        private void buttonUpdateInsurance_Click(object sender, EventArgs e)
        {
            new DatePickForm("Aktualizacja ubezpieczenia", "Podaj datę zawarcia nowego ubezpieczenia:", async pickedDate => {
                try
                {
                    await _vehicleRepository.UpdateInsuranceDateAsync(VehicleReference, pickedDate);
                    FillUpControls();
                    MessageBox.Show("Pomyślnie zaktualizowano dane o ubezpieczeniu");
                }
                catch (CustomArgumentException caex)
                {
                    MessageBox.Show(caex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.FullMessageWithStackTrace());
                }

            }).ShowDialog();
        }

        private void buttonUpdateTechnicalReview_Click(object sender, EventArgs e)
        {
            new DatePickForm("Aktualizacja przeglądu", "Podaj datę ostatnio przeprowadzonego przeglądu technicznego:", async pickedDate => {

                try
                {
                    await _vehicleRepository.UpdateTechnicalReviewDateAsync(VehicleReference, pickedDate);
                    FillUpControls();
                    MessageBox.Show("Pomyślnie zaktualizowano datę przeglądu technicznego");
                }
                catch (ArgumentException aex)
                {
                    MessageBox.Show(aex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.FullMessageWithStackTrace());
                }

            }).ShowDialog();
        }
    }
}
