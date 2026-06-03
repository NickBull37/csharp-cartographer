using csharp_cartographer_backend._02.Utilities.Providers;
using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace csharp_cartographer_backend._05.Services.Files
{
    public class FileProcessor : IFileProcessor
    {
        private const string DemoProjectName = "CSharpCartographer";
        private const string UserCodeProjectName = "UserCodeProject";
        private const string CodeSnippetProjectName = "CodeSnippetProject";
        private const string CodeSnippetFileName = "CodeSnippet";

        public FileData ReadInDemoFileData(string fileName)
        {
            var filePath = DemoOptionProvider.GetDemoFilePath(fileName);
            var sourceCode = File.ReadAllText(filePath);

            return CreateFileData(
                fileName: fileName,
                sourceCode: sourceCode,
                projectName: DemoProjectName
            );
        }

        public FileData ReadInUploadedFileData(GenerateArtifactDto requestDto)
        {
            return CreateFileData(
                fileName: requestDto.FileName,
                sourceCode: requestDto.FileContent,
                projectName: UserCodeProjectName
            );
        }

        public FileData ReadInCodeSnippetData(string snippet)
        {
            return CreateFileData(
                fileName: CodeSnippetFileName,
                sourceCode: snippet,
                projectName: CodeSnippetProjectName
            );
        }

        private static FileData CreateFileData(string fileName, string sourceCode, string projectName)
        {
            using var workspace = new AdhocWorkspace();
            var project = workspace.AddProject(CreateProjectInfo(projectName));

            var document = workspace.AddDocument(
                project.Id,
                fileName,
                SourceText.From(sourceCode)
            );

            return new FileData(fileName, sourceCode, document);
        }

        private static ProjectInfo CreateProjectInfo(string projectName)
        {
            return ProjectInfo.Create(
                id: ProjectId.CreateNewId(),
                version: VersionStamp.Create(),
                name: projectName,
                assemblyName: projectName,
                language: LanguageNames.CSharp
            );
        }
    }
}
