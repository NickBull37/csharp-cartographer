using System.Text.RegularExpressions;

namespace csharp_cartographer_backend._02.Utilities.Helpers
{
    public static partial class EnumExtensions
    {
        [GeneratedRegex("(?<!^)([A-Z])")]
        private static partial Regex CapitalLetterRegex();

        public static string ToSpacedString(this Enum value)
            => CapitalLetterRegex().Replace(value.ToString(), " $1");
    }
}
