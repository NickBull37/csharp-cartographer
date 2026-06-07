using System.Text.Json;

namespace csharp_cartographer_backend._02.Utilities.Providers
{
    public sealed class EmbeddedInsight
    {
        public required Guid ArtifactID { get; init; }
        public required string Label { get; init; }
        public required string Description { get; init; }
        public IEnumerable<int> Highlights { get; init; } = [];
        public IEnumerable<EmbeddedNote> Notes { get; init; } = [];
    }

    public sealed class EmbeddedNote
    {
        public required string Label { get; init; }
        public required string Text { get; init; }
        public IEnumerable<int> Highlights { get; init; } = [];
    }

    public static partial class InsightProvider
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private static readonly Lazy<IReadOnlyDictionary<string, EmbeddedInsight>> Insights
            = new(LoadInsights);

        public static void LoadAllInsights() => _ = Insights.Value;

        public static EmbeddedInsight? GetEmbeddedInsight(string fileName) =>
            Insights.Value.TryGetValue(fileName, out var insight)
                ? insight
                : null;

        private static Dictionary<string, EmbeddedInsight> LoadInsights()
        {
            var assembly = typeof(InsightProvider).Assembly;

            var resourceNames = assembly
                .GetManifestResourceNames()
                .Where(n => n.EndsWith("-insights.json", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (resourceNames.Length == 0)
                throw new InvalidOperationException(
                    "No embedded *-insights.json files found. Ensure Build Action = Embedded Resource.");

            var merged = new Dictionary<string, EmbeddedInsight>(StringComparer.OrdinalIgnoreCase);

            foreach (var resourceName in resourceNames)
            {
                using var stream = assembly.GetManifestResourceStream(resourceName)
                    ?? throw new InvalidOperationException($"Failed to open '{resourceName}'.");

                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                var dictionary = JsonSerializer.Deserialize<Dictionary<string, EmbeddedInsight>>(json, JsonOptions)
                    ?? new Dictionary<string, EmbeddedInsight>(StringComparer.OrdinalIgnoreCase);

                foreach (var (key, insight) in dictionary)
                {
                    if (merged.ContainsKey(key))
                        throw new InvalidOperationException($"Duplicate insight key '{key}' in '{resourceName}'.");

                    merged.Add(key, insight);
                }
            }

            return merged;
        }
    }
}
