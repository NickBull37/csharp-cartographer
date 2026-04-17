using csharp_cartographer_backend._08.Controllers.Insights.Dtos;
using System.Text.Json;

namespace csharp_cartographer_backend._02.Utilities.Providers
{
    //public sealed class EmbeddedInsight
    //{
    //    public Guid ArtifactID { get; set; }
    //    public string Description { get; set; } = string.Empty;
    //    public IEnumerable<int> Highlights { get; set; } = [];
    //    public IEnumerable<CreateNoteDto> NoteDtos { get; set; } = [];
    //}

    public static partial class InsightProvider
    {
        private static readonly Lazy<IReadOnlyDictionary<string, CreateInsightDto>> Insights
            = new(LoadInsights);

        public static CreateInsightDto? GetInsight(string fileName)
            => Insights.Value.TryGetValue(fileName, out var insight)
                ? insight
                : null;

        private static IReadOnlyDictionary<string, CreateInsightDto> LoadInsights()
        {
            var assembly = typeof(InsightProvider).Assembly;

            var resourceNames = assembly
                .GetManifestResourceNames()
                .Where(n => n.EndsWith("-insights.json", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (resourceNames.Length == 0)
                throw new InvalidOperationException(
                    "No embedded *-insights.json files found. Ensure Build Action = Embedded Resource.");

            var merged = new Dictionary<string, CreateInsightDto>(StringComparer.OrdinalIgnoreCase);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            foreach (var resourceName in resourceNames)
            {
                using var stream = assembly.GetManifestResourceStream(resourceName)
                    ?? throw new InvalidOperationException($"Failed to open '{resourceName}'.");

                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                var dictionary = JsonSerializer.Deserialize<Dictionary<string, CreateInsightDto>>(json, jsonOptions)
                    ?? new Dictionary<string, CreateInsightDto>(StringComparer.OrdinalIgnoreCase);

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
