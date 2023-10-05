namespace VehicleOrganizer.DesktopApp.Panels
{
    partial class VehiclePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelVehicleName = new Label();
            label1 = new Label();
            labelOilType = new Label();
            labelLatestMileage = new Label();
            label3 = new Label();
            buttonUpdateMileage = new Button();
            SuspendLayout();
            // 
            // labelVehicleName
            // 
            labelVehicleName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelVehicleName.Location = new Point(53, 39);
            labelVehicleName.Name = "labelVehicleName";
            labelVehicleName.Size = new Size(403, 36);
            labelVehicleName.TabIndex = 0;
            labelVehicleName.Text = "Nazwa pojazdu";
            labelVehicleName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Location = new Point(135, 88);
            label1.Name = "label1";
            label1.Size = new Size(116, 23);
            label1.TabIndex = 1;
            label1.Text = "Typ oleju";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelOilType
            // 
            labelOilType.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelOilType.Location = new Point(257, 88);
            labelOilType.Name = "labelOilType";
            labelOilType.Size = new Size(128, 23);
            labelOilType.TabIndex = 2;
            labelOilType.Text = "Typ oleju";
            labelOilType.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelLatestMileage
            // 
            labelLatestMileage.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelLatestMileage.Location = new Point(257, 120);
            labelLatestMileage.Name = "labelLatestMileage";
            labelLatestMileage.Size = new Size(128, 23);
            labelLatestMileage.TabIndex = 4;
            labelLatestMileage.Text = "Przebieg";
            labelLatestMileage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Location = new Point(135, 120);
            label3.Name = "label3";
            label3.Size = new Size(116, 23);
            label3.TabIndex = 3;
            label3.Text = "Aktualny przebieg";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonUpdateMileage
            // 
            buttonUpdateMileage.Location = new Point(184, 175);
            buttonUpdateMileage.Name = "buttonUpdateMileage";
            buttonUpdateMileage.Size = new Size(132, 23);
            buttonUpdateMileage.TabIndex = 5;
            buttonUpdateMileage.Text = "Zaktualizuj przebieg";
            buttonUpdateMileage.UseVisualStyleBackColor = true;
            buttonUpdateMileage.Click += buttonUpdateMileage_Click;
            // 
            // VehiclePanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(buttonUpdateMileage);
            Controls.Add(labelLatestMileage);
            Controls.Add(label3);
            Controls.Add(labelOilType);
            Controls.Add(label1);
            Controls.Add(labelVehicleName);
            Name = "VehiclePanel";
            Size = new Size(671, 337);
            ResumeLayout(false);
        }

        #endregion

        private Label labelVehicleName;
        private Label label1;
        private Label labelOilType;
        private Label labelLatestMileage;
        private Label label3;
        private Button buttonUpdateMileage;
    }
}
