using csharp_cartographer_backend._03.Models.Files;
using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace csharp_cartographer_backend._05.Services.Roslyn
{

    // TODO: See if it's possible pass in common c# libraries to analyzer to get
    //       more data on common types (DateTime, Console, Guid, etc.)

    public class RoslynAnalyzer : IRoslynAnalyzer
    {
        public SyntaxTree GetSyntaxTree(FileData fileData, CancellationToken cancellationToken)
        {
            return CSharpSyntaxTree.ParseText(
                fileData.Content,
                cancellationToken: cancellationToken);
        }

        public SemanticModel GetSemanticModel(SyntaxTree syntaxTree, CancellationToken cancellationToken)
        {
            var compilationUnit = CSharpCompilation.Create("ArtifactCompilation")
                .AddSyntaxTrees(syntaxTree)
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));

            return compilationUnit.GetSemanticModel(syntaxTree);
        }

        public void AddTokenSemanticData(
            NavToken token,
            SemanticModel semanticModel,
            SyntaxTree syntaxTree,
            CancellationToken cancellationToken)
        {
            if (token.Kind != SyntaxKind.IdentifierToken)
                return;

            token.SemanticData = GetSemanticData(token, semanticModel, syntaxTree, cancellationToken);
        }

        private static TokenSemanticData? GetSemanticData(
            NavToken token,
            SemanticModel semanticModel,
            SyntaxTree syntaxTree,
            CancellationToken cancellationToken)
        {
            var node = GetSemanticNode(token.RoslynToken);
            if (node is null)
                return null;

            var data = new TokenSemanticData();

            // 1) REFERENCES / BINDING: SymbolInfo for the node
            var symbolInfo = semanticModel.GetSymbolInfo(node, cancellationToken: cancellationToken);

            data.Symbol = symbolInfo.Symbol;

            // 2) DECLARATIONS: if this node declares something
            var declaredSymbol = TryGetDeclaredSymbol(semanticModel, node);
            if (declaredSymbol is not null)
            {
                data.IsDeclaredSymbol = true;
                data.DeclaredSymbol = declaredSymbol;
                data.Symbol = declaredSymbol;
            }

            // Only attempt alias binding when we did NOT already resolve this token as a declaration symbol.
            if (declaredSymbol is null)
            {
                // 0) ALIAS INFO (declarations + usages)
                IAliasSymbol? aliasSymbol = null;

                // A) Alias *usage* (e.g., IO.File, Handler x;)
                // Only attempt alias lookup when the identifier is acting as a qualifier (left side of '.' or '::')

                // Skip identifiers that are part of a using directive's target name (right side of using alias)
                var containingUsing = node.FirstAncestorOrSelf<UsingDirectiveSyntax>();
                var isInUsingTargetName =
                    containingUsing is not null &&
                    containingUsing.Alias is not null &&
                    containingUsing.Name is not null &&
                    node.Span.IntersectsWith(containingUsing.Name.Span); // RHS: System.IO / System.Action<int>

                if (!isInUsingTargetName && node is IdentifierNameSyntax identifierNameSyntax)
                {
                    var parent = identifierNameSyntax.Parent;

                    var isDotQualifier = parent is MemberAccessExpressionSyntax maes
                        && maes.Expression == identifierNameSyntax;

                    var isQualifiedNameQualifier = parent is QualifiedNameSyntax qns
                        && qns.Left == identifierNameSyntax;

                    var isAliasQualified = parent is AliasQualifiedNameSyntax aqns
                        && aqns.Alias == identifierNameSyntax;

                    if (isDotQualifier || isQualifiedNameQualifier || isAliasQualified)
                    {
                        aliasSymbol = semanticModel.GetAliasInfo(identifierNameSyntax, cancellationToken);
                        data.AliasSymbol = aliasSymbol;
                        //data.IsAliasSymbol = true;
                    }
                }

                // B) Alias *declaration* (e.g., using IO = System.IO;)
                if (aliasSymbol is null)
                {
                    var usingDirective = node.FirstAncestorOrSelf<UsingDirectiveSyntax>();

                    if (usingDirective?.Alias is not null)
                    {
                        // Only when *this token* is the alias identifier (IO / Handler)
                        if (token.RoslynToken == usingDirective.Alias.Name.Identifier)
                        {
                            aliasSymbol = semanticModel.GetDeclaredSymbol(usingDirective, cancellationToken) as IAliasSymbol;
                            data.AliasSymbol = aliasSymbol;
                            //data.IsAliasSymbol = true;
                        }
                    }
                }

                if (aliasSymbol is not null)
                {
                    data.IsAlias = true;
                    data.AliasName = aliasSymbol.Name;
                    data.AliasTargetSymbol = aliasSymbol.Target;
                    data.AliasTargetName = aliasSymbol.Target.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

                    // IMPORTANT: Keep BOTH so you can differentiate "this was an alias" vs "this resolves to X"
                    data.Symbol = aliasSymbol;              // preserve alias symbol identity
                    data.AliasTargetSymbol = aliasSymbol.Target;

                    // If you want your downstream mapping to act on the target, also store:
                    data.SymbolUnwrapped = aliasSymbol.Target;
                }
            }

            // 4) Fill symbol properties
            if (data.Symbol is ISymbol symbol)
            {
                var locations = symbol.Locations;
                data.IsInSourceCompilation = locations.Any(l => l.IsInSource);
                data.IsInReferencedAssemblies = locations.Any(l => l.IsInMetadata);
                data.IsInUploadedFile = locations.Any(l => l.IsInSource && ReferenceEquals(l.SourceTree, syntaxTree));

                data.SymbolName = symbol.Name;
                data.SymbolKind = symbol.Kind;

                data.ContainingType = symbol.ContainingType?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                data.ContainingNamespace = symbol.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                data.ContainingAssembly = symbol.ContainingAssembly?.Name;

                data.Accessibility = symbol.DeclaredAccessibility != Accessibility.NotApplicable ? symbol.DeclaredAccessibility : null;
                data.IsImplicitlyDeclared = symbol.IsImplicitlyDeclared;
                data.IsStatic = symbol.IsStatic;
                data.IsAbstract = symbol.IsAbstract;
                data.IsVirtual = symbol.IsVirtual;
                data.IsOverride = symbol.IsOverride;
                data.IsSealed = symbol.IsSealed;
                data.IsOriginalDefinition = symbol.IsDefinition;
                data.IsExtern = symbol.IsExtern;

                // 5) Grab member type / method signature when applicable
                switch (symbol)
                {
                    case IFieldSymbol f:
                        data.IsFieldSymbol = true;
                        data.MemberType = f.Type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                        data.MemberTypeKind = f.Type.Kind;
                        data.IsAbstract = f.IsAbstract;
                        data.IsConst = f.IsConst;
                        data.IsExtern = f.IsExtern;
                        data.IsOriginalDefinition = f.IsDefinition;
                        data.IsOverride = f.IsOverride;
                        data.IsReadOnly = f.IsReadOnly;
                        data.IsStatic = f.IsStatic;
                        data.IsSealed = f.IsSealed;
                        data.IsVirtual = f.IsVirtual;
                        data.IsVolatile = f.IsVolatile;
                        data.IsExplicitlyNamedTupleElement = f.IsExplicitlyNamedTupleElement;
                        break;

                    case IPropertySymbol p:
                        data.IsPropertySymbol = true;
                        data.MemberType = p.Type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                        data.MemberTypeKind = p.Type.Kind;
                        data.IsAbstract = p.IsAbstract;
                        data.IsIndexer = p.IsIndexer;
                        data.IsExtern = p.IsExtern;
                        data.IsOriginalDefinition = p.IsDefinition;
                        data.IsOverride = p.IsOverride;
                        data.IsReadOnly = p.IsReadOnly;
                        data.IsStatic = p.IsStatic;
                        data.IsSealed = p.IsSealed;
                        data.IsVirtual = p.IsVirtual;
                        break;

                    case ILocalSymbol l:
                        data.IsLocalSymbol = true;
                        data.MemberType = l.Type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                        data.MemberTypeKind = l.Type.Kind;
                        data.IsAbstract = l.IsAbstract;
                        data.IsConst = l.IsConst;
                        data.IsExtern = l.IsExtern;
                        data.IsForEachVar = l.IsForEach;
                        data.IsOriginalDefinition = l.IsDefinition;
                        data.IsOverride = l.IsOverride;
                        data.IsStatic = l.IsStatic;
                        data.IsSealed = l.IsSealed;
                        data.IsUsingVar = l.IsUsing;
                        data.IsVirtual = l.IsVirtual;
                        break;

                    case IParameterSymbol par:
                        data.IsParameterSymbol = true;
                        data.MemberType = par.Type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                        data.MemberTypeKind = par.Type.Kind;
                        data.IsAbstract = par.IsAbstract;
                        data.IsDiscard = par.IsDiscard;
                        data.IsExtern = par.IsExtern;
                        data.IsOriginalDefinition = par.IsDefinition;
                        data.IsOptional = par.IsOptional;
                        data.IsOverride = par.IsOverride;
                        data.IsStatic = par.IsStatic;
                        data.IsSealed = par.IsSealed;
                        data.IsVirtual = par.IsVirtual;
                        break;

                    case IMethodSymbol m:
                        data.IsMethodSymbol = true;
                        data.MemberType = m.ReturnType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                        data.MethodSignature = m.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                        data.MethodSignatureFullyQualified = m.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                        data.MethodKind = m.MethodKind;
                        data.ReturnType = m.ReturnType;
                        data.TypeParameters = m.TypeParameters;
                        data.IsAsync = m.IsAsync;
                        data.IsGenericMethod = m.IsGenericMethod;
                        data.IsReadOnly = m.IsReadOnly;
                        break;
                }
            }

            // 5) TypeInfo (+ conversion) for the node
            var typeInfo = semanticModel.GetTypeInfo(node, cancellationToken);
            data.TypeSymbol = typeInfo.Type;

            if (typeInfo.Type is ITypeSymbol type)
            {
                data.IsTypeSymbol = true;
                data.TypeKind = type.TypeKind;
            }

            if (typeInfo.ConvertedType is ITypeSymbol ctype)
            {
                data.IsConvertedTypeSymbol = true;
                data.ConvertedTypeKind = ctype.TypeKind;
            }

            // 6) Operations API (often *better* than SymbolInfo for expressions)
            //    This returns null on many declaration nodes; it’s still valuable when present.
            var op = semanticModel.GetOperation(node, cancellationToken);
            data.Operation = op;
            if (op != null)
            {
                data.IsOperation = true;
                data.OperationKind = op.Kind;
                data.OperationResultType = op.Type?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
            }

            return data;
        }

        private static SyntaxNode? GetSemanticNode(SyntaxToken token)
        {
            var parent = token.Parent;
            if (parent is null)
                return null;

            // 1) Declaration identifiers (best for DeclaredSymbol)
            // Identifier token in: field/local/var declarator => parent is VariableDeclaratorSyntax
            if (parent is VariableDeclaratorSyntax)
                return parent;

            // Parameter identifier token => parent is ParameterSyntax
            if (parent is ParameterSyntax)
                return parent;

            // Type / member declarations (if you ever pass tokens from these nodes)
            if (parent is MethodDeclarationSyntax
                or ConstructorDeclarationSyntax
                or PropertyDeclarationSyntax
                or ClassDeclarationSyntax
                or StructDeclarationSyntax
                or InterfaceDeclarationSyntax
                or RecordDeclarationSyntax
                or EnumDeclarationSyntax
                or DelegateDeclarationSyntax)
                return parent;

            // 2) Identifiers in expressions (best for SymbolInfo.Symbol)
            // Most identifier tokens in expressions have parent IdentifierNameSyntax.
            if (parent is IdentifierNameSyntax)
                return parent;

            // Generic names like List<int> (identifier token often sits under GenericNameSyntax)
            if (parent is GenericNameSyntax)
                return parent;

            // Qualified names A.B.C (identifier token can be inside QualifiedNameSyntax)
            if (parent is QualifiedNameSyntax)
                return parent;

            // Alias::Name (global::System, IO::File, etc.)
            if (parent.FirstAncestorOrSelf<AliasQualifiedNameSyntax>() is { } aqn)
            {
                // If the token is the alias identifier (global / IO), bind to that.
                if (token == aqn.Alias.Identifier)
                    return aqn.Alias;

                // Otherwise bind to the name side as usual.
                return aqn.Name;
            }

            // default literal / default(T)
            if (parent.FirstAncestorOrSelf<DefaultExpressionSyntax>() is { } defExpr)
                return defExpr;
            if (parent.FirstAncestorOrSelf<LiteralExpressionSyntax>() is { } litExpr)
                return litExpr;

            // 4) Fallback
            return parent;
        }

        private static ISymbol? TryGetDeclaredSymbol(SemanticModel semanticModel, SyntaxNode node)
        {
            // Declaration Identifier Nodes
            return node switch
            {
                ClassDeclarationSyntax cl => semanticModel.GetDeclaredSymbol(cl),
                ConstructorDeclarationSyntax co => semanticModel.GetDeclaredSymbol(co),
                DelegateDeclarationSyntax de => semanticModel.GetDeclaredSymbol(de),
                EnumDeclarationSyntax en => semanticModel.GetDeclaredSymbol(en),
                EventDeclarationSyntax enm => semanticModel.GetDeclaredSymbol(enm),
                EventFieldDeclarationSyntax efd => semanticModel.GetDeclaredSymbol(efd.Declaration?.Variables.FirstOrDefault()!), // best effort
                FileScopedNamespaceDeclarationSyntax fsn => semanticModel.GetDeclaredSymbol(fsn),
                IndexerDeclarationSyntax ind => semanticModel.GetDeclaredSymbol(ind),
                InterfaceDeclarationSyntax itf => semanticModel.GetDeclaredSymbol(itf),
                MethodDeclarationSyntax md => semanticModel.GetDeclaredSymbol(md),
                NamespaceDeclarationSyntax nsp => semanticModel.GetDeclaredSymbol(nsp),
                ParameterSyntax prm => semanticModel.GetDeclaredSymbol(prm),
                PropertyDeclarationSyntax pty => semanticModel.GetDeclaredSymbol(pty),
                RecordDeclarationSyntax rcd => semanticModel.GetDeclaredSymbol(rcd),
                SingleVariableDesignationSyntax svd => semanticModel.GetDeclaredSymbol(svd),
                StructDeclarationSyntax srt => semanticModel.GetDeclaredSymbol(srt),
                VariableDeclaratorSyntax var => semanticModel.GetDeclaredSymbol(var),
                _ => null
            };
        }
    }
}
