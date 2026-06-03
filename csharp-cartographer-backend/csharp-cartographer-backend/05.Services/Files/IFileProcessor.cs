using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;

namespace csharp_cartographer_backend._05.Services.Files
{
    public interface IFileProcessor
    {
        FileData GetDemoFileData(string fileName);

        FileData GetUploadedFileData(GenerateArtifactDto requestDto);

        FileData GetCodeSnippetFileData(string snippet);
    }
}
