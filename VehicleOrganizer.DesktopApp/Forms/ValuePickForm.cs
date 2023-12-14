using BachorzLibrary.Common.Extensions;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class ValuePickForm : Form
    {
        private readonly Action<string> _onValuePick;

        public ValuePickForm(string caption, string desctiption, Action<string> onValuePick)
        {
            InitializeComponent();
            _onValuePick = onValuePick;
            Text = caption;
            label1.Text = desctiption;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Nie podano żadnej wartości");
                return;
            }

            _onValuePick(textBox1.Text);
            Dispose();
        }
    }
}
