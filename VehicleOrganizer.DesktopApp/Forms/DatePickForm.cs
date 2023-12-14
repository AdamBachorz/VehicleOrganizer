namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class DatePickForm : Form
    {
        private readonly Action<DateTime> _onPickedDateAction;

        public DatePickForm(string caption, string desctiption, Action<DateTime> onPickedDateAction)
        {
            InitializeComponent();
            _onPickedDateAction = onPickedDateAction;
            Text = caption;
            label1.Text = desctiption;
        }

        private void buttonPickDate_Click(object sender, EventArgs e)
        {
            _onPickedDateAction(dateTimePicker1.Value.Date);
            Dispose();
        }
    }
}
