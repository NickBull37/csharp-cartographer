using Microsoft.CodeAnalysis;

namespace csharp_cartographer_backend._03.Models.Files
{
    public class FileData
    {
        public string FileName { get; init; }

        public string Content { get; init; }

        public Document Document { get; init; }

        public bool IsDemo { get; init; }

        public FileData(string fileName, string content, Document document, bool isDemo)
        {
            FileName = fileName;
            Content = content;
            Document = document;
            IsDemo = isDemo;
        }
    }
}
