namespace VehicleOrganizer.DesktopApp.Forms
{
    partial class MainForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            mainPanel = new Panel();
            menuStrip = new MenuStrip();
            toolStripMenuOptions = new ToolStripMenuItem();
            toolStripMenuItemAddNewVehicle = new ToolStripMenuItem();
            toolStripMenuItemSelectVehicle = new ToolStripMenuItem();
            pojazdToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItemOpenActivities = new ToolStripMenuItem();
            toolStripMenuAdmin = new ToolStripMenuItem();
            toolStripMenuItemOpenAdminTools = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(mainPanel, 0, 1);
            tableLayoutPanel1.Controls.Add(menuStrip, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.33333349F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 94.6666641F));
            tableLayoutPanel1.Size = new Size(1000, 557);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mainPanel
            // 
            mainPanel.AutoSize = true;
            mainPanel.Location = new Point(3, 32);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(0, 0);
            mainPanel.TabIndex = 0;
            // 
            // menuStrip
            // 
            menuStrip.BackColor = SystemColors.ControlLight;
            menuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuOptions, pojazdToolStripMenuItem, toolStripMenuAdmin });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1000, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuOptions
            // 
            toolStripMenuOptions.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItemAddNewVehicle, toolStripMenuItemSelectVehicle });
            toolStripMenuOptions.Name = "toolStripMenuOptions";
            toolStripMenuOptions.Size = new Size(50, 20);
            toolStripMenuOptions.Text = "Opcje";
            // 
            // toolStripMenuItemAddNewVehicle
            // 
            toolStripMenuItemAddNewVehicle.Name = "toolStripMenuItemAddNewVehicle";
            toolStripMenuItemAddNewVehicle.Size = new Size(175, 22);
            toolStripMenuItemAddNewVehicle.Text = "Dodaj nowy pojazd";
            toolStripMenuItemAddNewVehicle.Click += toolStripMenuItemAddNewVehicle_Click;
            // 
            // toolStripMenuItemSelectVehicle
            // 
            toolStripMenuItemSelectVehicle.Name = "toolStripMenuItemSelectVehicle";
            toolStripMenuItemSelectVehicle.Size = new Size(175, 22);
            toolStripMenuItemSelectVehicle.Text = "Wybierz pojazd";
            toolStripMenuItemSelectVehicle.Click += toolStripMenuItemSelectVehicle_Click;
            // 
            // pojazdToolStripMenuItem
            // 
            pojazdToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItemOpenActivities });
            pojazdToolStripMenuItem.Name = "pojazdToolStripMenuItem";
            pojazdToolStripMenuItem.Size = new Size(54, 20);
            pojazdToolStripMenuItem.Text = "Pojazd";
            // 
            // toolStripMenuItemOpenActivities
            // 
            toolStripMenuItemOpenActivities.Name = "toolStripMenuItemOpenActivities";
            toolStripMenuItemOpenActivities.Size = new Size(306, 22);
            toolStripMenuItemOpenActivities.Text = "Zobacz aktualne czynności dla tego pojazdu";
            toolStripMenuItemOpenActivities.Click += toolStripMenuItemOpenActivities_Click;
            // 
            // toolStripMenuAdmin
            // 
            toolStripMenuAdmin.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItemOpenAdminTools });
            toolStripMenuAdmin.Name = "toolStripMenuAdmin";
            toolStripMenuAdmin.Size = new Size(55, 20);
            toolStripMenuAdmin.Text = "Admin";
            // 
            // toolStripMenuItemOpenAdminTools
            // 
            toolStripMenuItemOpenAdminTools.Name = "toolStripMenuItemOpenAdminTools";
            toolStripMenuItemOpenAdminTools.Size = new Size(148, 22);
            toolStripMenuItemOpenAdminTools.Text = "Okno Admina";
            toolStripMenuItemOpenAdminTools.Click += toolStripMenuItemOpenAdminTools_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1000, 557);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            Load += MainForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel mainPanel;
        private MenuStrip menuStrip;
        private ToolStripMenuItem toolStripMenuOptions;
        private ToolStripMenuItem toolStripMenuItemAddNewVehicle;
        private ToolStripMenuItem toolStripMenuItemSelectVehicle;
        private ToolStripMenuItem toolStripMenuAdmin;
        private ToolStripMenuItem pojazdToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemOpenActivities;
        private ToolStripMenuItem toolStripMenuItemOpenAdminTools;
    }
}