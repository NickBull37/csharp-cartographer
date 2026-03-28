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
                new NavToken() {Text = ":", ColorAs = "punctuation"},
                new NavToken() {Text = ";", ColorAs = "punctuation"},
                new NavToken() {Text = ",", ColorAs = "punctuation"},
                new NavToken() {Text = "?", ColorAs = "punctuation"},
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
                new NavToken() {Text = "(", ColorAs = "delimiter"},
                new NavToken() {Text = ")", ColorAs = "delimiter"},
                new NavToken() {Text = "{", ColorAs = "delimiter"},
                new NavToken() {Text = "}", ColorAs = "delimiter"},
                new NavToken() {Text = "[", ColorAs = "delimiter"},
                new NavToken() {Text = "]", ColorAs = "delimiter"},
                new NavToken() {Text = "<", ColorAs = "delimiter"},
                new NavToken() {Text = ">", ColorAs = "delimiter"},
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
                new NavToken() {Text = "+", ColorAs = "operator"},
                new NavToken() {Text = "-", ColorAs = "operator"},
                new NavToken() {Text = "*", ColorAs = "operator"},
                new NavToken() {Text = "/", ColorAs = "operator"},
                new NavToken() {Text = "%", ColorAs = "operator"},
                new NavToken() {Text = "==", ColorAs = "operator"},
                new NavToken() {Text = "!=", ColorAs = "operator"},
                new NavToken() {Text = ">", ColorAs = "operator"},
                new NavToken() {Text = "<", ColorAs = "operator"},
                new NavToken() {Text = ">=", ColorAs = "operator"},
                new NavToken() {Text = "<=", ColorAs = "operator"},
                new NavToken() {Text = "&&", ColorAs = "operator"},
                new NavToken() {Text = "||", ColorAs = "operator"},
                new NavToken() {Text = "!", ColorAs = "operator"},
                new NavToken() {Text = "&", ColorAs = "operator"},
                new NavToken() {Text = "|", ColorAs = "operator"},
                new NavToken() {Text = "^", ColorAs = "operator"},
                new NavToken() {Text = "~", ColorAs = "operator"},
                new NavToken() {Text = "<<", ColorAs = "operator"},
                new NavToken() {Text = ">>", ColorAs = "operator"},
                new NavToken() {Text = "=", ColorAs = "operator"},
                new NavToken() {Text = "+=", ColorAs = "operator"},
                new NavToken() {Text = "-=", ColorAs = "operator"},
                new NavToken() {Text = "*=", ColorAs = "operator"},
                new NavToken() {Text = "/=", ColorAs = "operator"},
                new NavToken() {Text = "%=", ColorAs = "operator"},
                new NavToken() {Text = "&=", ColorAs = "operator"},
                new NavToken() {Text = "|=", ColorAs = "operator"},
                new NavToken() {Text = "^=", ColorAs = "operator"},
                new NavToken() {Text = "<<=", ColorAs = "operator"},
                new NavToken() {Text = ">>=", ColorAs = "operator"},
                new NavToken() {Text = "++", ColorAs = "operator"},
                new NavToken() {Text = "--", ColorAs = "operator"},
                new NavToken() {Text = "?", ColorAs = "operator"},
                new NavToken() {Text = "??", ColorAs = "operator"},
                new NavToken() {Text = "??=", ColorAs = "operator"},
                new NavToken() {Text = ":", ColorAs = "operator"},
                new NavToken() {Text = "=>", ColorAs = "operator"},
                new NavToken() {Text = "?.", ColorAs = "operator"},
                new NavToken() {Text = "::", ColorAs = "operator"},
                new NavToken() {Text = ".", ColorAs = "operator"},
                new NavToken() {Text = "..", ColorAs = "operator"},
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
                new NavToken() {Text = "_", ColorAs = "keyword"},
                new NavToken() {Text = "abstract", ColorAs = "keyword"},
                new NavToken() {Text = "as", ColorAs = "keyword"},
                new NavToken() {Text = "async", ColorAs = "keyword"},
                new NavToken() {Text = "await", ColorAs = "keyword"},
                new NavToken() {Text = "base", ColorAs = "keyword"},
                new NavToken() {Text = "bool", ColorAs = "keyword"},
                new NavToken() {Text = "char", ColorAs = "keyword"},
                new NavToken() {Text = "checked", ColorAs = "keyword"},
                new NavToken() {Text = "class", ColorAs = "keyword"},
                new NavToken() {Text = "const", ColorAs = "keyword"},
                new NavToken() {Text = "decimal", ColorAs = "keyword"},
                new NavToken() {Text = "double", ColorAs = "keyword"},
                new NavToken() {Text = "enum", ColorAs = "keyword"},
                new NavToken() {Text = "explicit", ColorAs = "keyword"},
                new NavToken() {Text = "false", ColorAs = "keyword"},
                new NavToken() {Text = "fixed", ColorAs = "keyword"},
                new NavToken() {Text = "float", ColorAs = "keyword"},
                new NavToken() {Text = "get", ColorAs = "keyword"},
                new NavToken() {Text = "global", ColorAs = "keyword"},
                new NavToken() {Text = "goto", ColorAs = "keyword"},
                new NavToken() {Text = "implicit", ColorAs = "keyword"},
                new NavToken() {Text = "init", ColorAs = "keyword"},
                new NavToken() {Text = "int", ColorAs = "keyword"},
                new NavToken() {Text = "interface", ColorAs = "keyword"},
                new NavToken() {Text = "internal", ColorAs = "keyword"},
                new NavToken() {Text = "is", ColorAs = "keyword"},
                new NavToken() {Text = "lock", ColorAs = "keyword"},
                new NavToken() {Text = "nameof", ColorAs = "keyword"},
                new NavToken() {Text = "namespace", ColorAs = "keyword"},
                new NavToken() {Text = "new", ColorAs = "keyword"},
                new NavToken() {Text = "not", ColorAs = "keyword"},
                new NavToken() {Text = "null", ColorAs = "keyword"},
                new NavToken() {Text = "object", ColorAs = "keyword"},
                new NavToken() {Text = "operator", ColorAs = "keyword"},
                new NavToken() {Text = "or", ColorAs = "keyword"},
                new NavToken() {Text = "out", ColorAs = "keyword"},
                new NavToken() {Text = "override", ColorAs = "keyword"},
                new NavToken() {Text = "private", ColorAs = "keyword"},
                new NavToken() {Text = "protected", ColorAs = "keyword"},
                new NavToken() {Text = "public", ColorAs = "keyword"},
                new NavToken() {Text = "readonly", ColorAs = "keyword"},
                new NavToken() {Text = "record", ColorAs = "keyword"},
                new NavToken() {Text = "ref", ColorAs = "keyword"},
                new NavToken() {Text = "sealed", ColorAs = "keyword"},
                new NavToken() {Text = "set", ColorAs = "keyword"},
                new NavToken() {Text = "sizeof", ColorAs = "keyword"},
                new NavToken() {Text = "stackalloc", ColorAs = "keyword"},
                new NavToken() {Text = "static", ColorAs = "keyword"},
                new NavToken() {Text = "string", ColorAs = "keyword"},
                new NavToken() {Text = "struct", ColorAs = "keyword"},
                new NavToken() {Text = "this", ColorAs = "keyword"},
                new NavToken() {Text = "true", ColorAs = "keyword"},
                new NavToken() {Text = "typeof", ColorAs = "keyword"},
                new NavToken() {Text = "unchecked", ColorAs = "keyword"},
                new NavToken() {Text = "unsafe", ColorAs = "keyword"},
                new NavToken() {Text = "using", ColorAs = "keyword"},
                new NavToken() {Text = "var", ColorAs = "keyword"},
                new NavToken() {Text = "virtual", ColorAs = "keyword"},
                new NavToken() {Text = "void", ColorAs = "keyword"},
                new NavToken() {Text = "volatile", ColorAs = "keyword"},
                new NavToken() {Text = "where", ColorAs = "keyword"},
            ];

            List<NavToken> purpleKeywordTokens =
            [
                new NavToken() {Text = "break", ColorAs = "keyword"},
                new NavToken() {Text = "case", ColorAs = "keyword"},
                new NavToken() {Text = "catch", ColorAs = "keyword"},
                new NavToken() {Text = "continue", ColorAs = "keyword"},
                new NavToken() {Text = "do", ColorAs = "keyword"},
                new NavToken() {Text = "else", ColorAs = "keyword"},
                new NavToken() {Text = "finally", ColorAs = "keyword"},
                new NavToken() {Text = "for", ColorAs = "keyword"},
                new NavToken() {Text = "foreach", ColorAs = "keyword"},
                new NavToken() {Text = "if", ColorAs = "keyword"},
                new NavToken() {Text = "in", ColorAs = "keyword"},
                new NavToken() {Text = "return", ColorAs = "keyword"},
                new NavToken() {Text = "switch", ColorAs = "keyword"},
                new NavToken() {Text = "throw", ColorAs = "keyword"},
                new NavToken() {Text = "try", ColorAs = "keyword"},
                new NavToken() {Text = "when", ColorAs = "keyword"},
                new NavToken() {Text = "while", ColorAs = "keyword"},
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
                new NavToken() {Text = "csharp_cartographer_backend", ColorAs = "identifier - namespace"},
                new NavToken() {Text = "_fileProcessor", ColorAs = "identifier - field declaration"},
                new NavToken() {Text = "_fileProcessor", ColorAs = "identifier - field reference"},
                new NavToken() {Text = "Index", ColorAs = "identifier - property declaration"},
                new NavToken() {Text = "Text", ColorAs = "identifier - property reference"},
                new NavToken() {Text = "DefaultErrorMsg", ColorAs = "identifier - constant"},
            ];

            List<NavToken> lightBlueIdentifierTokens =
            [
                new NavToken() {Text = "fileName", ColorAs = "identifier - parameter"},
                new NavToken() {Text = "navTokens", ColorAs = "identifier - parameter prefix"},
                new NavToken() {Text = "localVar", ColorAs = "identifier - local variable"},
            ];

            List<NavToken> yellowIdentifierTokens =
            [
                new NavToken() {Text = "GetDemoArtifact", ColorAs = "identifier - method declaration"},
                new NavToken() {Text = "IsNullOrWhiteSpace", ColorAs = "identifier - method invocation"},
            ];

            List<NavToken> greenIdentifierTokens =
            [
                new NavToken() {Text = "HttpGet", ColorAs = "identifier - attribute"},
                new NavToken() {Text = "NavToken", ColorAs = "identifier - class declaration"},
                new NavToken() {Text = "NavToken", ColorAs = "identifier - class constructor"},
                new NavToken() {Text = "NavToken", ColorAs = "identifier - class name"},
                new NavToken() {Text = "GenerateArtifactDto", ColorAs = "identifier - record declaration"},
                new NavToken() {Text = "GenerateArtifactDto", ColorAs = "identifier - record constructor"},
                new NavToken() {Text = "GenerateArtifactDto", ColorAs = "identifier - record name"},
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
                new NavToken() {Text = "I", ColorAs = "character literal"},
                new NavToken() {Text = "Hello ", ColorAs = "quoted string"},
                new NavToken() {Text = "@\"C:\\Projects\\Cartographer\\Logs\\analysis.log\"", ColorAs = "verbatim string"},
                new NavToken() {Text = "$\"", ColorAs = "interpolated string - start"},
                new NavToken() {Text = "Artifact contains", ColorAs = "interpolated string - text"},
                new NavToken() {Text = "\"", ColorAs = "interpolated string - end"},
                new NavToken() {Text = "$@\"", ColorAs = "interpolated verbatim string - start"},
                new NavToken() {Text = "C:\\Projects\\Cartographer\\Artifacts\\\r\n\r\n", ColorAs = "interpolated verbatim string - text"},
                new NavToken() {Text = "\"", ColorAs = "interpolated verbatim string - end"},
            ];

            List<NavToken> lightGreenLiteralTokens =
            [
                new NavToken() {Text = "1", ColorAs = "numeric literal"},
                new NavToken() {Text = "0b_0010", ColorAs = "numeric literal"},
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
