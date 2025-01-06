
using System;
using System.Windows.Forms;


namespace MedicineTracking
{
    public partial class Form1 : Form
    {


        public const string DefaultInventoryFolder = "X:\\REPOS\\000\\PATIENT_INVENTORY";

        public const string DefaultDosageFolder = "X:\\REPOS\\000\\PATIENT_DOSAGE";



        public Form1()
        {
            InitializeComponent();
            Text = nameof(MedicineTracking);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PatientInventoryFolderTextBox.Text = DefaultInventoryFolder;
            PatientDosageFolderTextBox.Text = DefaultDosageFolder;
        }


        private void patientInventoryForecast_Click(object sender, EventArgs e)
        {
            MedicineTracking.GenerateInventoryForecast(
                PatientInventoryFolderTextBox.Text,
                PatientDosageFolderTextBox.Text,
                dateTimePicker1.Value,
                dateTimePicker2.Value
            );
        }

        private void medicineQuantityProject_Click(object sender, EventArgs e)
        {

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
            this.Close();
        }
    }
}
