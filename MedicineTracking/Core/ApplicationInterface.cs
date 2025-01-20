
using System;
using System.Windows.Forms;

using MedicineTracking.Utility;

#nullable enable
namespace MedicineTracking.Core
{


    public static class ApplicationInterface
    {

        private static Form1 MainForm = Program.MainForm;

        private static ProgressBar? ProgressBar = MainForm.progressBar1;




        public static (string, string) GetDataBaseTableFolders()
        {
            return (
                MainForm.PatientInventoryFolderTextBox.Text.Trim(),
                MainForm.PatientDosageFolderTextBox.Text.Trim()
            );
        }

        public static void SetProgressBarValue(int value)
        {
            if (ProgressBar != null && ProgressBar.Value != value)
            {
                ProgressBar.Value = value;
            }
        }

        public static string MedicineDecrementQuery(DateTime dateFrom, DateTime dateTo)
        {
            DataBase db = new DataBase();
            Matrix result = Query.MedicineDecrement.GetResult(dateFrom, dateTo, db.Table_PatientInventory, db.Table_PatientDosage);

            return CsvParser.FromMatrix(result);
        }

        public static string MedicineDepletionProjectionQuery()
        {
            DataBase db = new DataBase();
            Matrix result = Query.MedicineDepletionProjection.GetResult(db.Table_PatientInventory, db.Table_PatientDosage);

            return CsvParser.FromMatrix(result);
        }
    }
}
#nullable disable
