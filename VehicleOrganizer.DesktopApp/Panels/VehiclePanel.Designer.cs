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
            label2 = new Label();
            label4 = new Label();
            labelRegistrationDate = new Label();
            labelPruchaseDate = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            labelYearOfProduction = new Label();
            label5 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            labelInsuranceTermination = new Label();
            labelInsuranceConclusion = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            buttonUpdateInsurance = new Button();
            buttonUpdateTechnicalReview = new Button();
            label9 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            labelNextTechnicalReview = new Label();
            labelLastTechnicalReview = new Label();
            label12 = new Label();
            label13 = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // labelVehicleName
            // 
            labelVehicleName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelVehicleName.Location = new Point(14, 11);
            labelVehicleName.Name = "labelVehicleName";
            labelVehicleName.Size = new Size(325, 36);
            labelVehicleName.TabIndex = 0;
            labelVehicleName.Text = "Nazwa pojazdu";
            labelVehicleName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 29);
            label1.Name = "label1";
            label1.Size = new Size(156, 29);
            label1.TabIndex = 1;
            label1.Text = "Typ oleju";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelOilType
            // 
            labelOilType.AutoSize = true;
            labelOilType.Dock = DockStyle.Fill;
            labelOilType.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelOilType.Location = new Point(165, 29);
            labelOilType.Name = "labelOilType";
            labelOilType.Size = new Size(157, 29);
            labelOilType.TabIndex = 2;
            labelOilType.Text = "Typ oleju";
            labelOilType.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelLatestMileage
            // 
            labelLatestMileage.AutoSize = true;
            labelLatestMileage.Dock = DockStyle.Fill;
            labelLatestMileage.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelLatestMileage.Location = new Point(165, 58);
            labelLatestMileage.Name = "labelLatestMileage";
            labelLatestMileage.Size = new Size(157, 29);
            labelLatestMileage.TabIndex = 4;
            labelLatestMileage.Text = "Przebieg";
            labelLatestMileage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 58);
            label3.Name = "label3";
            label3.Size = new Size(156, 29);
            label3.TabIndex = 3;
            label3.Text = "Aktualny przebieg";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonUpdateMileage
            // 
            buttonUpdateMileage.Location = new Point(104, 222);
            buttonUpdateMileage.Name = "buttonUpdateMileage";
            buttonUpdateMileage.Size = new Size(132, 23);
            buttonUpdateMileage.TabIndex = 5;
            buttonUpdateMileage.Text = "Zaktualizuj przebieg";
            buttonUpdateMileage.UseVisualStyleBackColor = true;
            buttonUpdateMileage.Click += buttonUpdateMileage_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 116);
            label2.Name = "label2";
            label2.Size = new Size(156, 29);
            label2.TabIndex = 7;
            label2.Text = "Data rejestracji";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(3, 87);
            label4.Name = "label4";
            label4.Size = new Size(156, 29);
            label4.TabIndex = 6;
            label4.Text = "Data zakupu";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelRegistrationDate
            // 
            labelRegistrationDate.AutoSize = true;
            labelRegistrationDate.Dock = DockStyle.Fill;
            labelRegistrationDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelRegistrationDate.Location = new Point(165, 116);
            labelRegistrationDate.Name = "labelRegistrationDate";
            labelRegistrationDate.Size = new Size(157, 29);
            labelRegistrationDate.TabIndex = 9;
            labelRegistrationDate.Text = "Data rejestracji";
            labelRegistrationDate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelPruchaseDate
            // 
            labelPruchaseDate.AutoSize = true;
            labelPruchaseDate.Dock = DockStyle.Fill;
            labelPruchaseDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelPruchaseDate.Location = new Point(165, 87);
            labelPruchaseDate.Name = "labelPruchaseDate";
            labelPruchaseDate.Size = new Size(157, 29);
            labelPruchaseDate.TabIndex = 8;
            labelPruchaseDate.Text = "Data zakupu";
            labelPruchaseDate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(labelYearOfProduction, 1, 0);
            tableLayoutPanel1.Controls.Add(label5, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(labelOilType, 1, 1);
            tableLayoutPanel1.Controls.Add(labelRegistrationDate, 1, 4);
            tableLayoutPanel1.Controls.Add(label2, 0, 4);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(labelPruchaseDate, 1, 3);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(labelLatestMileage, 1, 2);
            tableLayoutPanel1.Location = new Point(14, 50);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new Size(325, 145);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // labelYearOfProduction
            // 
            labelYearOfProduction.AutoSize = true;
            labelYearOfProduction.Dock = DockStyle.Fill;
            labelYearOfProduction.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelYearOfProduction.Location = new Point(165, 0);
            labelYearOfProduction.Name = "labelYearOfProduction";
            labelYearOfProduction.Size = new Size(157, 29);
            labelYearOfProduction.TabIndex = 11;
            labelYearOfProduction.Text = "9999";
            labelYearOfProduction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(156, 29);
            label5.TabIndex = 10;
            label5.Text = "Rocznik";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(labelInsuranceTermination, 1, 1);
            tableLayoutPanel2.Controls.Add(labelInsuranceConclusion, 1, 0);
            tableLayoutPanel2.Controls.Add(label6, 0, 0);
            tableLayoutPanel2.Controls.Add(label7, 0, 1);
            tableLayoutPanel2.Location = new Point(365, 50);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(201, 58);
            tableLayoutPanel2.TabIndex = 11;
            // 
            // labelInsuranceTermination
            // 
            labelInsuranceTermination.AutoSize = true;
            labelInsuranceTermination.Dock = DockStyle.Fill;
            labelInsuranceTermination.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelInsuranceTermination.Location = new Point(103, 29);
            labelInsuranceTermination.Name = "labelInsuranceTermination";
            labelInsuranceTermination.Size = new Size(95, 29);
            labelInsuranceTermination.TabIndex = 13;
            labelInsuranceTermination.Text = "Wygaśnięcie";
            labelInsuranceTermination.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelInsuranceConclusion
            // 
            labelInsuranceConclusion.AutoSize = true;
            labelInsuranceConclusion.Dock = DockStyle.Fill;
            labelInsuranceConclusion.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelInsuranceConclusion.Location = new Point(103, 0);
            labelInsuranceConclusion.Name = "labelInsuranceConclusion";
            labelInsuranceConclusion.Size = new Size(95, 29);
            labelInsuranceConclusion.TabIndex = 12;
            labelInsuranceConclusion.Text = "Utworzenie";
            labelInsuranceConclusion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(3, 0);
            label6.Name = "label6";
            label6.Size = new Size(94, 29);
            label6.TabIndex = 0;
            label6.Text = "Utworzenie";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(3, 29);
            label7.Name = "label7";
            label7.Size = new Size(94, 29);
            label7.TabIndex = 1;
            label7.Text = "Wygaśnięcie";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(365, 20);
            label8.Name = "label8";
            label8.Size = new Size(201, 20);
            label8.TabIndex = 12;
            label8.Text = "Ubezpiecznie";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonUpdateInsurance
            // 
            buttonUpdateInsurance.Location = new Point(365, 125);
            buttonUpdateInsurance.Name = "buttonUpdateInsurance";
            buttonUpdateInsurance.Size = new Size(201, 23);
            buttonUpdateInsurance.TabIndex = 13;
            buttonUpdateInsurance.Text = "Zaktualizuj ubezpieczenie";
            buttonUpdateInsurance.UseVisualStyleBackColor = true;
            buttonUpdateInsurance.Click += buttonUpdateInsurance_Click;
            // 
            // buttonUpdateTechnicalReview
            // 
            buttonUpdateTechnicalReview.Location = new Point(601, 125);
            buttonUpdateTechnicalReview.Name = "buttonUpdateTechnicalReview";
            buttonUpdateTechnicalReview.Size = new Size(201, 23);
            buttonUpdateTechnicalReview.TabIndex = 16;
            buttonUpdateTechnicalReview.Text = "Zaktualizuj przegląd";
            buttonUpdateTechnicalReview.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(601, 20);
            label9.Name = "label9";
            label9.Size = new Size(201, 20);
            label9.TabIndex = 15;
            label9.Text = "Przegląd";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(labelNextTechnicalReview, 1, 1);
            tableLayoutPanel3.Controls.Add(labelLastTechnicalReview, 1, 0);
            tableLayoutPanel3.Controls.Add(label12, 0, 0);
            tableLayoutPanel3.Controls.Add(label13, 0, 1);
            tableLayoutPanel3.Location = new Point(601, 50);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(201, 58);
            tableLayoutPanel3.TabIndex = 14;
            // 
            // labelNextTechnicalReview
            // 
            labelNextTechnicalReview.AutoSize = true;
            labelNextTechnicalReview.Dock = DockStyle.Fill;
            labelNextTechnicalReview.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelNextTechnicalReview.Location = new Point(103, 29);
            labelNextTechnicalReview.Name = "labelNextTechnicalReview";
            labelNextTechnicalReview.Size = new Size(95, 29);
            labelNextTechnicalReview.TabIndex = 13;
            labelNextTechnicalReview.Text = "Następny";
            labelNextTechnicalReview.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelLastTechnicalReview
            // 
            labelLastTechnicalReview.AutoSize = true;
            labelLastTechnicalReview.Dock = DockStyle.Fill;
            labelLastTechnicalReview.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelLastTechnicalReview.Location = new Point(103, 0);
            labelLastTechnicalReview.Name = "labelLastTechnicalReview";
            labelLastTechnicalReview.Size = new Size(95, 29);
            labelLastTechnicalReview.TabIndex = 12;
            labelLastTechnicalReview.Text = "Ostatni";
            labelLastTechnicalReview.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Dock = DockStyle.Fill;
            label12.Location = new Point(3, 0);
            label12.Name = "label12";
            label12.Size = new Size(94, 29);
            label12.TabIndex = 0;
            label12.Text = "Ostatni";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Dock = DockStyle.Fill;
            label13.Location = new Point(3, 29);
            label13.Name = "label13";
            label13.Size = new Size(94, 29);
            label13.TabIndex = 1;
            label13.Text = "Następny";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VehiclePanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(buttonUpdateTechnicalReview);
            Controls.Add(label9);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(buttonUpdateInsurance);
            Controls.Add(label8);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(buttonUpdateMileage);
            Controls.Add(labelVehicleName);
            Name = "VehiclePanel";
            Size = new Size(831, 263);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label labelVehicleName;
        private Label label1;
        private Label labelOilType;
        private Label labelLatestMileage;
        private Label label3;
        private Button buttonUpdateMileage;
        private Label label2;
        private Label label4;
        private Label labelRegistrationDate;
        private Label labelPruchaseDate;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelYearOfProduction;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel2;
        private Label labelInsuranceTermination;
        private Label labelInsuranceConclusion;
        private Label label6;
        private Label label7;
        private Label label8;
        private Button buttonUpdateInsurance;
        private Button buttonUpdateTechnicalReview;
        private Label label9;
        private TableLayoutPanel tableLayoutPanel3;
        private Label labelNextTechnicalReview;
        private Label labelLastTechnicalReview;
        private Label label12;
        private Label label13;
    }
}
