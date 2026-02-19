using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._06.Workflows.Artifacts;
using Microsoft.CodeAnalysis;
using Handler = System.Action<int>;
using IO = System.IO;
using MyToken = csharp_cartographer_backend._03.Models.Tokens;
using SB = System.Text.StringBuilder;

namespace csharp_cartographer_backend._01.Configuration.TestFiles
{
    public class OperatorDemo
    {
        private int _counter = 0;

        private readonly SB StringBuilder = new();

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

            if (obj is string sss)
                Console.WriteLine(sss);

            string? casted = obj as string;
            int sizeOfInt = sizeof(int);
            Type t = typeof(OperatorDemo);

            // lambda: =>
            Func<int, int> square = x => x * x;
            Func<int?, int?> squareTest = x => x * x;
            int squared = square(5);

            Handler typeAlias;

            // index & range: [], .., ^
            int[] numbers = new[] { 10, 20, 30, 40, 50 };
            int[] numbers2 = [10, 20, 30, 40, 50];
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

    public class FileReaderTest
    {
        public string ReadFirstLine(string path)
        {
            // IO is an alias for System.IO
            using var reader = new IO.StreamReader(path);

            System.Console.WriteLine("");

            return reader.ReadLine();
        }

        public bool FileExists(string path)
        {
            var token = new MyToken.NavToken();

            var test = new NavToken()
            {
                Index = 57
            };

            csharp_cartographer_backend._03.Models.Artifacts.Artifact artifact = new(
                "",
                TimeSpan.Zero,
                []);

            _03.Models.Artifacts.Artifact artifact2 = new(
                "",
                TimeSpan.Zero,
                []);

            // Alias used again
            return IO.File.Exists(path);
        }
    }

    public class TestNode
    {
        public string Name { get; }
        public TestNode? Parent { get; }

        public TestNode(string name, TestNode? parent = null)
        {
            Name = name;
            Parent = parent;
        }
    }

    public class Test
    {
        public int TestInt { get; set; }

        public static string NullConditionalOperatorTest()
        {
            // Build a simple node chain: root -> child -> grandchild
            var root = new TestNode("Root");
            var child = new TestNode("Child", root);
            var grandchild = new TestNode("Grandchild", child);

            TestNode? start = grandchild;
            for (int i = 1; i < 100; i++)
            {
                Console.WriteLine(i);
            }
            for (TestNode n = start; n != null; n = n.Parent)
            {
                Console.WriteLine(n.Name);
            }
            for (TestNode? n = start; n != null; n = n.Parent)
            {
                Console.WriteLine(n.Name);
            }

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

    public unsafe class SpecialCaseOperatorDemo
    {
        public static void Test()
        {
            // ------------------------------
            // !
            // ------------------------------

            // BooleanLogical: logical NOT
            bool flag = false;
            Console.WriteLine(!flag);

            // Null: null-forgiving operator
            string? maybeNull = "hello";
            Console.WriteLine(maybeNull!.Length);

            // ------------------------------
            // &
            // ------------------------------

            // BitwiseShift: bitwise AND
            int x = 6;   // 110
            int y = 3;   // 011
            Console.WriteLine(x & y); // 010

            // BooleanLogical: non-short-circuit AND
            bool a = true;
            bool b = false;
            Console.WriteLine(a & b);

            // Pointer: address-of
            int value = 10;
            int* pValue = &value;
            Console.WriteLine(*pValue);

            // ------------------------------
            // |
            // ------------------------------

            // BitwiseShift: bitwise OR
            Console.WriteLine(x | y); // 111

            // BooleanLogical: non-short-circuit OR
            Console.WriteLine(a | b);

            // ------------------------------
            // ^
            // ------------------------------

            // BitwiseShift: bitwise XOR
            Console.WriteLine(x ^ y); // 101

            // BooleanLogical: boolean XOR
            Console.WriteLine(a ^ b);

            // IndexRange: index-from-end
            int[] numbers = { 10, 20, 30, 40 };
            Console.WriteLine(numbers[^1]); // 40

            // ------------------------------
            // *
            // ------------------------------

            // Arithmetic: multiplication
            int product = x * y;
            Console.WriteLine(product);

            // Pointer: pointer type and dereference
            int* p2 = &value;
            Console.WriteLine(*p2);
        }
    }

    public class QueryExpressionDemo
    {
        private IEnumerable<int> _evens =
            from n in new[] { 1, 2, 3, 4, 102 }
            where n % 2 == 0
            select n;

        public IEnumerable<int> Odds { get; } =
            from n in new[] { 1, 2, 3, 4 }
            where n % 2 == 1
            select n;

        Func<IEnumerable<int>> func = () =>
            from n in new[] { 1, 2, 3, 4, 102 }
            select n;

        public IEnumerable<int> GetTest()
        {
            var count =
                (from n in _evens
                 where n > 10
                 select n).Count();

            return
                from n in _evens
                where n > 100
                select n;
        }

        public static void Main()
        {
            var numbers = new List<Item>
            {
                new Item { Id = 1, Value = 5,  Category = "A" },
                new Item { Id = 2, Value = 20, Category = "A" },
                new Item { Id = 3, Value = 15, Category = "B" },
                new Item { Id = 4, Value = 30, Category = "B" },
            };

            var labels = new List<Label>
            {
                new Label { Id = 1, Name = "One" },
                new Label { Id = 2, Name = "Two" },
                new Label { Id = 3, Name = "Three" },
                new Label { Id = 4, Name = "Four" },
            };

            // 1) Basic from / in / where / select
            var evens =
                from n in numbers
                where n.Value % 2 == 0
                select n.Value;

            // 2) let
            var squares =
                from n in numbers
                let doubled = n.Value * 2
                where doubled > 20
                select doubled;

            // 3) join / on / equals
            var joined =
                from n in numbers
                join l in labels on n.Id equals l.Id
                select new { n.Value, l.Name };

            // 4) group ... by
            var grouped =
                from n in numbers
                group n by n.Category;

            // 5) group ... by ... into
            var groupedFiltered =
                from n in numbers
                group n by n.Category into g
                where g.Count() > 1
                select new
                {
                    Category = g.Key,
                    Count = g.Count()
                };

            // 6) join ... into (group join)
            var groupJoin =
                from n in numbers
                join l in labels on n.Id equals l.Id into matches
                select new
                {
                    n.Id,
                    MatchCount = matches.Count()
                };

            // 7) orderby ascending / descending
            var ordered =
                from n in numbers
                orderby n.Category ascending, n.Value descending
                select n;

            // 8) Full example (uses almost every keyword)
            var full =
                from p in numbers
                join n in labels on p.Id equals n.Id
                let doubled = p.Value * 2
                where doubled > 10
                group new { p, n, doubled } by p.Category into g
                orderby g.Key ascending
                select new
                {
                    Category = g.Key,
                    Count = g.Count(),
                    MaxDoubled = g.Max(x => x.doubled)
                };

            // Force enumeration so everything actually runs
            Console.WriteLine("Done");
        }

        public class Item
        {
            public int Id { get; set; }
            public int Value { get; set; }
            public string Category { get; set; } = "";
        }

        public class Label
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }
    }
}
