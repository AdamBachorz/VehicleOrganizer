using AutoMapper;
using BachorzLibrary.Common.Extensions;
using VehicleOrganizer.Core;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.DesktopApp.Interfaces;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Controls
{
    public partial class OperationalActivityControl : UserControl, IDebugable
    {
        private readonly IOperationalActivityRepository _operationalActivityRepository;
        
        private readonly AddOrEditOperationalActivityForm _addOrEditOperationalActivityForm;

        private OperationalActivity _reference;
        private OperationalActivityPanel _operationalActivityPanel;

        public bool IsDebugMode => CommonPool.IsDebugMode;

        public OperationalActivityControl(IOperationalActivityRepository operationalActivityRepository, IMapper mapper,
            AddOrEditOperationalActivityForm addOrEditOperationalActivityForm,
            OperationalActivity reference, OperationalActivityPanel operationalActivityPanel)
        {
            InitializeComponent();
            _operationalActivityRepository = operationalActivityRepository;
            _addOrEditOperationalActivityForm = addOrEditOperationalActivityForm;
            _operationalActivityPanel = operationalActivityPanel;

            var view = mapper.Map<OperationalActivityView>(reference);
            _reference = reference;
            labelName.Text = view.Name;
            labelLastOperationDateOrMileageWhenPerformed.Text = view.LastOperationDateOrMileageWhenPerformed;
            labelSummaryPrompt.Text = view.SummaryPrompt;

            buttonEdit.Text = Codes.Icos.Edit;
            buttonDelete.Text = Codes.Icos.Delete;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            _addOrEditOperationalActivityForm.Init(_operationalActivityPanel, this, _reference, _reference.Vehicle);
            _addOrEditOperationalActivityForm.ShowDialog();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_reference.Id == 0 && !IsDebugMode)
            {
                MessageBox.Show("Nie można usunąć wskazanej aktywności ponieważ nie została ona poprawnie załadowana wcześniej");
                return;
            }

            try
            {
                if (!IsDebugMode)
                {
                    _operationalActivityRepository.Delete(_reference.Id);
                }

                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.FullMessageWithStackTrace());
            }
        }
    }
}
