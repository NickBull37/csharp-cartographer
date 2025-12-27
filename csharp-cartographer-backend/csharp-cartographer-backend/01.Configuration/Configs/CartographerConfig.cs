namespace csharp_cartographer_backend._01.Configuration.Configs
{
    public class CartographerConfig
    {
        /// <summary>
        ///     Will log the artifact data to 02.Utilities/Logging/Logs/ArtifactLog.txt if true.
        /// </summary>
        public bool ShouldLogArtifact { get; set; }

        /// <summary>
        ///     Will log any unidentified (red) tokens to 02.Utilities/Logging/Logs/TokenLog.txt if true.
        /// </summary>
        public bool ShouldLogUnidentifiedTokens { get; set; }

        // TODO: Move to ChatGpt client config
        /// <summary>
        ///     The ChatGpt endpoint url for generating code analysis..
        /// </summary>
        public string ChatGptUrl { get; set; } = string.Empty;

        /// <summary>
        ///     The prompt supplied to ChatGpt when generating code analysis.
        /// </summary>
        public string ChatGptPrompt { get; set; } = string.Empty;
    }
}
