namespace csharp_cartographer_backend._01.Configuration.Configs
{
    public class CartographerConfig
    {
        /// <summary>
        ///     If true the artifact data will be logged to /02.Utilities/Logging/Logs/ArtifactLog.txt.
        /// </summary>
        public bool ShouldLogArtifact { get; set; }

        /// <summary>
        ///     If true any identifier tokens with semantic data will be logged to /02.Utilities/Logging/Logs/TokenLog.txt.
        /// </summary>
        public bool ShouldLogSemanticData { get; set; }

        /// <summary>
        ///     If true any unidentified (red) tokens will be logged to /02.Utilities/Logging/Logs/TokenLog.txt.
        /// </summary>
        public bool ShouldLogUnidentifiedTokens { get; set; }

        /// <summary>
        ///     If true the SyntaxHighlighter will use Roslyn "internal implementation only"
        ///     semantic data to color tokens defined in referenced assembiles.
        /// </summary>
        public bool SemanticDataHighlightingEnabled { get; set; }

        // TODO: Move to ChatGpt client config
        /// <summary>
        ///     The ChatGpt endpoint url for generating code analysis.
        /// </summary>
        public string ChatGptUrl { get; set; } = string.Empty;

        /// <summary>
        ///     The prompt supplied to ChatGpt when generating code analysis.
        /// </summary>
        public string ChatGptPrompt { get; set; } = string.Empty;
    }
}
