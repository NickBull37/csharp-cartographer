using csharp_cartographer_backend._01.Configuration.Enums;
using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
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
        public static string GetRoleKey(NavToken token)
        {
            return token.GroupRole is not GroupRole.None
                ? token.GroupRole.ToString()
                : token.SemanticRole.ToString();
        }

        /// <summary>
        /// Gets the key used to find the focused definition.
        /// key structure: [kindabrv]:[extension1]:[extension2?]
        /// </summary>
        public static string? GetFocusedKey(NavToken token)
        {
            return token.PrimaryKind switch
            {
                PrimaryKind.Delimiter => GetDelimiterKey(token),
                PrimaryKind.Identifier => GetIdentifierKey(token),
                PrimaryKind.Keyword => GetKeywordKey(token),
                PrimaryKind.KeywordOperator => GetKeywordOperatorKey(token),
                PrimaryKind.Literal => GetLiteralKey(token),
                PrimaryKind.Operator => GetOperatorKey(token),
                PrimaryKind.Punctuation => GetPunctuationKey(token),
                _ => null,
            };
        }

        private static string Key(string kindAbrv, string extension)
            => $"{kindAbrv}:{extension}";

        private static string Key(string kindAbrv, string extension1, string extension2)
            => $"{kindAbrv}:{extension1}:{extension2}";
    }
}
