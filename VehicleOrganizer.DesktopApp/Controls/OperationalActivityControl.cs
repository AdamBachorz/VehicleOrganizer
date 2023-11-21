using BachorzLibrary.Common.Extensions;
using BachorzLibrary.Common.Utils;
using VehicleOrganizer.DesktopApp.Interfaces;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Controls
{
    public partial class OperationalActivityControl : UserControl, IDebugable
    {
        private readonly IOperationalActivityRepository _operationalActivityRepository;

        private int _reference;

        public bool IsDebugMode => EnvUtils.GetValueDependingOnEnvironment(true, false);

        public OperationalActivityControl(IOperationalActivityRepository operationalActivityRepository, OperationalActivityView view)
        {
            InitializeComponent();
            _operationalActivityRepository = operationalActivityRepository;

            _reference = view.Reference;
            labelName.Text = view.Name;
            labelLastOperationDateOrMileageWhenPerformed.Text = view.LastOperationDateOrMileageWhenPerformed;
            labelSummaryPrompt.Text = view.SummaryPrompt;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_reference == 0 && !IsDebugMode)
            {
                MessageBox.Show("Nie można usunąć wskazanej aktywności ponieważ nie została ona poprawnie załadowana wcześniej");
                return;
            }

            try
            {
                if (!IsDebugMode)
                {
                    _operationalActivityRepository.Delete(_reference);
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
