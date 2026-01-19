namespace KeywordDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class KeywordShowcase
    {
        // dynamic
        private dynamic _dynamicValue = 42;

        // required (if this errors too, your C# is older than 11)
        public required string Name { get; init; }

        // nint / nuint (if these error, your C# is older than 9)
        private nint _nativeInt = 10;
        private nuint _nativeUInt = 20;

        // event with add / remove
        private EventHandler? _onChanged;
        public event EventHandler Changed
        {
            add { _onChanged += value; }
            remove { _onChanged -= value; }
        }

        public void Run()
        {
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

            // LINQ keywords:
            // from, join, on, equals, let, where, group, into, orderby, ascending, descending, select, by
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

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Key}: {item.Count}");
            }

            // and pattern keyword (if this errors, your C# is older than 9)
            if (_dynamicValue is > 10 and < 100)
            {
                Console.WriteLine("Value is between 10 and 100");
            }

            // unmanaged constraint (if this errors, your C# is older than 7.3)
            Console.WriteLine(EchoUnmanaged(123));
        }

        public T EchoUnmanaged<T>(T value) where T : unmanaged
        {
            return value;
        }

        // yield
        public IEnumerable<int> GetSequence()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
    }

    // with (if this errors, your C# is older than 9)
    public record Person(string FirstName, string LastName);

    public static class RecordDemo
    {
        public static void Run()
        {
            var p1 = new Person("Jane", "Doe");
            var p2 = p1 with { LastName = "Smith" };

            Console.WriteLine($"{p1} -> {p2}");
        }
    }
}
