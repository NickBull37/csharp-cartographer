using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public sealed class TokenSemanticData
    {
        /// ******************************************************************
        /// |                       Raw Roslyn objects                       |
        /// ******************************************************************

        /// <summary>
        /// The "best" bound symbol for the node/token, after any alias unwrapping logic.
        /// Usually populated for identifier usages (IdentifierNameSyntax), member access (.Length),
        /// invocations, object creation, etc.
        ///
        /// Expected tokens:
        /// - Identifier tokens that refer to something: variables, fields, properties, methods, types, namespaces
        /// - Member access name tokens (the right side of <c>obj.Member</c>)
        ///
        /// Source:
        /// - Derived from <see cref="SemanticModel.GetSymbolInfo(SyntaxNode)"/> (and then refined).
        /// </summary>
        public ISymbol? Symbol { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public INamespaceOrTypeSymbol? SymbolUnwrapped { get; set; }

        /// <summary>
        /// The symbol declared by the syntax node when the token is at a declaration site.
        ///
        /// Expected tokens:
        /// - Field declarations, local declarations, parameter declarations
        /// - Method / constructor / type / property declarations (depending on which node is passed)
        ///
        /// Source:
        /// - Derived from <see cref="SemanticModel.GetDeclaredSymbol(SyntaxNode)"/>.
        /// </summary>
        public ISymbol? DeclaredSymbol { get; set; }

        /// <summary>
        /// True when <see cref="DeclaredSymbol"/> is the authoritative symbol for this token/node
        /// (i.e., the token is considered a declaration site rather than a reference).
        /// </summary>
        public bool IsDeclaredSymbol { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public IAliasSymbol? AliasSymbol { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAliasSymbol { get; set; } = false;

        /// <summary>
        /// The unwrapped target symbol when the bound symbol is an alias.
        /// For example:
        /// <code>
        /// using X = System.Collections.Generic.List&lt;int&gt;;
        /// X values = new();
        /// </code>
        /// The symbol for <c>X</c> may bind as an alias; this property stores the resolved target.
        ///
        /// Source:
        /// - Typically obtained by detecting <see cref="IAliasSymbol"/> and using its <c>Target</c>.
        /// </summary>
        public ISymbol? AliasTargetSymbol { get; set; }

        /// <summary>
        /// True when <see cref="AliasTargetSymbol"/> is present and represents the "real" symbol to treat
        /// as the semantic target (rather than the alias symbol itself).
        /// </summary>
        public bool IsAliasTargetSymbol { get; set; } = false;

        /// <summary>
        /// The inferred type of the node (TypeInfo.Type). This is the "natural" type of an expression
        /// or the type represented by a type syntax node.
        ///
        /// Expected tokens/nodes:
        /// - Expressions: <c>a + b</c>, <c>obj.Member</c>, <c>new Foo()</c>, <c>default</c>, literals
        /// - Type syntax nodes: <c>List&lt;int&gt;</c>, <c>int?</c>, <c>MyType</c>
        ///
        /// Source:
        /// - Derived from <see cref="SemanticModel.GetTypeInfo(SyntaxNode)"/>.
        /// </summary>
        public ITypeSymbol? TypeSymbol { get; set; }

        /// <summary>
        /// True when <see cref="TypeSymbol"/> was successfully computed for this token/node and should be used.
        /// </summary>
        public bool IsTypeSymbol { get; set; } = false;

        /// <summary>
        /// The type after implicit conversion (TypeInfo.ConvertedType).
        /// Useful when an expression is converted to match a target type.
        ///
        /// Expected tokens/nodes:
        /// - Assignments: <c>long x = 1;</c> (int converted to long)
        /// - Argument conversions: passing <c>int</c> to a <c>long</c> parameter
        /// - Conditional operator / null-coalescing where one side is converted
        ///
        /// Source:
        /// - Derived from <see cref="SemanticModel.GetTypeInfo(SyntaxNode)"/>.
        /// </summary>
        public ITypeSymbol? ConvertedTypeSymbol { get; set; }

        /// <summary>
        /// True when <see cref="ConvertedTypeSymbol"/> is present and differs from the natural type,
        /// indicating an implicit conversion was applied.
        /// </summary>
        public bool IsConvertedTypeSymbol { get; set; } = false;

        /// <summary>
        /// The operation node produced by Roslyn's Operations API for the provided syntax node.
        /// This is often easier to reason about than raw syntax because conversions and lowered forms
        /// are explicit.
        ///
        /// Expected tokens/nodes (depends on the SyntaxNode you pass in):
        /// - Expressions: binary ops, invocations, member access, conditional access, assignments, literals, etc.
        /// - Operators such as <c>?.</c>, <c>??</c>, <c>!</c>, casts, interpolated strings, etc.
        ///
        /// Source:
        /// - Derived from <see cref="SemanticModel.GetOperation(SyntaxNode, System.Threading.CancellationToken)"/>.
        /// </summary>
        public IOperation? Operation { get; set; }

        /// <summary>
        /// True when <see cref="Operation"/> is present and should be used as the semantic representation
        /// of the node.
        /// </summary>
        public bool IsOperation { get; set; } = false;


        /// ******************************************************************
        /// |                          Location Data                         |
        /// ******************************************************************

        /// <summary>
        /// True only when symbol is declared in the uploaded file’s syntax tree.
        /// </summary>
        public bool IsInUploadedFile { get; set; } = false;

        /// <summary>
        /// True when the symbol is declared in any source file in the current compilation
        /// (including the uploaded file and any additional source files included in the compilation).
        /// </summary>
        public bool IsInSourceCompilation { get; set; } = false;

        /// <summary>
        /// True when the symbol comes from referenced assemblies (metadata), such as BCL/framework types
        /// and third-party libraries.
        /// </summary>
        public bool IsInReferencedAssemblies { get; set; } = false;

        /// <summary>
        /// The name of the namespace that contains the symbol.
        /// For global namespace, this may be null or empty depending on how it is populated.
        /// </summary>
        public string? ContainingNamespace { get; set; }

        /// <summary>
        /// The name of the assembly that contains the symbol (e.g., "System.Private.CoreLib").
        /// </summary>
        public string? ContainingAssembly { get; set; }


        /// ******************************************************************
        /// |                          Alias Support                         |
        /// ******************************************************************

        /// <summary>
        /// True when the bound symbol represents a C# alias.
        /// </summary>
        public bool IsAlias { get; set; }

        /// <summary>
        /// The alias identifier text (e.g., "X" in <c>using X = System.Text.StringBuilder;</c>).
        /// Empty when <see cref="IsAlias"/> is false.
        /// </summary>
        public string AliasName { get; set; } = string.Empty;

        /// <summary>
        /// A display-friendly name for the alias target (e.g., "System.Text.StringBuilder").
        /// Null when <see cref="IsAlias"/> is false or when the target cannot be determined.
        /// </summary>
        public string? AliasTargetName { get; set; }


        /// ******************************************************************
        /// |                          Symbol Data                           |
        /// ******************************************************************

        /// <summary>
        /// The simple name of the selected symbol (e.g., "Length", "Console", "WriteLine").
        /// Empty when no symbol is available.
        /// </summary>
        public string SymbolName { get; set; } = string.Empty;

        /// <summary>
        /// The Roslyn <see cref="Microsoft.CodeAnalysis.SymbolKind"/> for the selected symbol.
        /// Useful for high-level categorization (method/field/property/type/namespace/etc.).
        /// </summary>
        public SymbolKind? SymbolKind { get; set; }

        /// <summary>
        /// The containing type name (simple display) when the symbol is a member
        /// (e.g., "String" for <c>string.Length</c>).
        /// </summary>
        public string? ContainingType { get; set; }

        /// <summary>
        /// The display parts for the symbol, as produced by Roslyn's symbol display APIs.
        /// Useful for building rich UI output with classification (keywords, punctuation, type names, etc.).
        /// </summary>
        public ImmutableArray<SymbolDisplayPart> DisplayParts { get; set; } = [];


        /// ******************************************************************
        /// |                     Symbol characteristics                     |
        /// ******************************************************************

        /// <summary>
        /// The declared accessibility of the symbol when applicable (Public/Private/Protected/etc.).
        /// </summary>
        public Accessibility? Accessibility { get; set; }

        /// <summary>
        /// True when the symbol is abstract, when applicable (types/methods/properties).
        /// </summary>
        public bool? IsAbstract { get; set; }

        /// <summary>
        /// True when the symbol is async, when applicable (methods/local functions/lambdas when represented as symbols).
        /// </summary>
        public bool? IsAsync { get; set; }

        /// <summary>
        /// True when the symbol is const, when applicable (fields/locals).
        /// </summary>
        public bool? IsConst { get; set; }

        /// <summary>
        /// True when the symbol represents a discard (e.g., <c>_</c>) when applicable.
        /// </summary>
        public bool? IsDiscard { get; set; }

        /// <summary>
        /// True when the symbol represents a foreach iteration variable declared with <c>var</c>.
        /// </summary>
        public bool? IsForEachVar { get; set; }

        /// <summary>
        /// True when the symbol is an indexer property (i.e., <c>this[...]</c>).
        /// </summary>
        public bool? IsIndexer { get; set; }

        /// <summary>
        /// True when the symbol is optional, when applicable (parameters).
        /// </summary>
        public bool? IsOptional { get; set; }

        /// <summary>
        /// True when the symbol overrides a base member, when applicable.
        /// </summary>
        public bool? IsOverride { get; set; }

        /// <summary>
        /// True when the symbol is sealed, when applicable.
        /// </summary>
        public bool? IsSealed { get; set; }

        /// <summary>
        /// True when the symbol is readonly, when applicable (fields/struct members/parameters with modifiers).
        /// </summary>
        public bool? IsReadOnly { get; set; }

        /// <summary>
        /// True when the symbol is required, when applicable (C# required members).
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// True when the symbol is static, when applicable.
        /// </summary>
        public bool? IsStatic { get; set; }

        /// <summary>
        /// True when the symbol represents a using declaration variable (<c>using var x = ...;</c>).
        /// </summary>
        public bool? IsUsingVar { get; set; }

        /// <summary>
        /// True when the symbol is virtual, when applicable.
        /// </summary>
        public bool? IsVirtual { get; set; }

        /// <summary>
        /// True when the symbol is volatile, when applicable (fields).
        /// </summary>
        public bool? IsVolatile { get; set; }

        /// <summary>
        /// True when the member is write-only, when applicable (rare; typically properties/events with restricted accessors).
        /// </summary>
        public bool? IsWriteOnly { get; set; }

        /// <summary>
        /// True when the symbol was implicitly declared by the compiler (e.g., backing fields, synthesized members).
        /// </summary>
        public bool? IsImplicitlyDeclared { get; set; }

        /// <summary>
        /// True when the symbol is extern, when applicable.
        /// </summary>
        public bool? IsExtern { get; set; }

        /// <summary>
        /// True when the symbol is an explicitly named tuple element (e.g., <c>(int x, int y)</c>).
        /// </summary>
        public bool? IsExplicitlyNamedTupleElement { get; set; }

        /// <summary>
        /// True is the symbol is the original definition. False if the symbol is derived from another symbol.
        /// </summary>
        public bool? IsOriginalDefinition { get; set; }


        /// ******************************************************************
        /// |               Member-ish Details (when available)              |
        /// ******************************************************************

        /// <summary>
        /// True when the selected symbol is a field (<see cref="IFieldSymbol"/>).
        /// </summary>
        public bool IsFieldSymbol { get; set; }

        /// <summary>
        /// True when the selected symbol is a property (<see cref="IPropertySymbol"/>).
        /// </summary>
        public bool IsPropertySymbol { get; set; }

        /// <summary>
        /// True when the selected symbol is a local variable (<see cref="ILocalSymbol"/>).
        /// </summary>
        public bool IsLocalSymbol { get; set; }

        /// <summary>
        /// True when the selected symbol is a parameter (<see cref="IParameterSymbol"/>).
        /// </summary>
        public bool IsParameterSymbol { get; set; }

        /// <summary>
        /// True when the selected symbol is a method (<see cref="IMethodSymbol"/>).
        /// </summary>
        public bool IsMethodSymbol { get; set; }

        /// <summary>
        /// A display-friendly member type name for the symbol, when applicable.
        /// For example, for a field/property/local this is often the declared type;
        /// for a method this may be the return type or a normalized label depending on population rules.
        /// </summary>
        public string MemberType { get; set; } = string.Empty;

        /// <summary>
        /// The <see cref="SymbolKind"/> of the member type symbol (if a member type is resolved).
        /// </summary>
        public SymbolKind MemberTypeKind { get; set; }

        /// ------------------------------- Methods only -------------------------------
        /// <summary>
        /// The Roslyn <see cref="Microsoft.CodeAnalysis.MethodKind"/> for method symbols when available
        /// (Ordinary, Constructor, PropertyGet, LocalFunction, AnonymousFunction, etc.).
        /// </summary>
        public MethodKind? MethodKind { get; set; }

        /// <summary>
        /// A display-friendly signature for the method (e.g., <c>WriteLine(string)</c>).
        /// Population rules are determined by the mapping layer.
        /// </summary>
        public string? MethodSignature { get; set; }

        /// <summary>
        /// A fully-qualified signature for the method (e.g., <c>System.Console.WriteLine(string)</c>),
        /// when available.
        /// </summary>
        public string? MethodSignatureFullyQualified { get; set; }

        /// <summary>
        /// True when the method symbol is generic (e.g., <c>T M&lt;T&gt;(T value)</c>).
        /// </summary>
        public bool? IsGenericMethod { get; set; }

        /// <summary>
        /// True when the method symbol is an extension method.
        /// </summary>
        public bool? IsExtensionMethod { get; set; }

        /// <summary>
        /// The method return type when available.
        /// </summary>
        public ITypeSymbol? ReturnType { get; set; }

        /// <summary>
        /// The type parameters for a generic method when available.
        /// Null when the method is not generic or when a method symbol is not selected.
        /// </summary>
        public ImmutableArray<ITypeParameterSymbol>? TypeParameters { get; set; }


        /// ******************************************************************
        /// |                           Type Info                            |
        /// ******************************************************************

        /// <summary>
        /// The <see cref="Microsoft.CodeAnalysis.TypeKind"/> for <see cref="TypeSymbol"/>, when available.
        /// Useful for differentiating class/struct/interface/enum/delegate/etc.
        /// </summary>
        public TypeKind? TypeKind { get; set; }

        /// <summary>
        /// The <see cref="Microsoft.CodeAnalysis.TypeKind"/> for <see cref="ConvertedTypeSymbol"/>, when available.
        /// </summary>
        public TypeKind? ConvertedTypeKind { get; set; }


        /// ******************************************************************
        /// |                           Operations                           |
        /// ******************************************************************

        /// <summary>
        /// The <see cref="Microsoft.CodeAnalysis.Operations.OperationKind"/> for the <see cref="Operation"/> node.
        /// This is useful for mapping operator tokens and expression categories consistently.
        /// </summary>
        public OperationKind OperationKind { get; set; }

        /// <summary>
        /// A display-friendly name for the result type of the operation/expression when available.
        /// For example: "int", "string", "bool", or a fully qualified type name depending on population rules.
        /// </summary>
        public string? OperationResultType { get; set; }
    }
}
