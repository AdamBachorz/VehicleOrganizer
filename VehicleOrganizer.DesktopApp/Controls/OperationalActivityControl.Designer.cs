namespace VehicleOrganizer.DesktopApp.Controls
{
    partial class OperationalActivityControl
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
            tableLayoutPanel1 = new TableLayoutPanel();
            labelSummaryPrompt = new Label();
            labelLastOperationDateOrMileageWhenPerformed = new Label();
            labelName = new Label();
            buttonEdit = new Button();
            buttonDelete = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.61702F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56.38298F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 570F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 58F));
            tableLayoutPanel1.Controls.Add(buttonDelete, 4, 0);
            tableLayoutPanel1.Controls.Add(labelSummaryPrompt, 0, 0);
            tableLayoutPanel1.Controls.Add(labelLastOperationDateOrMileageWhenPerformed, 0, 0);
            tableLayoutPanel1.Controls.Add(labelName, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonEdit, 3, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1224, 58);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // labelSummaryPrompt
            // 
            labelSummaryPrompt.AutoSize = true;
            labelSummaryPrompt.Dock = DockStyle.Fill;
            labelSummaryPrompt.Location = new Point(233, 1);
            labelSummaryPrompt.Name = "labelSummaryPrompt";
            labelSummaryPrompt.Size = new Size(290, 56);
            labelSummaryPrompt.TabIndex = 2;
            labelSummaryPrompt.Text = "Podsumowanie";
            labelSummaryPrompt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelLastOperationDateOrMileageWhenPerformed
            // 
            labelLastOperationDateOrMileageWhenPerformed.AutoSize = true;
            labelLastOperationDateOrMileageWhenPerformed.Dock = DockStyle.Fill;
            labelLastOperationDateOrMileageWhenPerformed.Location = new Point(530, 1);
            labelLastOperationDateOrMileageWhenPerformed.Name = "labelLastOperationDateOrMileageWhenPerformed";
            labelLastOperationDateOrMileageWhenPerformed.Size = new Size(564, 56);
            labelLastOperationDateOrMileageWhenPerformed.TabIndex = 1;
            labelLastOperationDateOrMileageWhenPerformed.Text = "Ostatnia operacja (data lub km)";
            labelLastOperationDateOrMileageWhenPerformed.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Dock = DockStyle.Fill;
            labelName.Location = new Point(4, 1);
            labelName.Name = "labelName";
            labelName.Size = new Size(222, 56);
            labelName.TabIndex = 0;
            labelName.Text = "Nazwa";
            labelName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonEdit
            // 
            buttonEdit.Dock = DockStyle.Fill;
            buttonEdit.Location = new Point(1101, 4);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(59, 50);
            buttonEdit.TabIndex = 3;
            buttonEdit.Text = "E";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Dock = DockStyle.Fill;
            buttonDelete.Location = new Point(1167, 4);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(53, 50);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "D";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // OperationalActivityControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "OperationalActivityControl";
            Size = new Size(1224, 58);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label labelName;
        private Label labelLastOperationDateOrMileageWhenPerformed;
        private Label labelSummaryPrompt;
        private Button buttonDelete;
        private Button buttonEdit;
    }
}
