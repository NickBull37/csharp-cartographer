namespace csharp_cartographer_backend._02.Utilities.Helpers
{
    public static class StringHelpers
    {
        /// <summary>
        /// Counts the number of times a substring occurs in an input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="sequence">The substring.</param>
        /// <returns>The number of times the substring occured.</returns>
        public static int CountOccurrences(string input, string sequence)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(sequence))
            {
                return 0;
            }

            int count = 0;
            int index = input.IndexOf(sequence);

            while (index != -1)
            {
                count++;
                index = input.IndexOf(sequence, index + sequence.Length);
            }
            return count;
        }

        /// <summary>
        /// Determines if an input string contains multiple spaces in a row.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>True if the input string has sequential spaces, false otherwise.</returns>
        public static bool HasSequentialSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return input.Contains("  ");
        }

        /// <summary>
        /// Extracts a string containing sequential spaces from an input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>A string containing the sequential spaces.</returns>
        public static string PullSequentialSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            int count = 0;
            foreach (char c in input)
            {
                if (c == ' ')
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return new string(' ', count); // Create a string of spaces
        }
    }
}
