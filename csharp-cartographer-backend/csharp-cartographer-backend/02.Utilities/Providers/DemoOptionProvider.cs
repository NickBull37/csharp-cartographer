using System.Text.Json;

namespace csharp_cartographer_backend._02.Utilities.Providers
{
    public sealed class DemoOption
    {
        public required string Name { get; init; }
        public required string Type { get; init; }
        public IEnumerable<string> Insights { get; init; } = [];
        public string? Description { get; init; }
        public required string RelativePath { get; init; }
    }

    public class DemoOptionProvider
    {
        private const string EmbeddedJsonFile = "demo-file-data.json";
        private static readonly string _solutionRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private static readonly Lazy<IReadOnlyDictionary<string, DemoOption>> DemoOptions
            = new(LoadDemoOptions);

        public static IReadOnlyCollection<DemoOption> GetDemoOptions()
            => DemoOptions.Value.Values.ToArray();

        public static string GetDemoFilePath(string fileName)
        {
            var relativePath = DemoOptions.Value.Values
                .Where(option => option.Name == fileName)
                .Select(option => option.RelativePath)
                .Single();

            return Path.Combine(_solutionRoot, relativePath);
        }

        private static Dictionary<string, DemoOption> LoadDemoOptions()
        {
            var assembly = typeof(DemoOptionProvider).Assembly;

            var resourceName = assembly
                .GetManifestResourceNames()
                .SingleOrDefault(file => file.EndsWith(EmbeddedJsonFile, StringComparison.OrdinalIgnoreCase));

            if (resourceName is null)
                throw new InvalidOperationException("No embedded demo-file-data.json file found. Ensure Build Action = Embedded Resource.");

            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Failed to open '{resourceName}'.");

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            return JsonSerializer.Deserialize<Dictionary<string, DemoOption>>(json, JsonOptions)
                ?? new Dictionary<string, DemoOption>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
