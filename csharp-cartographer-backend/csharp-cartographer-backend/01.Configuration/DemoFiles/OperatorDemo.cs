namespace csharp_cartographer_backend._01.Configuration.DemoFiles
{
    using System;
    using System.Collections.Generic;

    namespace csharp_cartographer_backend._01.Configuration.DemoFiles
    {
        public class OperatorDemo
        {
            public void Demo(int a, int b)
            {
                // ------------------------------------------------------------
                // Arithmetic: +, -, *, /, %, ++, --
                // Unary: +, -, ++, --, !, ~
                // ------------------------------------------------------------
                int sum = a + b;
                int diff = a - b;
                int prod = a * b;
                int quot = a / b;
                int mod = a % b;

                int unaryPlus = +a;
                int unaryMinus = -a;

                a++;                                    // increment
                b--;                                    // decrement

                bool boolVal = true;
                bool logicalNot = !boolVal;             // ! (logical not)
                int bitwiseNot = ~a;                    // ~

                // ------------------------------------------------------------
                // Comparison: ==, !=, >, <, >=, <=
                // ------------------------------------------------------------
                bool eq = a == b;
                bool neq = a != b;
                bool gt = a > b;
                bool lt = a < b;
                bool gte = a >= b;
                bool lte = a <= b;

                // ------------------------------------------------------------
                // Boolean Logical: &&, ||, & , |, ^
                // ------------------------------------------------------------
                bool andAlso = (a > 0) && (b > 0);           // &&
                bool orElse = (a > 0) || (b < 0);            // ||
                bool andBool = (a > 0) & (b > 0);            // & (non-short-circuit boolean)
                bool orBool = (a > 0) | (b < 0);             // | (non-short-circuit boolean)
                bool xorBool = (a > 0) ^ (b > 0);            // ^

                // ------------------------------------------------------------
                // Bitwise: &, |, ^, ~
                // ------------------------------------------------------------
                int andBits = a & b;
                int orBits = a | b;
                int xorBits = a ^ b;
                int bitNot = ~a;

                // ------------------------------------------------------------
                // Shift: <<, >>, >>>
                // ------------------------------------------------------------
                int leftShift = a << 1;
                int rightShift = a >> 1;            // arithmetic right shift (sign-preserving)
                int logicalRightShift = a >>> 1;    // logical right shift (zero-fill)


                // ------------------------------------------------------------
                // Assignment: =, +=, -=, *=, /=, %=, &=, |=, ^=, <<=, >>=
                // ------------------------------------------------------------
                int value = 1;
                value += 2;
                value -= 1;
                value *= 3;
                value /= 2;
                value %= 2;
                value &= 0b_1111;
                value |= 0b_0001;
                value ^= 0b_0010;
                value <<= 1;
                value >>= 1;


                // ------------------------------------------------------------
                // Null: ??, ??=, ?. , ?[] , !
                // ------------------------------------------------------------
                string? maybeName = null;
                string name = maybeName ?? "default-name";   // ??
                maybeName ??= "now-initialized";             // ??=

                string? nullableString = null;
                int? length = nullableString?.Length;        // ?.

                int[]? maybeNumbers = null;
                int? first = maybeNumbers?[0];               // ?[] (null-conditional indexer)

                string definitelyNotNull = maybeName!;       // ! (null-forgiving)


                // ------------------------------------------------------------
                // Type / Pattern: is, as, nameof, sizeof, typeof, default(T)
                // ------------------------------------------------------------
                object obj = "hello";

                if (obj is string s) { }                     // is
                string? casted = obj as string;              // as
                string className = nameof(OperatorDemo);     // nameof
                int sizeOfInt = sizeof(int);                 // sizeof
                Type t = typeof(OperatorDemo);               // typeof
                string defaultString = default(string);      // default(T)


                // ============================================================
                // Member Access: ., ?., [], ?[], ()
                // ============================================================
                string text = "text";

                int textLength = text.Length;                   // .
                string? nullableText = null;
                int? nullableLength = nullableText?.Length;     // ?.
                int[] numbers = { 10, 20, 30 };
                int firstNumber = numbers[0];                   // []
                int[]? nullableNumbers = null;
                int? nullableFirst = nullableNumbers?[0];       // ?[]
                string methodCallResult = text.ToString();      // ()


                // ------------------------------------------------------------
                // Lambda: =>
                // ------------------------------------------------------------
                Func<int, int> square = x => x * x;


                // ------------------------------------------------------------
                // Index & range: ^, ..
                // ------------------------------------------------------------
                int[] numbersArray = { 10, 20, 30, 40, 50 };

                int lastElement = numbersArray[^1];               // ^  (Index)
                int[] middle = numbersArray[1..^1];               // .. (Range)


                // ------------------------------------------------------------
                // new, checked, unchecked, stackalloc
                // ------------------------------------------------------------
                var list = new List<int> { 1, 2, 3 };        // new

                checked                                      // checked
                {
                    int max = int.MaxValue;
                }
                unchecked                                    // unchecked
                {
                    int max2 = int.MaxValue;
                }
                Span<int> span = stackalloc int[5];          // stackalloc


                // ------------------------------------------------------------
                // with expression (copy-and-mutate)
                // ------------------------------------------------------------
                var item = new Item("Apples", 1.19m);
                var saleItem = item with { Price = 0.79m };    // with
            }

            public record Item(string Name, decimal Price);
        }
    }
}
