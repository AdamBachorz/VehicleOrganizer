namespace VehicleOrganizer.DesktopApp.Forms
{
    partial class AddOrEditOperationalActivityForm
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
            buttonAddOrUpdate = new Button();
            label1 = new Label();
            textBoxName = new TextBox();
            groupBoxDateOperated = new GroupBox();
            label4 = new Label();
            numericUpDownYearStep = new NumericUpDown();
            label3 = new Label();
            dateTimePickerLastOperationDate = new DateTimePicker();
            groupBoxMileageOperated = new GroupBox();
            label8 = new Label();
            label7 = new Label();
            textBoxMileageStep = new TextBox();
            label6 = new Label();
            textBoxMileageWhenPerformed = new TextBox();
            label5 = new Label();
            radioButtonIsDateOperated = new RadioButton();
            radioButtonIsMileageOperated = new RadioButton();
            label2 = new Label();
            checkBoxDebugMode = new CheckBox();
            groupBoxDateOperated.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownYearStep).BeginInit();
            groupBoxMileageOperated.SuspendLayout();
            SuspendLayout();
            // 
            // buttonAddOrUpdate
            // 
            buttonAddOrUpdate.Location = new Point(158, 313);
            buttonAddOrUpdate.Name = "buttonAddOrUpdate";
            buttonAddOrUpdate.Size = new Size(260, 23);
            buttonAddOrUpdate.TabIndex = 0;
            buttonAddOrUpdate.Text = "Zaktualizuj dane";
            buttonAddOrUpdate.UseVisualStyleBackColor = true;
            buttonAddOrUpdate.Click += buttonAddOrAddOrUpdate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 15);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 1;
            label1.Text = "Nazwa operacji";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(12, 33);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(269, 23);
            textBoxName.TabIndex = 2;
            // 
            // groupBoxDateOperated
            // 
            groupBoxDateOperated.Controls.Add(label4);
            groupBoxDateOperated.Controls.Add(numericUpDownYearStep);
            groupBoxDateOperated.Controls.Add(label3);
            groupBoxDateOperated.Controls.Add(dateTimePickerLastOperationDate);
            groupBoxDateOperated.Location = new Point(11, 135);
            groupBoxDateOperated.Name = "groupBoxDateOperated";
            groupBoxDateOperated.Size = new Size(261, 145);
            groupBoxDateOperated.TabIndex = 3;
            groupBoxDateOperated.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 81);
            label4.Name = "label4";
            label4.Size = new Size(157, 15);
            label4.TabIndex = 4;
            label4.Text = "Co ile lat trzeba ją powtarzać";
            // 
            // numericUpDownYearStep
            // 
            numericUpDownYearStep.Location = new Point(9, 98);
            numericUpDownYearStep.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownYearStep.Name = "numericUpDownYearStep";
            numericUpDownYearStep.Size = new Size(40, 23);
            numericUpDownYearStep.TabIndex = 3;
            numericUpDownYearStep.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 19);
            label3.Name = "label3";
            label3.Size = new Size(136, 15);
            label3.TabIndex = 2;
            label3.Text = "Data wykonania operacji";
            // 
            // dateTimePickerLastOperationDate
            // 
            dateTimePickerLastOperationDate.Location = new Point(9, 37);
            dateTimePickerLastOperationDate.Name = "dateTimePickerLastOperationDate";
            dateTimePickerLastOperationDate.Size = new Size(200, 23);
            dateTimePickerLastOperationDate.TabIndex = 0;
            // 
            // groupBoxMileageOperated
            // 
            groupBoxMileageOperated.Controls.Add(label8);
            groupBoxMileageOperated.Controls.Add(label7);
            groupBoxMileageOperated.Controls.Add(textBoxMileageStep);
            groupBoxMileageOperated.Controls.Add(label6);
            groupBoxMileageOperated.Controls.Add(textBoxMileageWhenPerformed);
            groupBoxMileageOperated.Controls.Add(label5);
            groupBoxMileageOperated.Location = new Point(294, 135);
            groupBoxMileageOperated.Name = "groupBoxMileageOperated";
            groupBoxMileageOperated.Size = new Size(242, 145);
            groupBoxMileageOperated.TabIndex = 4;
            groupBoxMileageOperated.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(100, 105);
            label8.Name = "label8";
            label8.Size = new Size(24, 15);
            label8.TabIndex = 8;
            label8.Text = "km";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(154, 43);
            label7.Name = "label7";
            label7.Size = new Size(24, 15);
            label7.TabIndex = 7;
            label7.Text = "km";
            // 
            // textBoxMileageStep
            // 
            textBoxMileageStep.Enabled = false;
            textBoxMileageStep.Location = new Point(7, 97);
            textBoxMileageStep.Name = "textBoxMileageStep";
            textBoxMileageStep.Size = new Size(87, 23);
            textBoxMileageStep.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(7, 80);
            label6.Name = "label6";
            label6.Size = new Size(213, 15);
            label6.TabIndex = 5;
            label6.Text = "Co ile kilometrów należy ją wykonywać";
            // 
            // textBoxMileageWhenPerformed
            // 
            textBoxMileageWhenPerformed.Enabled = false;
            textBoxMileageWhenPerformed.Location = new Point(7, 37);
            textBoxMileageWhenPerformed.Name = "textBoxMileageWhenPerformed";
            textBoxMileageWhenPerformed.Size = new Size(141, 23);
            textBoxMileageWhenPerformed.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 19);
            label5.Name = "label5";
            label5.Size = new Size(229, 15);
            label5.TabIndex = 3;
            label5.Text = "Przebieg w momencie wykonania operacji";
            // 
            // radioButtonIsDateOperated
            // 
            radioButtonIsDateOperated.AutoSize = true;
            radioButtonIsDateOperated.Checked = true;
            radioButtonIsDateOperated.Location = new Point(11, 110);
            radioButtonIsDateOperated.Name = "radioButtonIsDateOperated";
            radioButtonIsDateOperated.Size = new Size(109, 19);
            radioButtonIsDateOperated.TabIndex = 5;
            radioButtonIsDateOperated.TabStop = true;
            radioButtonIsDateOperated.Text = "Datę wykonania";
            radioButtonIsDateOperated.UseVisualStyleBackColor = true;
            radioButtonIsDateOperated.CheckedChanged += radioButtonIsDateOperated_CheckedChanged;
            // 
            // radioButtonIsMileageOperated
            // 
            radioButtonIsMileageOperated.AutoSize = true;
            radioButtonIsMileageOperated.Location = new Point(301, 110);
            radioButtonIsMileageOperated.Name = "radioButtonIsMileageOperated";
            radioButtonIsMileageOperated.Size = new Size(70, 19);
            radioButtonIsMileageOperated.TabIndex = 6;
            radioButtonIsMileageOperated.TabStop = true;
            radioButtonIsMileageOperated.Text = "Przebieg";
            radioButtonIsMileageOperated.UseVisualStyleBackColor = true;
            radioButtonIsMileageOperated.CheckedChanged += radioButtonIsMileageOperated_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(126, 81);
            label2.Name = "label2";
            label2.Size = new Size(292, 15);
            label2.TabIndex = 7;
            label2.Text = "Czy powyższa czynność jest wykonywana w oparciu o:";
            // 
            // checkBoxDebugMode
            // 
            checkBoxDebugMode.AutoSize = true;
            checkBoxDebugMode.Checked = true;
            checkBoxDebugMode.CheckState = CheckState.Checked;
            checkBoxDebugMode.Location = new Point(11, 349);
            checkBoxDebugMode.Name = "checkBoxDebugMode";
            checkBoxDebugMode.Size = new Size(100, 19);
            checkBoxDebugMode.TabIndex = 8;
            checkBoxDebugMode.Text = "DEBUG MODE";
            checkBoxDebugMode.UseVisualStyleBackColor = true;
            // 
            // AddOrEditOperationalActivityForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 395);
            Controls.Add(checkBoxDebugMode);
            Controls.Add(label2);
            Controls.Add(radioButtonIsMileageOperated);
            Controls.Add(radioButtonIsDateOperated);
            Controls.Add(groupBoxMileageOperated);
            Controls.Add(groupBoxDateOperated);
            Controls.Add(textBoxName);
            Controls.Add(label1);
            Controls.Add(buttonAddOrUpdate);
            Name = "AddOrEditOperationalActivityForm";
            Text = "AddOrEditOperationalActivityForm";
            groupBoxDateOperated.ResumeLayout(false);
            groupBoxDateOperated.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownYearStep).EndInit();
            groupBoxMileageOperated.ResumeLayout(false);
            groupBoxMileageOperated.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAddOrUpdate;
        private Label label1;
        private TextBox textBoxName;
        private GroupBox groupBoxDateOperated;
        private RadioButton radioButtonIsDateOperated;
        private GroupBox groupBoxMileageOperated;
        private RadioButton radioButtonIsMileageOperated;
        private Label label2;
        private Label label3;
        private DateTimePicker dateTimePickerLastOperationDate;
        private Label label4;
        private NumericUpDown numericUpDownYearStep;
        private Label label8;
        private Label label7;
        private TextBox textBoxMileageStep;
        private Label label6;
        private TextBox textBoxMileageWhenPerformed;
        private Label label5;
        private CheckBox checkBoxDebugMode;
    }
}