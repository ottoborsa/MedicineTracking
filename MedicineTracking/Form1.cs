

using System;
using System.Windows.Forms;

using MedicineTracking.Core;


namespace MedicineTracking
{
    public partial class Form1 : Form
    {


        public const string DefaultInventoryFolder = "x:\\REPOS\\_test_medicine_database\\patient inventory\\";

        public const string DefaultDosageFolder = "x:\\REPOS\\_test_medicine_database\\patient dosage\\";



        public Form1()
        {
            InitializeComponent();
            Text = nameof(Common);
            tabControl1.SelectedTab = tabPage1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PatientInventoryFolderTextBox.Text = DefaultInventoryFolder;
            PatientDosageFolderTextBox.Text = DefaultDosageFolder;
        }


        private void medicineDecrement_Click(object sender, EventArgs e)
        {
            Common.MedicineDecrementQuery(
                PatientInventoryFolderTextBox.Text,
                PatientDosageFolderTextBox.Text,
                dateTimePicker1.Value,
                dateTimePicker2.Value
            );
        }

        private void medicineDepletionProjection_Click(object sender, EventArgs e)
        {
            Common.MedicineDepletionProjectionQuery(
                PatientInventoryFolderTextBox.Text,
                PatientDosageFolderTextBox.Text,
                dateTimePicker1.Value,
                dateTimePicker2.Value
            );
        }


        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = PatientInventoryFolderTextBox.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                PatientInventoryFolderTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = PatientDosageFolderTextBox.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                PatientDosageFolderTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
