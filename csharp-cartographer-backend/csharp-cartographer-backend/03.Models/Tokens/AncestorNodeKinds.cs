using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public readonly struct AncestorNodeKinds
    {
        public ImmutableArray<SyntaxKind> Ancestors { get; }

        //public AncestorNodeKinds(ImmutableArray<SyntaxKind> ancestorKinds)
        //{
        //    Ancestors = ancestorKinds.IsDefault
        //        ? ImmutableArray<SyntaxKind>.Empty
        //        : ancestorKinds;
        //}

        public AncestorNodeKinds(SyntaxToken roslynToken)
        {
            var builder = ImmutableArray.CreateBuilder<SyntaxKind>();
            SyntaxNode? currentNode = roslynToken.Parent;

            while (currentNode is not null)
            {
                if (!currentNode.IsKind(SyntaxKind.CompilationUnit))
                {
                    builder.Add(currentNode.Kind());
                }
                currentNode = currentNode.Parent;
            }

            var array = builder.ToImmutable();

            Ancestors = array.IsDefault
                ? ImmutableArray<SyntaxKind>.Empty
                : array;
        }

        public bool HasAncestorAt(int index, SyntaxKind kind) =>
            index < Ancestors.Length && Ancestors[index] == kind;

        public bool HasAncestor(SyntaxKind kind) =>
            Ancestors.Contains(kind);

        public SyntaxKind? GetParent() => Ancestors.Length > 0
            ? Ancestors[0]
            : null;

        public SyntaxKind? GetGrandParent() => Ancestors.Length > 1
            ? Ancestors[1]
            : null;

        public SyntaxKind? GetGreatGrandParent() => Ancestors.Length > 2
            ? Ancestors[2]
            : null;

        public SyntaxKind? GetLastAncestor() => Ancestors.Length > 0
            ? Ancestors[^1]
            : null;

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

        public static explicit operator SyntaxKind[](AncestorNodeKinds value) =>
            value.Ancestors.ToArray();

        public static implicit operator AncestorNodeKinds(SyntaxToken roslynToken) =>
            new(roslynToken);
    }
}
