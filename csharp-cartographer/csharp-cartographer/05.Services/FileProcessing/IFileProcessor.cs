using csharp_cartographer._03.Models.FileProcessing;

namespace csharp_cartographer._05.Services.FileProcessing
{
    public interface IFileProcessor
    {
        FileData ReadInTestFileData(string fileName);

        FileData ReadInFileData(string fileName, string sourceCode);
    }
}
