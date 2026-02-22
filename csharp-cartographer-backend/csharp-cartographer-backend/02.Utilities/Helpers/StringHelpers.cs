using System.Text.RegularExpressions;

namespace csharp_cartographer_backend._02.Utilities.Helpers
{
    public static partial class StringHelpers
    {
        [GeneratedRegex("([A-Z])", RegexOptions.Compiled)]
        private static partial Regex CapitalRegex();

        /// <summary>
        ///     Counts the number of times a substring occurs in an input string.
        /// </summary>
        public static int CountOccurrences(string input, string substring)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(substring))
                return 0;

            int count = 0;
            int index = input.IndexOf(substring);

            while (index != -1)
            {
                count++;
                index = input.IndexOf(substring, index + substring.Length);
            }
            return count;
        }

        /// <summary>
        ///     Returns true if an input string has sequential spaces, false otherwise.
        /// </summary>
        public static bool HasSequentialSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return input.Contains("  ");
        }

        /// <summary>
        ///     Extracts a string containing sequential spaces from an input string.
        /// </summary>
        public static string PullSequentialSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            int count = 0;
            foreach (char c in input)
            {
                if (c == ' ')
                    count++;

                else
                    break;
            }

            return new string(' ', count);
        }

        /// <summary>
        ///     Adds spaces to Enum member names for UI display.
        /// </summary>
        public static string AddSpaces(string input)
        {
            return CapitalRegex().Replace(input, " $1").TrimStart();
        }


        /*
         *  Demo for string types (quoted, verbatim, interpolated)
         */
        public static string QuotedStringDemo(string name)
        {
            return "Hello " + name + "! Welcome to the Cartographer tool.";
        }

        public static string QuotedStringWithEscapeSequencesDemo(string name)
        {
            return "Hello \"" + name + "\"! Welcome to the Cartographer tool.";
        }

        public static string VerbatimStringDemo()
        {
            var test = @"First" + @"+" + @"Second";

            var test2 = $"First" + "+" + @"Second";

            return @"C:\Projects\Cartographer\Logs\analysis.log";
        }

        public static string InterpolatedStringDemo(int tokenCount)
        {
            return $"Artifact contains {tokenCount} tokens.";
        }

        public static string VerbatimInterpolatedStringDemo(string fileName)
        {
            return $@"C:\Projects\Cartographer\Artifacts\{fileName}";
        }

        public static string InterpolatedStringWithFormatDemo(TimeSpan time)
        {
            if (time.TotalSeconds < 1)
                return $"{time.TotalMilliseconds:0} ms"; // Less than 1 second: show milliseconds

            else if (time.TotalMinutes < 1)
                return $"{time.Seconds}.{time.Milliseconds:000} seconds"; // Less than 1 minute: show seconds and milliseconds

            else
                return time.ToString(@"hh\:mm\:ss\.fff"); // Standard HH:MM:SS.MS format
        }
    }
}
