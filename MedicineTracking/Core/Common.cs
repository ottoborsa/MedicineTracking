
using System;
using System.Collections.Generic;
using System.IO;

using MedicineTracking.Model;
using MedicineTracking.Utility;


namespace MedicineTracking.Core
{


    public static class Common
    {

        public const string FileExtension = "csv";


        private static Dictionary<string, string> GetFolderContent(string folder)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (string file in Directory.EnumerateFiles(folder, $"*.{FileExtension}"))
            {
                result.Add(file, File.ReadAllText(file));
            }

            return result;
        }

        public static void MedicineDecrementQuery(string inventoryFolder, string dosageFolder, DateTime dateFrom, DateTime dateTo)
        {
            List<PatientInventory> patientInventories = Table.PatientInventory.Parse(GetFolderContent(inventoryFolder));
            List<MedicineDosage> medicineDosages = Table.PatientDosage.Parse(GetFolderContent(dosageFolder));


            Matrix result = Query.MedicineDecrement.GetResult(dateFrom, dateTo, patientInventories, medicineDosages);

        }



        public static void MedicineDepletionProjectionQuery(string inventoryFolder, string dosageFolder, DateTime dateFrom, DateTime dateTo)
        {
            List<PatientInventory> patientInventories = Table.PatientInventory.Parse(GetFolderContent(inventoryFolder));
            List<MedicineDosage> medicineDosages = Table.PatientDosage.Parse(GetFolderContent(dosageFolder));


            Matrix result = Query.MedicineDepletionProjection.GetResult(dateFrom, dateTo, patientInventories, medicineDosages);

        }
    }
}
