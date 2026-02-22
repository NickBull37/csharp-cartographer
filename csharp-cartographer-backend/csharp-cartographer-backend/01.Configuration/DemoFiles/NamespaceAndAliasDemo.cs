// Static import (namespace/member access variation)
using static System.Console;

// Namespace alias & type alias
using Handler = System.Action<int>;
using IO = System.IO;
using MyToken = csharp_cartographer_backend._03.Models.Tokens;
using SB = System.Text.StringBuilder;

// Generic type alias (2 gen args)
using StringIntDict = System.Collections.Generic.Dictionary<string, int>;

namespace Testing_Multiple_Namespaces_In_One_File
{
    public class TestClass
    {

    }
}

namespace csharp_cartographer_backend._01.Configuration.DemoFiles
{
    public class NamespaceAndAliasDemo
    {
        public void Demo()
        {
            // ------------------------------------------------------------
            // Base namespace qualification: System.* vs using static
            // ------------------------------------------------------------
            System.Console.WriteLine("System.Console.WriteLine");
            WriteLine("using static System.Console; WriteLine"); // uses static import


            // ------------------------------------------------------------
            // Global Alias: global:: + ::
            // ------------------------------------------------------------
            // resolves ambiguity with other Console classes
            global::System.Console.WriteLine(global::System.DateTime.Now);
            global::System.Console.WriteLine(global::System.String.Empty);


            // ------------------------------------------------------------
            // Type alias: Handler, SB
            // ------------------------------------------------------------
            Handler handler = number => WriteLine($"Handler called: {number}");
            SB stringBuilder = new SB();


            // ------------------------------------------------------------
            // Namespace Alias: MyToken, IO
            // ------------------------------------------------------------
            using var wr = new StreamWriter("demo.txt");
            using StreamWriter wrt = new StreamWriter("demo.txt");
            using StreamWriter? wrt2 = new StreamWriter("demo.txt");

            using (var writer = new IO.StreamWriter("demo.txt"))    // nested alias
            {
                writer.WriteLine("Hello from IO.StreamWriter");
            }
            using (StreamWriter writer = new IO.StreamWriter("demo.txt"))    // nested alias
            {
                writer.WriteLine("Hello from IO.StreamWriter");
            }
            using (StreamWriter? writer = new IO.StreamWriter("demo.txt"))    // nested alias
            {
                writer.WriteLine("Hello from IO.StreamWriter");
            }
            var token = new MyToken.NavToken();                     // internal
            bool fileExists = IO.File.Exists("demo.txt");           // external


            // ------------------------------------------------------------
            // Generic Type Alias
            // ------------------------------------------------------------
            StringIntDict dict = new()
            {
                ["a"] = 1,
                ["b"] = 2
            };


            // ------------------------------------------------------------
            // Side-by-Side: IO.File, System.IO.File
            // ------------------------------------------------------------
            bool fromAlias = IO.File.Exists("demo.txt");
            bool fullyQualified = System.IO.File.Exists("demo.txt");
        }
    }
}