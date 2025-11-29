namespace csharp_cartographer_backend._01.Configuration.Configs
{
    public class CartographerConfig
    {
        /// <summary>
        ///     A boolean flag that colors all unhighlighted tokens red when enabled.
        /// </summary>
        public bool ShowUnhighlightedTokens { get; set; }

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
