using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public readonly struct AncestorNodeKinds
    {
        public ImmutableArray<SyntaxKind> Ancestors { get; }

        public AncestorNodeKinds(ImmutableArray<SyntaxKind> ancestorKinds)
        {
            Ancestors = ancestorKinds.IsDefault
                ? ImmutableArray<SyntaxKind>.Empty
                : ancestorKinds;
        }

        public bool Equals(AncestorNodeKinds other) =>
            Ancestors.SequenceEqual(other.Ancestors);

        public override bool Equals(object? obj) =>
            obj is AncestorNodeKinds other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var k in Ancestors)
                {
                    hash = (hash * 31) + (int)k;
                }
                return hash;
            }
        }

        public static bool operator ==(AncestorNodeKinds left, AncestorNodeKinds right) =>
            left.Equals(right);

        public static bool operator !=(AncestorNodeKinds left, AncestorNodeKinds right) =>
            !left.Equals(right);

        public bool HasAny(SyntaxKind kind) =>
            Ancestors.Contains(kind);
    }
}
