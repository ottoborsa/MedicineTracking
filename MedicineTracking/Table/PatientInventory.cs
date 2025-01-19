
using System.Collections.Generic;
using System;

using MedicineTracking.Utility;



namespace MedicineTracking.Table
{
    internal static class PatientInventory
    {

        private static char FileNameSeparator = '_';

        private static char FileExtensionSeparator = '.';

        private static char DynamicColumnNameSeparator = '_';



        public const string medicine_id = nameof(medicine_id);

        public const string medicine_name = nameof(medicine_name);

        public const string inventory_prefix = "inventory";

        public const string increment_prefix = "increment";




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
                string patientId = fileName.Split(FileNameSeparator)[1].Split(FileExtensionSeparator)[0];

                Matrix inventoryMatrix = CsvParser.Parse(fileContent);


                string[] signature = inventoryMatrix.Signature;
                string lastInventoryColumn = String.Empty;
                List<string> incrementColumns = new List<string>();
                foreach(string columnName in signature)
                {
                    if (columnName.StartsWith(inventory_prefix))
                    {
                        lastInventoryColumn = columnName;
                    }
                    if (columnName.StartsWith(increment_prefix))
                    {
                        incrementColumns.Add(columnName);
                    }
                }


                for (int i = 0; i < inventoryMatrix.GetSize(); i++)
                {
                    string medicineId = inventoryMatrix.GetValue(medicine_id, i);
                    string medicineName = inventoryMatrix.GetValue(medicine_name, i);

                    DateTime inventoryDate = DateTime.Parse(lastInventoryColumn.Split(DynamicColumnNameSeparator)[1]);
                    float medicineCount = Single.Parse(inventoryMatrix.GetValue(lastInventoryColumn, i));

                    Dictionary<DateTime, float> incrementations = new Dictionary<DateTime, float>();

                    for (int j = 0; j < incrementColumns.Count; j++)
                    {
                        DateTime incrementDate = DateTime.Parse(incrementColumns[j].Split(DynamicColumnNameSeparator)[1]);

                        if (incrementDate >= inventoryDate)
                        {
                            string incrementValueString = inventoryMatrix.GetValue(incrementColumns[j], i);
                            float incrementValue = Single.Parse(String.IsNullOrEmpty(incrementValueString) ? "0" : incrementValueString);

                            if (incrementValue != 0)
                            {
                                incrementations.Add(incrementDate, incrementValue);
                            }
                        }
                    }

                    list.Add(new PatientInventoryRecord(medicineId, medicineName, inventoryDate, medicineCount, incrementations));
                }


                Model.PatientInventory patient = new Model.PatientInventory(patientId, patientName, list);
                result.Add(patient);
            }

            return result;
        }
    }
}
