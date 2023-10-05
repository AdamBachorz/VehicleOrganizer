using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleOrganizer.DesktopApp.Panels;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories.Interfaces;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;

        public MainForm(IMapper mapper, IVehicleRepository vehicleRepository)
        {
            InitializeComponent();
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;

            var v = new Vehicle 
            { 
                Name = "XYZ", 
                OilType = "5W-40", 
            };
            v.MileageHistory = new List<MileageHistory> 
            { 
                new MileageHistory { Mileage = 155000, Vehicle = v }, 
                new MileageHistory { Mileage = 200000 , Vehicle = v }, 
            };
            var vehiclePanel = new VehiclePanel(_mapper.Map<VehicleView>(v));
            PlacePanel(mainPanel, vehiclePanel);
        }

        private void toolStripMenuItemAddNewVehicle_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItemSelectVehicle_Click(object sender, EventArgs e)
        {

        }

        private void PlacePanel(Control baseControl, Control control)
        {
            baseControl.Controls.Clear();
            baseControl.Controls.Add(control);
        }
    }
}
