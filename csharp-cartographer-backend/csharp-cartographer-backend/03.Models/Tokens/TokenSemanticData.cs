using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public sealed class TokenSemanticData
    {
        /// ******************************************************************
        /// |                       Raw Roslyn objects                       |
        /// ******************************************************************

        public ISymbol? Symbol { get; set; }

        public bool IsNamespaceSymbol => Symbol is INamespaceSymbol;

        public ISymbol? DeclaredSymbol { get; set; }

        public bool IsDeclaredSymbol { get; set; } = false;


        /// ******************************************************************
        /// |                          Location Data                         |
        /// ******************************************************************

        public bool IsInUploadedFile { get; set; } = false;

        public bool IsInSourceCompilation { get; set; } = false;

        public bool IsInReferencedAssemblies { get; set; } = false;

        public string? ContainingNamespace { get; set; }

        public string? ContainingAssembly { get; set; }


        /// ******************************************************************
        /// |                          Alias Support                         |
        /// ******************************************************************

        public IAliasSymbol? AliasSymbol { get; set; }

        public ISymbol? AliasTargetSymbol { get; set; }

        public bool IsAliasSymbol { get; set; } = false;

        public bool IsAliasTargetSymbol { get; set; } = false;

        public string? AliasName { get; set; }

        public string? AliasTargetName { get; set; }

        /// ******************************************************************
        /// |                           Type Support                            |
        /// ******************************************************************

        public ITypeSymbol? TypeSymbol { get; set; }

        public ITypeSymbol? ConvertedTypeSymbol { get; set; }

        public TypeKind? TypeKind { get; set; }

        public TypeKind? ConvertedTypeKind { get; set; }

        public bool IsTypeSymbol => TypeSymbol is not null;

        public bool IsNamedTypeSymbol => TypeSymbol is INamedTypeSymbol;

        public bool? IsConvertedTypeSymbol { get; set; }


        /// ******************************************************************
        /// |                          Symbol Data                           |
        /// ******************************************************************

        public string? SymbolName { get; set; }

        public SymbolKind? SymbolKind { get; set; }

        public string? ContainingType { get; set; }

        public ImmutableArray<SymbolDisplayPart> DisplayParts { get; set; } = [];


        /// ******************************************************************
        /// |                     Symbol characteristics                     |
        /// ******************************************************************

        public Accessibility? Accessibility { get; set; }

        public bool? IsAbstract { get; set; }

        public bool? IsAsync { get; set; }

        public bool? IsConst { get; set; }

        public bool? IsDiscard { get; set; }

        public bool? IsForEachVar { get; set; }

        public bool? IsIndexer { get; set; }

        public bool? IsOptional { get; set; }

        public bool? IsOverride { get; set; }

        public bool? IsSealed { get; set; }

        public bool? IsReadOnly { get; set; }

        public bool? IsRequired { get; set; }

        public bool? IsStatic { get; set; }

        public bool? IsUsingVar { get; set; }

        public bool? IsVirtual { get; set; }

        public bool? IsVolatile { get; set; }

        public bool? IsWriteOnly { get; set; }

        public bool? IsImplicitlyDeclared { get; set; }

        public bool? IsExtern { get; set; }

        public bool? IsExplicitlyNamedTupleElement { get; set; }

        public bool? IsOriginalDefinition { get; set; }


        /// ******************************************************************
        /// |               Member-ish Details (when available)              |
        /// ******************************************************************

        public bool IsFieldSymbol { get; set; }

        public bool IsPropertySymbol { get; set; }

        public bool IsLocalSymbol { get; set; }

        public bool IsParameterSymbol { get; set; }

        public bool IsMethodSymbol { get; set; }

        public string? MemberType { get; set; }

        public SymbolKind MemberTypeKind { get; set; }

        public MethodKind? MethodKind { get; set; }

        public string? MethodSignature { get; set; }

        public string? MethodSignatureFullyQualified { get; set; }

        public bool? IsGenericMethod { get; set; }

        public bool? IsExtensionMethod { get; set; }

        public ITypeSymbol? ReturnType { get; set; }

        public ImmutableArray<ITypeParameterSymbol>? TypeParameters { get; set; }


        /// ******************************************************************
        /// |                           Operations                           |
        /// ******************************************************************

        public IOperation? Operation { get; set; }

        public bool IsOperation { get; set; } = false;

        public OperationKind OperationKind { get; set; }

        public string? OperationResultType { get; set; }
    }
}
