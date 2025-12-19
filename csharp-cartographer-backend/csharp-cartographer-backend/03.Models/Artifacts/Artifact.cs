using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._03.Models.Artifacts
{
    public class Artifact
    {
        public int ID { get; set; }

        public DateTime CreatedDate { get; init; }

        public string Language { get; } = "C#";

        public string ArtifactType { get; set; } = string.Empty;

        public int NumTokensAnalyzed { get; set; }

        public int NumLanguageElementTags { get; set; }

        public int NumAncestorsMapped { get; set; }

        public string TimeToGenerate { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<NavToken> NavTokens { get; set; } = [];

        public Artifact(string fileName, TimeSpan timeToGenerate, List<NavToken> navTokens)
        {
            CreatedDate = DateTime.Now;
            ArtifactType = GetArtifactType(fileName);
            NumTokensAnalyzed = navTokens.Count;
            NumLanguageElementTags = CountArtifactTags(navTokens);
            NumAncestorsMapped = CountAncestorsMapped(navTokens);
            TimeToGenerate = FormatTimeSpan(timeToGenerate);
            Title = fileName;
            NavTokens = navTokens;
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

        private static int CountArtifactTags(List<NavToken> navTokens)
        {
            var tagCount = 0;
            foreach (var token in navTokens)
            {
                foreach (var tag in token.Tags)
                {
                    tagCount++;
                }
            }
            return tagCount;
        }

        private static int CountAncestorsMapped(List<NavToken> navTokens)
        {
            var chartCount = 0;
            foreach (var token in navTokens)
            {
                foreach (var chart in token.Charts)
                {
                    chartCount++;
                }
            }
            return chartCount;
        }

        private static string FormatTimeSpan(TimeSpan timeToGenerate)
        {
            if (timeToGenerate.TotalSeconds < 1)
            {
                return $"{timeToGenerate.TotalMilliseconds:0} ms"; // Less than 1 second: show milliseconds
            }
            else if (timeToGenerate.TotalMinutes < 1)
            {
                return $"{timeToGenerate.Seconds}.{timeToGenerate.Milliseconds:000} seconds"; // Less than 1 minute: show seconds and milliseconds
            }
            else
            {
                return timeToGenerate.ToString(@"hh\:mm\:ss\.fff"); // Standard HH:MM:SS.MS format
            }
        }
    }
}
