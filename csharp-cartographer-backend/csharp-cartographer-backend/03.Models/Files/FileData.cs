using Microsoft.CodeAnalysis;

namespace csharp_cartographer_backend._03.Models.Files
{
    public class FileData
    {
        public string FileName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public AdhocWorkspace Workspace { get; set; } = new();

        public Document? Document { get; set; }

        public FileData(string fileName, string content, AdhocWorkspace workspace, Document document)
        {
            FileName = fileName;
            Content = content;
            Workspace = workspace;
            Document = document;
        }
    }
}
