
namespace MedicineTracking.Model
{
    internal class FileRecord
    {
        public string FilePath { get; private set; }

        public string Content { get; private set; }


        public FileRecord(string filePath, string content)
        {
            FilePath = filePath;
            Content = content;
        }
    }
}
