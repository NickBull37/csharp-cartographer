namespace csharp_cartographer_backend._02.Utilities.Helpers
{
    public static class StringHelpers
    {
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
    }
}
