using csharp_cartographer._03.Models.FileProcessing;

namespace csharp_cartographer._05.Services.FileProcessing
{
    public class FileProcessor : IFileProcessor
    {
        private readonly string _modelDefinitionDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer\csharp-cartographer\csharp-cartographer\01.Configuration\TestFiles\Animal.cs";
        private readonly string _workflowDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer\csharp-cartographer\csharp-cartographer\05.Services\Artifacts\ArtifactGenerator.cs";
        private readonly string _serviceDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer\csharp-cartographer\csharp-cartographer\05.Services\SyntaxHighlighting\SyntaxHighlighter.cs";
        private readonly string _repositoryDemoFilePath = "";
        private readonly string _controllerDemoFilePath = @"C:\Users\nbuli\source\repos\csharp-cartographer\csharp-cartographer\csharp-cartographer\06.Controllers\Artifacts\ArtifactController.cs";

        private readonly string _testFilePath1 = @"C:\Users\nbuli\source\repos\csharp-cartographer\csharp-cartographer\csharp-cartographer\01.Configuration\TestFiles\Animal.cs";
        private readonly string _testFilePath2 = @"C:\Users\nbuli\source\repos\ss-navigator\ss-navigator\05.Services\Roslyn\RoslynAnalyzer.cs";
        private readonly string _testFilePath3 = @"C:\Users\nbuli\source\repos\ss-navigator-v2\ss-navigator-v2\05.Services\Constructs\InterfaceWizard.cs";
        private readonly string _testFilePath4 = @"C:\Users\nbuli\source\repos\csharp-cartographer\csharp-cartographer\csharp-cartographer\06.Controllers\Artifacts\ArtifactController.cs";

        public FileProcessor()
        {
        }

        public FileData ReadInTestFileData(string fileName)
        {
            string testFile = string.Empty;

            if (fileName == "Animal.cs")
            {
                testFile = _modelDefinitionDemoFilePath;
            }
            else if (fileName == "RoslynAnalyzer.cs")
            {
                testFile = _workflowDemoFilePath;
            }
            else if (fileName == "SyntaxHighlighter.cs")
            {
                testFile = _serviceDemoFilePath;
            }
            else if (fileName == "ArtifactController.cs")
            {
                testFile = _controllerDemoFilePath;
            }

            if (!string.IsNullOrEmpty(testFile) && !File.Exists(testFile))
            {
                Console.WriteLine("File does not exist.");
                throw new FileNotFoundException();
            }

            var fileLines = new List<string>();
            using (StreamReader sr = new(testFile))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
            }

            return new FileData
            {
                FileName = Path.GetFileName(testFile),
                FileLines = fileLines,
                Content = File.ReadAllText(testFile)
            };
        }

        public FileData ReadInFileData(string fileName, string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
            {
                Console.WriteLine("Source code is empty or null.");
                throw new ArgumentException("Source code cannot be null or empty.");
            }

            var fileLines = new List<string>();
            using (StringReader sr = new(sourceCode))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
            }

            return new FileData
            {
                FileName = fileName,
                FileLines = fileLines,
                Content = sourceCode
            };
        }
    }
}
