namespace csharp_cartographer._03.Models.FileProcessing
{
    public class FileData
    {
        public string FileName { get; set; } = string.Empty;

        public List<string> FileLines { get; set; } = [];

        public string Content { get; set; } = string.Empty;
    }
}
