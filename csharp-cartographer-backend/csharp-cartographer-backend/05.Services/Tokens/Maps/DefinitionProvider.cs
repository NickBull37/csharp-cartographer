using System.Text.Json;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    public sealed record DefinitionEntry(string Definition);

    public sealed record FocusedDefinitionEntry(
        string Definition,
        string[]? Highlights = null
    );

    public static class DefinitionProvider
    {
        public static string? GetDefinition(string key)
            => Definitions.Value.TryGetValue(key, out var entry)
                ? entry.Definition
                : null;

        private static readonly Lazy<IReadOnlyDictionary<string, DefinitionEntry>> Definitions
            = new(LoadDefinitions);

        private static IReadOnlyDictionary<string, DefinitionEntry> LoadDefinitions()
        {
            var assembly = typeof(DefinitionProvider).Assembly;

            var resourceNames = assembly
                .GetManifestResourceNames()
                .Where(n => n.EndsWith("-definitions.json", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (resourceNames.Length == 0)
                throw new InvalidOperationException("No embedded *-definitions.json files found. Ensure Build Action = Embedded Resource.");

            var merged = new Dictionary<string, DefinitionEntry>(StringComparer.OrdinalIgnoreCase);

            foreach (var resourceName in resourceNames)
            {
                using var stream = assembly.GetManifestResourceStream(resourceName)
                    ?? throw new InvalidOperationException($"Failed to open '{resourceName}'.");

                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                var dict = JsonSerializer.Deserialize<Dictionary<string, DefinitionEntry>>(json)
                    ?? new Dictionary<string, DefinitionEntry>(StringComparer.OrdinalIgnoreCase);

                foreach (var kvp in dict)
                {
                    if (merged.ContainsKey(kvp.Key))
                        throw new InvalidOperationException($"Duplicate definition key '{kvp.Key}' in '{resourceName}'.");

                    merged.Add(kvp.Key, kvp.Value);
                }
            }

            return merged;
        }
    }
}
