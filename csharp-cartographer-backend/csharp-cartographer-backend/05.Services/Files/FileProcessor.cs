using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;

namespace csharp_cartographer_backend._05.Services.Files
{
    public class FileProcessor : IFileProcessor
    {
        // TODO: Update to use dynamic file paths
        private readonly string _modelDefinitionDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\03.Models\Tokens\NavToken.cs";
        private readonly string _workflowDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\06.Workflows\Artifacts\GenerateArtifactWorkflow.cs";
        private readonly string _serviceDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\05.Services\Highlighting\SyntaxHighlighter.cs";
        private readonly string _repositoryDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\04.DataAccess\Artifacts\ArtifactRepository.cs";
        private readonly string _controllerDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\07.Controllers\Artifacts\ArtifactController.cs";
        private readonly string _helperClassDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\02.Utilities\Helpers\StringHelpers.cs";
        private readonly string _configDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\01.Configuration\CartographerConfig\CartographerConfig.cs";
        private readonly string _dtoDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\07.Controllers\Artifacts\DtoModels\GenerateArtifactDto.cs";
        private readonly string _providerDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer-be\csharp-cartographer-be\csharp-cartographer-be\08.Providers\ChatGpt\ChatGptProvider.cs";

        public FileData ReadInTestFileData(string fileName)
        {
            var demoFile = fileName switch
            {
                "NavToken.cs" => _modelDefinitionDemoFilePath,
                "GenerateArtifactWorkflow.cs" => _workflowDemoFilePath,
                "SyntaxHighlighter.cs" => _serviceDemoFilePath,
                "ArtifactRepository.cs" => _repositoryDemoFilePath,
                "ArtifactController.cs" => _controllerDemoFilePath,
                "StringHelpers.cs" => _helperClassDemoFilePath,
                "CartographerConfig.cs" => _configDemoFilePath,
                "GenerateArtifactDto.cs" => _dtoDemoFilePath,
                "ChatGptProvider.cs" => _providerDemoFilePath,
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

            return new FileData
            {
                FileName = Path.GetFileName(demoFile),
                FileLines = fileLines,
                Content = File.ReadAllText(demoFile)
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

            return new FileData
            {
                FileName = requestDto.FileName,
                FileLines = fileLines,
                Content = requestDto.FileContent
            };
        }
    }
}
