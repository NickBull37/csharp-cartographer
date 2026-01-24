using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public sealed class TokenSemanticData
    {
        /// **************************************************
        /// |               Raw Roslyn objects               |
        /// **************************************************

        /// <summary>
        /// The "best" symbol for the node/token (after any alias unwrapping logic).
        /// Usually populated for identifier usages (IdentifierNameSyntax), member access (.Length),
        /// invocations, object creation, etc.
        ///
        /// Expected tokens:
        /// - Identifier tokens that refer to something: variables, fields, properties, methods, types, namespaces
        /// - Member access name tokens (the right side of `obj.Member`)
        /// - `this`, `base` sometimes (symbol can be type/member depending on context)
        ///
        /// Often null for:
        /// - pure punctuation tokens (., ;, (), {}, [], etc.)
        /// - many keywords (if, for, return) unless you bind a node that produces a symbol (rare)
        /// </summary>
        public ISymbol? Symbol { get; set; }

        /// <summary>
        /// The symbol declared *by* the syntax node, when the token is at a declaration site.
        /// Use this for fields/locals/parameters/methods/types declarations.
        ///
        /// Expected tokens:
        /// - Field declarations: `private readonly IFileProcessor _fileProcessor;` (the declarator)
        /// - Local declarations: `int x = 0;`
        /// - Parameter declarations: `void M(int x)`
        /// - Method / constructor / type / property declarations (depending on which node you pass)
        ///
        /// Usually null for:
        /// - references/usages like `_fileProcessor = ...;`
        /// - most expression nodes
        /// </summary>
        public ISymbol? DeclaredSymbol { get; set; }

        public bool IsDeclaredSymbol { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ISymbol? AliasTargetSymbol { get; set; }

        /// <summary>
        /// The IOperation tree node for this syntax node (Roslyn Operations API).
        /// Very useful for expression semantics because it's normalized (e.g., conversions are explicit).
        ///
        /// Expected tokens/nodes (depends heavily on what SyntaxNode you pass in):
        /// - Expressions: binary ops, invocations, member access, conditional access, assignments, literals, etc.
        /// - `?.`, `??`, `!`, casts, interpolated strings, etc. are often clearer here than in raw syntax.
        ///
        /// Often null for:
        /// - many declaration nodes (field declarations, type declarations)
        /// - trivia / punctuation tokens
        /// </summary>
        public IOperation? Operation { get; set; }

        /// <summary>
        /// The inferred type of the node (TypeInfo.Type).
        /// This is the "natural" type of an expression or type syntax.
        ///
        /// Expected tokens/nodes:
        /// - Expressions: `a + b`, `obj.Member`, `new Foo()`, `default`, literals
        /// - Type syntax nodes: `List<int>`, `int?`, `MyType`
        ///
        /// Often null for:
        /// - statements that don't have a type
        /// - punctuation/trivia
        /// </summary>
        public ITypeSymbol? TypeSymbol { get; set; }

        /// <summary>
        /// The type after implicit conversion (TypeInfo.ConvertedType).
        /// This is useful when an expression is converted to match a target type.
        ///
        /// Expected tokens/nodes:
        /// - Assignments: `string s = obj;` (obj converted to string? if possible)
        /// - Method argument conversions: passing `int` to a `long` parameter, etc.
        /// - Conditional operator / null-coalescing where one side is converted
        ///
        /// Often null when there is no meaningful conversion or the node is not an expression/type.
        /// </summary>
        public ITypeSymbol? ConvertedTypeSymbol { get; set; }



        /// **************************************************
        /// |                  Alias Support                 |
        /// **************************************************
        public bool IsAlias { get; set; }

        public string AliasName { get; set; } = string.Empty;

        public string? AliasTargetName { get; set; }



        /// **************************************************
        /// |                  Location Data                 |
        /// **************************************************

        /// <summary>
        /// True only when symbol is declared in the uploaded file’s syntax tree.
        /// </summary>
        public bool? IsInUploadedFile { get; set; }

        /// <summary>
        /// True for symbols declared in any source file in this compilation.
        /// </summary>
        public bool? IsInSourceCompilation { get; set; }

        /// <summary>
        /// True for symbols coming from referenced assemblies (metadata).
        /// </summary>
        public bool? IsInReferencedAssemblies { get; set; }

        /// <summary>
        /// The file path when symbol is declared in any source file in this compilation.
        /// </summary>
        public string? DeclaredInFilePath { get; set; }

        public string? ContainingNamespace { get; set; }

        public string? ContainingNamespaceFullyQualified { get; set; }

        public string? ContainingAssembly { get; set; }



        /// **************************************************
        /// |                  Symbol Data                   |
        /// **************************************************

        public string SymbolName { get; set; } = string.Empty;

        public SymbolKind SymbolKind { get; set; }

        public string? ContainingType { get; set; }

        public string? ContainingTypeFullyQualified { get; set; }



        /// **************************************************
        /// |             Symbol characteristics             |
        /// **************************************************

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


        /// <summary>
        /// True is the symbol is the original definition. False if the symbol is derived from another symbol.
        /// Helpful for methods/types.
        /// </summary>
        public bool? IsOriginalDefinition { get; set; }

        public bool? CanBeReferencedByName { get; set; }



        /// **************************************************
        /// |       Member-ish Details (when available)      |
        /// **************************************************

        public bool IsFieldSymbol { get; set; }

        public bool IsPropertySymbol { get; set; }

        public bool IsLocalSymbol { get; set; }

        public bool IsParameterSymbol { get; set; }

        public bool IsMethodSymbol { get; set; }

        public string MemberType { get; set; } = string.Empty;

        public string MemberTypeFullyQualified { get; set; } = string.Empty;

        public SymbolKind MemberTypeKind { get; set; }

        // Methods Only
        public MethodKind? MethodKind { get; set; }

        public string? MethodSignature { get; set; }

        public string? MethodSignatureFullyQualified { get; set; }

        public string? MethodSignatureErrorFormat { get; set; }

        public bool? IsGenericMethod { get; set; }

        public bool? IsExtensionMethod { get; set; }

        public ITypeSymbol? ReturnType { get; set; }

        public ImmutableArray<ITypeParameterSymbol>? TypeParameters { get; set; }



        /// **************************************************
        /// |                    Type Info                   |
        /// **************************************************
        /// 
        public TypeKind? TypeKind { get; set; }

        public TypeKind? ConvertedTypeKind { get; set; }



        /// **************************************************
        /// |             Operations / constants             |
        /// **************************************************

        public OperationKind OperationKind { get; set; }

        public string? OperationResultType { get; set; }

        public string? OperationResultTypeFullyQualified { get; set; }
    }
}
