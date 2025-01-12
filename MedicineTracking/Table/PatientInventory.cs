
using System.Collections.Generic;
using System;


namespace MedicineTracking.Table
{
    internal static class PatientInventory
    {

        private static char FileNameSeparator = '_';


        public const string medicine_id = nameof(medicine_id);

        public const string medicine_name = nameof(medicine_name);

        public const string inventory_prefix = "inventory_";

        public const string increment_prefix = "increment_";




        public static List<Model.PatientInventory> Parse(Dictionary<string, string> files)
        {
            List<Model.PatientInventory> result = new List<Model.PatientInventory>();

            foreach (KeyValuePair<string, string> file in files)
            {
                List<PatientInventoryRecord> list = new List<PatientInventoryRecord>();

                string filePath = file.Key;
                string[] path = filePath.Split('\\');
                string fileName = path[path.Length - 1];
                string fileContent = file.Value;

                string patientName = fileName.Split(FileNameSeparator)[0];
                string patientId = fileName.Split(FileNameSeparator)[1];

                CsvParser.Matrix inventory = CsvParser.CsvParser.Parse(fileContent);

                for (int i = 0; i < inventory.GetSize(); i++)
                {
                    string medicineId = inventory.GetValue(medicine_id, i);

                    string medicineName = inventory.GetValue(medicine_name, i);



                    // TODO: inventory and increment

                    //int count = Int32.Parse(inventory.GetValue("inventory_2025-01-01", i) ?? "0");





                    list.Add(new PatientInventoryRecord(medicineId, medicineName, new DateTime(2025, 1, 1), 0));
                }

                Model.PatientInventory patient = new Model.PatientInventory(patientId, patientName, list);

                result.Add(patient);
            }

            return result;
        }
    }
}
