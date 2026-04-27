using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._01.Configuration.DemoFiles
{
    public class PatternMatchingDemo
    {
        public void Demo()
        {
            object obj = "hello";
            object? maybeObj = null;
            int number = 5;
            (int, int) tuple = (1, 2);
            int[] array = [1, 2, 3, 4];

            // --- Basic pattern forms ---

            _ = obj is string;                     // type pattern
            _ = obj is NavToken;                     // type pattern
            _ = obj is string s;                   // declaration pattern (captures)
            _ = number is 5;                       // constant pattern
            _ = maybeObj is null;                  // constant pattern
            _ = maybeObj is not null;              // constant pattern
            _ = number is > 0;                     // relational pattern
            _ = tuple is (var a, var b);           // positional pattern
            _ = array is [1, ..];                  // list pattern
            _ = array is [1, .. var rest];         // list pattern
            _ = obj is var x;                      // var pattern (always matches)

            string[] words = ["hello", "world"];

            if (words is ["hello", "world"]) // exact match
            {
            }

            object[] items = [1, "test", 3.14];

            if (items is [int, string, double]) // types match
            {
            }

            int[] numbers = [1, 2, 3, 4];

            if (numbers is [var first, var second, var third]) // first, second, third
            {
            }

            if (numbers is [1, var middle, 3]) // middle = middle
            {
            }

            if (numbers is [_, _, 15]) // ends with 15
            {
            }

            if (numbers is [1, .. var remaining]) // remaining = [2, 3, 4]
            {
            }

            if (numbers is [..]) // matches if numbers is not null
            {
            }

            if (numbers is [.. var startingSlice, 50]) // slices all elements before first pattern
            {
            }

            if (numbers is [1, .. var middleSlice, 50]) // slices all elements between constant patterns
            {
            }

            if (numbers is [_, _, _]) // exactly 3 elements
            {
            }

            if (numbers is [1, var z, 3] && z > 1) // Middle is greater than 1
            {
            }

            object numericObj = number;
            _ = numericObj is int or long;         // combinator type patterns

            string? maybeString = obj as string;

            int testx = 5;
            testx = (int)testx;

            decimal testy = 5;
            testy = (int)testy;



            _ = maybeString is { Length: > 0 };    // property pattern

            // --- Binding behavior examples ---

            if (obj is string s2)               // binds s2 to obj (as string)
            {
            }

            if (obj is var x2)                  // binds x2 to obj
            {
            }

            if (number is int i and > 0)        // binds i
            {
            }

            if (number is int j and > 0 and < 100)        // binds j
            {
            }

            if (array is [.. var rest2])        // binds rest2
            {
            }

            if (tuple is (int a2, int b2) t)    // binds t (whole tuple), binds a2/b2 (parts)
            {
            }
        }
    }
}
