namespace csharp_cartographer_backend._03.Models.Artifacts
{
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
