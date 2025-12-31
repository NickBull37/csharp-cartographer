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

    public class TestClass
    {
        private readonly IGenerateArtifactWorkflow? _generateArtifactWorkflow;

        private readonly IEnumerable<NavToken> Tokens = [];

        private readonly IEnumerable<NavToken>? TestTokens = [];

        private readonly IEnumerable<NavToken?> DemoTokens = [];

        private readonly IEnumerable<NavToken?>? FakeTokens = [];
    }
}
