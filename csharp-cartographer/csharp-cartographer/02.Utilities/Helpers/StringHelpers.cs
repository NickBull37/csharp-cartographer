namespace csharp_cartographer._02.Utilities.Helpers
{
    public static class StringHelpers
    {
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

        public static bool HasSequentialSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input.Contains("  ");
        }

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
