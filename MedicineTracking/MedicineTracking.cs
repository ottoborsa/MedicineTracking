
using System;
using System.Collections.Generic;
using System.IO;
using Csv;
using MedicineTracking.Model;


namespace MedicineTracking
{

    /// <summary>
    /// fullPath, fileContent
    /// </summary>
    using FolderContent = Dictionary<string, string>;


    public static class MedicineTracking
    {

        public const string FileExtension = "csv";

        public const char Separator = '_';



        public const string ColumnMedicineId = "medicine_id";

        public const string ColumnMedicineName = "medicine_name";

        public const string ColumnIngredientContent = "ingredient_content";

        public const string ColumnInventoryPrefix = "inventory_";

        public const string ColumnIncrementPrefix = "increment_";





        public const string ColumnDailyDosage = "daily_dosage";

        public const string ColumnValidFrom = "valid_from";

        public const string ColumnValidTo = "valid_to";





        public static string[] PatientInventoryResultSignature = new string[]
        {
            "patient_id",
            "patient_name",
            "medicine_id",
            "medicine_name",
            "zero_quantity_threshold_date"
        };

        public static string[] MedicineQuantityResultSignature = new string[]
        {
            "medicine_id",
            "medicine_name",
            "quantity_increase"
        };







        private static FolderContent GetFolderContent(string folder)
        {
            FolderContent result = new FolderContent();

            foreach (string file in Directory.EnumerateFiles(folder, $"*.{FileExtension}"))
            {
                result.Add(file, File.ReadAllText(file));
            }

            return result;
        }



        public static void GenerateInventoryForecast(string inventoryFolder, string dosageFolder, DateTime dateFrom, DateTime dateTo)
        {

            List<PatientInventory> patientInventories = ParsePatientInventory(GetFolderContent(inventoryFolder));
            List<MedicineDosage> medicineDosages = ParsePatientDosage(GetFolderContent(dosageFolder));





            foreach (PatientInventory inventory in patientInventories)
            {
                string patientId = inventory.PatientId;
                string patientName = inventory.PatientName;

                foreach(PatientInventoryRecord record in inventory.PatientInventoryRecords)
                {
                    string medicineId = record.MedicineId;
                    string medicineName = record.MedicineName;

                    int count = record.MedicineCount;
                }


            }


            foreach (MedicineDosage inventory in medicineDosages)
            {
                string patientId = inventory.PatientId;

                

            }




            /*
            var rows = new[]
            {
                new [] { "0", "John Doe" },
                new [] { "1", "Jane Doe" }
            };

            var csv = CsvWriter.WriteToText(columnNames, rows, ',');
            File.WriteAllText($"result.{FileExtension}", csv);
            */
        }


        private static List<MedicineDosage> ParsePatientDosage(FolderContent folder)
        {
            List<MedicineDosage> result = new List<MedicineDosage>();

            foreach (KeyValuePair<string, string> item in folder)
            {
                List<MedicineDosageRecord> list = new List<MedicineDosageRecord>();

                string filePath = item.Key;
                string[] path = filePath.Split('\\');
                string fileName = path[path.Length - 1];
                string fileContent = item.Value;

                string patientId = fileName.Split(Separator)[0];
                string patientName = fileName.Split(Separator)[1];

                foreach (var line in CsvReader.ReadFromText(fileContent, new CsvOptions { HeaderMode = HeaderMode.HeaderPresent }))
                {
                    string medicineId = line[ColumnMedicineId];
                    int dailyDosage = Int32.Parse(line[ColumnDailyDosage]);
                    DateTime validFrom = DateTime.Parse(String.IsNullOrEmpty(line[ColumnValidFrom]) ? "1900-01-01" : line[ColumnValidFrom]);
                    DateTime validTo = DateTime.Parse(String.IsNullOrEmpty(line[ColumnValidTo]) ? "2100-12-31" : line[ColumnValidTo]);

                    list.Add(new MedicineDosageRecord(medicineId, dailyDosage, validFrom, validTo));
                }

                MedicineDosage patient = new MedicineDosage(patientId, patientName, list);
                result.Add(patient);
            }

            return result;
        }


        private static List<PatientInventory> ParsePatientInventory(FolderContent folder)
        {
            List<PatientInventory> result = new List<PatientInventory>();

            foreach (KeyValuePair<string, string> item in folder)
            {
                List<PatientInventoryRecord> list = new List<PatientInventoryRecord>();

                string filePath = item.Key;
                string[] path = filePath.Split('\\');
                string fileName = path[path.Length - 1];
                string fileContent = item.Value;

                string patientId = fileName.Split(Separator)[0];
                string patientName = fileName.Split(Separator)[1];

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
