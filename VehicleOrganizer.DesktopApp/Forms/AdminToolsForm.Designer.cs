namespace VehicleOrganizer.DesktopApp.Forms
{
    partial class AdminToolsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            checkBoxDebugMode = new CheckBox();
            buttonRunReminders = new Button();
            SuspendLayout();
            // 
            // checkBoxDebugMode
            // 
            checkBoxDebugMode.AutoSize = true;
            checkBoxDebugMode.Checked = true;
            checkBoxDebugMode.CheckState = CheckState.Checked;
            checkBoxDebugMode.Location = new Point(12, 12);
            checkBoxDebugMode.Name = "checkBoxDebugMode";
            checkBoxDebugMode.Size = new Size(100, 19);
            checkBoxDebugMode.TabIndex = 7;
            checkBoxDebugMode.Text = "DEBUG MODE";
            checkBoxDebugMode.UseVisualStyleBackColor = true;
            checkBoxDebugMode.CheckedChanged += checkBoxDebugMode_CheckedChanged;
            // 
            // buttonRunReminders
            // 
            buttonRunReminders.Location = new Point(11, 45);
            buttonRunReminders.Name = "buttonRunReminders";
            buttonRunReminders.Size = new Size(185, 23);
            buttonRunReminders.TabIndex = 8;
            buttonRunReminders.Text = "Uruchom powiadomienia";
            buttonRunReminders.UseVisualStyleBackColor = true;
            buttonRunReminders.Click += buttonRunReminders_Click;
            // 
            // AdminToolsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(232, 79);
            Controls.Add(buttonRunReminders);
            Controls.Add(checkBoxDebugMode);
            Name = "AdminToolsForm";
            Text = "Narzędzia Admina";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBoxDebugMode;
        private Button buttonRunReminders;
    }
}