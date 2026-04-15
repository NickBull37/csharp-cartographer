using csharp_cartographer_backend._03.Models.Insights;
using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._03.Models.Artifacts
{
    public sealed class Artifact
    {
        public Guid ID { get; } = Guid.NewGuid();

        public DateTime CreatedDate { get; } = DateTime.Now;

        public string Language { get; } = "C#";

        public string ArtifactType { get; }

        public string FileName { get; }

        public int TokenCount { get; }

        public int AncestorCount { get; }

        public List<NavToken> NavTokens { get; }

        public ArtifactTimes Timings { get; }

        public string? Description { get; set; }

        public IEnumerable<Insight> Insights { get; set; } = [];

        public Artifact(
            string fileName,
            List<NavToken> navTokens,
            ArtifactTimes timings)
        {
            ArtifactType = GetArtifactType(fileName);
            TokenCount = navTokens.Count;
            AncestorCount = CountAncestorsMapped(navTokens);
            FileName = fileName;
            NavTokens = navTokens;
            Timings = timings;
        }

        private static string GetArtifactType(string fileName)
        {
            return fileName switch
            {
                "NavToken.cs" => "Model Definition",
                "GenerateArtifactWorkflow.cs" => "Workflow Class",
                "SyntaxHighlighter.cs" => "Service Class",
                "ArtifactRepository.cs" => "DataAccess Class",
                "ArtifactController.cs" => "API Controller",
                "StringHelpers.cs" => "Extension/Helper Class",
                "ChatGptProvider.cs" => "External Client",
                "GenerateArtifactDto.cs" => "Data Transfer Object (DTO)",
                _ => "User uploaded file",
            };
        }

        private static int CountAncestorsMapped(IEnumerable<NavToken> navTokens)
        {
            int count = 0;
            foreach (var token in navTokens)
            {
                count += token.Charts.Count;
            }
            return count;
        }
    }

    public sealed class ArtifactTimes
    {
        public string TokenGenerationTime { get; }
        public string ChartGenerationTime { get; }
        public string MappingTime { get; }
        public string HighlightTime { get; }
        public string TotalTime { get; }

        public ArtifactTimes(
            TimeSpan tokenGenerationTime,
            TimeSpan chartGenerationTime,
            TimeSpan mappingTime,
            TimeSpan highlightTime,
            TimeSpan totalTime)
        {
            TokenGenerationTime = FormatTimeSpan(tokenGenerationTime);
            ChartGenerationTime = FormatTimeSpan(chartGenerationTime);
            MappingTime = FormatTimeSpan(mappingTime);
            HighlightTime = FormatTimeSpan(highlightTime);
            TotalTime = FormatTimeSpan(totalTime);
        }

        private static string FormatTimeSpan(TimeSpan time)
        {
            if (time.TotalSeconds < 1)
            {
                return $"{time.TotalMilliseconds:0}ms";
            }

            if (time.TotalMinutes < 1)
            {
                return $"{time.TotalSeconds:0.000}s";
            }

            return time.ToString(@"hh\:mm\:ss\.fff");
        }
    }
}
