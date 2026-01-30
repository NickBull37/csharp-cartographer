using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    public sealed record DefinitionEntry(string Definition);

    public static partial class DefinitionProvider
    {
        public static MapText? GetMapText(string key)
            => Definitions.Value.TryGetValue(key, out var mapText)
                ? mapText
                : null;

        private static readonly Lazy<IReadOnlyDictionary<string, MapText>> Definitions
            = new(LoadDefinitions);

        private static IReadOnlyDictionary<string, MapText> LoadDefinitions()
        {
            var assembly = typeof(DefinitionProvider).Assembly;

            var resourceNames = assembly
                .GetManifestResourceNames()
                .Where(n => n.EndsWith("-definitions.json", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (resourceNames.Length == 0)
                throw new InvalidOperationException(
                    "No embedded *-definitions.json files found. Ensure Build Action = Embedded Resource.");

            var merged = new Dictionary<string, MapText>(StringComparer.OrdinalIgnoreCase);

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

                var dict = JsonSerializer.Deserialize<Dictionary<string, DefinitionEntry>>(json, jsonOptions)
                    ?? new Dictionary<string, DefinitionEntry>(StringComparer.OrdinalIgnoreCase);

                foreach (var (key, entry) in dict)
                {
                    if (merged.ContainsKey(key))
                        throw new InvalidOperationException($"Duplicate definition key '{key}' in '{resourceName}'.");

                    var mapText = ParseMarkupToMapText(entry.Definition);
                    merged.Add(key, mapText);
                }
            }

            return merged;
        }

        private static MapText ParseMarkupToMapText(string markup)
        {
            var segments = new List<TextSegment>();
            if (string.IsNullOrEmpty(markup))
                return new MapText { ID = Guid.NewGuid(), Segments = segments };

            int lastIndex = 0;

            // {c:class1 class2}TEXT{/c}
            foreach (Match m in StyledSpanRegex().Matches(markup))
            {
                // add text before styled span
                if (m.Index > lastIndex)
                {
                    segments.Add(new TextSegment
                    {
                        Text = markup.Substring(lastIndex, m.Index - lastIndex),
                        Classes = []
                    });
                }

                var classString = m.Groups["classes"].Value;
                var innerText = m.Groups["text"].Value;

                var classes = classString
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                segments.Add(new TextSegment
                {
                    Text = innerText,
                    Classes = classes
                });

                lastIndex = m.Index + m.Length;
            }

            // trailing text
            if (lastIndex < markup.Length)
            {
                segments.Add(new TextSegment
                {
                    Text = markup.Substring(lastIndex),
                    Classes = []
                });
            }

            return new MapText
            {
                ID = Guid.NewGuid(),
                Segments = segments
            };
        }

        [GeneratedRegex(@"\{c:(?<classes>[^}]+)\}(?<text>.*?)\{\/c\}", RegexOptions.Singleline)]
        private static partial Regex StyledSpanRegex();
    }
}