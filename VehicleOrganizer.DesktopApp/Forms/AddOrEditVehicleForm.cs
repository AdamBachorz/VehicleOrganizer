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
        private bool _isFromFirstRun;

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

        public void Init(MainForm mainForm, Vehicle vehicle, bool isEditMode, bool isFromFirstRun = false)
        {
            _mainForm = mainForm;
            _isEditMode = isEditMode;
            _isFromFirstRun = isFromFirstRun;
            buttonAddOrUpdate.Text = isEditMode ? "Zaktualizuj dane" : "Dodaj pojazd";

            if (isEditMode)
            {
                FillUpControls(vehicle);
                textBoxMileage.Enabled = false;
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
                VehicleTypeIsNotSelected = comboBoxType.SelectedIndex == -1,
                ShouldCheckMileage = vehicle.VehicleType.IsOilBased(),
                MileageIsNotDigit = textBoxMileage.Text.IsNotDigit(),
            };
            var validationResult = _validator.ValidateToBulletPointString(vehicle, criteria);

            if (validationResult.HasValue())
            {
                MessageBox.Show(validationResult);
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

            _mainForm.Init(view);
            if (_isFromFirstRun)
            {
                _mainForm.ShowDialog();
            }

            Close();
        }
    }
}
