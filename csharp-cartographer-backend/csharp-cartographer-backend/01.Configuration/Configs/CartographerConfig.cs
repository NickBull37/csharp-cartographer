namespace csharp_cartographer_backend._01.Configuration.Configs
{
    public class CartographerConfig
    {
        /// <summary>
        ///     Will log the full list of tokens to 02.Utilities/Logging/Logs/TokenListLog.txt if true.
        /// </summary>
        public bool LogArtifact { get; set; }

        /// <summary>
        ///     Will log any unidentified (red) tokens to 02.Utilities/Logging/Logs/TokenLog.txt if true.
        /// </summary>
        public bool LogUnidentifiedTokens { get; set; }

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
