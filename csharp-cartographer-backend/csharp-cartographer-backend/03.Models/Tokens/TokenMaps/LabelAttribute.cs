namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class LabelAttribute : Attribute
    {
        public string Label { get; }

        public LabelAttribute(string label)
        {
            Label = label;
        }
    }
}
