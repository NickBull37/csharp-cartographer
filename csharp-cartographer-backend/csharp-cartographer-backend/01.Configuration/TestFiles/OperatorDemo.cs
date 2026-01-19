using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._06.Workflows.Artifacts;
using Microsoft.CodeAnalysis;

namespace csharp_cartographer_backend._01.Configuration.TestFiles
{
    public class OperatorDemo
    {
        private int _counter = 0;

        public void DemoAll(int a, int b)
        {
            // arithmetic: +, -, *, /, %
            int sum = a + b;
            int diff = a - b;
            int prod = a * b;
            int quot = a / b;
            int mod = a % b;

            // unary +, -, ++, --, !, ~
            int unaryPlus = +a;
            int unaryMinus = -a;
            a++;
            b--;
            bool boolVal = true;
            bool notBool = !boolVal;
            int bitNot = ~a;

            // comparison (relational): ==, !=, >, <, >=, <=
            bool eq = a == b;
            bool neq = a != b;
            bool gt = a > b;
            bool lt = a < b;
            bool gte = a >= b;
            bool lte = a <= b;

            // logical: &&, ||
            bool logicalAnd = (a > 0) && (b > 0);
            bool logicalOr = (a > 0) || (b < 0);

            // bitwise: &, |, ^, ~, <<, >>
            int andBits = a & b;
            int orBits = a | b;
            int xorBits = a ^ b;
            int leftShift = a << 1;
            int rightShift = a >> 1;

            // assignment: =, +=, -=, *=, /=, %=, &=, |=, ^=, <<=, >>=
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

            // null-coalescing: ??, ??=
            string? maybeName = null;
            string name = maybeName ?? "default-name";
            maybeName ??= "now-initialized";

            // type: is, as, sizeof, typeof
            object obj = "hello";
            if (obj is string s)
            {
                Console.WriteLine(s);
            }

            string? casted = obj as string;
            int sizeOfInt = sizeof(int);
            Type t = typeof(OperatorDemo);

            // lambda: =>
            Func<int, int> square = x => x * x;
            int squared = square(5);

            // index & range: [], .., ^
            int[] numbers = new[] { 10, 20, 30, 40, 50 };
            int firstElement = numbers[0];           // []
            int lastElement = numbers[^1];           // ^ (index from end)
            int[] middle = numbers[1..^1];           // .. (range)

            // member access: ., ?., ::
            Console.WriteLine(firstElement);         // .
            string? nullableString = null;
            int? length = nullableString?.Length;    // ?.

            // global:: alias with ::
            global::System.Console.WriteLine(global::System.DateTime.Now);

            // misc: new, checked, unchecked, default, nameof, stackalloc
            var list = new List<int> { 1, 2, 3 };    // new

            checked
            {
                int max = int.MaxValue;
                // This will throw at runtime if executed, but it's valid C#
                // max++;
            }

            unchecked
            {
                int max2 = int.MaxValue;
                max2++; // overflow ignored
            }

            int defaultInt = default;               // default literal
            string defaultString = default(string); // default(T) form
            string className = nameof(OperatorDemo);

            // stackalloc with Span<T> (safe context)
            Span<int> span = stackalloc int[5];
            span[0] = 42;

            // use the field with operators
            _counter += sum;
            _counter -= diff;

            var test = new Test
            {
                TestInt = 5
            };
            string stringX = test.TestInt.ToString();
        }
    }

    public class Test
    {
        public int TestInt { get; set; }

        public static string NullConditionalOperatorTest()
        {
            int test = GetText().Length;

            int? test2 = GetText()?.Length;

            string? text = GetText()?.Trim()?.ToUpperInvariant();

            int? length = text?.Split(' ', StringSplitOptions.RemoveEmptyEntries)?.FirstOrDefault()?.Length;

            char? firstChar = text?.FirstOrDefault();

            string? replaced = text?.Replace("A", "X")?.Replace("E", "Y");

            string result =
                $"Text: {text ?? "null"}, " +
                $"Length: {length?.ToString() ?? "null"}, " +
                $"FirstChar: {firstChar?.ToString() ?? "null"}, " +
                $"Replaced: {replaced ?? "null"}";

            return result;
        }

        static void ReadIn(in int value)
        {
            Console.WriteLine($"In value = {value}");
            // value = 99; // ❌ compile error: cannot modify in parameter

            var numbers = new[]
            {
                new { Id = 1, Value = 10 },
                new { Id = 2, Value = 20 },
                new { Id = 3, Value = 10 }
            };

            var labels = new[]
            {
                new { Id = 1, Label = "A" },
                new { Id = 2, Label = "B" },
                new { Id = 3, Label = "C" }
            };

            var query =
                from n in numbers
                join l in labels on n.Id equals l.Id
                let doubled = n.Value * 2
                where doubled > 15
                group new { n, l, doubled } by n.Value
                into g
                orderby g.Key ascending, g.Count() descending
                select new
                {
                    Key = g.Key,
                    Count = g.Count()
                };
        }

        private static string? GetText() =>
            DateTime.Now.Second % 2 == 0 ? "hello world" : null;

        public static (T Value, int Length) Describe<T>(T value)
            where T : notnull
        {
            Dictionary<string, int> lookup;

            return (value, value.ToString()!.Length);
        }

        static string Classify(object value) =>
            value switch
            {
                int or long or short => "Integer number",
                float or double or decimal => "Floating-point number",
                string or char => "Text",
                null => "Null",
                _ => "Other"
            };
    }

    public class Cache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _items = new();

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            if (!_items.TryGetValue(key, out var value))
            {
                value = factory(key);
                _items[key] = value;
            }

            return value;
        }
    }

    public class NodeFinder
    {
        public string DemoTest(TestClass demoParam)
        {
            return default;
        }

        public T GetValue<T>()
        {
            return default;
        }

        public T? Find<T>() where T : class
        {
            return null;
        }

        public Nullable<T> Get<T>() where T : struct
        {
            return null;
        }

        public T? GetTest<T>() where T : struct
            => default;

        public T? FindTest<T>() where T : class?
        {
            return null;
        }

        /// <summary>
        /// Finds all descendant syntax nodes of the specified type.
        /// </summary>
        public IEnumerable<TSyntax> FindNodes<TSyntax>(SyntaxNode root)
            where TSyntax : SyntaxNode
        {
            return root.DescendantNodes().OfType<TSyntax>();
        }

        /// <summary>
        /// Finds the first syntax node of the specified type.
        /// </summary>
        public TSyntax? FindFirstNode<TSyntax>(SyntaxNode root)
            where TSyntax : SyntaxNode
        {
            return root.DescendantNodes().OfType<TSyntax?>().FirstOrDefault();
        }

        /// <summary>
        /// Counts how many nodes of a given syntax type exist.
        /// </summary>
        public int CountNodes<TSyntax>(SyntaxNode root)
            where TSyntax : SyntaxNode
        {
            return root.DescendantNodes().OfType<TSyntax>().Count();
        }

        public TSyntax? FindFirstNodeTest<TSyntax>(SyntaxNode root)
            where TSyntax : SyntaxNode
        {
            return root.DescendantNodes().OfType<TSyntax>().FirstOrDefault();
        }
    }

    public class TestClass2
    {
        private readonly IGenerateArtifactWorkflow? _generateArtifactWorkflow;

        private readonly IEnumerable<NavToken> Tokens = [];

        private readonly IEnumerable<NavToken>? TestTokens = [];

        private readonly IEnumerable<NavToken?> DemoTokens = [];

        private readonly IEnumerable<NavToken?>? FakeTokens = [];
    }

    public class Test3
    {
        public record Person(string Name, int Age);

        public void DoSomething()
        {

            object input = new Person("Alice", 30);

            switch (input)
            {
                // Type pattern + property pattern
                case Person { Age: >= 18 } adult:
                    Console.WriteLine($"Adult: {adult.Name}");
                    break;

                // Type pattern + when guard
                case Person p when p.Age < 18:
                    Console.WriteLine($"Minor: {p.Name}");
                    break;

                // Constant pattern
                case null:
                    Console.WriteLine("Input is null");
                    break;

                // Discard pattern
                default:
                    Console.WriteLine("Unknown input");
                    break;
            }
        }
    }
}
