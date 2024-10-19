using csharp_cartographer._03.Models.FileProcessing;

namespace csharp_cartographer._05.Services.FileProcessing
{
    public class FileProcessor : IFileProcessor
    {
        private readonly string _testFilePath1 = @"C:\Users\nbuli\source\repos\ss-navigator\ss-navigator\01.Configuration\TestFiles\Animal.cs";
        private readonly string _testFilePath2 = @"C:\Users\nbuli\source\repos\ss-navigator\ss-navigator\05.Services\Roslyn\RoslynAnalyzer.cs";
        private readonly string _testFilePath3 = @"C:\Users\nbuli\source\repos\ss-navigator-v2\ss-navigator-v2\05.Services\Constructs\InterfaceWizard.cs";

        public FileProcessor()
        {
        }

        public FileData ReadInTestFileData()
        {
            var testFile = _testFilePath2;

            if (!File.Exists(testFile))
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
