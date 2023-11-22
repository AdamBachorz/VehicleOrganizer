using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.DesktopApp.Interfaces;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class AddOrEditOperationalActivityForm : Form, IModelable<OperationalActivity>, IDebugable
    {
        private readonly IOperationalActivityRepository _operationalActivityRepository;

        private OperationActivityPanel _operationActivityPanel;
        private bool _isEditMode;
        private int _vehicleId;

        public bool IsDebugMode => checkBoxDebugMode.Checked;

        public AddOrEditOperationalActivityForm(IOperationalActivityRepository operationalActivityRepository)
        {
            InitializeComponent();
            _operationalActivityRepository = operationalActivityRepository;
        }

        public void Init(OperationActivityPanel operationActivityPanel, OperationalActivity operationalActivity, int vehicleId, bool isEditMode)
        {
            _operationActivityPanel = operationActivityPanel;
            _isEditMode = isEditMode;
            _vehicleId = vehicleId;

            Text = (isEditMode ? "Edycja" : "Dodawanie") + " czynności związanej z pojazdem";
            FillUpControls(isEditMode ? operationalActivity : null);
        }

        public OperationalActivity ApplyModelDataFromControls()
        {
            var result = new OperationalActivity
            {
                Name = textBoxName.Text,
                IsDateOperated = radioButtonIsDateOperated.Checked,
            };

            if (result.IsDateOperated)
            {
                result.LastOperationDate = dateTimePickerLastOperationDate.Value.Date;
                result.YearsStep = (int)numericUpDownYearStep.Value;
            }
            else
            {
                result.MileageWhenPerformed = textBoxMileageWhenPerformed.Text.ToInt();
                result.MileageStep = textBoxMileageStep.Text.ToInt();
            }

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

        private void buttonAddOrEditOperationalActivity_Click(object sender, EventArgs e)
        {
            var operationalActivity = ApplyModelDataFromControls();

            if (_isEditMode)
            {

            }
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
