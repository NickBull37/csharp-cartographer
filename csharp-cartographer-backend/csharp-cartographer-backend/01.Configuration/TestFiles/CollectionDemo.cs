using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace csharp_cartographer_backend._01.Configuration.TestFiles
{
    public class CollectionDemo
    {
        public void Demo()
        {
            // =======================
            // Non-generic collections
            // =======================
            ArrayList arrayList = new ArrayList { 1, 2, 3 };

            Hashtable hashtable = new Hashtable
            {
                ["one"] = 1,
                ["two"] = 2
            };

            Queue queue = new Queue();
            queue.Enqueue("a");
            queue.Enqueue("b");

            Stack stack = new Stack();
            stack.Push("x");
            stack.Push("y");

            SortedList sortedList = new SortedList
            {
                ["alpha"] = 1,
                ["beta"] = 2
            };

            BitArray bits = new BitArray(8);
            bits.Set(0, true);

            // Interfaces (non-generic)
            ICollection nonGenericCollection = arrayList;
            IList nonGenericList = arrayList;
            IDictionary nonGenericDictionary = hashtable;
            IEnumerable nonGenericEnumerable = arrayList;

            // ========================
            // Specialized collections
            // ========================
            NameValueCollection nameValueCollection = new NameValueCollection
            {
                ["Key1"] = "Value1",
                ["Key2"] = "Value2"
            };

            StringCollection stringCollection = new StringCollection();
            stringCollection.Add("Item1");
            stringCollection.Add("Item2");

            StringDictionary stringDictionary = new StringDictionary
            {
                ["A"] = "1",
                ["B"] = "2"
            };

            HybridDictionary hybridDictionary = new HybridDictionary
            {
                ["X"] = "100",
                ["Y"] = "200"
            };

            ListDictionary listDictionary = new ListDictionary
            {
                ["Key"] = "Value"
            };

            OrderedDictionary orderedDictionary = new OrderedDictionary
            {
                ["First"] = 1,
                ["Second"] = 2
            };

            // ==================
            // Generic collections
            // ==================
            List<int> list = new List<int> { 1, 2, 3 };

            Dictionary<string, int> dictionary = new Dictionary<string, int>
            {
                ["One"] = 1,
                ["Two"] = 2
            };

            HashSet<string> hashSet = new HashSet<string> { "A", "B", "C" };

            SortedSet<string> sortedSet = new SortedSet<string> { "B", "A", "C" };

            LinkedList<string> linkedList = new LinkedList<string>();
            linkedList.AddLast("First");
            linkedList.AddLast("Second");

            Queue<string> genericQueue = new Queue<string>();
            genericQueue.Enqueue("Q1");
            genericQueue.Enqueue("Q2");

            Stack<string> genericStack = new Stack<string>();
            genericStack.Push("S1");
            genericStack.Push("S2");

            SortedDictionary<string, int> sortedDictionary = new SortedDictionary<string, int>
            {
                ["Apple"] = 10,
                ["Banana"] = 20
            };

            SortedList<string, int> genericSortedList = new SortedList<string, int>
            {
                ["Key1"] = 1,
                ["Key2"] = 2
            };

            // Generic interfaces
            IReadOnlyList<int> readOnlyList = list;
            IReadOnlyCollection<int> readOnlyCollection = list;
            ICollection<int> genericCollection = list;
            IList<int> genericListInterface = list;
            IDictionary<string, int> genericDictionaryInterface = dictionary;
            ISet<string> setInterface = hashSet;
            IEnumerable<int> genericEnumerable = list;

            // =========================
            // ObjectModel collections
            // =========================
            Collection<int> collection = new Collection<int> { 10, 20, 30 };

            ObservableCollection<string> observableCollection = new ObservableCollection<string>
            {
                "Obs1", "Obs2"
            };

            ReadOnlyCollection<int> readOnlyCollectionWrapper = new ReadOnlyCollection<int>(list);
            ReadOnlyDictionary<string, int> readOnlyDictionary =
            new ReadOnlyDictionary<string, int>(dictionary);

            MyKeyedCollection keyedCollection = new MyKeyedCollection();
            keyedCollection.Add("ItemA");
            keyedCollection.Add("ItemB");

            // =========================
            // Concurrent collections
            // =========================
            ConcurrentDictionary<string, int> concurrentDictionary = new ConcurrentDictionary<string, int>();
            concurrentDictionary.TryAdd("C1", 100);

            ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();
            concurrentQueue.Enqueue(1);
            concurrentQueue.Enqueue(2);

            ConcurrentStack<int> concurrentStack = new ConcurrentStack<int>();
            concurrentStack.Push(10);
            concurrentStack.Push(20);

            ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();
            concurrentBag.Add(5);
            concurrentBag.Add(15);

            BlockingCollection<int> blockingCollection = new BlockingCollection<int>();
            blockingCollection.Add(42);
            blockingCollection.CompleteAdding();

            // Just a couple of "reads" to keep things obviously used.
            Console.WriteLine(list[0]);
            Console.WriteLine(dictionary["One"]);
            Console.WriteLine(nameValueCollection["Key1"]);
            Console.WriteLine(concurrentDictionary["C1"]);
        }
    }

    // Example KeyedCollection implementation
    public class MyKeyedCollection : KeyedCollection<string, string>
    {
        protected override string GetKeyForItem(string item) => item;
    }
}
