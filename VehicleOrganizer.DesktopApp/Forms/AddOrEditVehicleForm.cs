using AutoMapper;
using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Desktop.Extensions;
using System.Windows.Forms;
using VehicleOrganizer.Domain.Abstractions.Enums;
using VehicleOrganizer.Domain.Abstractions.Extensions;
using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;
using VehicleOrganizer.Infrastructure.Validators.Criteria;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class AddOrEditVehicleForm : Form
    {
        private readonly IValidator<Vehicle, VehicleValidationCriteria> _validator;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        private bool _isEditMode;

        public AddOrEditVehicleForm(IValidator<Vehicle, VehicleValidationCriteria> validator, IVehicleRepository vehicleRepository, IMapper mapper)
        {
            InitializeComponent();
            _validator = validator;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;

            comboBoxType.LoadWithEnums<VehicleType>();
            comboBoxType.SelectedIndex = 0;
            numericUpDownYearOfProduction.Maximum = numericUpDownYearOfProduction.Value = DateTime.Now.Year;
        }

        public void Init(Vehicle vehicle, bool isEditMode)
        {
            _isEditMode = isEditMode;
            buttonAddOrUpdate.Text = isEditMode ? "Zaktualizuj dane" : "Dodaj pojazd";

            if (isEditMode)
            {
                FillUpControls(vehicle);
                textBoxMileage.Enabled = false;
            }
            else // Adding new vehicle
            {

            }
        }

        private void FillUpControls(Vehicle vehicle)
        {
            textBoxName.Text = vehicle.Name;
            comboBoxType.SelectedIndex = (int)vehicle.VehicleType;
            textBoxOilType.Text = vehicle.OilType;
            textBoxMileage.Text = vehicle.LatestMileage.ToString();
            dateTimePickerPurchaseDate.Value = vehicle.PurchaseDate;
            dateTimePickerRegistrationDate.Value = vehicle.RegistrationDate;
            dateTimePickerInsuranceConclusion.Value = vehicle.InsuranceConclusion;
            dateTimePickerInsuranceTermination.Value = vehicle.InsuranceTermination;
            dateTimePickerLastTechnicalReview.Value = vehicle.LastTechnicalReview;
            dateTimePickerNextTechnicalReview.Value = vehicle.NextTechnicalReview;

            numericUpDownYearOfProduction.Value = vehicle.YearOfProduction;
        }

        private Vehicle ApplyModelDataFromControls()
        {
            return new Vehicle
            {
                Name = textBoxName.Text,
                VehicleType = (VehicleType)comboBoxType.SelectedIndex,
                OilType = textBoxOilType.Text,
                PurchaseDate = dateTimePickerPurchaseDate.Value.Date,
                RegistrationDate = dateTimePickerRegistrationDate.Value.Date,
                InsuranceConclusion = dateTimePickerInsuranceConclusion.Value.Date,
                InsuranceTermination = dateTimePickerInsuranceTermination.Value.Date,
                LastTechnicalReview = dateTimePickerLastTechnicalReview.Value.Date,
                NextTechnicalReview = dateTimePickerNextTechnicalReview.Value.Date,
                YearOfProduction = (int)numericUpDownYearOfProduction.Value,
            };
        }


        private void dateTimePickerInsuranceConclusion_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerInsuranceTermination.Value = dateTimePickerInsuranceConclusion.Value.AddYears(1);
        }

        private void dateTimePickerLastTechnicalReview_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerNextTechnicalReview.Value = dateTimePickerLastTechnicalReview.Value.AddYears(1);
        }

        private async void buttonAddOrUpdate_Click(object sender, EventArgs e)
        {
            var vehicle = ApplyModelDataFromControls();

            var criteria = new VehicleValidationCriteria
            {
                ShouldCheckOilType = vehicle.VehicleType.IsOilBased(),
                ShouldCheckMileage = vehicle.VehicleType.IsOilBased(),
                MileageIsNotDigit = textBoxMileage.Text.IsNotDigit(),
            };
            var validationResult = _validator.ValidateToBulletPointString(vehicle, criteria);

            if (validationResult.HasValue())
            {
                MessageBox.Show(validationResult);
                return;
            }

            if (_isEditMode)
            {
                _vehicleRepository.Update(vehicle);
            }
            else
            {
                await _vehicleRepository.AddVehicleAsync(vehicle, textBoxMileage.Text.ToInt());
            }
        }
    }
}
