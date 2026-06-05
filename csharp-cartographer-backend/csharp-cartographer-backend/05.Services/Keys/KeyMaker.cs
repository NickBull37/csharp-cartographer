using csharp_cartographer_backend._02.Utilities.Helpers;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /// Key Structure: [kindabrv]:[extension]:[modifier]

        private const string DL = "DL";
        private const string ID = "ID";
        private const string KW = "KW";
        private const string KWOP = "KWOP";
        private const string LT = "LT";
        private const string OP = "OP";
        private const string PN = "PN";

        /// <summary>
        /// Gets the key used to find the SemanticRole definition.
        /// </summary>
        public static string GetRoleKey(SemanticRole role)
        {
            /*
             * The SemanticRole is used as the definition key by default. 
             * Delimiters are the exception since they have much more overlap
             * than keywords, operators, etc. Use the label on the SemanticRole
             * as the key for Delimiters.
             */

            return role.GetLabel() ?? role.ToString();
        }

        /// <summary>
        /// Gets the key used to find the focused definition.
        /// </summary>
        public static string? GetFocusedKey(NavToken token)
        {
            return token.PrimaryKind switch
            {
                PrimaryKind.Delimiter => GetDelimiterKey(token),
                PrimaryKind.Operator => GetOperatorKey(token),
                PrimaryKind.Identifier => GetIdentifierKey(token),
                PrimaryKind.Literal => GetLiteralKey(token),
                PrimaryKind.Keyword => GetKeywordKey(token),
                PrimaryKind.KeywordOperator => GetKeywordOperatorKey(token),
                _ => null,
            };
        }

        private static string Key(string kindAbrv, string extension)
            => $"{kindAbrv}:{extension}";

        private static string Key(string kindAbrv, string extension1, string extension2)
            => $"{kindAbrv}:{extension1}:{extension2}";
    }
}
