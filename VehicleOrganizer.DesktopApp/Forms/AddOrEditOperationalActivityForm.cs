using AutoMapper;
using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Tools;
using VehicleOrganizer.DesktopApp.Controls;
using VehicleOrganizer.DesktopApp.Interfaces;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Abstractions.Validators.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class AddOrEditOperationalActivityForm : Form, IModelable<OperationalActivity>, IDebugable
    {
        private readonly IValidator<OperationalActivity, OperationalActivityValidationCriteria> _validator;
        private readonly IMapper _mapper;
        private readonly IOperationalActivityRepository _operationalActivityRepository;

        private OperationalActivityPanel _operationalActivityPanel;
        private OperationalActivityControl _operationalActivityControl;

        
        private bool _isEditMode;
        private Vehicle _vehicle;

        public OperationalActivity Model { get; set; }
        public bool IsDebugMode => checkBoxDebugMode.Checked;


        public AddOrEditOperationalActivityForm(IValidator<OperationalActivity, OperationalActivityValidationCriteria> validator,
            IMapper mapper,
            IOperationalActivityRepository operationalActivityRepository)
        {
            InitializeComponent();
            _validator = validator;
            _mapper = mapper;
            _operationalActivityRepository = operationalActivityRepository;
        }

        public void Init(OperationalActivityPanel operationalActivityPanel, OperationalActivityControl operationalActivityControl, 
            OperationalActivity operationalActivity, Vehicle vehicle)
        {
            Model = operationalActivity;
            _operationalActivityPanel = operationalActivityPanel;
            _operationalActivityControl = operationalActivityControl;
            _isEditMode = operationalActivity is not null;
            _vehicle = vehicle;

            Text = (_isEditMode ? "Edycja" : "Dodawanie") + " czynności związanej z pojazdem";
            buttonAddOrEditOperationalActivity.Text = _isEditMode ? "Zaktualizuj dane" : "Dodaj czynność";
            FillUpControls(_isEditMode ? Model : null);
        }

        public OperationalActivity ApplyModelDataFromControls()
        {
            var result = new OperationalActivity
            {
                Name = textBoxName.Text,
                IsDateOperated = radioButtonIsDateOperated.Checked,
                LastOperationDate = dateTimePickerLastOperationDate.Value.Date,
                YearsStep = (int)numericUpDownYearStep.Value,
                MileageWhenPerformed = textBoxMileageWhenPerformed.Text.ToInt(),
                MileageStep = textBoxMileageStep.Text.ToInt(),
                Vehicle = _vehicle,
            };

            return result;        
        }

        public void SaveChangesToExistingEntity(OperationalActivity newData)
        {
            Model.Name = newData.Name;
            Model.IsDateOperated = newData.IsDateOperated;
            Model.LastOperationDate = newData.LastOperationDate;
            Model.YearsStep = newData.YearsStep;
            Model.MileageWhenPerformed = newData.MileageWhenPerformed;
            Model.MileageStep = newData.MileageStep;
        }

        public void FillUpControls(OperationalActivity operationalActivity)
        {
            textBoxName.Text = operationalActivity?.Name;
            EnableProperControls(operationalActivity);

            if (operationalActivity?.IsDateOperated ?? true)
            {
                dateTimePickerLastOperationDate.Value = operationalActivity?.LastOperationDate.Date ?? DateTime.Now.Date;
                numericUpDownYearStep.Value = operationalActivity?.YearsStep ?? 1;
            }
            else
            {
                textBoxMileageWhenPerformed.Text = operationalActivity?.MileageWhenPerformed.ToString() ?? string.Empty;
                textBoxMileageStep.Text = operationalActivity?.MileageStep.ToString() ?? string.Empty;
            }
        }

        private async void buttonAddOrEditOperationalActivity_Click(object sender, EventArgs e)
        {
            var operationalActivity = ApplyModelDataFromControls();
            
            var criteria = new OperationalActivityValidationCriteria
            {
                ActivityOperationIsNotSet = !radioButtonIsDateOperated.Checked && !radioButtonIsMileageOperated.Checked,
                MileageWhenPerformedIsNotDigit = textBoxMileageWhenPerformed.Text.IsNotDigit(),
                MileageWhenPerformedIsNegative = textBoxMileageWhenPerformed.Text.IsDigit() ? textBoxMileageWhenPerformed.Text.ToInt() < 0 : false,
                MileageWhenPerformedIsLessThanLatestMileage = textBoxMileageWhenPerformed.Text.IsDigit()
                                                            ? textBoxMileageWhenPerformed.Text.ToInt() < operationalActivity.Vehicle.LatestMileage : false,
                MileageStepIsNotDigit = textBoxMileageStep.Text.IsNotDigit(),
                MileageStepIsNegative = textBoxMileageStep.Text.IsDigit() ? textBoxMileageStep.Text.ToInt() < 0 : false,
                LastOperationDateIsEarlierThanVehiclePurchaseDate = operationalActivity.IsDateOperated 
                                                                  ? dateTimePickerLastOperationDate.Value < operationalActivity.Vehicle.PurchaseDate : false,

            };
            var validationResult = _validator.ValidateToBulletPointString(operationalActivity, criteria);

            if (validationResult.HasValue())
            {
                MessageBox.Show($"Wykryto następujące błędy:{Environment.NewLine}{Environment.NewLine}{validationResult}");
                return;
            }

            if (_isEditMode)
            {
                SaveChangesToExistingEntity(operationalActivity);
                if (!IsDebugMode)
                {
                    _operationalActivityRepository.Update(Model);
                }
                _operationalActivityPanel.UpdateActivityOnTable(_operationalActivityControl, Model);
            }
            else
            {
                var justAddedOperationalActivity = !IsDebugMode 
                    ? await _operationalActivityRepository.AddOperationalActivityForVehicleAsync(_vehicle.Id, operationalActivity) 
                    : operationalActivity;

                if (IsDebugMode)
                {
                    justAddedOperationalActivity.Id = RandomFactory.RandomNumber(1, 1000, includeBound: true);
                }

                _operationalActivityPanel.AddActivityToTable(justAddedOperationalActivity);
            }

            Close();
        }
        
        private void radioButtonIsDateOperated_CheckedChanged(object sender, EventArgs e)
        {
            EnableDateOperatedControls();
        }

        private void radioButtonIsMileageOperated_CheckedChanged(object sender, EventArgs e)
        {
            EnableMileageOperatedControls();
        }

        private void EnableProperControls(OperationalActivity operationalActivity)
        {
            if (operationalActivity is not null)
            {
                radioButtonIsDateOperated.Checked = operationalActivity.IsDateOperated;
                radioButtonIsMileageOperated.Checked = !operationalActivity.IsDateOperated;

                if (operationalActivity.IsDateOperated)
                {
                    EnableDateOperatedControls();
                }
                else
                {
                    EnableMileageOperatedControls();
                }
            }
            else
            {
                EnableDateOperatedControls();
            }
        }

        private void EnableDateOperatedControls()
        {
            foreach (Control control in groupBoxDateOperated.Controls)
            {
                control.Enabled = radioButtonIsDateOperated.Checked;
            }
        }

        private void EnableMileageOperatedControls()
        {
            foreach (Control control in groupBoxMileageOperated.Controls)
            {
                control.Enabled = radioButtonIsMileageOperated.Checked;
            }
        }

    }
}
