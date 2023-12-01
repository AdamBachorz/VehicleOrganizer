using AutoMapper;
using BachorzLibrary.Common.Utils;
using System.Diagnostics;
using VehicleOrganizer.Core;
using VehicleOrganizer.DesktopApp.Controls;
using VehicleOrganizer.DesktopApp.Forms;
using VehicleOrganizer.DesktopApp.Interfaces;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Panels
{
    public partial class OperationalActivityPanel : UserControl, IDebugable
    {
        private readonly IMapper _mapper;
        private readonly IOperationalActivityRepository _operationalActivityRepository;

        private readonly AddOrEditOperationalActivityForm _addOrEditOperationalActivityForm;

        private MainForm _mainForm;
        private VehiclePanel _vehiclePanel;

        public bool IsDebugMode => checkBoxDebugMode.Checked;

        public OperationalActivityPanel(IMapper mapper, IOperationalActivityRepository operationalActivityRepository, 
            AddOrEditOperationalActivityForm addOrEditOperationalActivityForm)
        {
            InitializeComponent();
            _mapper = mapper;
            _operationalActivityRepository = operationalActivityRepository;
            _addOrEditOperationalActivityForm = addOrEditOperationalActivityForm;

            checkBoxDebugMode.Visible = checkBoxDebugMode.Enabled = EnvUtils.GetValueDependingOnEnvironment(true, false);
            checkBoxDebugMode.Checked = CommonPool.IsDebugMode;
        }

        public async Task Init(MainForm mainForm, VehiclePanel vehiclePanel)
        {
            _mainForm = mainForm;
            _vehiclePanel = vehiclePanel;

            flowLayoutPanelActivities.Controls.Clear();
            if (IsDebugMode)
            {
                SetMockData(count: 6);
            }

            foreach (var activity in await _operationalActivityRepository.GetOperationalActivitiesForVehicleAndUserAsync(_vehiclePanel.VehicleReference.Id, User.Default))
            {
                AddActivityToTable(activity);
            }         
        }

        private void SetMockData(int count)
        {
            OperationalActivity oa = null;

            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    oa = new OperationalActivity
                    {
                        Name = "Some name (D)" + i,
                        IsDateOperated = true,
                        LastOperationDate = DateTime.Now.Date,
                        YearsStep = 1,
                        Vehicle = _vehiclePanel.VehicleReference,
                    };
                }
                else
                {
                    oa = new OperationalActivity
                    {
                        Name = "Some name (M)" + i,
                        IsDateOperated = false,
                        MileageWhenPerformed = i * 10000,
                        MileageStep = i * 100,
                        Vehicle = _vehiclePanel.VehicleReference,
                    };
                }

                AddActivityToTable(oa);
            }
        }

        public void AddActivityToTable(OperationalActivity activity)
        {
            var control = new OperationalActivityControl(_operationalActivityRepository, _mapper, _addOrEditOperationalActivityForm, activity, this);
            flowLayoutPanelActivities.Controls.Add(control);
        }

        public void UpdateActivityOnTable(OperationalActivityControl control, OperationalActivity activity)
        {
            if (control is not null)
            {
                flowLayoutPanelActivities.Controls.Remove(control);
                AddActivityToTable(activity);
            }
            else
            {
                throw new ArgumentNullException(nameof(control), "Cannot locate control");
            }

        }

        private void buttonAddActivity_Click(object sender, EventArgs e)
        {
            _addOrEditOperationalActivityForm.Init(this, operationalActivityControl: null, operationalActivity: null, _vehiclePanel.VehicleReference);
            _addOrEditOperationalActivityForm.ShowDialog();
        }

        private void buttonGoBack_Click(object sender, EventArgs e)
        {
            _mainForm.PlacePanel(_vehiclePanel);
        }
    }
}
