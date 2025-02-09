

using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

using MedicineTracking.Core;
using MedicineTracking.Utility;
using MedicineTracking.Messaging;


namespace MedicineTracking
{
    public partial class Form1 : Form
    {


        public const string DefaultInventoryFolder = "patient inventory";

        public const string DefaultDosageFolder = "patient dosage";



        public Form1()
        {
            InitializeComponent();

            Text = nameof(MedicineTracking);
            tabControl1.SelectedTab = tabPage1;

            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1).AddDays(-1);

            PatientInventoryFolderTextBox.Text = Path.Combine(Directory.GetCurrentDirectory(), DefaultInventoryFolder);
            PatientDosageFolderTextBox.Text = Path.Combine(Directory.GetCurrentDirectory(), DefaultDosageFolder);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }





        private void Try(Action action)
        {
            try
            {
                SetControlsState(false);
                action.Invoke();
            }
            catch (SerializedException ex)
            {
                Exception(ex);
            }
            catch (Exception ex)
            {
                Exception(ex);
            }
            finally
            {
                ApplicationInterface.SetProgressBarValue(0);
                SetControlsState(true);
            }
        }

        private void medicineDecrement_Click(object sender, EventArgs e)
        {
            DateTime dateFrom = dateTimePicker1.Value;
            DateTime dateTo = dateTimePicker2.Value;
            string dateStr = DateTools.GetTodayString();
            string dateFromStr = dateFrom.ToString(DateTools.DayPattern);
            string dateToStr = dateTo.ToString(DateTools.DayPattern);

            Try(() =>
            {
                SaveFile(
                    $"{dateStr} - {nameof(ApplicationInterface.MedicineDecrementQuery)} - {dateFrom} - {dateTo}",
                    ApplicationInterface.MedicineDecrementQuery(dateFrom, dateTo),
                    DataBase.FileExtension
                );
            });
        }

        private void medicineDepletionProjection_Click(object sender, EventArgs e)
        {
            string dateStr = DateTools.GetTodayString();

            Try(() =>
            {
                SaveFile(
                    $"{dateStr} - {nameof(ApplicationInterface.MedicineDepletionProjectionQuery)}",
                    ApplicationInterface.MedicineDepletionProjectionQuery(),
                    DataBase.FileExtension
                );
            });
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

        private void SaveFile(string fileName, string fileContent, string fileExtension)
        {
            using (saveFileDialog1)
            {
                saveFileDialog1.FileName = fileName;
                saveFileDialog1.Filter = $"{fileExtension} files (*.{fileExtension})|*.{fileExtension}|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, fileContent, ApplicationInterface.ApplicationEncoding);
                }
            }
        }

        private void SetControlsState(bool state)
        {
            button1.Enabled = state;
            button2.Enabled = state;
            medicineDecrement.Enabled = state;
            medicineDepletionProjection.Enabled = state;
            dateTimePicker1.Enabled = state;
            dateTimePicker2.Enabled = state;
        }

        private static void Exception(Exception ex)
        {
            ErrorDialog(ex.GetType(), ex.Message);
        }

        private static void Exception(SerializedException ex)
        {
            SystemError error = JsonConvert.DeserializeObject<SystemError>(SystemError.ParseException(ex).ErrorMessage);

            ErrorDialog(ex.GetType(), JsonConvert.SerializeObject(error, Formatting.Indented));
        }

        private static DialogResult ErrorDialog(Type type, string message)
        {
            return MessageBox.Show(
                message,
                $"Application {type} occured!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        private void releaseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                JsonConvert.SerializeObject(new ReleaseInfo(), Formatting.Indented),
                "Release information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
