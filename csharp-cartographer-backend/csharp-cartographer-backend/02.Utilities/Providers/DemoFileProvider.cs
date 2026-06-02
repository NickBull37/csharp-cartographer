using System.Text.Json;

namespace csharp_cartographer_backend._02.Utilities.Providers
{
    public sealed class DemoFile
    {
        public required string Name { get; init; }
        public required string Type { get; init; }
        public IEnumerable<string> Insights { get; init; } = [];
        public string? Description { get; init; }
        public required string RelativePath { get; init; }
    }

    public class DemoFileProvider
    {
        private const string EmbeddedJsonFile = "demo-file-data.json";
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private static readonly Lazy<IReadOnlyDictionary<string, DemoFile>> DemoFiles
            = new(LoadDemoFiles);

        public static IReadOnlyCollection<DemoFile> GetDemoFiles()
            => DemoFiles.Value.Values.ToArray();

        private static Dictionary<string, DemoFile> LoadDemoFiles()
        {
            var assembly = typeof(DemoFileProvider).Assembly;

            var resourceName = assembly
                .GetManifestResourceNames()
                .SingleOrDefault(file => file.EndsWith(EmbeddedJsonFile, StringComparison.OrdinalIgnoreCase));

            if (resourceName is null)
                throw new InvalidOperationException("No embedded demo-file-data.json file found. Ensure Build Action = Embedded Resource.");

            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Failed to open '{resourceName}'.");

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            return JsonSerializer.Deserialize<Dictionary<string, DemoFile>>(json, JsonOptions)
                ?? new Dictionary<string, DemoFile>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
