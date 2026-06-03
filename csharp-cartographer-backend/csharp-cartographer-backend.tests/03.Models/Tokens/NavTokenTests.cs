using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._05.Services.Files;
using csharp_cartographer_backend._05.Services.Roslyn;
using csharp_cartographer_backend._05.Services.Tokens;
using FluentAssertions;

namespace csharp_cartographer_backend.tests._03.Models.Tokens
{
    public class NavTokenTests
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly INavTokenGenerator _navTokenGenerator;

        public NavTokenTests()
        {
            _fileProcessor = new FileProcessor();
            _navTokenGenerator = new NavTokenGenerator(new RoslynAnalyzer());
        }

        [Theory]
        [InlineData(new int[] { 3, 4 }, "public bool IsBracket() => Kind is SyntaxKind.OpenBracketToken or SyntaxKind.CloseBracketToken;")]
        public async Task IsDelimiter_Pass(int[] passingIndices, string code)
        {
            List<int> matchedValues = [];

            // Arrange
            var navTokens = await GenerateNavTokens(code);

            // Act
            foreach (var token in navTokens)
            {
                if (token.IsDelimiter())
                {
                    matchedValues.Add(token.Index);
                }
            }

            // Assert
            passingIndices.Should().BeEquivalentTo(matchedValues);
        }

        private async Task<List<NavToken>> GenerateNavTokens(string code)
        {
            var fileData = _fileProcessor.GetCodeSnippetFileData(code);
            return await _navTokenGenerator.GenerateNavTokens(fileData, CancellationToken.None);
        }
    }
}
