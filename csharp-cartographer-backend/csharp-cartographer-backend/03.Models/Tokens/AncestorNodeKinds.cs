using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public readonly struct AncestorNodeKinds
    {
        public SyntaxKind Parent { get; }

        public SyntaxKind GrandParent { get; }

        public SyntaxKind GreatGrandParent { get; }

        public SyntaxKind GreatGreatGrandParent { get; }

        public AncestorNodeKinds(
            SyntaxKind parent,
            SyntaxKind grandParent,
            SyntaxKind greatGrandParent,
            SyntaxKind greatGreatGrandParent)
        {
            Parent = parent;
            GrandParent = grandParent;
            GreatGrandParent = greatGrandParent;
            GreatGreatGrandParent = greatGreatGrandParent;
        }

        public bool HasAny(SyntaxKind kind) =>
            Parent == kind ||
            GrandParent == kind ||
            GreatGrandParent == kind ||
            GreatGreatGrandParent == kind;

        public override string ToString() =>
            $"Parent={Parent}, GrandParent={GrandParent}, GreatGrandParent={GreatGrandParent}, GreatGreatGrandParent={GreatGreatGrandParent}";
    }
}
