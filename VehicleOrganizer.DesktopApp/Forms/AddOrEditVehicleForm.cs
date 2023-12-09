using AutoMapper;
using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Tools;
using BachorzLibrary.Common.Utils;
using VehicleOrganizer.Core;
using VehicleOrganizer.DesktopApp.Extensions;
using VehicleOrganizer.DesktopApp.Interfaces;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.DesktopApp.Utils;
using VehicleOrganizer.Domain.Abstractions.Enums;
using VehicleOrganizer.Domain.Abstractions.Extensions;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Abstractions.Validators.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class AddOrEditVehicleForm : Form, IModelable<Vehicle>, IDebugable
    {
        private readonly IValidator<Vehicle, VehicleValidationCriteria> _validator;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        private MainForm _mainForm;

        private bool _isEditMode;

        public Vehicle Model { get; set; }
        public bool IsDebugMode => checkBoxDebugMode.Checked;

        public AddOrEditVehicleForm(IValidator<Vehicle, VehicleValidationCriteria> validator, IVehicleRepository vehicleRepository, IMapper mapper)
        {
            InitializeComponent();
            _validator = validator;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;

            checkBoxDebugMode.Visible = checkBoxDebugMode.Enabled = EnvUtils.IsDev();
            checkBoxDebugMode.Checked = CommonPool.IsDebugMode;
            comboBoxType.LoadWithEnums<VehicleType>(useEnumDescriptions: true, autoPickFirstItem: true);
            numericUpDownYearOfProduction.Maximum = numericUpDownYearOfProduction.Value = DateTime.Now.Year;
        }

        public void Init(MainForm mainForm, Vehicle vehicle)
        {
            Model = vehicle;
            _mainForm = mainForm;
            _isEditMode = vehicle is not null;
            Text = (_isEditMode ? "Edycja" : "Dodawanie") + " pojazdu";
            buttonAddOrUpdate.Text = _isEditMode ? "Zaktualizuj dane" : "Dodaj pojazd";
            FillUpControls(_isEditMode ? Model : null);
            textBoxMileage.Enabled = !_isEditMode;
        }

        public void FillUpControls(Vehicle vehicle)
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

        public Vehicle ApplyModelDataFromControls()
        {
            return new Vehicle
            {
                User = User.Default,
                Name = textBoxName.Text,
                VehicleType = EnumUtils.ParseEnum<VehicleType>(comboBoxType.SelectedItem.ToString(), isEnumDescription: true),
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

        public void SaveChangesToExistingEntity(Vehicle newData)
        {
            Model.Name = newData.Name;
            Model.VehicleType = newData.VehicleType;
            Model.OilType = newData.OilType;
            Model.PurchaseDate = newData.PurchaseDate;
            Model.RegistrationDate = newData.RegistrationDate;
            Model.InsuranceConclusion = newData.InsuranceConclusion;
            Model.InsuranceTermination = newData.InsuranceTermination;
            Model.LastTechnicalReview = newData.LastTechnicalReview;
            Model.NextTechnicalReview = newData.NextTechnicalReview;
            Model.YearOfProduction = newData.YearOfProduction;
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
                MileageIsNegative = textBoxMileage.Text.IsDigit() ? textBoxMileage.Text.ToInt() < 0 : false,
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
                SaveChangesToExistingEntity(vehicle);
                if (!IsDebugMode)
                {
                    _vehicleRepository.Update(Model);
                }
                view = _mapper.Map<VehicleView>(Model);

                _mainForm.PlacePanel(new VehiclePanel(view, Model, _vehicleRepository));
            }
            else
            {
                var justAddedVehicle = !IsDebugMode ? await _vehicleRepository.AddVehicleAsync(vehicle, textBoxMileage.Text.ToInt()) : vehicle;

                if (IsDebugMode)
                {
                    justAddedVehicle.Id = RandomFactory.RandomNumber(1, 1000, includeBound: true);
                    justAddedVehicle.MileageHistory = new List<MileageHistory> 
                    { 
                        new MileageHistory 
                        {
                            Id = RandomFactory.RandomNumber(1, 1000, includeBound: true),
                            Mileage = textBoxMileage.Text.ToInt(),
                            Vehicle = justAddedVehicle,
                        } 
                    };
                }

                view = _mapper.Map<VehicleView>(justAddedVehicle);
                _mainForm.PlacePanel(new VehiclePanel(view, justAddedVehicle, _vehicleRepository));
            }

            Dispose();
        }

    }
}
