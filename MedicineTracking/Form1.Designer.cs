
namespace MedicineTracking
{
    partial class Form1
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



        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.PatientInventoryFolderTextBox = new System.Windows.Forms.TextBox();
            this.PatientDosageFolderTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.patientInventoryForecast = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.medicineQuantityProject = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Patient inventory folder:";
            // 
            // PatientInventoryFolderTextBox
            // 
            this.PatientInventoryFolderTextBox.Location = new System.Drawing.Point(129, 33);
            this.PatientInventoryFolderTextBox.Name = "PatientInventoryFolderTextBox";
            this.PatientInventoryFolderTextBox.Size = new System.Drawing.Size(362, 20);
            this.PatientInventoryFolderTextBox.TabIndex = 1;
            // 
            // PatientDosageFolderTextBox
            // 
            this.PatientDosageFolderTextBox.Location = new System.Drawing.Point(129, 59);
            this.PatientDosageFolderTextBox.Name = "PatientDosageFolderTextBox";
            this.PatientDosageFolderTextBox.Size = new System.Drawing.Size(362, 20);
            this.PatientDosageFolderTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Patient dosage folder:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Date range:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(120, 16);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(210, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // patientInventoryForecast
            // 
            this.patientInventoryForecast.Location = new System.Drawing.Point(336, 44);
            this.patientInventoryForecast.Name = "patientInventoryForecast";
            this.patientInventoryForecast.Size = new System.Drawing.Size(210, 23);
            this.patientInventoryForecast.TabIndex = 6;
            this.patientInventoryForecast.Text = "Patient inventory forecast";
            this.patientInventoryForecast.UseVisualStyleBackColor = true;
            this.patientInventoryForecast.Click += new System.EventHandler(this.patientInventoryForecast_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(497, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Select folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(497, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Select folder";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(336, 16);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(210, 20);
            this.dateTimePicker2.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 121);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 128);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.medicineQuantityProject);
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Controls.Add(this.dateTimePicker2);
            this.tabPage1.Controls.Add(this.patientInventoryForecast);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(552, 102);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Medicine tracking";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // medicineQuantityProject
            // 
            this.medicineQuantityProject.Location = new System.Drawing.Point(336, 73);
            this.medicineQuantityProject.Name = "medicineQuantityProject";
            this.medicineQuantityProject.Size = new System.Drawing.Size(210, 23);
            this.medicineQuantityProject.TabIndex = 10;
            this.medicineQuantityProject.Text = "Medicine quantity project";
            this.medicineQuantityProject.UseVisualStyleBackColor = true;
            this.medicineQuantityProject.Click += new System.EventHandler(this.medicineQuantityProject_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.hToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // hToolStripMenuItem
            // 
            this.hToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.releaseInfoToolStripMenuItem});
            this.hToolStripMenuItem.Name = "hToolStripMenuItem";
            this.hToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hToolStripMenuItem.Text = "Help";
            // 
            // releaseInfoToolStripMenuItem
            // 
            this.releaseInfoToolStripMenuItem.Name = "releaseInfoToolStripMenuItem";
            this.releaseInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.releaseInfoToolStripMenuItem.Text = "Release info";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PatientDosageFolderTextBox);
            this.Controls.Add(this.PatientInventoryFolderTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PatientInventoryFolderTextBox;
        private System.Windows.Forms.TextBox PatientDosageFolderTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button patientInventoryForecast;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button medicineQuantityProject;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseInfoToolStripMenuItem;
    }
}

