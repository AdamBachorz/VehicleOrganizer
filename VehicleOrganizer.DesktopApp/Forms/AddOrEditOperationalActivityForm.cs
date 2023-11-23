using AutoMapper;
using BachorzLibrary.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows.Forms;
using VehicleOrganizer.DesktopApp.Interfaces;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Abstractions.Validators.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class AddOrEditOperationalActivityForm : Form, IModelable<OperationalActivity>, IDebugable
    {
        private readonly IValidator<OperationalActivity, OperationalActivityValidationCriteria> _validator;
        private readonly IMapper _mapper;
        private readonly IOperationalActivityRepository _operationalActivityRepository;

        private MainForm _mainForm;
        private OperationalActivityPanel _operationalActivityPanel;

        private OperationalActivity _operationActivity;
        private bool _isEditMode;
        private int _vehicleId;

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

        public void Init(MainForm mainForm, OperationalActivityPanel operationalActivityPanel, OperationalActivity operationalActivity, int vehicleId)
        {
            _mainForm = mainForm;
            _operationalActivityPanel = operationalActivityPanel;
            _operationActivity = operationalActivity;
            _isEditMode = operationalActivity is not null;
            _vehicleId = vehicleId;

            Text = (_isEditMode ? "Edycja" : "Dodawanie") + " czynności związanej z pojazdem";
            FillUpControls(_isEditMode ? operationalActivity : null);
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
            };

            return result;
        }

        public void FillUpControls(OperationalActivity operationalActivity)
        {
            textBoxName.Text = operationalActivity?.Name;
            EnableProperControls(operationalActivity);

            if (operationalActivity.IsDateOperated)
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
                MileageWhenPerformedIsNotDigit = textBoxMileageWhenPerformed.Text.IsNotDigit(),
                MileageWhenPerformedIsNegative = textBoxMileageWhenPerformed.Text.IsDigit() ? textBoxMileageWhenPerformed.Text.ToInt() < 0 : false,
                MileageStepIsNotDigit = textBoxMileageStep.Text.IsNotDigit(),
                MileageStepIsNegative = textBoxMileageStep.Text.IsDigit() ? textBoxMileageStep.Text.ToInt() < 0 : false,
            };
            var validationResult = _validator.ValidateToBulletPointString(operationalActivity, criteria);

            if (validationResult.HasValue())
            {
                MessageBox.Show($"Wykryto następujące błędy:{Environment.NewLine}{Environment.NewLine}{validationResult}");
                return;
            }

            OperationalActivityView view = null;
            if (_isEditMode)
            {
                if (!IsDebugMode && _operationActivity is not null)
                {
                    _operationalActivityRepository.Update(_operationActivity);
                }
                view = _mapper.Map<OperationalActivityView>(_operationActivity);
                _operationalActivityPanel.UpdateActivityOnTable(_operationActivity.Name, view);
            }
            else
            {
                var justAddedOperationalActivity = !IsDebugMode 
                    ? await _operationalActivityRepository.AddOperationalActivityForVehicleAsync(_vehicleId, operationalActivity) 
                    : operationalActivity;
                view = _mapper.Map<OperationalActivityView>(justAddedOperationalActivity);
                _operationalActivityPanel.AddActivityToTable(view);
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

            radioButtonIsMileageOperated.Checked = false;
        }

        private void EnableMileageOperatedControls()
        {
            foreach (Control control in groupBoxMileageOperated.Controls)
            {
                control.Enabled = radioButtonIsMileageOperated.Checked;
            }

            radioButtonIsDateOperated.Checked = false;
        }
    }
}
