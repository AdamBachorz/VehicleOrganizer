namespace VehicleOrganizer.DesktopApp.Panels
{
    partial class OperationalActivityPanel
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
            flowLayoutPanelActivities = new FlowLayoutPanel();
            buttonAddActivity = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            buttonGoBack = new Button();
            checkBoxDebugMode = new CheckBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // flowLayoutPanelActivities
            // 
            flowLayoutPanelActivities.AutoScroll = true;
            flowLayoutPanelActivities.Location = new Point(27, 84);
            flowLayoutPanelActivities.Name = "flowLayoutPanelActivities";
            flowLayoutPanelActivities.Size = new Size(1224, 430);
            flowLayoutPanelActivities.TabIndex = 0;
            // 
            // buttonAddActivity
            // 
            buttonAddActivity.Location = new Point(27, 18);
            buttonAddActivity.Name = "buttonAddActivity";
            buttonAddActivity.Size = new Size(398, 23);
            buttonAddActivity.TabIndex = 1;
            buttonAddActivity.Text = "Dodaj nową czynność";
            buttonAddActivity.UseVisualStyleBackColor = true;
            buttonAddActivity.Click += buttonAddActivity_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(27, 55);
            label1.Name = "label1";
            label1.Size = new Size(152, 26);
            label1.TabIndex = 2;
            label1.Text = "Nazwa";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(409, 55);
            label2.Name = "label2";
            label2.Size = new Size(528, 26);
            label2.TabIndex = 3;
            label2.Text = "Podsumowanie";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(185, 55);
            label3.Name = "label3";
            label3.Size = new Size(218, 26);
            label3.TabIndex = 4;
            label3.Text = "Ostatnia operacja (data lub km)";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonGoBack
            // 
            buttonGoBack.Location = new Point(431, 18);
            buttonGoBack.Name = "buttonGoBack";
            buttonGoBack.Size = new Size(258, 23);
            buttonGoBack.TabIndex = 5;
            buttonGoBack.Text = "Wróć do widoku ze szczegółami pojazdu";
            buttonGoBack.UseVisualStyleBackColor = true;
            buttonGoBack.Click += buttonGoBack_Click;
            // 
            // checkBoxDebugMode
            // 
            checkBoxDebugMode.AutoSize = true;
            checkBoxDebugMode.Checked = true;
            checkBoxDebugMode.CheckState = CheckState.Checked;
            checkBoxDebugMode.Location = new Point(27, 536);
            checkBoxDebugMode.Name = "checkBoxDebugMode";
            checkBoxDebugMode.Size = new Size(100, 19);
            checkBoxDebugMode.TabIndex = 6;
            checkBoxDebugMode.Text = "DEBUG MODE";
            checkBoxDebugMode.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(943, 55);
            label4.Name = "label4";
            label4.Size = new Size(308, 26);
            label4.TabIndex = 7;
            label4.Text = "Operacje";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // OperationalActivityPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label4);
            Controls.Add(checkBoxDebugMode);
            Controls.Add(buttonGoBack);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonAddActivity);
            Controls.Add(flowLayoutPanelActivities);
            Name = "OperationalActivityPanel";
            Size = new Size(1259, 569);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelActivities;
        private Button buttonAddActivity;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button buttonGoBack;
        private CheckBox checkBoxDebugMode;
        private Label label4;
    }
}
