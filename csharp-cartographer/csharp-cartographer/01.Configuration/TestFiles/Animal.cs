namespace csharp_cartographer._01.Configuration.TestFiles
{
    public class Animal
    {
        const string TEST_LITERAL = "Nick";

        const decimal PI = 3.14m;

        private float _testFloat = 0.09f;

        private string _type = string.Empty;

        public int Age { get; set; }

        public string? Name { get; set; }

        public Animal(string type)
        {
            _type = type;
        }

        public string GetAnimalType()
        {
            return _type;
        }

        public int GetAge()
        {
            return Age;
        }
    }
}
