using BachorzLibrary.Common.Extensions;
using BachorzLibrary.DAL.DotNetSix.EntityFrameworkCore;
using VehicleOrganizer.Core;
using VehicleOrganizer.Core.Services.Interfaces;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class AdminToolsForm : Form
    {
        private readonly IEFCCustomConfig _config;
        private readonly IBackgroundActionInvokeService _backgroundActionInvokeService;

        public AdminToolsForm(IEFCCustomConfig config, IBackgroundActionInvokeService backgroundActionInvokeService)
        {
            if (!User.Default.IsWorthy)
            {
                Dispose();
            }
            InitializeComponent();
            _config = config;
            _backgroundActionInvokeService = backgroundActionInvokeService;
        }

        private void checkBoxDebugMode_CheckedChanged(object sender, EventArgs e)
        {
            CommonPool.IsDebugMode = checkBoxDebugMode.Checked;
            _config.ValuesBag["DebugMode"] = checkBoxDebugMode.Checked;
        }

        private async void buttonRunReminders_Click(object sender, EventArgs e)
        {
            await _backgroundActionInvokeService.RunRemindersAsync();
            var errors = _backgroundActionInvokeService.CurrentErrors();
            MessageBox.Show(errors.IsNotNullOrEmpty() ? errors.Select(x => x).Join(Environment.NewLine) : "Wysłano powiadomienia");
        }
    }
}
