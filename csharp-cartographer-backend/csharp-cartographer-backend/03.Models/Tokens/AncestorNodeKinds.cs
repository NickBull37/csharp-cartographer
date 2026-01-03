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

        public bool HasAny(SyntaxKind kind) =>
            Ancestors.Contains(kind);
    }
}
