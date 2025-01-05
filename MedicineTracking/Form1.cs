
using System;
using System.Windows.Forms;


namespace MedicineTracking
{
    public partial class Form1 : Form
    {


        public const string DefaultInventoryFolder = "Y:\\000000000000000\\PATIENT_INVENTORY";

        public const string DefaultDosageFolder = "Y:\\000000000000000\\PATIENT_DOSAGE";



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

        private void generate_Click(object sender, EventArgs e)
        {
            MedicineTracking.Generate(PatientInventoryFolderTextBox.Text, PatientDosageFolderTextBox.Text);
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
    }
}
