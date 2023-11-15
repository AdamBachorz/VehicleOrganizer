using AutoMapper;
using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Utils;
using VehicleOrganizer.DesktopApp.Extensions;
using VehicleOrganizer.Domain.Abstractions.Enums;
using VehicleOrganizer.Domain.Abstractions.Extensions;
using VehicleOrganizer.Domain.Abstractions.Views;
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

        private MainForm _mainForm;

        private bool _isEditMode;

        public AddOrEditVehicleForm(IValidator<Vehicle, VehicleValidationCriteria> validator, IVehicleRepository vehicleRepository, IMapper mapper)
        {
            InitializeComponent();
            _validator = validator;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;

            checkBoxDebugMode.Visible = checkBoxDebugMode.Enabled = checkBoxDebugMode.Checked = EnvUtils.GetValueDependingOnEnvironment(true, false);
            comboBoxType.LoadWithEnums<VehicleType>(useEnumDescriptions: true, autoPickFirstItem: true);
            numericUpDownYearOfProduction.Maximum = numericUpDownYearOfProduction.Value = DateTime.Now.Year;
        }

        public void Init(MainForm mainForm, Vehicle vehicle, bool isEditMode)
        {
            _mainForm = mainForm;
            _isEditMode = isEditMode;
            buttonAddOrUpdate.Text = isEditMode ? "Zaktualizuj dane" : "Dodaj pojazd";
            FillUpControls(isEditMode ? vehicle : null);
            textBoxMileage.Enabled = !isEditMode;
        }

        private void FillUpControls(Vehicle vehicle)
        {
            textBoxName.Text = vehicle?.Name ?? string.Empty;
            comboBoxType.SelectedIndex = vehicle is not null ? (int)vehicle.VehicleType : 0;
            textBoxOilType.Text = vehicle?.OilType ?? string.Empty;
            textBoxMileage.Text = vehicle?.LatestMileage.ToString();

            dateTimePickerPurchaseDate.Value = vehicle?.PurchaseDate ?? DateTime.Now.Date;
            dateTimePickerRegistrationDate.Value = vehicle?.RegistrationDate ?? DateTime.Now.Date;
            dateTimePickerInsuranceConclusion.Value = vehicle?.InsuranceConclusion ?? DateTime.Now.Date;
            dateTimePickerInsuranceTermination.Value = vehicle?.InsuranceTermination ?? DateTime.Now.Date;
            dateTimePickerLastTechnicalReview.Value = vehicle?.LastTechnicalReview ?? DateTime.Now.Date;
            dateTimePickerNextTechnicalReview.Value = vehicle?.NextTechnicalReview ?? DateTime.Now.Date;

            numericUpDownYearOfProduction.Value = vehicle?.YearOfProduction ?? DateTime.Now.Year;
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
                VehicleTypeIsNotSelected = comboBoxType.SelectedIndex == -1,
                ShouldCheckMileage = vehicle.VehicleType.IsOilBased(),
                MileageIsNotDigit = textBoxMileage.Text.IsNotDigit(),
            };
            var validationResult = _validator.ValidateToBulletPointString(vehicle, criteria);

            if (validationResult.HasValue())
            {
                MessageBox.Show($"Wykryto następujące błędy:{Environment.NewLine}{Environment.NewLine}{validationResult}");
                return;
            }

            VehicleView view = null;
            if (_isEditMode)
            {
                if (!checkBoxDebugMode.Checked)
                {
                    _vehicleRepository.Update(vehicle);
                }
                view = _mapper.Map<VehicleView>(vehicle);
            }
            else
            {
                var justAddedVehicle = !checkBoxDebugMode.Checked
                    ? await _vehicleRepository.AddVehicleAsync(vehicle, textBoxMileage.Text.OrDefault("0").ToInt())
                    : vehicle;
                view = _mapper.Map<VehicleView>(justAddedVehicle);
            }

            Close();
            _mainForm.Init(view);
        }
    }
}
