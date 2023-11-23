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
    public partial class OperationalActivityPanel : UserControl, IDebugable
    {
        private readonly IMapper _mapper;
        private readonly IOperationalActivityRepository _operationalActivityRepository;

        private MainForm _mainForm;
        private VehiclePanel _vehiclePanel;

        public bool IsDebugMode => checkBoxDebugMode.Checked;

        public OperationalActivityPanel(IMapper mapper, IOperationalActivityRepository operationalActivityRepository)
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
                        LastOperationDateOrMileageWhenPerformed = (i + 1) * 100000 + " km",
                        SummaryPrompt = "Summary prompt " + i,
                    };
                    
                    AddActivityToTable(oav);
                }
            }

            foreach (var activity in await _operationalActivityRepository.GetOperationalActivitiesForVehicleAndUserAsync(_vehiclePanel.GetVehicleReference(), User.Default))
            {
                AddActivityToTable(_mapper.Map<OperationalActivityView>(activity));
            }         
        }

        public void AddActivityToTable(OperationalActivityView view)
        {
            flowLayoutPanelActivities.Controls.Add(new OperationalActivityControl(_operationalActivityRepository, view));
        }

        public void UpdateActivityOnTable(string operationalActivityName, OperationalActivityView newView)
        {
            var control = (OperationalActivityControl)flowLayoutPanelActivities.Controls.Find(operationalActivityName, searchAllChildren: false).FirstOrDefault();
            
            if (control is not null)
            {
                flowLayoutPanelActivities.Controls.Remove(control);
                AddActivityToTable(newView);
            }
            else
            {
                throw new ArgumentNullException(nameof(control), "Cannot locate control with key: " + operationalActivityName);
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
