using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public class TokenSemanticData
    {
        public ISymbol? Symbol { get; set; }

        public ImmutableArray<ISymbol> CandidateSymbols { get; set; } = [];

        public CandidateReason CandidateReason { get; set; }

        public string SymbolName { get; set; } = string.Empty;

        public SymbolKind SymbolKind { get; set; }

        public string? ContainingType { get; set; }

        public string? ContainingNamespace { get; set; }

        public string? TypeName { get; set; }

        public string? TypeKind { get; set; }

        public bool IsNullable { get; set; }
    }
}
