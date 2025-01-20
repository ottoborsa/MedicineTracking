
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using MedicineTracking.Model;
using MedicineTracking.Utility;

namespace MedicineTracking.Core
{


    public static class Common
    {

        public const string FileExtension = "csv";

        private static ProgressBar ProgressBar;






        public static void SetProgressBarValue(int value)
        {
            if (ProgressBar != null && ProgressBar.Value != value)
            {
                ProgressBar.Value = value;
            }
        }

        private static Dictionary<string, string> GetFolderContent(string folder)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (string file in Directory.EnumerateFiles(folder, $"*.{FileExtension}"))
            {
                result.Add(file, File.ReadAllText(file));
            }

            return result;
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

            List<PatientInventory> patientInventories = Table.PatientInventory.Parse(GetFolderContent(inventoryFolderPath));
            List<MedicineDosage> medicineDosages = Table.PatientDosage.Parse(GetFolderContent(dosageFolderPath));

            Matrix result = Query.MedicineDecrement.GetResult(dateFrom, dateTo, patientInventories, medicineDosages);

            return CsvParser.FromMatrix(result);
        }

        public static string MedicineDepletionProjectionQuery(
            ProgressBar progressBar,
            string inventoryFolderPath,
            string dosageFolderPath
        )
        {
            ProgressBar = progressBar;

            List<PatientInventory> patientInventories = Table.PatientInventory.Parse(GetFolderContent(inventoryFolderPath));
            List<MedicineDosage> medicineDosages = Table.PatientDosage.Parse(GetFolderContent(dosageFolderPath));

            Matrix result = Query.MedicineDepletionProjection.GetResult(patientInventories, medicineDosages);

            return CsvParser.FromMatrix(result);
        }
    }
}
