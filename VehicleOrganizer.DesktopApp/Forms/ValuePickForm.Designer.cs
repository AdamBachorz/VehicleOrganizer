namespace VehicleOrganizer.DesktopApp.Forms
{
    partial class ValuePickForm
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
            label1 = new Label();
            textBox1 = new TextBox();
            buttonApply = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(124, 15);
            label1.TabIndex = 0;
            label1.Text = "Podaj żądaną wartość:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 36);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(224, 23);
            textBox1.TabIndex = 1;
            // 
            // buttonApply
            // 
            buttonApply.Location = new Point(12, 65);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(224, 23);
            buttonApply.TabIndex = 2;
            buttonApply.Text = "Zastosuj";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // ValuePickForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(248, 101);
            Controls.Add(buttonApply);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "ValuePickForm";
            Text = "ValuePickForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button buttonApply;
    }
}