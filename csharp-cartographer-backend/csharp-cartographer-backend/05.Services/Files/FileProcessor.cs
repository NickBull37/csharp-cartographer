using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace csharp_cartographer_backend._05.Services.Files
{
    public class FileProcessor : IFileProcessor
    {
        private readonly string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        private readonly string solutionRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;

        private readonly string _modelDefinitionDemoFilePath = @"03.Models\Tokens\NavToken.cs";
        private readonly string _workflowDemoFilePath = @"06.Workflows\Artifacts\GenerateArtifactWorkflow.cs";
        private readonly string _serviceDemoFilePath = @"05.Services\SyntaxHighlighting\SyntaxHighlighter.cs";
        private readonly string _repositoryDemoFilePath = @"04.DataAccess\Artifacts\ArtifactRepository.cs";
        private readonly string _controllerDemoFilePath = @"08.Controllers\Artifacts\ArtifactController.cs";
        private readonly string _helperClassDemoFilePath = @"02.Utilities\Helpers\StringHelpers.cs";
        private readonly string _configDemoFilePath = @"01.Configuration\Configs\CartographerConfig.cs";
        private readonly string _dtoDemoFilePath = @"08.Controllers\Artifacts\Dtos\GenerateArtifactDto.cs";
        private readonly string _clientDemoFilePath = @"07.Clients\ChatGpt\ChatGptClient.cs";
        private readonly string _unitTestDemoFilePath = @"csharp-cartographer-backend.tests\05.Services\SyntaxHighlighting\SyntaxHighlighterTests.cs";

        private readonly string _operatorDemoFilePath = @"01.Configuration\TestFiles\OperatorDemo.cs";
        private readonly string _collectionDemoFilePath = @"01.Configuration\TestFiles\CollectionDemo.cs";

        public FileData ReadInTestFileData(string fileName)
        {
            var demoFile = fileName switch
            {
                "NavToken.cs" => Path.Combine(projectRoot, _modelDefinitionDemoFilePath),
                "GenerateArtifactWorkflow.cs" => Path.Combine(projectRoot, _workflowDemoFilePath),
                "SyntaxHighlighter.cs" => Path.Combine(projectRoot, _serviceDemoFilePath),
                "ArtifactRepository.cs" => Path.Combine(projectRoot, _repositoryDemoFilePath),
                "ArtifactController.cs" => Path.Combine(projectRoot, _controllerDemoFilePath),
                "StringHelpers.cs" => Path.Combine(projectRoot, _helperClassDemoFilePath),
                "CartographerConfig.cs" => Path.Combine(projectRoot, _configDemoFilePath),
                "GenerateArtifactDto.cs" => Path.Combine(projectRoot, _dtoDemoFilePath),
                "ChatGptProvider.cs" => Path.Combine(projectRoot, _clientDemoFilePath),
                "SyntaxHighlighterTests.cs" => Path.Combine(solutionRoot, _unitTestDemoFilePath),
                "OperatorDemo.cs" => Path.Combine(projectRoot, _operatorDemoFilePath),
                "CollectionDemo.cs" => Path.Combine(projectRoot, _collectionDemoFilePath),
                _ => string.Empty
            };

            var fileLines = new List<string>();
            using (StreamReader sr = new(demoFile))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
            }

            // TODO: move workspace and document generation to RoslynAnalyzer.cs
            var workspace = new AdhocWorkspace();
            var sourceCode = File.ReadAllText(demoFile);
            var projectInfo = ProjectInfo.Create(
                ProjectId.CreateNewId(),
                VersionStamp.Create(),
                name: "UserCodeProject",
                assemblyName: "UserCodeProject",
                language: LanguageNames.CSharp
            );
            var project = workspace.AddProject(projectInfo);
            var document = workspace.AddDocument(project.Id, "UserUpload.cs", SourceText.From(sourceCode));

            return new FileData
            {
                FileName = Path.GetFileName(demoFile),
                FileLines = fileLines,
                Content = File.ReadAllText(demoFile),
                Workspace = workspace,
                Document = document
            };
        }

        public FileData ReadInFileData(GenerateArtifactDto requestDto)
        {
            var fileLines = new List<string>();
            using (StringReader sr = new(requestDto.FileContent))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
            }

            // Create workspace
            var workspace = new AdhocWorkspace();

            // Use the uploaded file's content directly
            var sourceCode = requestDto.FileContent;

            // Create a Roslyn project
            var projectInfo = ProjectInfo.Create(
                ProjectId.CreateNewId(),
                VersionStamp.Create(),
                name: "UserCodeProject",
                assemblyName: "UserCodeProject",
                language: LanguageNames.CSharp
            );
            var project = workspace.AddProject(projectInfo);

            // Add the document with the uploaded content
            var document = workspace.AddDocument(
                project.Id,
                requestDto.FileName,
                SourceText.From(sourceCode)
            );

            return new FileData
            {
                FileName = requestDto.FileName,
                FileLines = fileLines,
                Content = requestDto.FileContent,
                Workspace = workspace,
                Document = document
            };
        }
    }
}
