using BachorzLibrary.Common.Extensions;

namespace VehicleOrganizer.DesktopApp.Forms
{
    public partial class ValuePickForm : Form
    {
        private readonly Action<string> _onValuePick;

        public ValuePickForm(Action<string> onValuePick)
        {
            InitializeComponent();
            _onValuePick = onValuePick;
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
