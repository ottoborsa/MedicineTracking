
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

        public static string MedicineDecrementQuery(ProgressBar progressBar, DateTime dateFrom, DateTime dateTo)
        {
            ProgressBar = progressBar;

            DataBase db = new DataBase();
            Matrix result = Query.MedicineDecrement.GetResult(dateFrom, dateTo, db.Table_PatientInventory, db.Table_PatientDosage);

            return CsvParser.FromMatrix(result);
        }

        public static string MedicineDepletionProjectionQuery(ProgressBar progressBar)
        {
            ProgressBar = progressBar;

            DataBase db = new DataBase();
            Matrix result = Query.MedicineDepletionProjection.GetResult(db.Table_PatientInventory, db.Table_PatientDosage);

            return CsvParser.FromMatrix(result);
        }
    }
}
