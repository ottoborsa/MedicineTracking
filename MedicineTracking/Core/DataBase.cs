
using System.Collections.Generic;
using System.IO;

using MedicineTracking.Model;


namespace MedicineTracking.Core
{
    internal class DataBase
    {

        public const string FileExtension = "csv";

        public const char FileExtensionSeparator = '.';



        public string Path_PatientInventory { get; private set; }

        public string Path_PatientDosage { get; private set; }

        public List<PatientInventory> Table_PatientInventory { get; private set; }

        public List<MedicineDosage> Table_PatientDosage { get; private set; }



        public DataBase()
        {
            Path_PatientInventory = ApplicationInterface.GetDataBaseTableFolders()[nameof(Table.PatientInventory)];
            Path_PatientDosage = ApplicationInterface.GetDataBaseTableFolders()[nameof(Table.PatientDosage)];

            Table_PatientInventory = Table.PatientInventory.Parse(GetFolderContent(Path_PatientInventory));
            Table_PatientDosage = Table.PatientDosage.Parse(GetFolderContent(Path_PatientDosage));
        }


        private static List<FileRecord> GetFolderContent(string path)
        {
            List<FileRecord> result = new();

            foreach (string file in Directory.EnumerateFiles(path, $"*{FileExtensionSeparator}{FileExtension}"))
            {
                result.Add(new FileRecord(file, File.ReadAllText(file, ApplicationInterface.ApplicationEncoding)));
            }

            return result;
        }
    }
}
