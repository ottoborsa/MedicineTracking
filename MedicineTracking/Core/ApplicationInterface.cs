
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using MedicineTracking.Utility;

#nullable enable
namespace MedicineTracking.Core
{


    public static class ApplicationInterface
    {

        private static Form1 MainForm = Program.MainForm;

        private static ProgressBar? ProgressBar = MainForm.progressBar1;

        public static System.Text.Encoding ApplicationEncoding { get; private set; } = System.Text.Encoding.Default;




        public static Dictionary<string, string> GetDataBaseTableFolders()
        {
            return new()
            {
                { nameof(Table.PatientInventory), MainForm.PatientInventoryFolderTextBox.Text.Trim() },
                { nameof(Table.PatientDosage), MainForm.PatientDosageFolderTextBox.Text.Trim() }
            };
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
            return CsvParser.FromMatrix(Query.MedicineDecrement.GetResult(new DataBase(), dateFrom, dateTo));
        }

        public static string MedicineDepletionProjectionQuery()
        {
            return CsvParser.FromMatrix(Query.MedicineDepletionProjection.GetResult(new DataBase()));
        }
    }
}
#nullable disable
