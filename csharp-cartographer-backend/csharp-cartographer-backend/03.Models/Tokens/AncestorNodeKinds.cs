namespace csharp_cartographer_backend._03.Models.Tokens
{
    public readonly struct AncestorNodeKinds
    {
        public string? Parent { get; }

        public string? GrandParent { get; }

        public string? GreatGrandParent { get; }

        public string? GreatGreatGrandParent { get; }

        public AncestorNodeKinds(
            string? parent,
            string? grandParent,
            string? greatGrandParent,
            string? greatGreatGrandParent)
        {
            Parent = parent;
            GrandParent = grandParent;
            GreatGrandParent = greatGrandParent;
            GreatGreatGrandParent = greatGreatGrandParent;
        }

        public bool HasAny(string kind) =>
            Parent == kind ||
            GrandParent == kind ||
            GreatGrandParent == kind ||
            GreatGreatGrandParent == kind;

        public override string ToString() =>
            $"Parent={Parent}, GrandParent={GrandParent}, GreatGrandParent={GreatGrandParent}, GreatGreatGrandParent={GreatGreatGrandParent}";
    }

    public readonly record struct NodeKindChain(
        string? Parent,
        string? GrandParent,
        string? GreatGrandParent,
        string? GreatGreatGrandParent)
    {
        public bool HasAny(string kind) =>
            Parent == kind ||
            GrandParent == kind ||
            GreatGrandParent == kind ||
            GreatGreatGrandParent == kind;
    }
}
