using csharp_cartographer_backend._03.Models.Shared;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace csharp_cartographer_backend._02.Utilities.Providers
{
    public sealed record DefinitionEntry(string Definition);

    public static partial class DefinitionProvider
    {
        [GeneratedRegex(@"\{c:(?<classes>[^}]+)\}(?<text>.*?)\{\/c\}", RegexOptions.Singleline)]
        private static partial Regex StyledSpanRegex();

        private const string LineBreakInsert = "<break/>";

        private static readonly Dictionary<string, string> ExtensionReplacements = new()
        {
            {"{HovExt}", "<break/>Hover your cusor over the {c:color-yellow bold}method{/c} name to see addition details like what the method will return or what types the provided arguments need to be." },
            {"{JumpExt}", "<break/>Put your cursor inside the identifier name in your IDE and hit {c:keyword}F12{/c} to jump to the identifier's definition."},
            {"{RefExt}", "<break/>Look for a {c:underline}references{/c} link above the declaration in your IDE to see everywhere it's currently being used."}
        };

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private static readonly Lazy<IReadOnlyDictionary<string, StyledText>> Definitions
            = new(LoadDefinitions);

        public static StyledText? GetStyledText(string key)
            => Definitions.Value.TryGetValue(key, out var styledText)
                ? styledText
                : null;

        private static Dictionary<string, StyledText> LoadDefinitions()
        {
            var assembly = typeof(DefinitionProvider).Assembly;

            var resourceNames = assembly
                .GetManifestResourceNames()
                .Where(n => n.EndsWith("-definitions.json", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (resourceNames.Length == 0)
                throw new InvalidOperationException(
                    "No embedded *-definitions.json files found. Ensure Build Action = Embedded Resource.");

            var merged = new Dictionary<string, StyledText>(StringComparer.OrdinalIgnoreCase);

            foreach (var resourceName in resourceNames)
            {
                using var stream = assembly.GetManifestResourceStream(resourceName)
                    ?? throw new InvalidOperationException($"Failed to open '{resourceName}'.");

                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                var dictionary = JsonSerializer.Deserialize<Dictionary<string, DefinitionEntry>>(json, JsonOptions)
                    ?? new Dictionary<string, DefinitionEntry>(StringComparer.OrdinalIgnoreCase);

                foreach (var (key, entry) in dictionary)
                {
                    if (merged.ContainsKey(key))
                        throw new InvalidOperationException($"Duplicate definition key '{key}' in '{resourceName}'.");

                    var styledText = ParseMarkupToStyledText(entry.Definition);
                    merged.Add(key, styledText);
                }
            }

            return merged;
        }

        private static StyledText ParseMarkupToStyledText(string markup)
        {
            if (string.IsNullOrWhiteSpace(markup))
                return StyledText.NotFound();

            InsertExtensions(markup);
            List<TextSegment> segments = [];

            int index = 0;
            foreach (Match match in StyledSpanRegex().Matches(markup))
            {
                // try adding plain text segment
                if (match.Index > index)
                {
                    AddSegment(
                        segments,
                        markup[index..match.Index],
                        [] // no classes for plain text
                    );
                }

                // add styled segment
                var cssString = match.Groups["classes"].Value;
                var innerText = match.Groups["text"].Value;

                var classes = cssString
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                AddSegment(
                    segments,
                    innerText,
                    classes
                );

                // skip to first char after match
                index = match.Index + match.Length;
            }

            // check for any remaining plain text after last match
            if (index < markup.Length)
            {
                AddSegment(
                    segments,
                    markup[index..],
                    []
                );
            }

            return new StyledText(segments);
        }

        private static void InsertExtensions(string markup)
        {
            if (string.IsNullOrWhiteSpace(markup))
                return;

            foreach (var entry in ExtensionReplacements)
            {
                markup = markup.Replace(
                    entry.Key,
                    entry.Value,
                    StringComparison.OrdinalIgnoreCase
                );
            }
        }

        private static void AddSegment(List<TextSegment> segments, string text, string[] classes)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            var parts = text.Split(LineBreakInsert, StringSplitOptions.None);

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Length > 0)
                {
                    var segment = new TextSegment()
                    {
                        Text = parts[i],
                        Classes = classes
                    };

                    segments.Add(segment);
                }

                // insert break between parts
                if (i < parts.Length - 1)
                {
                    segments.Add(TextSegment.LineBreak());
                }
            }
        }
    }
}
