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

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class FragmentAttribute : Attribute
    {
        public bool IsFragment { get; }

        public FragmentAttribute(bool isFragment)
        {
            IsFragment = isFragment;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UseLabelAsKeyAttribute : Attribute
    {
        public bool UseLabelAsKey { get; } = false;

        public UseLabelAsKeyAttribute(bool useLabelAsKey)
        {
            UseLabelAsKey = useLabelAsKey;
        }
    }
}
