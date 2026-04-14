namespace csharp_cartographer_backend._05.Services.Keys
{
    public sealed record DefinitionKey
    {
        public string Category { get; }

        public string Subject { get; }

        public IReadOnlyList<string> Qualifiers { get; }

        public DefinitionKey(
            string category,
            string subject,
            IReadOnlyList<string> qualifiers)
        {
            Category = category;
            Subject = subject;
            Qualifiers = qualifiers;
        }

        public override string ToString()
        {
            if (Qualifiers.Count == 0)
            {
                return $"{Category}:{Subject}";
            }

            var builder = new StringBuilder();
            builder.Append(Category);
            builder.Append(':');
            builder.Append(Subject);

            foreach (var qualifier in Qualifiers)
            {
                builder.Append(':');
                builder.Append(qualifier);
            }

            return builder.ToString();
        }
    }

    public sealed record DefinitionKeyShort(
        string Category,
        string Subject,
        IReadOnlyList<string> Qualifiers)
    {
        public override string ToString()
            => string.Join(":", new[] { Category, Subject }.Concat(Qualifiers));
    }
}
