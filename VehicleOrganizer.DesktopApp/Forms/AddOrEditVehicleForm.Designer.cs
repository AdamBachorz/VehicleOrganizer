namespace VehicleOrganizer.DesktopApp.Forms
{
    partial class AddOrEditVehicleForm
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
            textBoxName = new TextBox();
            numericUpDownYearOfProduction = new NumericUpDown();
            comboBoxType = new ComboBox();
            buttonAddOrUpdate = new Button();
            label1 = new Label();
            label2 = new Label();
            textBoxOilType = new TextBox();
            label3 = new Label();
            textBoxMileage = new TextBox();
            label4 = new Label();
            label5 = new Label();
            dateTimePickerPurchaseDate = new DateTimePicker();
            label6 = new Label();
            label7 = new Label();
            dateTimePickerRegistrationDate = new DateTimePicker();
            label8 = new Label();
            dateTimePickerInsuranceTermination = new DateTimePicker();
            label9 = new Label();
            dateTimePickerInsuranceConclusion = new DateTimePicker();
            label10 = new Label();
            dateTimePickerNextTechnicalReview = new DateTimePicker();
            label11 = new Label();
            dateTimePickerLastTechnicalReview = new DateTimePicker();
            label12 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownYearOfProduction).BeginInit();
            SuspendLayout();
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(23, 38);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(244, 23);
            textBoxName.TabIndex = 0;
            // 
            // numericUpDownYearOfProduction
            // 
            numericUpDownYearOfProduction.Location = new Point(188, 109);
            numericUpDownYearOfProduction.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            numericUpDownYearOfProduction.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            numericUpDownYearOfProduction.Name = "numericUpDownYearOfProduction";
            numericUpDownYearOfProduction.Size = new Size(50, 23);
            numericUpDownYearOfProduction.TabIndex = 2;
            numericUpDownYearOfProduction.Value = new decimal(new int[] { 1900, 0, 0, 0 });
            // 
            // comboBoxType
            // 
            comboBoxType.FormattingEnabled = true;
            comboBoxType.Location = new Point(23, 105);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(121, 23);
            comboBoxType.TabIndex = 1;
            // 
            // buttonAddOrUpdate
            // 
            buttonAddOrUpdate.Location = new Point(310, 212);
            buttonAddOrUpdate.Name = "buttonAddOrUpdate";
            buttonAddOrUpdate.Size = new Size(186, 45);
            buttonAddOrUpdate.TabIndex = 3;
            buttonAddOrUpdate.Text = "Dodaj pojazd";
            buttonAddOrUpdate.UseVisualStyleBackColor = true;
            buttonAddOrUpdate.Click += buttonAddOrUpdate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 20);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 4;
            label1.Text = "Nazwa pojazdu";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 216);
            label2.Name = "label2";
            label2.Size = new Size(232, 15);
            label2.TabIndex = 6;
            label2.Text = "Symbol oleju (jeśli to pojazd mechaniczny)";
            // 
            // textBoxOilType
            // 
            textBoxOilType.Location = new Point(23, 234);
            textBoxOilType.Name = "textBoxOilType";
            textBoxOilType.Size = new Size(87, 23);
            textBoxOilType.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 159);
            label3.Name = "label3";
            label3.Size = new Size(291, 15);
            label3.TabIndex = 8;
            label3.Text = "Przebieg w dniu nabycia (jeśli to pojazd mechaniczny)";
            // 
            // textBoxMileage
            // 
            textBoxMileage.Location = new Point(23, 177);
            textBoxMileage.Name = "textBoxMileage";
            textBoxMileage.Size = new Size(196, 23);
            textBoxMileage.TabIndex = 3;
            textBoxMileage.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(187, 87);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 9;
            label4.Text = "Rok produkcji";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(23, 87);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 10;
            label5.Text = "Typ pojazdu";
            // 
            // dateTimePickerPurchaseDate
            // 
            dateTimePickerPurchaseDate.Location = new Point(310, 38);
            dateTimePickerPurchaseDate.Name = "dateTimePickerPurchaseDate";
            dateTimePickerPurchaseDate.Size = new Size(200, 23);
            dateTimePickerPurchaseDate.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(310, 20);
            label6.Name = "label6";
            label6.Size = new Size(72, 15);
            label6.TabIndex = 12;
            label6.Text = "Data zakupu";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(310, 87);
            label7.Name = "label7";
            label7.Size = new Size(84, 15);
            label7.TabIndex = 14;
            label7.Text = "Data rejestracji";
            // 
            // dateTimePickerRegistrationDate
            // 
            dateTimePickerRegistrationDate.Location = new Point(310, 105);
            dateTimePickerRegistrationDate.Name = "dateTimePickerRegistrationDate";
            dateTimePickerRegistrationDate.Size = new Size(200, 23);
            dateTimePickerRegistrationDate.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(574, 87);
            label8.Name = "label8";
            label8.Size = new Size(175, 15);
            label8.TabIndex = 18;
            label8.Text = "Data wygaśnięcia ubezpieczenia";
            // 
            // dateTimePickerInsuranceTermination
            // 
            dateTimePickerInsuranceTermination.Location = new Point(574, 105);
            dateTimePickerInsuranceTermination.Name = "dateTimePickerInsuranceTermination";
            dateTimePickerInsuranceTermination.Size = new Size(200, 23);
            dateTimePickerInsuranceTermination.TabIndex = 8;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(574, 20);
            label9.Name = "label9";
            label9.Size = new Size(156, 15);
            label9.TabIndex = 16;
            label9.Text = "Data zawarcia ubezpieczenia";
            // 
            // dateTimePickerInsuranceConclusion
            // 
            dateTimePickerInsuranceConclusion.Location = new Point(574, 38);
            dateTimePickerInsuranceConclusion.Name = "dateTimePickerInsuranceConclusion";
            dateTimePickerInsuranceConclusion.Size = new Size(200, 23);
            dateTimePickerInsuranceConclusion.TabIndex = 7;
            dateTimePickerInsuranceConclusion.ValueChanged += dateTimePickerInsuranceConclusion_ValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(574, 238);
            label10.Name = "label10";
            label10.Size = new Size(225, 15);
            label10.TabIndex = 22;
            label10.Text = "Data następnego przeglądu technicznego";
            // 
            // dateTimePickerNextTechnicalReview
            // 
            dateTimePickerNextTechnicalReview.Location = new Point(574, 256);
            dateTimePickerNextTechnicalReview.Name = "dateTimePickerNextTechnicalReview";
            dateTimePickerNextTechnicalReview.Size = new Size(200, 23);
            dateTimePickerNextTechnicalReview.TabIndex = 10;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(574, 171);
            label11.Name = "label11";
            label11.Size = new Size(219, 15);
            label11.TabIndex = 20;
            label11.Text = "Data ostatniego przeglądu technicznego";
            // 
            // dateTimePickerLastTechnicalReview
            // 
            dateTimePickerLastTechnicalReview.Location = new Point(574, 189);
            dateTimePickerLastTechnicalReview.Name = "dateTimePickerLastTechnicalReview";
            dateTimePickerLastTechnicalReview.Size = new Size(200, 23);
            dateTimePickerLastTechnicalReview.TabIndex = 9;
            dateTimePickerLastTechnicalReview.ValueChanged += dateTimePickerLastTechnicalReview_ValueChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(221, 185);
            label12.Name = "label12";
            label12.Size = new Size(24, 15);
            label12.TabIndex = 23;
            label12.Text = "km";
            // 
            // AddOrEditVehicleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(807, 305);
            Controls.Add(label12);
            Controls.Add(label10);
            Controls.Add(dateTimePickerNextTechnicalReview);
            Controls.Add(label11);
            Controls.Add(dateTimePickerLastTechnicalReview);
            Controls.Add(label8);
            Controls.Add(dateTimePickerInsuranceTermination);
            Controls.Add(label9);
            Controls.Add(dateTimePickerInsuranceConclusion);
            Controls.Add(label7);
            Controls.Add(dateTimePickerRegistrationDate);
            Controls.Add(label6);
            Controls.Add(dateTimePickerPurchaseDate);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBoxMileage);
            Controls.Add(label2);
            Controls.Add(textBoxOilType);
            Controls.Add(label1);
            Controls.Add(buttonAddOrUpdate);
            Controls.Add(comboBoxType);
            Controls.Add(numericUpDownYearOfProduction);
            Controls.Add(textBoxName);
            Name = "AddOrEditVehicleForm";
            Text = "Add or edit vehicle";
            ((System.ComponentModel.ISupportInitialize)numericUpDownYearOfProduction).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxName;
        private NumericUpDown numericUpDownYearOfProduction;
        private ComboBox comboBoxType;
        private Button buttonAddOrUpdate;
        private Label label1;
        private Label label2;
        private TextBox textBoxOilType;
        private Label label3;
        private TextBox textBoxMileage;
        private Label label4;
        private Label label5;
        private DateTimePicker dateTimePickerPurchaseDate;
        private Label label6;
        private Label label7;
        private DateTimePicker dateTimePickerRegistrationDate;
        private Label label8;
        private DateTimePicker dateTimePickerInsuranceTermination;
        private Label label9;
        private DateTimePicker dateTimePickerInsuranceConclusion;
        private Label label10;
        private DateTimePicker dateTimePickerNextTechnicalReview;
        private Label label11;
        private DateTimePicker dateTimePickerLastTechnicalReview;
        private Label label12;
    }
}