using AutoMapper;
using BachorzLibrary.Common.Utils;
using VehicleOrganizer.DesktopApp.Controls;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.DesktopApp.Interfaces;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Panels
{
    public partial class OperationActivityPanel : UserControl, IDebugable
    {
        private readonly IMapper _mapper;
        private readonly IOperationalActivityRepository _operationalActivityRepository;

        private MainForm _mainForm;
        private VehiclePanel _vehiclePanel;

        public bool IsDebugMode => checkBoxDebugMode.Checked;

        public OperationActivityPanel(IMapper mapper, IOperationalActivityRepository operationalActivityRepository)
        {
            InitializeComponent();
            _mapper = mapper;
            _operationalActivityRepository = operationalActivityRepository;

            checkBoxDebugMode.Visible = checkBoxDebugMode.Enabled = checkBoxDebugMode.Checked = EnvUtils.GetValueDependingOnEnvironment(true, false);
        }

        public async Task Init(MainForm mainForm, VehiclePanel vehiclePanel)
        {
            _mainForm = mainForm;
            _vehiclePanel = vehiclePanel;

            if (IsDebugMode)
            {
                for (int i = 0; i < 5; i++)
                {
                    var oav = new OperationalActivityView
                    {
                        Name = "Some name " + i,
                        LastOperationDateOrMileageWhenPerformed = i + " km",
                        SummaryPrompt = "Summary prompt " + i,
                    };
                    
                    flowLayoutPanelActivities.Controls.Add(new OperationalActivityControl(_operationalActivityRepository, oav));
                }
            }

            foreach (var activity in await _operationalActivityRepository.GetOperationalActivitiesForVehicleAndUserAsync(_vehiclePanel.GetVehicleReference(), User.Default))
            {
                var control = new OperationalActivityControl(_operationalActivityRepository, _mapper.Map<OperationalActivityView>(activity));
                flowLayoutPanelActivities.Controls.Add(control);
            }         
        }

        private void buttonAddActivity_Click(object sender, EventArgs e)
        {

        }

        private void buttonGoBack_Click(object sender, EventArgs e)
        {
            _mainForm.PlacePanel(_vehiclePanel);
        }
    }
}
