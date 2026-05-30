using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace csharp_cartographer_backend._05.Services.Files
{
    public class FileProcessor : IFileProcessor
    {
        private readonly string _projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        private readonly string _solutionRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;

        private readonly Dictionary<string, string> _projectDemoFiles = new()
        {
            ["NavToken.cs"] = @"03.Models\Tokens\NavToken.cs",
            ["GenerateArtifactWorkflow.cs"] = @"06.Workflows\Artifacts\GenerateArtifactWorkflow.cs",
            ["SyntaxHighlighter.cs"] = @"05.Services\SyntaxHighlighting\SyntaxHighlighter.cs",
            ["ArtifactRepository.cs"] = @"04.DataAccess\Artifacts\ArtifactRepository.cs",
            ["ArtifactController.cs"] = @"08.Controllers\Artifacts\ArtifactController.cs",
            ["StringHelpers.cs"] = @"02.Utilities\Helpers\StringHelpers.cs",
            ["CartographerConfig.cs"] = @"01.Configuration\Configs\CartographerConfig.cs",
            ["GenerateArtifactDto.cs"] = @"08.Controllers\Artifacts\Dtos\GenerateArtifactDto.cs",
            ["ChatGptProvider.cs"] = @"07.Clients\ChatGpt\ChatGptClient.cs",
            ["OperatorDemo.cs"] = @"01.Configuration\TestFiles\OperatorDemo.cs",
            ["SyntaxHighlighterTests.cs"] = @"csharp-cartographer-backend.tests\05.Services\SyntaxHighlighting\SyntaxHighlighterTests.cs"
        };

        private readonly Dictionary<string, string> _solutionDemoFiles = new()
        {
            ["SyntaxHighlighterTests.cs"] = @"csharp-cartographer-backend.tests\05.Services\SyntaxHighlighting\SyntaxHighlighterTests.cs"
        };

        public FileData ReadInDemoFileData(string fileName)
        {
            var filePath = GetDemoFilePath(fileName);

            return CreateFileData(
                fileName: Path.GetFileName(filePath),
                sourceCode: File.ReadAllText(filePath),
                projectName: "DemoCodeProject"
            );
        }

        public FileData ReadInUploadedFileData(GenerateArtifactDto requestDto)
        {
            return CreateFileData(
                fileName: requestDto.FileName,
                sourceCode: requestDto.FileContent,
                projectName: "UserCodeProject"
            );
        }

        public FileData ReadInCodeSnippetData(string snippet)
        {
            return CreateFileData(
                fileName: "CodeSnippet",
                sourceCode: snippet,
                projectName: "CodeSnippetProject"
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

        private string GetDemoFilePath(string fileName)
        {
            if (_projectDemoFiles.TryGetValue(fileName, out var projectRelativePath))
                return Path.Combine(_projectRoot, projectRelativePath);

            if (_solutionDemoFiles.TryGetValue(fileName, out var solutionRelativePath))
                return Path.Combine(_solutionRoot, solutionRelativePath);

            throw new ArgumentException(
                $"The demo file '{fileName}' is not supported.",
                nameof(fileName)
            );
        }
    }
}
