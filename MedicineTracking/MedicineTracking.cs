
using System;
using System.Collections.Generic;
using System.IO;
using Csv;
using MedicineTracking.Model;


namespace MedicineTracking
{

    using FolderContent = Dictionary<string, string>;


    public static class MedicineTracking
    {

        public const string ColumnMedicineId = "medicine_id";

        public const string ColumnMedicineName = "medicine_name";

        public const string ColumnIngredientContent = "ingredient_content";

        public const string ColumnInventoryPrefix = "inventory_";

        public const string ColumnDailyDosage = "daily_dosage";

        public const string ColumnValidFrom = "valid_from";

        public const string ColumnValidTo = "valid_to";







        private static FolderContent GetFolderContent(string folder)
        {
            FolderContent result = new FolderContent();

            foreach (string file in Directory.EnumerateFiles(folder, "*.csv"))
            {
                result.Add(file, File.ReadAllText(file));
            }

            return result;
        }



        public static void Generate(string inventoryFolder, string dosageFolder)
        {

            List<PatientInventory> patientInventories = ParsePatientInventory(GetFolderContent(inventoryFolder));


            List<MedicineDosage> medicineDosages = ParsePatientDosage(GetFolderContent(dosageFolder));





            ;




        }


        private static List<MedicineDosage> ParsePatientDosage(FolderContent dosage)
        {
            List<MedicineDosage> result = new List<MedicineDosage>();

            foreach (KeyValuePair<string, string> item in dosage)
            {
                List<MedicineDosageRecord> list = new List<MedicineDosageRecord>();

                string filePath = item.Key;
                string[] path = filePath.Split('\\');
                string fileName = path[path.Length - 1];
                string fileContent = item.Value;

                string patientId = fileName.Split('_')[0];
                string patientName = fileName.Split('_')[1];

                foreach (var line in CsvReader.ReadFromText(fileContent, new CsvOptions { HeaderMode = HeaderMode.HeaderPresent }))
                {
                    string medicineId = line[ColumnMedicineId];
                    int dailyDosage = Int32.Parse(line[ColumnDailyDosage]);
                    DateTime validFrom = DateTime.Parse(line[ColumnValidFrom]);
                    DateTime validTo = DateTime.Parse(String.IsNullOrEmpty(line[ColumnValidTo]) ? "2100.12.31" : line[ColumnValidTo]);

                    list.Add(new MedicineDosageRecord(medicineId, dailyDosage, validFrom, validTo));
                }

                MedicineDosage patient = new MedicineDosage(patientId, patientName, list);
                result.Add(patient);
            }

            return result;
        }


        private static List<PatientInventory> ParsePatientInventory(FolderContent inventory)
        {
            List<PatientInventory> result = new List<PatientInventory>();

            foreach (KeyValuePair<string, string> item in inventory)
            {
                List<PatientInventoryRecord> list = new List<PatientInventoryRecord>();

                string filePath = item.Key;
                string[] path = filePath.Split('\\');
                string fileName = path[path.Length - 1];
                string fileContent = item.Value;

                string patientId = fileName.Split('_')[0];
                string patientName = fileName.Split('_')[1];

                foreach (var line in CsvReader.ReadFromText(fileContent, new CsvOptions { HeaderMode = HeaderMode.HeaderPresent }))
                {
                    string medicineId = line[ColumnMedicineId];
                    string medicineName = line[ColumnMedicineName];
                    int count = Int32.Parse(line["inventory_2025-01-01"]);

                    list.Add(new PatientInventoryRecord(medicineId, medicineName, new DateTime(2025, 1, 1), count));
                }

                PatientInventory patient = new PatientInventory(patientId, patientName, list);

                result.Add(patient);
            }

            return result;
        }
    }
}
