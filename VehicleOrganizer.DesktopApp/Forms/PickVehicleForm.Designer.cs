namespace VehicleOrganizer.DesktopApp.Forms
{
    partial class PickVehicleForm
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
            comboBoxVehicles = new ComboBox();
            buttonPickVehicle = new Button();
            buttonAddNewVehicle = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // comboBoxVehicles
            // 
            comboBoxVehicles.FormattingEnabled = true;
            comboBoxVehicles.Location = new Point(22, 30);
            comboBoxVehicles.Name = "comboBoxVehicles";
            comboBoxVehicles.Size = new Size(263, 23);
            comboBoxVehicles.TabIndex = 0;
            comboBoxVehicles.SelectedIndexChanged += comboBoxVehicles_SelectedIndexChanged;
            // 
            // buttonPickVehicle
            // 
            buttonPickVehicle.Enabled = false;
            buttonPickVehicle.Location = new Point(291, 30);
            buttonPickVehicle.Name = "buttonPickVehicle";
            buttonPickVehicle.Size = new Size(135, 23);
            buttonPickVehicle.TabIndex = 1;
            buttonPickVehicle.TabStop = false;
            buttonPickVehicle.Text = "Wybierz pojazd";
            buttonPickVehicle.UseVisualStyleBackColor = true;
            buttonPickVehicle.Click += buttonPickVehicle_Click;
            // 
            // buttonAddNewVehicle
            // 
            buttonAddNewVehicle.Location = new Point(432, 30);
            buttonAddNewVehicle.Name = "buttonAddNewVehicle";
            buttonAddNewVehicle.Size = new Size(130, 23);
            buttonAddNewVehicle.TabIndex = 2;
            buttonAddNewVehicle.Text = "Dodaj nowy pojazd";
            buttonAddNewVehicle.UseVisualStyleBackColor = true;
            buttonAddNewVehicle.Click += buttonAddNewVehicle_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 9);
            label1.Name = "label1";
            label1.Size = new Size(263, 15);
            label1.TabIndex = 3;
            label1.Text = "Wybierz pojazd z poniższej listy (lub dodaj nowe)";
            // 
            // PickVehicleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(575, 72);
            Controls.Add(label1);
            Controls.Add(buttonAddNewVehicle);
            Controls.Add(buttonPickVehicle);
            Controls.Add(comboBoxVehicles);
            Name = "PickVehicleForm";
            Text = "Wybierz pojazd";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxVehicles;
        private Button buttonPickVehicle;
        private Button buttonAddNewVehicle;
        private Label label1;
    }
}