

using System;
using System.IO;
using System.Windows.Forms;

using MedicineTracking.Core;
using MedicineTracking.Utility;


namespace MedicineTracking
{
    public partial class Form1 : Form
    {


        public const string DefaultInventoryFolder = "x:\\REPOS\\_test_medicine_database\\patient inventory\\";

        public const string DefaultDosageFolder = "x:\\REPOS\\_test_medicine_database\\patient dosage\\";



        public Form1()
        {
            InitializeComponent();

            Text = nameof(MedicineTracking);
            tabControl1.SelectedTab = tabPage1;

            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1).AddDays(-1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PatientInventoryFolderTextBox.Text = DefaultInventoryFolder;
            PatientDosageFolderTextBox.Text = DefaultDosageFolder;
        }


        private void medicineDecrement_Click(object sender, EventArgs e)
        {
            SaveFile(
                $"{GetNow()} - {nameof(Common.MedicineDecrementQuery)} - {dateTimePicker1.Value.ToString(DateTools.DayPattern)} - {dateTimePicker2.Value.ToString(DateTools.DayPattern)}",
                Common.MedicineDecrementQuery(
                    PatientInventoryFolderTextBox.Text,
                    PatientDosageFolderTextBox.Text,
                    dateTimePicker1.Value,
                    dateTimePicker2.Value
                ),
                Common.FileExtension
            );
        }

        private void medicineDepletionProjection_Click(object sender, EventArgs e)
        {
            SaveFile(
                $"{GetNow()} - {nameof(Common.MedicineDepletionProjectionQuery)}",
                Common.MedicineDepletionProjectionQuery(
                    PatientInventoryFolderTextBox.Text,
                    PatientDosageFolderTextBox.Text
                ),
                Common.FileExtension
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

        private void SaveFile(string defaultFilename, string fileContent, string fileExtension)
        {
            using (saveFileDialog1)
            {
                saveFileDialog1.FileName = defaultFilename;
                saveFileDialog1.Filter = $"{fileExtension} files (*.{fileExtension})|*.{fileExtension}|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, fileContent);
                }
            }
        }

        private static string GetNow()
        {
            return DateTime.Now.ToString(DateTools.DayPattern);
        }




        private void releaseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            DialogResult result = MessageBox.Show("message", "caption", buttons);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
