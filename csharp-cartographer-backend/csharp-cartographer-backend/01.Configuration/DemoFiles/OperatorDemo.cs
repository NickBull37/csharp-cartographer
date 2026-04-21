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
                // Unary: +, -, ++, --
                // ------------------------------------------------------------
                int sum = a + b;
                int diff = a - b;
                int prod = a * b;
                int quot = a / b;
                int mod = a % b;

                int unaryPlus = +a;
                int unaryMinus = -a;

                a++;         // post-increment
                b--;         // post-decrement
                ++a;         // pre-increment
                --b;         // pre-decrement


                // ------------------------------------------------------------
                // Assignment: =, +=, -=, *=, /=, %=, &=, |=, ^=, <<=, >>=, >>>=
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
                value >>>= 1;


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
                // Boolean Logical: &&, ||, !
                // ------------------------------------------------------------
                bool andAlso = (a > 0) && (b > 0);           // && (logical AND)
                bool orElse = (a > 0) || (b < 0);            // || (logical OR)
                bool boolVal = true;
                bool logicalNot = !boolVal;                  // !  (logical NOT)


                // ------------------------------------------------------------
                // Null: ??, ??=, ?. , ?[] , !, x?.y
                // ------------------------------------------------------------
                string? nullableString = null;
                string name = nullableString ?? "default-name";   // ??  (null-coalescing)
                nullableString ??= "now-initialized";             // ??= (null-coalescing assignment)
                int? length = nullableString?.Length;             // ?.  (null-conditional access)

                int[]? maybeNumbers = null;
                int? first = maybeNumbers?[0];                    // ?[] (null-conditional indexer)

                string definitelyNotNull = nullableString!;       // !   (null-forgiving)

                var ternary = nullableString is null ? "default" : nullableString;


                // ------------------------------------------------------------
                // Bitwise: &, |, ^, ~
                // ------------------------------------------------------------
                int andBits = a & b;                         // & bitwise AND
                int orBits = a | b;                          // | bitwise OR
                int xorBits = a ^ b;                         // ^ bitwise XOR
                int bitNot = ~a;                             // ~ bitwise NOT


                // ------------------------------------------------------------
                // Shift: <<, >>, >>>
                // ------------------------------------------------------------
                int leftShift = a << 1;
                int rightShift = a >> 1;            // arithmetic right shift (sign-preserving)
                int logicalRightShift = a >>> 1;    // logical right shift (zero-fill)


                // ------------------------------------------------------------
                // Keyword: nameof, sizeof, typeof, default(T)
                // ------------------------------------------------------------
                string className = nameof(OperatorDemo);     // nameof
                int sizeOfInt = sizeof(int);                 // sizeof
                Type type = typeof(OperatorDemo);               // typeof
                string defaultString = default(string);      // default(T)


                // ------------------------------------------------------------
                // Member Access: ., ?., [], ?[]
                // ------------------------------------------------------------
                string text = "text";

                int textLength = text.Length;                   // .
                string? nullableText = null;
                int? nullableLength = nullableText?.Length;     // ?.
                int[] numbers = { 10, 20, 30 };
                int firstNumber = numbers[0];                   // []
                int[]? nullableNumbers = null;
                int? nullableFirst = nullableNumbers?[0];       // ?[]
                int textTest = text!.Length;


                // ------------------------------------------------------------
                // Lambda: =>
                // ------------------------------------------------------------
                Func<int, int> square = x => x * x;


                // ------------------------------------------------------------
                // Index & range: ^, ..
                // ------------------------------------------------------------
                int index = 1;
                int offset = 1;
                int start = 1;
                int end = 2;

                int[] numbersArray = { 10, 20, 30, 40, 50 };

                int indexDemo = numbersArray[^1];                    // ^  (Index)
                int[] rangeDemo = numbersArray[1..^1];               // .. (Range)

                // ----- Index from end (^) -----

                int lastElement = numbersArray[^1];                 // literal offset from end
                int lastElementVar = numbersArray[^offset];         // variable offset from end
                int lastElementExpr = numbersArray[^(offset + 1)];  // expression offset from end

                // ----- Direct index -----

                int firstIndex = numbersArray[0];                   // literal index
                int byVar = numbersArray[index];                    // variable index
                int byExpr = numbersArray[index + 2];               // expression index

                // ----- Range (..) -----

                int[] middle = numbersArray[1..^1];                        // literal start and end-from-end
                int[] middleVar = numbersArray[start..^end];               // variable start and end-from-end
                int[] middleExpr = numbersArray[(start + 1)..^(end - 1)];  // expression bounds

                // ----- Open-ended ranges -----

                int[] fromStart = numbersArray[..^1];               // start omitted
                int[] toEnd = numbersArray[1..];                    // end omitted
                int[] fullCopy = numbersArray[..];                  // entire range

                // ----- Using Index struct -----

                Index idx = ^2;
                int fromIndexStruct = numbersArray[idx];            // Index value

                Index idxExpr = ^(offset + 1);
                int fromIndexExpr = numbersArray[idxExpr];

                // ----- Using Range struct -----

                Range r = 1..^1;
                int[] fromRangeStruct = numbersArray[r];

                Range rVar = start..^end;
                int[] fromRangeVar = numbersArray[rVar];

                // ----- Mixed forms -----

                int mixed1 = numbersArray[^start];                  // variable in ^
                int[] mixed2 = numbersArray[index..^end];           // direct + from-end
                int[] mixed3 = numbersArray[(index + 1)..];         // expression start
                int[] mixed4 = numbersArray[..^(offset + 2)];       // expression end-from-end


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
                var testItem = GetItem() with { Price = 0.79m };


                // ------------------------------------------------------------
                // Indirection: *p / &x
                // ------------------------------------------------------------
                unsafe
                {
                    int val = 10;
                    int* pValue = &val;             // & (AddressOf)
                    Console.WriteLine(*pValue);     // * (Dereference)
                }
            }

            public record Item(string Name, decimal Price);

            public Item GetItem()
            {
                return new Item("", 0.0m);
            }
        }
    }
}
