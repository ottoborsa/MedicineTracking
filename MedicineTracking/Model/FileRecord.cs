
namespace MedicineTracking.Model
{
    internal class FileRecord
    {
        public string Path { get; private set; }

        public string Content { get; private set; }


        public FileRecord(string path, string content)
        {
            Path = path;
            Content = content;
        }
    }
}
