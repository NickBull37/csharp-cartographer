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
            _ = obj is string s;                   // declaration pattern (captures)
            _ = number is 5;                       // constant pattern
            _ = maybeObj is null;                  // constant pattern
            _ = maybeObj is not null;              // constant pattern
            _ = number is > 0;                     // relational pattern
            _ = tuple is (var a, var b);           // positional pattern
            _ = array is [1, .. var rest];         // list pattern
            _ = obj is var x;                      // var pattern (always matches)

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
