using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
using System.Reflection;
using System.Text.RegularExpressions;

namespace csharp_cartographer_backend._02.Utilities.Helpers
{
    public static partial class EnumExtensions
    {
        [GeneratedRegex("(?<!^)([A-Z])")]
        private static partial Regex CapitalLetterRegex();

        public static string ToSpacedString(this Enum value)
            => CapitalLetterRegex().Replace(value.ToString(), " $1");

        public static string? GetLabel(this Enum value)
        {
            var type = value.GetType();
            var name = value.ToString();

            var field = type.GetField(name);
            if (field == null)
                return name;

            var attr = field.GetCustomAttribute<LabelAttribute>();
            return attr?.Label;
        }

        public static string? GetSpacedLabel(this Enum value)
        {
            var label = value.GetLabel();
            if (label is null)
                return null;

            return CapitalLetterRegex().Replace(label, " $1");
        }
    }
}
