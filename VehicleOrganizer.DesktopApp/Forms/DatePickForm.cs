namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class DatePickForm : Form
    {
        Action<DateTime> _onPickedDateAction;

        public DatePickForm(Action<DateTime> onPickedDateAction)
        {
            InitializeComponent();
            _onPickedDateAction = onPickedDateAction;
        }

        private void buttonPickDate_Click(object sender, EventArgs e)
        {
            _onPickedDateAction(dateTimePicker1.Value.Date);
            Dispose();
        }
    }
}
