using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._01.Configuration.TestFiles
{
    public class Animal
    {
        const string TEST_LITERAL = "Nick";

        const decimal PI = 3.14m;

        private float _testFloat = 0.09f;

        private string _type = string.Empty;

        /// <summary>The age of the animal.</summary>
        public int Age { get; set; }

        public List<string> Names { get; set; }

        public List<int> Numbers { get; set; }

        public IEnumerable<int> Values { get; set; } = Enumerable.Empty<int>();

        public List<NavToken> Tokens { get; set; }

        public string? Name { get; set; }

        public Animal(string type)
        {
            _type = type;
        }

        /// <summary>
        ///     Gets the animal type.
        /// </summary>
        /// <returns>A string.</returns>
        public string GetAnimalType()
        {
            return _type;
        }

        public int GetAge()
        {
            return Age;
        }

        public bool IsOlderThan10()
        {
            if (Age > 10)
            {
                return true;
            }
            return false;
        }

        public bool IsYoungerThan10()
        {
            if (Age < 10)
            {
                return true;
            }
            return false;
        }

        public void SetName()
        {
            Name = TEST_LITERAL;
        }
    }

    public class TestClass
    {
        public void DoSomething()
        {
            Animal animal = new("testType")
            {
                Name = "Holly"
            };

            if (animal.Name == "Johnny")
            {

            }
        }
    }
}
