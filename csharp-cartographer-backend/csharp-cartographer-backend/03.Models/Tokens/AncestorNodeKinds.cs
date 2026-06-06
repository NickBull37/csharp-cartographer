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

        public bool HasAncestorAt(int index, SyntaxKind kind)
        {
            return index < Ancestors.Length
                && Ancestors[index] == kind;
        }

        public bool HasAncestor(SyntaxKind kind) => Ancestors.Contains(kind);

        public SyntaxKind? GetParent()
        {
            if (Ancestors.Length == 0)
                return null;

            return Ancestors[0];
        }

        public SyntaxKind? GetGrandParent()
        {
            if (Ancestors.Length <= 1)
                return null;

            return Ancestors[1];
        }

        public SyntaxKind? GetGreatGrandParent()
        {
            if (Ancestors.Length <= 2)
                return null;

            return Ancestors[2];
        }

        public SyntaxKind? GetLastAncestor()
        {
            if (Ancestors.Length == 0)
                return null;

            return Ancestors[^1];
        }

        public SyntaxKind? GetSecondToLastAncestor()
        {
            if (Ancestors.Length < 2)
                return null;

            return Ancestors[^2];
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

        // Testing operators

        public static bool operator !=(AncestorNodeKinds left, AncestorNodeKinds right) =>
            !left.Equals(right);

        public static explicit operator SyntaxKind[](AncestorNodeKinds value) =>
            value.Ancestors.ToArray();

        public static implicit operator AncestorNodeKinds(ImmutableArray<SyntaxKind> ancestorKinds) =>
            new(ancestorKinds);


    }
}
