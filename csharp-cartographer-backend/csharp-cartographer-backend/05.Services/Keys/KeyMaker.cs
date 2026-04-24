using csharp_cartographer_backend._02.Utilities.Helpers;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /*
         * Default Key Structure: [Kind Abrv]:[distinguishing ext]:[optional extensions]
         */

        private const string DelimiterKind = "DL";
        private const string IdentifierKind = "ID";
        private const string KeywordKind = "KW";
        private const string KeywordOperatorKind = "KWOP";
        private const string LiteralKind = "LT";
        private const string OperatorKind = "OP";
        private const string PunctuationKind = "PN";

        /// <summary>
        /// Gets the SemanticRole definition key.
        /// </summary>
        public static string? GetKey(SemanticRole role)
        {
            return role.GetLabel() ?? role.ToString();
        }

        /// <summary>
        /// Gets the focused definition key.
        /// </summary>
        public static string? GetKey(NavToken token)
        {
            var key = token.PrimaryKind switch
            {
                PrimaryKind.Delimiter => GetDelimiterKey(token),
                PrimaryKind.Operator => GetOperatorKey(token),
                PrimaryKind.Identifier => GetIdentifierKey(token),
                PrimaryKind.Literal => GetLiteralKey(token),
                PrimaryKind.Keyword => GetKeywordKey(token),
                PrimaryKind.KeywordOperator => GetKeywordOperatorKey(token),
                _ => null,
            };

            return key?.ToString();
        }
    }
}
