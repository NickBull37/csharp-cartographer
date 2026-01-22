using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public sealed class TokenSemanticData
    {
        /// **************************************************
        /// |       Raw Roslyn objects       |
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
        /// |       Alias support      |
        /// **************************************************

        /// <summary>
        /// True when the symbol resolved from GetSymbolInfo is an IAliasSymbol (e.g., using-alias, extern alias, global::).
        ///
        /// Expected tokens:
        /// - `global::System.String` (the `global` alias)
        /// - `using Foo = System.Text.StringBuilder;` then `Foo sb = ...;` (the `Foo` identifier)
        /// - `extern alias SomeLib;` then `SomeLib::Type` (the `SomeLib` identifier)
        ///
        /// Rare/usually false for most everyday identifiers.
        /// </summary>
        public bool IsAlias { get; set; }

        /// <summary>
        /// The alias identifier name itself (e.g., "Foo" in `using Foo = ...;` or "global").
        ///
        /// Expected tokens:
        /// - alias identifiers only
        /// </summary>
        public string? AliasName { get; set; }

        /// <summary>
        /// The target symbol that the alias points to (e.g., the real type/namespace).
        /// This is usually what you want to use for classification/semantic roles.
        ///
        /// Expected tokens:
        /// - alias identifiers only
        /// </summary>
        public ISymbol? AliasTargetSymbol { get; set; }

        /// <summary>
        /// Display string for the alias target (typically fully qualified), useful for logging and debugging.
        ///
        /// Expected tokens:
        /// - alias identifiers only
        /// </summary>
        public string? AliasTargetDisplayString { get; set; }


        /// **************************************************
        /// |       High-signal identity fields      |
        /// **************************************************

        /// <summary>
        /// Symbol.Name (simple name). Examples: "_fileProcessor", "ToString", "List".
        ///
        /// Expected tokens:
        /// - Any token/node that successfully binds to a symbol (identifiers, member names, types)
        ///
        /// Usually null/empty when Symbol is null.
        /// </summary>
        public string? SymbolName { get; set; }

        /// <summary>
        /// Symbol.Kind (Field, Local, Parameter, Method, NamedType, Namespace, Property, Event, Alias, etc.)
        ///
        /// Expected tokens:
        /// - Any token/node that successfully binds to a symbol
        ///
        /// This is the property you were using to distinguish Field vs Alias vs Method, etc.
        /// </summary>
        public SymbolKind? SymbolKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? ContainingType { get; set; }

        public string? ContainingNamespace { get; set; }

        public string? ContainingAssembly { get; set; }


        /// **************************************************
        /// |       Symbol characteristics      |
        /// **************************************************

        /// <summary>
        /// Declared accessibility (Public/Private/Protected/Internal/etc.) of the symbol.
        ///
        /// Expected tokens:
        /// - Declared members/types: fields, properties, methods, classes, etc.
        ///
        /// Often null for:
        /// - locals, some synthesized symbols, or when Symbol is null
        /// </summary>
        public Accessibility? Accessibility { get; set; }

        /// <summary>
        /// True if the symbol is static.
        ///
        /// Expected tokens:
        /// - Types, methods, fields, properties
        ///
        /// Not applicable / often null for locals and parameters.
        /// </summary>
        public bool? IsStatic { get; set; }

        /// <summary>
        /// True if the symbol is abstract.
        ///
        /// Expected tokens:
        /// - Abstract classes, abstract methods/properties/events
        ///
        /// Usually null/false for non-abstract constructs.
        /// </summary>
        public bool? IsAbstract { get; set; }

        /// <summary>
        /// True if the symbol is virtual.
        ///
        /// Expected tokens:
        /// - Virtual methods/properties/events
        /// </summary>
        public bool? IsVirtual { get; set; }

        /// <summary>
        /// True if the symbol overrides a base member.
        ///
        /// Expected tokens:
        /// - Overriding methods/properties/events
        /// </summary>
        public bool? IsOverride { get; set; }

        /// <summary>
        /// True if the symbol is sealed (sealed class / sealed override member).
        ///
        /// Expected tokens:
        /// - Sealed classes, sealed override methods/properties/events
        /// </summary>
        public bool? IsSealed { get; set; }

        /// <summary>
        /// True if the symbol is readonly.
        ///
        /// Expected tokens:
        /// - readonly fields
        /// - readonly ref returns/locals (depending on symbol kind)
        ///
        /// Often null for non-field symbols.
        /// </summary>
        public bool? IsReadonly { get; set; }

        /// <summary>
        /// True if the symbol is async (methods only).
        ///
        /// Expected tokens:
        /// - async methods / local functions / lambdas (method symbols)
        ///
        /// Typically null for non-method symbols.
        /// </summary>
        public bool? IsAsync { get; set; }

        /// <summary>
        /// True if the symbol is extern.
        ///
        /// Expected tokens:
        /// - extern methods
        /// - extern alias declarations (but note: the alias itself is SymbolKind.Alias)
        /// </summary>
        public bool? IsExtern { get; set; }

        /// <summary>
        /// True if Roslyn synthesized the symbol (compiler-generated / implicit).
        ///
        /// Expected tokens:
        /// - property backing members, synthesized locals, implicit constructors, etc. (varies)
        ///
        /// Often false for explicitly declared symbols in normal code.
        /// </summary>
        public bool? IsImplicitlyDeclared { get; set; }

        /// <summary>
        /// True if this symbol is the "original definition" rather than a constructed/reduced instance.
        /// Useful for generics and reduced extension methods.
        ///
        /// Expected tokens:
        /// - Generic types/methods: `List<int>` vs `List<T>`
        /// - Extension method invocations (reduced method symbol)
        ///
        /// Often true for non-generic / non-reduced symbols.
        /// </summary>
        public bool? IsDefinition { get; set; }


        /// **************************************************
        /// |       Member-ish Details (when available)      |
        /// **************************************************

        /// <summary>
        /// The "type of the symbol", when the symbol has an associated type.
        /// Examples:
        /// - Field/property/local/parameter: their declared type
        /// - Method: return type
        ///
        /// Expected tokens:
        /// - Identifier tokens that bind to fields/properties/locals/parameters
        /// - Method identifiers (return type)
        ///
        /// Not meaningful for:
        /// - namespaces
        /// - some named types (you usually use TypeSymbol for those)
        /// </summary>
        public string? MemberTypeDisplayString { get; set; }

        public SymbolKind? MemberTypeKind { get; set; }

        public MethodKind MethodKind { get; set; }

        public bool IsGenericMethod { get; set; }

        public ITypeSymbol? ReturnType { get; set; }

        public bool IsReadOnly { get; set; }

        public ImmutableArray<ITypeParameterSymbol> TypeParameters { get; set; }

        /// <summary>
        /// A human-readable method signature string.
        ///
        /// Expected tokens:
        /// - Method symbols only (invocations, method group, method declarations)
        ///
        /// Null for non-method symbols.
        /// </summary>
        public string? MethodSignature { get; set; }

        /// <summary>
        /// The Roslyn TypeKind for TypeSymbol (Class, Struct, Interface, Enum, Delegate, TypeParameter, Error, etc.).
        ///
        /// Expected tokens/nodes:
        /// - Same as TypeDisplayString / TypeSymbol
        /// </summary>
        public TypeKind TypeKind { get; set; }

        /// <summary>
        /// The TypeKind for ConvertedTypeSymbol.
        ///
        /// Expected tokens/nodes:
        /// - Same contexts as ConvertedTypeDisplayString
        /// </summary>
        public TypeKind ConvertedTypeKind { get; set; }

        /// <summary>
        /// Nullable flow state for the expression (FlowState: NotNull / MaybeNull).
        /// This is about data-flow analysis (what Roslyn thinks at this point), not just the declared type.
        ///
        /// Expected tokens/nodes:
        /// - Expression nodes where TypeInfo is meaningful, especially with nullable reference types enabled
        ///
        /// Often null when TypeInfo isn't available.
        /// </summary>
        public NullableFlowState? NullabilityFlowState { get; set; }

        /// <summary>
        /// Nullable annotation on the type symbol (Annotated / NotAnnotated / None).
        /// This reflects declared nullability (e.g., `string?` vs `string`) more than flow.
        ///
        /// Expected tokens/nodes:
        /// - Expression/type nodes with TypeSymbol
        /// - Useful for `T?`, `string?`, etc.
        /// </summary>
        public NullableAnnotation? NullabilityAnnotation { get; set; }

        /// <summary>
        /// Flow state associated with the converted type context.
        /// (Often the same flow state, but saved separately because your pipeline stores converted type too.)
        ///
        /// Expected tokens/nodes:
        /// - Nodes where ConvertedTypeSymbol is present
        /// </summary>
        public NullableFlowState? ConvertedNullabilityFlowState { get; set; }

        /// <summary>
        /// Annotation of the converted type symbol.
        ///
        /// Expected tokens/nodes:
        /// - Nodes where ConvertedTypeSymbol is present
        /// </summary>
        public NullableAnnotation? ConvertedNullabilityAnnotation { get; set; }


        /// **************************************************
        /// |       Operations / constants      |
        /// **************************************************

        /// <summary>
        /// String form of Operation.Kind (e.g., Invocation, SimpleAssignment, Binary, ConditionalAccess, etc.).
        /// This is usually more consistent than guessing from syntax node names.
        ///
        /// Expected tokens/nodes:
        /// - Expression-related nodes where Operation is available
        ///
        /// Null when Operation is null.
        /// </summary>
        public OperationKind OperationKind { get; set; }

        /// <summary>
        /// The type of the operation (Operation.Type) as a display string.
        ///
        /// Expected tokens/nodes:
        /// - Expression nodes where Operation is available
        ///
        /// Null when Operation or Operation.Type is null.
        /// </summary>
        public string? OperationResultType { get; set; }

        /// <summary>
        /// True if Roslyn can evaluate the node as a compile-time constant.
        ///
        /// Expected tokens/nodes:
        /// - Literal expressions: `123`, `"hi"`, `true`, `null`
        /// - const variable references
        /// - simple constant expressions: `1 + 2`, `nameof(X)`, etc. (when valid)
        /// </summary>
        public bool HasConstantValue { get; set; }

        /// <summary>
        /// The constant value (stringified) when HasConstantValue is true.
        ///
        /// Expected tokens/nodes:
        /// - Same contexts as HasConstantValue
        /// </summary>
        public string? ConstantValue { get; set; }


        public bool? IsInSource { get; set; }          // declared in any source in the compilation
        public bool? IsInMetadata { get; set; }        // declared in referenced assemblies
        public bool? IsInUploadedFile { get; set; }    // declared in the uploaded file specifically
        public string? DeclaredInFilePath { get; set; } // optional (when in source)
    }
}
