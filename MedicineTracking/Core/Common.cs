
using System;
using System.Windows.Forms;
using MedicineTracking.Utility;

namespace MedicineTracking.Core
{


    public static class Common
    {

        private static ProgressBar ProgressBar;




        public static void SetProgressBarValue(int value)
        {
            if (ProgressBar != null && ProgressBar.Value != value)
            {
                ProgressBar.Value = value;
            }
        }

        public static string MedicineDecrementQuery(
            ProgressBar progressBar,
            string inventoryFolderPath,
            string dosageFolderPath,
            DateTime dateFrom,
            DateTime dateTo
        )
        {
            ProgressBar = progressBar;

            DataBase db = new DataBase(inventoryFolderPath, dosageFolderPath);
            Matrix result = Query.MedicineDecrement.GetResult(dateFrom, dateTo, db.Table_PatientInventory, db.Table_PatientDosage);

            return CsvParser.FromMatrix(result);
        }

        public static string MedicineDepletionProjectionQuery(
            ProgressBar progressBar,
            string inventoryFolderPath,
            string dosageFolderPath
        )
        {
            ProgressBar = progressBar;

            DataBase db = new DataBase(inventoryFolderPath, dosageFolderPath);
            Matrix result = Query.MedicineDepletionProjection.GetResult(db.Table_PatientInventory, db.Table_PatientDosage);

            return CsvParser.FromMatrix(result);
        }
    }
}
