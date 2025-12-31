using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._05.Services.SyntaxHighlighting;

namespace csharp_cartographer_backend.tests._05.Services.SyntaxHighlighting
{
    public class SyntaxHighlighterTests
    {
        public SyntaxHighlighter _syntaxHighlighter;

        public SyntaxHighlighterTests()
        {
            _syntaxHighlighter = new();
        }

        [Fact]
        public void SyntaxHighlighter_HighlightPunctuationTokens_Success()
        {
            // Arrange
            List<NavToken> mockNavTokens =
            [
                new NavToken() {Text = ":", UpdatedClassification = "punctuation"},
                new NavToken() {Text = ";", UpdatedClassification = "punctuation"},
                new NavToken() {Text = ",", UpdatedClassification = "punctuation"},
                new NavToken() {Text = "?", UpdatedClassification = "punctuation"},
            ];

            // Act
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(mockNavTokens);

            // Assert
            Assert.Collection(mockNavTokens,
                token => Assert.Equal("color-white", token.HighlightColor),
                token => Assert.Equal("color-white", token.HighlightColor),
                token => Assert.Equal("color-white", token.HighlightColor),
                token => Assert.Equal("color-gray", token.HighlightColor)
            );
        }

        [Fact]
        public void SyntaxHighlighter_HighlightDelimiterTokens_Success()
        {
            // Arrange
            List<NavToken> mockNavTokens =
            [
                new NavToken() {Text = "(", UpdatedClassification = "delimiter"},
                new NavToken() {Text = ")", UpdatedClassification = "delimiter"},
                new NavToken() {Text = "{", UpdatedClassification = "delimiter"},
                new NavToken() {Text = "}", UpdatedClassification = "delimiter"},
                new NavToken() {Text = "[", UpdatedClassification = "delimiter"},
                new NavToken() {Text = "]", UpdatedClassification = "delimiter"},
                new NavToken() {Text = "<", UpdatedClassification = "delimiter"},
                new NavToken() {Text = ">", UpdatedClassification = "delimiter"},
            ];

            // Act
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(mockNavTokens);

            // Assert
            foreach (var token in mockNavTokens)
            {
                Assert.Equal("color-white", token.HighlightColor);
            }
        }

        [Fact]
        public void SyntaxHighlighter_HighlightOperatorTokens_Success()
        {
            // Arrange
            List<NavToken> mockNavTokens =
            [
                new NavToken() {Text = "+", UpdatedClassification = "operator"},
                new NavToken() {Text = "-", UpdatedClassification = "operator"},
                new NavToken() {Text = "*", UpdatedClassification = "operator"},
                new NavToken() {Text = "/", UpdatedClassification = "operator"},
                new NavToken() {Text = "%", UpdatedClassification = "operator"},
                new NavToken() {Text = "==", UpdatedClassification = "operator"},
                new NavToken() {Text = "!=", UpdatedClassification = "operator"},
                new NavToken() {Text = ">", UpdatedClassification = "operator"},
                new NavToken() {Text = "<", UpdatedClassification = "operator"},
                new NavToken() {Text = ">=", UpdatedClassification = "operator"},
                new NavToken() {Text = "<=", UpdatedClassification = "operator"},
                new NavToken() {Text = "&&", UpdatedClassification = "operator"},
                new NavToken() {Text = "||", UpdatedClassification = "operator"},
                new NavToken() {Text = "!", UpdatedClassification = "operator"},
                new NavToken() {Text = "&", UpdatedClassification = "operator"},
                new NavToken() {Text = "|", UpdatedClassification = "operator"},
                new NavToken() {Text = "^", UpdatedClassification = "operator"},
                new NavToken() {Text = "~", UpdatedClassification = "operator"},
                new NavToken() {Text = "<<", UpdatedClassification = "operator"},
                new NavToken() {Text = ">>", UpdatedClassification = "operator"},
                new NavToken() {Text = "=", UpdatedClassification = "operator"},
                new NavToken() {Text = "+=", UpdatedClassification = "operator"},
                new NavToken() {Text = "-=", UpdatedClassification = "operator"},
                new NavToken() {Text = "*=", UpdatedClassification = "operator"},
                new NavToken() {Text = "/=", UpdatedClassification = "operator"},
                new NavToken() {Text = "%=", UpdatedClassification = "operator"},
                new NavToken() {Text = "&=", UpdatedClassification = "operator"},
                new NavToken() {Text = "|=", UpdatedClassification = "operator"},
                new NavToken() {Text = "^=", UpdatedClassification = "operator"},
                new NavToken() {Text = "<<=", UpdatedClassification = "operator"},
                new NavToken() {Text = ">>=", UpdatedClassification = "operator"},
                new NavToken() {Text = "++", UpdatedClassification = "operator"},
                new NavToken() {Text = "--", UpdatedClassification = "operator"},
                new NavToken() {Text = "?", UpdatedClassification = "operator"},
                new NavToken() {Text = "??", UpdatedClassification = "operator"},
                new NavToken() {Text = "??=", UpdatedClassification = "operator"},
                new NavToken() {Text = ":", UpdatedClassification = "operator"},
                new NavToken() {Text = "=>", UpdatedClassification = "operator"},
                new NavToken() {Text = "?.", UpdatedClassification = "operator"},
                new NavToken() {Text = "::", UpdatedClassification = "operator"},
                new NavToken() {Text = ".", UpdatedClassification = "operator"},
                new NavToken() {Text = "..", UpdatedClassification = "operator"},
            ];

            // Act
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(mockNavTokens);

            // Assert
            Assert.All(mockNavTokens.Where(token => token.Text != "." && token.Text != ".."), token => Assert.Equal("color-gray", token.HighlightColor));

            var memberAccessOperator = mockNavTokens.Single(token => token.Text == ".");
            Assert.Equal("color-white", memberAccessOperator.HighlightColor);

            var rangeOperator = mockNavTokens.Single(token => token.Text == "..");
            Assert.Equal("color-white", rangeOperator.HighlightColor);
        }

        [Fact]
        public void SyntaxHighlighter_HighlightKeywordTokens_Success()
        {
            // Arrange
            List<NavToken> blueKeywordTokens =
            [
                new NavToken() {Text = "_", UpdatedClassification = "keyword"},
                new NavToken() {Text = "abstract", UpdatedClassification = "keyword"},
                new NavToken() {Text = "as", UpdatedClassification = "keyword"},
                new NavToken() {Text = "async", UpdatedClassification = "keyword"},
                new NavToken() {Text = "await", UpdatedClassification = "keyword"},
                new NavToken() {Text = "base", UpdatedClassification = "keyword"},
                new NavToken() {Text = "bool", UpdatedClassification = "keyword"},
                new NavToken() {Text = "char", UpdatedClassification = "keyword"},
                new NavToken() {Text = "checked", UpdatedClassification = "keyword"},
                new NavToken() {Text = "class", UpdatedClassification = "keyword"},
                new NavToken() {Text = "const", UpdatedClassification = "keyword"},
                new NavToken() {Text = "decimal", UpdatedClassification = "keyword"},
                new NavToken() {Text = "double", UpdatedClassification = "keyword"},
                new NavToken() {Text = "enum", UpdatedClassification = "keyword"},
                new NavToken() {Text = "explicit", UpdatedClassification = "keyword"},
                new NavToken() {Text = "false", UpdatedClassification = "keyword"},
                new NavToken() {Text = "fixed", UpdatedClassification = "keyword"},
                new NavToken() {Text = "float", UpdatedClassification = "keyword"},
                new NavToken() {Text = "get", UpdatedClassification = "keyword"},
                new NavToken() {Text = "global", UpdatedClassification = "keyword"},
                new NavToken() {Text = "goto", UpdatedClassification = "keyword"},
                new NavToken() {Text = "implicit", UpdatedClassification = "keyword"},
                new NavToken() {Text = "init", UpdatedClassification = "keyword"},
                new NavToken() {Text = "int", UpdatedClassification = "keyword"},
                new NavToken() {Text = "interface", UpdatedClassification = "keyword"},
                new NavToken() {Text = "internal", UpdatedClassification = "keyword"},
                new NavToken() {Text = "is", UpdatedClassification = "keyword"},
                new NavToken() {Text = "lock", UpdatedClassification = "keyword"},
                new NavToken() {Text = "nameof", UpdatedClassification = "keyword"},
                new NavToken() {Text = "namespace", UpdatedClassification = "keyword"},
                new NavToken() {Text = "new", UpdatedClassification = "keyword"},
                new NavToken() {Text = "not", UpdatedClassification = "keyword"},
                new NavToken() {Text = "null", UpdatedClassification = "keyword"},
                new NavToken() {Text = "object", UpdatedClassification = "keyword"},
                new NavToken() {Text = "operator", UpdatedClassification = "keyword"},
                new NavToken() {Text = "or", UpdatedClassification = "keyword"},
                new NavToken() {Text = "out", UpdatedClassification = "keyword"},
                new NavToken() {Text = "override", UpdatedClassification = "keyword"},
                new NavToken() {Text = "private", UpdatedClassification = "keyword"},
                new NavToken() {Text = "protected", UpdatedClassification = "keyword"},
                new NavToken() {Text = "public", UpdatedClassification = "keyword"},
                new NavToken() {Text = "readonly", UpdatedClassification = "keyword"},
                new NavToken() {Text = "record", UpdatedClassification = "keyword"},
                new NavToken() {Text = "ref", UpdatedClassification = "keyword"},
                new NavToken() {Text = "sealed", UpdatedClassification = "keyword"},
                new NavToken() {Text = "set", UpdatedClassification = "keyword"},
                new NavToken() {Text = "sizeof", UpdatedClassification = "keyword"},
                new NavToken() {Text = "stackalloc", UpdatedClassification = "keyword"},
                new NavToken() {Text = "static", UpdatedClassification = "keyword"},
                new NavToken() {Text = "string", UpdatedClassification = "keyword"},
                new NavToken() {Text = "struct", UpdatedClassification = "keyword"},
                new NavToken() {Text = "this", UpdatedClassification = "keyword"},
                new NavToken() {Text = "true", UpdatedClassification = "keyword"},
                new NavToken() {Text = "typeof", UpdatedClassification = "keyword"},
                new NavToken() {Text = "unchecked", UpdatedClassification = "keyword"},
                new NavToken() {Text = "unsafe", UpdatedClassification = "keyword"},
                new NavToken() {Text = "using", UpdatedClassification = "keyword"},
                new NavToken() {Text = "var", UpdatedClassification = "keyword"},
                new NavToken() {Text = "virtual", UpdatedClassification = "keyword"},
                new NavToken() {Text = "void", UpdatedClassification = "keyword"},
                new NavToken() {Text = "volatile", UpdatedClassification = "keyword"},
                new NavToken() {Text = "where", UpdatedClassification = "keyword"},
            ];

            List<NavToken> purpleKeywordTokens =
            [
                new NavToken() {Text = "break", UpdatedClassification = "keyword"},
                new NavToken() {Text = "case", UpdatedClassification = "keyword"},
                new NavToken() {Text = "catch", UpdatedClassification = "keyword"},
                new NavToken() {Text = "continue", UpdatedClassification = "keyword"},
                new NavToken() {Text = "do", UpdatedClassification = "keyword"},
                new NavToken() {Text = "else", UpdatedClassification = "keyword"},
                new NavToken() {Text = "finally", UpdatedClassification = "keyword"},
                new NavToken() {Text = "for", UpdatedClassification = "keyword"},
                new NavToken() {Text = "foreach", UpdatedClassification = "keyword"},
                new NavToken() {Text = "if", UpdatedClassification = "keyword"},
                new NavToken() {Text = "in", UpdatedClassification = "keyword"},
                new NavToken() {Text = "return", UpdatedClassification = "keyword"},
                new NavToken() {Text = "switch", UpdatedClassification = "keyword"},
                new NavToken() {Text = "throw", UpdatedClassification = "keyword"},
                new NavToken() {Text = "try", UpdatedClassification = "keyword"},
                new NavToken() {Text = "when", UpdatedClassification = "keyword"},
                new NavToken() {Text = "while", UpdatedClassification = "keyword"},
            ];

            var mockNavTokens = blueKeywordTokens.Concat(purpleKeywordTokens).ToList();

            // Act
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(mockNavTokens);

            // Assert
            Assert.All(blueKeywordTokens, token => Assert.Equal("color-blue", token.HighlightColor));
            Assert.All(purpleKeywordTokens, token => Assert.Equal("color-purple", token.HighlightColor));

        }

        [Fact]
        public void SyntaxHighlighter_HighlightIdentifierTokens_Success()
        {
            // Arrange
            List<NavToken> whiteIdentifierTokens =
            [
                new NavToken() {Text = "csharp_cartographer_backend", UpdatedClassification = "identifier - namespace"},
                new NavToken() {Text = "_fileProcessor", UpdatedClassification = "identifier - field declaration"},
                new NavToken() {Text = "_fileProcessor", UpdatedClassification = "identifier - field reference"},
                new NavToken() {Text = "Index", UpdatedClassification = "identifier - property declaration"},
                new NavToken() {Text = "Text", UpdatedClassification = "identifier - property reference"},
                new NavToken() {Text = "DefaultErrorMsg", UpdatedClassification = "identifier - constant"},
            ];

            List<NavToken> lightBlueIdentifierTokens =
            [
                new NavToken() {Text = "fileName", UpdatedClassification = "identifier - parameter"},
                new NavToken() {Text = "navTokens", UpdatedClassification = "identifier - parameter prefix"},
                new NavToken() {Text = "localVar", UpdatedClassification = "identifier - local variable"},
            ];

            List<NavToken> yellowIdentifierTokens =
            [
                new NavToken() {Text = "GetDemoArtifact", UpdatedClassification = "identifier - method declaration"},
                new NavToken() {Text = "IsNullOrWhiteSpace", UpdatedClassification = "identifier - method invocation"},
            ];

            List<NavToken> greenIdentifierTokens =
            [
                new NavToken() {Text = "HttpGet", UpdatedClassification = "identifier - attribute"},
                new NavToken() {Text = "NavToken", UpdatedClassification = "identifier - class declaration"},
                new NavToken() {Text = "NavToken", UpdatedClassification = "identifier - class constructor"},
                new NavToken() {Text = "NavToken", UpdatedClassification = "identifier - class name"},
                new NavToken() {Text = "GenerateArtifactDto", UpdatedClassification = "identifier - record declaration"},
                new NavToken() {Text = "GenerateArtifactDto", UpdatedClassification = "identifier - record constructor"},
                new NavToken() {Text = "GenerateArtifactDto", UpdatedClassification = "identifier - record name"},
            ];

            var mockNavTokens = whiteIdentifierTokens
                .Concat(lightBlueIdentifierTokens)
                .Concat(greenIdentifierTokens)
                .Concat(yellowIdentifierTokens)
                .ToList();

            // Act
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(mockNavTokens);

            // Assert
            Assert.All(whiteIdentifierTokens, token => Assert.Equal("color-white", token.HighlightColor));
            Assert.All(lightBlueIdentifierTokens, token => Assert.Equal("color-light-blue", token.HighlightColor));
            Assert.All(greenIdentifierTokens, token => Assert.Equal("color-green", token.HighlightColor));
            Assert.All(yellowIdentifierTokens, token => Assert.Equal("color-yellow", token.HighlightColor));
        }

        [Fact]
        public void SyntaxHighlighter_HighlightLiteralTokens_Success()
        {
            // Arrange
            List<NavToken> orangeLiteralTokens =
            [
                new NavToken() {Text = "I", UpdatedClassification = "character literal"},
                new NavToken() {Text = "Hello ", UpdatedClassification = "quoted string"},
                new NavToken() {Text = "@\"C:\\Projects\\Cartographer\\Logs\\analysis.log\"", UpdatedClassification = "verbatim string"},
                new NavToken() {Text = "$\"", UpdatedClassification = "interpolated string - start"},
                new NavToken() {Text = "Artifact contains", UpdatedClassification = "interpolated string - text"},
                new NavToken() {Text = "\"", UpdatedClassification = "interpolated string - end"},
                new NavToken() {Text = "$@\"", UpdatedClassification = "interpolated verbatim string - start"},
                new NavToken() {Text = "C:\\Projects\\Cartographer\\Artifacts\\\r\n\r\n", UpdatedClassification = "interpolated verbatim string - text"},
                new NavToken() {Text = "\"", UpdatedClassification = "interpolated verbatim string - end"},
            ];

            List<NavToken> lightGreenLiteralTokens =
            [
                new NavToken() {Text = "1", UpdatedClassification = "numeric literal"},
                new NavToken() {Text = "0b_0010", UpdatedClassification = "numeric literal"},
            ];

            var mockNavTokens = orangeLiteralTokens
                .Concat(lightGreenLiteralTokens)
                .ToList();

            // Act
            _syntaxHighlighter.AddSyntaxHighlightingToNavTokens(mockNavTokens);

            // Assert
            Assert.All(orangeLiteralTokens, token => Assert.Equal("color-orange", token.HighlightColor));
            Assert.All(lightGreenLiteralTokens, token => Assert.Equal("color-light-green", token.HighlightColor));
        }
    }
}
