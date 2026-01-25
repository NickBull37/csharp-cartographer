using Handler = System.Action<int>;

namespace DelegateAndEventDemo
{
    // 1. Define a delegate
    // This defines the *shape* of methods that can handle the event
    public delegate void TemperatureChangedHandler(int newTemperature);

    // 2. Publisher class
    public class Thermostat
    {
        private int _temperature;

        // 3. Define an event based on the delegate
        public event TemperatureChangedHandler? TemperatureChanged;

        public event TemperatureChangedHandler TemperatureChangedAgain;

        public event Action<int> TemperatureChangedMore;

        public event Func<int, (int min, int max)> RangeRequested;

        public int Temperature
        {
            get => _temperature;
            set
            {
                if (_temperature != value)
                {
                    _temperature = value;

                    // 4. Raise the event
                    OnTemperatureChanged(_temperature);
                }
            }
        }

        protected virtual void OnTemperatureChanged(int newTemperature)
        {
            // Invoke the event safely
            TemperatureChanged?.Invoke(newTemperature);
        }
    }

    // 5. Subscriber class
    public class Display
    {
        public void ShowTemperature(int temperature)
        {
            Console.WriteLine($"Temperature is now {temperature}°");
        }
    }

    class DemoProgram
    {
        static void Main()
        {
            var thermostat = new Thermostat();
            var display = new Display();

            // 6. Subscribe to the event
            thermostat.TemperatureChanged += display.ShowTemperature;

            thermostat.Temperature = 72;
            thermostat.Temperature = 75;
            thermostat.Temperature = 75; // No event (no change)
            thermostat.Temperature = 68;

            // 7. Unsubscribe
            thermostat.TemperatureChanged -= display.ShowTemperature;
        }
    }

    public class BuiltInDelegates
    {
        public event Action TemperatureChanged;

        public event Action<int> TemperatureChangedWithValue;

        public event Func<int, int> ValueTransformed;

        public event Func<int, (int min, int max)> RangeRequested;
    }

    namespace MyApp.Events
    {
        public delegate void TemperatureChangedHandler(int value);

        public delegate bool Predicate(string value);

        public delegate AliasExample PredicateTest(string value);
    }

    public class FullyQualifiedExample
    {
        public event MyApp.Events.TemperatureChangedHandler TemperatureChanged;
    }

    public class AliasExample
    {
        public event Handler TemperatureChanged;
    }
}
