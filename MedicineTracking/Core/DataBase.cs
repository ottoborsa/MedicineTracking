
using System.Collections.Generic;
using System.IO;

using MedicineTracking.Model;


namespace MedicineTracking.Core
{
    internal class DataBase
    {

        public const string FileExtension = "csv";


        public string Path_PatientInventoryTable { get; private set; }

        public string Path_PatientDosageTable { get; private set; }

        public List<PatientInventory> Table_PatientInventory { get; private set; }

        public List<MedicineDosage> Table_PatientDosage { get; private set; }



        public DataBase(string patientInventoryTableFolderPath, string patientDosageTableFolderPath)
        {
            Path_PatientInventoryTable = patientInventoryTableFolderPath;
            Path_PatientDosageTable = patientDosageTableFolderPath;

            Table_PatientInventory = Table.PatientInventory.Parse(GetFolderContent(Path_PatientInventoryTable));
            Table_PatientDosage = Table.PatientDosage.Parse(GetFolderContent(Path_PatientDosageTable));
        }


        private static Dictionary<string, string> GetFolderContent(string path)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (string file in Directory.EnumerateFiles(path, $"*.{FileExtension}"))
            {
                result.Add(file, File.ReadAllText(file));
            }

            return result;
        }
    }
}
