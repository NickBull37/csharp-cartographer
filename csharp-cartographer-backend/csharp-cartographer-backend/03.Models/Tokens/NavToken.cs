using csharp_cartographer_backend._01.Configuration;
using csharp_cartographer_backend._02.Utilities.Helpers;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public enum TestEnum
    {
        Camaro,
        Corvette,
        Viper,
    }

    public readonly record struct NodeKindChainTest(
        string? Parent,
        string? GrandParent,
        string? GreatGrandParent,
        string? GreatGreatGrandParent)
    {
        public bool HasAny(string kind) =>
            Parent == kind ||
            GrandParent == kind ||
            GreatGrandParent == kind ||
            GreatGreatGrandParent == kind;
    }

    /// <summary>
    ///     A Model Definition for the NavToken class.
    /// </summary>
    public class NavToken
    {
        const string TEST_STRING = "Test";

        /// <summary>The unique identifier for a NavToken.</summary>
        public Guid ID { get; set; }

        /// <summary>The index value for the token in the list of NavTokens.</summary>
        public int Index { get; set; }

        /// <summary>The text string value of the token.</summary>
        public string Text { get; set; }

        /// <summary>The Roslyn SyntaxKind of the token.</summary>
        [JsonIgnore]
        public SyntaxKind Kind { get; set; }

        /// <summary>The Roslyn SyntaxKind of the token as a string.</summary>
        public string RoslynKind { get; set; }

        /// <summary>The token classification.</summary>
        public string? RoslynClassification { get; set; }

        /// <summary>The updated token classification.</summary>
        public string? UpdatedClassification { get; set; }

        public TokenMap? Map { get; set; }

        public TestEnum TestEnum { get; set; }

        /// <summary>The TextSpan of the token text.</summary>
        [JsonIgnore]
        public TextSpan Span { get; set; }

        /// <summary>A list of the tokens leading trivia strings.</summary>
        public List<string> LeadingTrivia { get; set; } = [];

        /// <summary>A list of the tokens trailing trivia strings.</summary>
        public List<string> TrailingTrivia { get; set; } = [];

        /// <summary>The Roslyn generated SyntaxToken.</summary>
        [JsonIgnore]
        public SyntaxToken RoslynToken { get; set; }

        /// <summary>The token that comes before this one sequentially.</summary>
        [JsonIgnore]
        public NavToken? PrevToken { get; set; }

        /// <summary>The token that comes after this one sequentially.</summary>
        [JsonIgnore]
        public NavToken? NextToken { get; set; }

        /// <summary>The Roslyn generated SyntaxNode of the token's parent.</summary>
        [JsonIgnore]
        public SyntaxNode? Parent { get; set; }

        /// <summary>The Roslyn generated SyntaxNode of the token's grand parent.</summary>
        [JsonIgnore]
        public SyntaxNode? GrandParent { get; set; }

        /// <summary>The Roslyn generated SyntaxNode of the token's great grand parent.</summary>
        [JsonIgnore]
        public SyntaxNode? GreatGrandParent { get; set; }

        /// <summary>The Roslyn generated SyntaxKind of the token's parent as a string.</summary>
        public string? ParentNodeKind { get; set; }

        /// <summary>The Roslyn generated SyntaxKind of the token's grand parent as a string.</summary>
        public string? GrandParentNodeKind { get; set; }

        /// <summary>The Roslyn generated SyntaxKind of the token's great grand parent as a string.</summary>
        public string? GreatGrandParentNodeKind { get; set; }

        /// <summary>The Roslyn generated SyntaxKind of the token's great great grand parent as a string.</summary>
        public string? GreatGreatGrandParentNodeKind { get; set; }

        public AncestorNodeKinds AncestorKinds { get; set; }

        public NodeKindChainTest AncestorKindChain { get; set; }

        /// <summary>The token's semantic data.</summary>
        [JsonIgnore]
        public TokenSemanticData? SemanticData { get; set; }

        /// <summary>A list of references to this token in the source code file.</summary>
        public List<string> References { get; set; } = [];

        /// <summary>The color the token will be highlighted in the UI.</summary>
        public string? HighlightColor { get; set; }

        /// <summary>A list of token tags attached to the token.</summary>
        public List<TokenTag> Tags { get; set; } = [];

        /// <summary>A list of ancestor nodes & data attached to the token.</summary>
        public List<TokenChart> Charts { get; set; } = [];

        // TODO: Currently only used for unit tests: move constructor to factory class
        public NavToken()
        {
        }

        /// <summary>Constructor for NavToken model.</summary>
        /// <param name="roslynToken">The SyntaxToken generated by the Roslyn code analysis library.</param>
        /// <param name="semanticModel">The semantic model generated from the source code.</param>
        /// <param name="syntaxTree">The syntax tree generated from the source code.</param>
        /// <param name="index">The index of the token in the list.</param>
        public NavToken(SyntaxToken roslynToken, SemanticModel semanticModel, SyntaxTree syntaxTree, int index, string? classification)
        {
            int a, b, c;

            List<int>? test = [];

            TestEnum = TestEnum.Camaro;

            ID = Guid.NewGuid();
            Index = index;

            #region Lexical (token) data
            Text = roslynToken.Text;
            Kind = roslynToken.Kind();
            RoslynKind = roslynToken.Kind().ToString();
            RoslynClassification = classification;
            Span = roslynToken.Span;
            LeadingTrivia = GetLeadingTrivia(roslynToken);
            TrailingTrivia = GetTrailingTrivia(roslynToken);
            RoslynToken = roslynToken;
            Parent = GetAncestorNode(roslynToken, 1);
            GrandParent = GetAncestorNode(roslynToken, 2);
            GreatGrandParent = GetAncestorNode(roslynToken, 3);
            #endregion

            #region Syntax data
            ParentNodeKind = GetAncestorNodeKind(roslynToken, 1);
            GrandParentNodeKind = GetAncestorNodeKind(roslynToken, 2);
            GreatGrandParentNodeKind = GetAncestorNodeKind(roslynToken, 3);
            GreatGreatGrandParentNodeKind = GetAncestorNodeKind(roslynToken, 4);
            AncestorKinds = GetAncestorKinds(roslynToken);
            AncestorKindChain = GetAncestorKindChain(roslynToken);
            #endregion

            #region Contextual data
            References = GetContextualData(semanticModel, syntaxTree, roslynToken);
            #endregion
        }

        public bool IsDelimiter() => GlobalConstants.Delimiters.Contains(Text);

        public bool IsPunctuation() => GlobalConstants.Punctuators.Contains(Text);

        public bool IsOperator() => GlobalConstants.Operators.Contains(Text);

        public bool IsIdentifier() => RoslynToken.IsKind(SyntaxKind.IdentifierToken);

        public bool IsKeyword() => SyntaxFacts.IsKeywordKind(Kind);

        public bool IsAccessStaticMember() => HasAncestorAt(1, SyntaxKind.SimpleMemberAccessExpression);

        private bool HasAncestorAt(int index, SyntaxKind kind)
        {
            var ancestors = AncestorKinds.Ancestors;
            return !ancestors.IsEmpty
                && index >= 0
                && index < ancestors.Length
                && ancestors[index] == kind;
        }

        public bool IsReturnValue()
        {
            bool isValidToken = Kind == SyntaxKind.IdentifierToken
                || Kind == SyntaxKind.DefaultKeyword
                || SyntaxFacts.IsLiteralExpression(Kind);

            return isValidToken && HasAncestorAt(1, SyntaxKind.ReturnStatement);
        }

        #region Delimiter Checks
        public bool IsAccessorListDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.AccessorList)
                && (Kind == SyntaxKind.OpenBraceToken || Kind == SyntaxKind.CloseBraceToken);
        }

        public bool IsArgumentListDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.ArgumentList)
                && (Kind == SyntaxKind.OpenParenToken || Kind == SyntaxKind.CloseParenToken);
        }

        public bool IsAttributeListDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.AttributeList)
                && (Kind == SyntaxKind.OpenBracketToken || Kind == SyntaxKind.CloseBracketToken);
        }

        public bool IsAttributeArgumentListDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.AttributeArgumentList)
                && (Kind == SyntaxKind.OpenParenToken || Kind == SyntaxKind.CloseParenToken);
        }

        public bool IsCastTypeDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.CastExpression)
                && (Kind == SyntaxKind.OpenParenToken || Kind == SyntaxKind.CloseParenToken);
        }

        public bool IsClassDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.ClassDeclaration)
                && (Kind == SyntaxKind.OpenBraceToken || Kind == SyntaxKind.CloseBraceToken);
        }

        public bool IsCollectionExpressionDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.CollectionExpression)
                && (Kind == SyntaxKind.OpenBracketToken || Kind == SyntaxKind.CloseBracketToken);
        }

        public bool IsForEachBlockDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.Block)
                && HasAncestorAt(1, SyntaxKind.ForEachStatement)
                && (Kind == SyntaxKind.OpenBraceToken || Kind == SyntaxKind.CloseBraceToken);
        }

        public bool IsForBlockDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.Block)
                && HasAncestorAt(1, SyntaxKind.ForStatement)
                && (Kind == SyntaxKind.OpenBraceToken || Kind == SyntaxKind.CloseBraceToken);
        }

        public bool IsIfBlockDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.Block)
                && HasAncestorAt(1, SyntaxKind.IfStatement)
                && (Kind == SyntaxKind.OpenBraceToken || Kind == SyntaxKind.CloseBraceToken);
        }

        public bool IsIfConditionDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.IfStatement)
                && (Kind == SyntaxKind.OpenParenToken || Kind == SyntaxKind.CloseParenToken);
        }

        public bool IsTupleTypeDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.TupleType)
                && (Kind == SyntaxKind.OpenParenToken || Kind == SyntaxKind.CloseParenToken);
        }

        public bool IsTypeArgumentListDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.TypeArgumentList)
                && (Kind == SyntaxKind.LessThanToken || Kind == SyntaxKind.GreaterThanToken);
        }

        public bool IsTypeParameterListDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.TypeParameterList)
                && (Kind == SyntaxKind.LessThanToken || Kind == SyntaxKind.GreaterThanToken);
        }

        public bool IsNamespaceDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.NamespaceDeclaration)
                && (Kind == SyntaxKind.OpenBraceToken || Kind == SyntaxKind.CloseBraceToken);
        }

        public bool IsObjectInitializerDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.ObjectInitializerExpression)
                && (Kind == SyntaxKind.OpenBraceToken || Kind == SyntaxKind.CloseBraceToken);
        }

        public bool IsParameterListDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.ParameterList)
                && (Kind == SyntaxKind.OpenParenToken || Kind == SyntaxKind.CloseParenToken);
        }

        public bool IsInterpolatedValueDelimiter()
        {
            return IsDelimiter()
                && HasAncestorAt(0, SyntaxKind.Interpolation)
                && (Kind == SyntaxKind.OpenBraceToken || Kind == SyntaxKind.CloseBraceToken);
        }
        #endregion

        #region Identifier Checks
        public bool IsTypeConstraint()
        {
            if (Kind == SyntaxKind.QuestionToken)
                return false;

            return HasAncestorAt(1, SyntaxKind.TypeParameterConstraintClause)
                || HasAncestorAt(2, SyntaxKind.TypeParameterConstraintClause);
        }

        public bool IsBaseType()
        {
            return Kind == SyntaxKind.IdentifierToken &&
                HasAncestorAt(1, SyntaxKind.SimpleBaseType);
        }

        public bool IsCastType()
        {
            return (Kind == SyntaxKind.IdentifierToken || IsPredefinedType())
                && HasAncestorAt(1, SyntaxKind.CastExpression);
        }

        public bool IsCastTargetType()
        {
            return Kind == SyntaxKind.IdentifierToken &&
                HasAncestorAt(1, SyntaxKind.AsExpression) &&
                PrevToken?.Text == "as";
        }

        public bool IsExceptionType()
        {
            return Kind == SyntaxKind.IdentifierToken &&
                HasAncestorAt(1, SyntaxKind.CatchDeclaration);
        }

        public bool IsMethodInvocation()
        {
            var nextTokenText = NextToken?.Text;
            var hasPermittedNextToken = nextTokenText == "(" || nextTokenText == "<";
            var hasInvocationAncestor = HasAncestorAt(1, SyntaxKind.InvocationExpression) ||
                HasAncestorAt(2, SyntaxKind.InvocationExpression);

            return hasPermittedNextToken && hasInvocationAncestor;
        }

        public bool IsObjectCreationExpression()
        {
            if (HasAncestorAt(1, SyntaxKind.ObjectCreationExpression))
                return true;

            // alias qualified constructors
            return HasAncestorAt(1, SyntaxKind.QualifiedName)
                && HasAncestorAt(2, SyntaxKind.ObjectCreationExpression)
                && PrevToken?.Kind == SyntaxKind.DotToken;
        }

        public bool IsObjCreationPropertyAssignment()
        {
            return RoslynClassification is not null
                && RoslynClassification == "identifier"
                && HasAncestorAt(1, SyntaxKind.SimpleAssignmentExpression)
                && HasAncestorAt(2, SyntaxKind.ObjectInitializerExpression);
        }

        public bool IsExternallyDefinedObjectCreationExpression()
        {
            return RoslynClassification is not null
                && RoslynClassification == "identifier"
                && HasAncestorAt(1, SyntaxKind.ObjectCreationExpression);
        }

        public bool IsParameterLabel()
        {
            return Kind == SyntaxKind.IdentifierToken &&
                HasAncestorAt(1, SyntaxKind.NameColon) &&
                NextToken?.Text == ":";
        }

        public bool IsPropertyOrEnumMemberReference()
        {
            return Kind == SyntaxKind.IdentifierToken
                && HasAncestorAt(1, SyntaxKind.SimpleMemberAccessExpression)
                && !HasAncestorAt(2, SyntaxKind.SimpleMemberAccessExpression)
                && PrevToken?.Kind == SyntaxKind.DotToken;
        }

        /*
         *  -----------------------------------------------------------------------
         *      Declaration Identifiers
         *  -----------------------------------------------------------------------
         */

        public bool IsAttributeDeclaration() =>
            HasAncestorAt(1, SyntaxKind.Attribute);

        public bool IsClassDeclaration() =>
            HasAncestorAt(0, SyntaxKind.ClassDeclaration);

        public bool IsClassConstructorDeclaration() =>
            RoslynClassification is not null &&
            RoslynClassification == "class name" &&
            HasAncestorAt(0, SyntaxKind.ConstructorDeclaration);

        public bool IsDelegateDeclaration() =>
            HasAncestorAt(0, SyntaxKind.DelegateDeclaration);

        public bool IsEnumDeclaration() =>
            HasAncestorAt(0, SyntaxKind.EnumDeclaration);

        public bool IsEnumMemberDeclaration() =>
            HasAncestorAt(0, SyntaxKind.EnumMemberDeclaration);

        public bool IsEventFieldDeclaration() =>
            HasAncestorAt(0, SyntaxKind.VariableDeclarator) &&
            HasAncestorAt(1, SyntaxKind.VariableDeclaration) &&
            HasAncestorAt(2, SyntaxKind.EventFieldDeclaration);

        public bool IsFieldDeclaration() =>
            HasAncestorAt(0, SyntaxKind.VariableDeclarator) &&
            HasAncestorAt(2, SyntaxKind.FieldDeclaration);

        public bool IsFieldDeclaration2() => SemanticData?.DeclaredSymbol?.Kind == SymbolKind.Field;

        public bool IsLocalVariableDeclaration() =>
            HasAncestorAt(2, SyntaxKind.LocalDeclarationStatement)
            && !HasAncestorAt(0, SyntaxKind.GenericName)
            && !HasAncestorAt(0, SyntaxKind.IdentifierName);

        public bool IsLocalForLoopVariableDeclaration() =>
            HasAncestorAt(2, SyntaxKind.ForStatement);

        public bool IsLocalForeachLoopVariableDeclaration() =>
            HasAncestorAt(0, SyntaxKind.ForEachStatement);

        public bool IsMethodDeclaration() =>
            HasAncestorAt(0, SyntaxKind.MethodDeclaration);

        public bool IsParameterDeclaration() =>
            HasAncestorAt(0, SyntaxKind.Parameter);

        public bool IsPropertyDeclaration() =>
            HasAncestorAt(0, SyntaxKind.PropertyDeclaration);

        public bool IsRecordDeclaration() =>
            HasAncestorAt(0, SyntaxKind.RecordDeclaration);

        public bool IsRecordConstructorDeclaration() =>
            RoslynClassification is not null &&
            RoslynClassification == "record class name" &&
            HasAncestorAt(0, SyntaxKind.ConstructorDeclaration);

        public bool IsRecordStructDeclaration() =>
            HasAncestorAt(0, SyntaxKind.RecordStructDeclaration);

        public bool IsRecordStructConstructorDeclaration() =>
            RoslynClassification is not null &&
            RoslynClassification == "record struct name" &&
            HasAncestorAt(0, SyntaxKind.ConstructorDeclaration);

        public bool IsStructDeclaration() =>
            HasAncestorAt(0, SyntaxKind.StructDeclaration);

        public bool IsStructConstructorDeclaration() =>
            RoslynClassification is not null &&
            RoslynClassification == "struct name" &&
            HasAncestorAt(0, SyntaxKind.ConstructorDeclaration);

        /*
         *  -----------------------------------------------------------------------
         *      Reference Identifiers
         *  -----------------------------------------------------------------------
         */

        public bool IsFieldReference() => SemanticData?.SymbolKind == SymbolKind.Field
            && SemanticData.OperationKind == OperationKind.FieldReference;

        /*
         *  -----------------------------------------------------------------------
         *      DataType Identifiers
         *  -----------------------------------------------------------------------
         */

        public bool IsEventFieldType()
        {
            // covers nullable and non-nullable types
            return HasAncestorAt(2, SyntaxKind.EventFieldDeclaration)
                || HasAncestorAt(3, SyntaxKind.EventFieldDeclaration);
        }

        public bool IsFieldDataType() =>
            HasAncestorAt(2, SyntaxKind.FieldDeclaration) ||
            HasAncestorAt(3, SyntaxKind.FieldDeclaration);

        public bool IsLocalVariableDataType() =>
            HasAncestorAt(2, SyntaxKind.LocalDeclarationStatement) ||
            HasAncestorAt(3, SyntaxKind.LocalDeclarationStatement);

        public bool IsForEachLoopLocalVariableDataType()
            => HasAncestorAt(1, SyntaxKind.ForEachStatement);

        public bool IsTupleElementName()
            => HasAncestorAt(0, SyntaxKind.TupleElement);

        public bool IsTupleElementType()
            => HasAncestorAt(1, SyntaxKind.TupleElement);

        public bool IsMethodReturnType()
        {
            if (IsTupleElementName() || IsTupleElementType())
                return false;

            return HasAncestorAt(1, SyntaxKind.MethodDeclaration)
                || HasAncestorAt(2, SyntaxKind.MethodDeclaration);
        }

        public bool IsDelegateReturnType()
        {
            if (IsTupleElementName() || IsTupleElementType())
                return false;

            return HasAncestorAt(1, SyntaxKind.DelegateDeclaration)
                || HasAncestorAt(2, SyntaxKind.DelegateDeclaration);
        }

        public bool IsParameterDataType() =>
            HasAncestorAt(1, SyntaxKind.Parameter) ||
            HasAncestorAt(2, SyntaxKind.Parameter);

        public bool IsPropertyDataType() =>
            HasAncestorAt(1, SyntaxKind.PropertyDeclaration) ||
            HasAncestorAt(2, SyntaxKind.PropertyDeclaration);

        /*
         *  -----------------------------------------------------------------------
         *      Alias / Qualifier Identifiers
         *  -----------------------------------------------------------------------
         */

        public bool IsUsingDirectiveQualifier()
        {
            // skips using keyword, DotToken and SemiColonToken in using directives
            if (RoslynClassification is not ("namespace name" or "identifier"))
                return false;

            // skips alias declarations
            if (NextToken?.Text == "=")
                return false;

            return AncestorKinds.Ancestors.LastOrDefault() == SyntaxKind.UsingDirective;
        }

        public bool IsNamespaceDeclarationQualifier()
        {
            // skip using dir qualifiers that are shared between using dirs and namespace declaration
            if (AncestorKinds.Ancestors.LastOrDefault() == SyntaxKind.UsingDirective)
                return false;

            // for single segment namespace declarations
            if (RoslynClassification is not null
                && RoslynClassification == "namespace name"
                && AncestorKinds.GetLast() == SyntaxKind.NamespaceDeclaration
                && PrevToken?.Text == "namespace")
            {
                return true;
            }

            return RoslynClassification is not null
                && RoslynClassification == "namespace name"
                && AncestorKinds.GetLast() == SyntaxKind.NamespaceDeclaration
                && AncestorKinds.GetSecondToLast() == SyntaxKind.QualifiedName;
        }

        public bool IsNamespaceQualifier()
        {
            // skip using dir qualifiers that are shared between using dirs and namespace declaration
            if (AncestorKinds.GetLast() == SyntaxKind.UsingDirective)
                return false;

            if (RoslynClassification is not null && RoslynClassification == "namespace name")
                return true;

            //             ⌄                ⌄    ⌄       ⌄ 
            // csharp_cartographer_backend._03.Models.Artifacts.Artifact artifact = new();
            if (HasAncestorAt(0, SyntaxKind.IdentifierName)
                && HasAncestorAt(1, SyntaxKind.QualifiedName)
                && HasAncestorAt(2, SyntaxKind.QualifiedName)
                && NextToken?.Text == "."
                && AncestorKinds.GetLast() != SyntaxKind.UsingDirective)
            {
                return true;
            }

            //            ⌄
            // global::System.DateTime.Now
            if (HasAncestorAt(0, SyntaxKind.IdentifierName)
                && HasAncestorAt(1, SyntaxKind.AliasQualifiedName)
                && PrevToken?.Text == "::")
            {
                return true;
            }

            return false;
        }

        public bool IsAliasDeclarationIdentifier()
        {
            //       ⌄
            // using IO = System.IO;
            return HasAncestorAt(0, SyntaxKind.IdentifierName)
                && HasAncestorAt(1, SyntaxKind.NameEquals)
                && PrevToken?.Text == "using";
        }

        public bool IsAliasQualifier()
        {
            //                    ⌄
            // var token = new MyToken.NavToken();
            if (HasAncestorAt(0, SyntaxKind.IdentifierName)
                && HasAncestorAt(1, SyntaxKind.QualifiedName)
                && !HasAncestorAt(2, SyntaxKind.QualifiedName)
                && PrevToken?.Text != "."
                && NextToken?.Text == ".")
                return true;

            //        ⌄
            // return IO.File.Exists(path);
            if (HasAncestorAt(0, SyntaxKind.IdentifierName)
                && HasAncestorAt(1, SyntaxKind.SimpleMemberAccessExpression)
                && PrevToken?.Text != "."
                && NextToken?.Text == "."
                && SemanticData?.SymbolKind == SymbolKind.Namespace
                && SemanticData?.MemberTypeKind == SymbolKind.Alias)
                return true;

            return false;
        }

        public bool IsTypeQualifier()
        {
            // skips 
            if (NextToken?.Text != ".")
                return false;

            // skips query expression vars when they ref a property
            //               ⌄
            // let doubled = n.Value * 2 
            if (IsQueryExpressionVariable())
                return false;

            //           ⌄
            // System.Console.WriteLine(text);
            if (PrevToken?.Text == ".")
                return HasAncestorAt(0, SyntaxKind.IdentifierName)
                    && HasAncestorAt(1, SyntaxKind.SimpleMemberAccessExpression)
                    && HasAncestorAt(2, SyntaxKind.SimpleMemberAccessExpression)
                    && PrevToken?.PrevToken?.Map?.SemanticRole is SemanticRole.NamespaceQualifer or SemanticRole.AliasQualifier;

            //    ⌄                         ⌄
            // Console.WriteLine(text);    Guid.NewGuid();
            if (PrevToken?.Text != ".")
                return HasAncestorAt(0, SyntaxKind.IdentifierName)
                    && HasAncestorAt(1, SyntaxKind.SimpleMemberAccessExpression);

            return false;
        }

        /*
         *  -----------------------------------------------------------------------
         *      Query Expression Identifiers
         *  -----------------------------------------------------------------------
         */

        public bool IsQueryExpressionVariable()
        {
            List<SyntaxKind> kinds =
            [
                SyntaxKind.FromClause,
                SyntaxKind.LetClause,
                SyntaxKind.JoinClause,
                SyntaxKind.QueryContinuation,
                SyntaxKind.QueryBody,
                SyntaxKind.QueryExpression,
                SyntaxKind.WhereClause,
                SyntaxKind.WhenClause,
                SyntaxKind.GroupClause,
                SyntaxKind.OrderByClause,
                SyntaxKind.SelectClause,
                SyntaxKind.AscendingOrdering,
                SyntaxKind.DescendingOrdering,
            ];

            foreach (var kind in AncestorKinds.Ancestors)
            {
                if (kinds.Contains(kind))
                    return true;
            }

            return false;
        }

        public bool IsRangeVariable()
        {
            //                  ⌄
            // var query = from n in numbers
            return HasAncestorAt(0, SyntaxKind.FromClause)
                && HasAncestorAt(1, SyntaxKind.QueryExpression)
                && PrevToken?.Text == "from";
        }

        public bool IsQueryVariableReference()
        {
            // TODO: split this into specific query exp vars (not reliable)
            //                                       ⌄
            // from n in numbers join l in labels on n.Id
            return SemanticData?.SymbolKind == SymbolKind.RangeVariable;
        }

        public bool IsQuerySource()
        {
            //                          ⌄
            // var query = from n in numbers
            return HasAncestorAt(0, SyntaxKind.IdentifierName)
                && HasAncestorAt(1, SyntaxKind.FromClause)
                && PrevToken?.Text == "in";
        }

        public bool IsJoinRangeVariable()
        {
            //      ⌄
            // join l in labels on n.Id equals l.Id
            return HasAncestorAt(0, SyntaxKind.JoinClause)
                && HasAncestorAt(1, SyntaxKind.QueryBody)
                && PrevToken?.Text == "join";
        }

        public bool IsJoinSource()
        {
            //             ⌄
            // join l in labels on n.Id equals l.Id
            return HasAncestorAt(0, SyntaxKind.IdentifierName)
                && HasAncestorAt(1, SyntaxKind.JoinClause)
                && PrevToken?.Text == "in";
        }

        public bool IsLetVariable()
        {
            //        ⌄
            // let doubled = n.Value * 2
            return HasAncestorAt(0, SyntaxKind.LetClause)
                && HasAncestorAt(1, SyntaxKind.QueryBody)
                && PrevToken?.Text == "let";
        }

        public bool IsGroupContinuationRangeVariable()
        {
            //                                             ⌄
            // group new { n, l, doubled } by n.Value into g
            return HasAncestorAt(0, SyntaxKind.QueryContinuation)
                && HasAncestorAt(1, SyntaxKind.QueryBody)
                && PrevToken?.Text == "into";
        }

        public bool IsJoinIntoRangeVariable()
        {
            //                                              ⌄
            // join l in labels on n.Id equals l.Id into matches
            return HasAncestorAt(0, SyntaxKind.JoinIntoClause)
                && HasAncestorAt(1, SyntaxKind.JoinClause)
                && PrevToken?.Text == "into";
        }
        #endregion

        #region Literal Checks
        public bool IsNumericLiteral()
        {
            return RoslynClassification is not null
                && RoslynClassification == "number"
                && Kind == SyntaxKind.NumericLiteralToken
                && HasAncestorAt(0, SyntaxKind.NumericLiteralExpression);
        }

        public bool IsBooleanLiteral()
        {
            return RoslynClassification is not null
                && RoslynClassification == "keyword"
                && (Kind == SyntaxKind.TrueKeyword || Kind == SyntaxKind.FalseKeyword);
        }

        public bool IsCharacterLiteral()
        {
            return RoslynClassification is not null
                && RoslynClassification == "string"
                && Kind == SyntaxKind.CharacterLiteralToken
                && HasAncestorAt(0, SyntaxKind.CharacterLiteralExpression);
        }

        public bool IsNullLiteral()
        {
            return RoslynClassification is not null
                && RoslynClassification == "keyword"
                && Kind == SyntaxKind.NullKeyword;
        }

        public bool IsQuotedString()
        {
            return RoslynClassification is not null
                && RoslynClassification == "string"
                && Kind == SyntaxKind.StringLiteralToken
                && HasAncestorAt(0, SyntaxKind.StringLiteralExpression);
        }

        public bool IsVerbatimString()
        {
            return RoslynClassification is not null
                && RoslynClassification == "string - verbatim"
                && Kind == SyntaxKind.StringLiteralToken
                && HasAncestorAt(0, SyntaxKind.StringLiteralExpression);
        }

        public bool IsInterpolatedString()
        {
            return RoslynClassification is not null
                && RoslynClassification == "string"
                &&
                    (
                        Kind == SyntaxKind.InterpolatedStringStartToken ||
                        Kind == SyntaxKind.InterpolatedStringTextToken ||
                        Kind == SyntaxKind.InterpolatedStringEndToken
                    );
        }

        public bool IsInterpolatedVerbatimString()
        {
            return RoslynClassification is not null
                && RoslynClassification == "string - verbatim"
                &&
                    (
                        Kind == SyntaxKind.InterpolatedVerbatimStringStartToken ||
                        Kind == SyntaxKind.InterpolatedStringTextToken ||
                        Kind == SyntaxKind.InterpolatedStringEndToken
                    );
        }
        #endregion

        #region Operator Checks
        public bool IsArithmeticOperator() =>
            Text is "+" or "-" or "*" or "/" or "%" or "++" or "--";

        public bool IsAssignmentOperator() =>
            Text is "=" or "+=" or "-=" or "*=" or "/=" or "%=" or "&=" or "|=" or "^=" or "<<=" or ">>=" or ">>>=";

        public bool IsBitwiseShiftOperator() =>
            Text is "&" or "|" or "^" or "~" or "<<" or ">>" or ">>>";

        public bool IsBooleanLogicalOperator() =>
            Text is "!" or "&" or "|" or "^" or "&&" or "||";

        public bool IsComparisonOperator() =>
            Text is "<" or ">" or "<=" or ">=" or "==" or "!=";

        public bool IsIndexOrRangeOperator()
        {
            // index
            if (Kind == SyntaxKind.CaretToken && HasAncestorAt(0, SyntaxKind.IndexExpression))
                return true;

            // range
            if (Kind == SyntaxKind.DotDotToken && HasAncestorAt(0, SyntaxKind.RangeExpression))
                return true;

            return false;
        }

        public bool IsLambdaOperator() => Text is "=>";

        public bool IsMemberAccessOperator() => Kind == SyntaxKind.DotToken
            && HasAncestorAt(0, SyntaxKind.SimpleMemberAccessExpression);

        public bool IsConditionalMemberAccessOperator() => Kind == SyntaxKind.DotToken
            && HasAncestorAt(0, SyntaxKind.MemberBindingExpression);

        public bool IsNamespaceAliasQualifier() => Kind == SyntaxKind.ColonColonToken
            && HasAncestorAt(0, SyntaxKind.AliasQualifiedName);

        public bool IsNullOperator() =>
            Text is "!" or "??" or "??=" or "?[";

        public bool IsPointerOperator() =>
            Text is "&" or "*" or "->";
        #endregion

        #region Punctuation Checks
        public bool IsArgumentSeperator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.ArgumentList)
                && Text == ",";
        }

        public bool IsBaseTypeSeperator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.BaseList)
                && Text == ":";
        }

        public bool IsEnumMemberSeparator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.EnumDeclaration)
                && Text == ",";
        }

        public bool IsNullConditionalGuard() => Kind == SyntaxKind.QuestionToken
            && HasAncestorAt(0, SyntaxKind.ConditionalAccessExpression);

        public bool IsNullableTypeMarker() => Kind == SyntaxKind.QuestionToken
            && HasAncestorAt(0, SyntaxKind.NullableType);

        public bool IsNullableConstraintTypeMarker()
        {
            if (Kind != SyntaxKind.QuestionToken)
                return false;

            return HasAncestorAt(0, SyntaxKind.ClassConstraint)
                || HasAncestorAt(0, SyntaxKind.StructConstraint);
        }

        public bool IsParameterSeparator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.ParameterList)
                && Text == ",";
        }

        public bool IsTypeArgumentSeperator() => Kind == SyntaxKind.CommaToken
            && HasAncestorAt(0, SyntaxKind.TypeArgumentList);

        public bool IsTypeParameterSeparator() => Kind == SyntaxKind.CommaToken
            && HasAncestorAt(0, SyntaxKind.TypeParameterList);

        public bool IsSwitchArmSeperator() => Kind == SyntaxKind.CommaToken
            && HasAncestorAt(0, SyntaxKind.SwitchExpression);

        public bool IsTupleElementSeperator() => Kind == SyntaxKind.CommaToken
            && HasAncestorAt(0, SyntaxKind.TupleType);

        public bool IsTypeParameterConstraintClauseSeperator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.TypeParameterConstraintClause)
                && Text == ":";
        }

        public bool IsVariableDeclaratorSeparator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.VariableDeclaration)
                && Text == ",";
        }

        public bool IsStatementTerminator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && Text == ";";
        }

        public bool IsSwitchCaseLabelTerminator() => Kind == SyntaxKind.ColonToken
            && HasAncestorAt(0, SyntaxKind.CaseSwitchLabel);

        public bool IsParameterLabelTerminator() => Kind == SyntaxKind.ColonToken
            && HasAncestorAt(0, SyntaxKind.NameColon);
        #endregion

        #region Type Checks
        public bool IsGenericTypeArgument()
        {
            return HasAncestorAt(1, SyntaxKind.TypeArgumentList) ||
                HasAncestorAt(2, SyntaxKind.TypeArgumentList);
        }

        public bool IsGenericType() => HasAncestorAt(0, SyntaxKind.GenericName);

        public bool IsGenericTypeParameter() => RoslynClassification is not null
            && RoslynClassification == "type parameter name"
            && HasAncestorAt(0, SyntaxKind.TypeParameter);

        public bool IsNullableType() => HasAncestorAt(1, SyntaxKind.NullableType);

        public bool IsNullableConstraintType() => IsTypeConstraint() && NextToken?.Text == "?";

        public bool IsPredefinedType() => SyntaxFacts.IsPredefinedType(Kind);

        public bool IsTypePatternType() =>
            HasAncestorAt(1, SyntaxKind.ConstantPattern) ||
            HasAncestorAt(1, SyntaxKind.DeclarationPattern) ||
            HasAncestorAt(1, SyntaxKind.IsExpression);
        #endregion

        /// <summary>Gets the token's leading trivia.</summary>
        /// <param name="roslynToken">The SyntaxToken generated by the Roslyn code analysis library.</param>
        /// <returns>A list of leading trivia strings.</returns>
        private static List<string> GetLeadingTrivia(SyntaxToken roslynToken)
        {
            if (!roslynToken.HasLeadingTrivia)
            {
                return [];
            }

            List<string> leadingTriviaStrings = [];

            foreach (SyntaxTrivia trivia in roslynToken.LeadingTrivia)
            {

            }

            List<int> test = [];

            foreach (int num in test)
            {

            }

            foreach (var trivia in roslynToken.LeadingTrivia)
            {
                if (trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia))
                {
                    leadingTriviaStrings.AddRange(GetLeadingSingleLineDocumentationCommentTrivia(trivia.ToString()));
                    continue;
                }
                else if (trivia.IsKind(SyntaxKind.MultiLineCommentTrivia))
                {
                    leadingTriviaStrings.AddRange(GetLeadingMultilineCommentTrivia(trivia.ToString()));
                    continue;
                }

                leadingTriviaStrings.Add(trivia.ToString());

                if (trivia.IsKind(SyntaxKind.RegionDirectiveTrivia)
                    || trivia.IsKind(SyntaxKind.EndRegionDirectiveTrivia))
                {
                    leadingTriviaStrings.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                }
            }

            return leadingTriviaStrings;
        }

        /// <summary>Splits a SingleLineDocumentationCommentTrivia trivia into multiple strings.</summary>
        /// <param name="triviaString">The trivia string that needs to be split.</param>
        /// <returns>A leading single-line documentation comment trivia split into a list of strings.</returns>
        private static List<string> GetLeadingSingleLineDocumentationCommentTrivia(string triviaString)
        {
            List<string> triviaToAdd = [];
            triviaString = "///" + triviaString;

            if (StringHelpers.CountOccurrences(triviaString, "///") == 1)
            {
                triviaToAdd.Add(triviaString);
                triviaToAdd.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
            }
            if (StringHelpers.CountOccurrences(triviaString, "///") > 1)
            {
                var newStrings = triviaString.Split("\r\n");

                var count = 1;
                var numOfStrings = newStrings.Length;
                foreach (var newString in newStrings)
                {
                    // handle scenarios where comments have extra spaces
                    if (StringHelpers.HasSequentialSpaces(newString))
                    {
                        var spacesString = StringHelpers.PullSequentialSpaces(newString);
                        triviaToAdd.Add(spacesString);
                    }

                    triviaToAdd.Add(newString.Trim());
                    if (count < numOfStrings)
                    {
                        // add additional line break trivia for multi-line comments
                        triviaToAdd.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                    }
                    count++;
                }
            }
            return triviaToAdd;
        }

        /// <summary>Splits a MultilineCommentTrivia trivia into multiple strings.</summary>
        /// <param name="triviaString">The trivia string that needs to be split.</param>
        /// <returns>A list of leading multi-line comment trivia strings.</returns>
        private static List<string> GetLeadingMultilineCommentTrivia(string triviaString)
        {
            List<string> triviaToAdd = [];
            var newStrings = triviaString.Split("\r\n");

            var count = 1;
            var numOfStrings = newStrings.Length;
            foreach (var newString in newStrings)
            {
                // check if string has sequential spaces
                // if so, cut them and create a new space trivia with them
                if (StringHelpers.HasSequentialSpaces(newString))
                {
                    var spacesString = StringHelpers.PullSequentialSpaces(newString);
                    triviaToAdd.Add(spacesString);
                }

                // add new trivia strings
                triviaToAdd.Add(newString.Trim());
                if (count < numOfStrings)
                {
                    triviaToAdd.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                }
                count++;
            }
            return triviaToAdd;
        }

        /// <summary>Gets the token's trailing trivia.</summary>
        /// <param name="roslynToken">The SyntaxToken generated by the Roslyn code analysis library.</param>
        /// <returns>A list of trailing trivia strings.</returns>
        private static List<string> GetTrailingTrivia(SyntaxToken roslynToken)
        {
            if (!roslynToken.HasTrailingTrivia)
            {
                return [];
            }

            List<string> trailingTriviaStrings = [];
            foreach (var trivia in roslynToken.TrailingTrivia)
            {
                // handle trailing trivia that contains "\n" instead of "\r\n"
                if (trivia.IsKind(SyntaxKind.EndOfLineTrivia))
                {
                    trailingTriviaStrings.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                    continue;
                }
                trailingTriviaStrings.Add(trivia.ToString());
            }
            return trailingTriviaStrings;
        }

        /// <summary>Returns the SyntaxKind of the ancestor node at the specified level.</summary>
        /// <param name="roslynToken">The SyntaxToken generated by the Roslyn code analysis library.</param>
        /// <param name="level">The number of ancestors to climb in the syntax tree.</param>
        /// <returns>The ancestor node kind if found; otherwise, <c>null</c>.</returns>
        private static string? GetAncestorNodeKind(SyntaxToken roslynToken, int level)
        {
            var ancestor = GetAncestorNode(roslynToken, level);
            return ancestor?.Kind().ToString();
        }

        /// <summary>Returns the ancestor SyntaxNode at the specified level.</summary>
        /// <param name="roslynToken">The SyntaxToken generated by the Roslyn code analysis library.</param>
        /// <param name="level">The number of ancestors to climb in the syntax tree.</param>
        /// <returns>The ancestor node if found; otherwise, <c>null</c>.</returns>
        private static SyntaxNode? GetAncestorNode(SyntaxToken roslynToken, int level)
        {
            SyntaxNode? currentNode = roslynToken.Parent;
            for (int i = 1; i < level && currentNode is not null; i++)
            {
                currentNode = currentNode.Parent;
            }
            return currentNode;
        }

        /// <summary>Returns the SyntaxKind of the ancestor nodes in a struct.</summary>
        /// <param name="roslynToken">The SyntaxToken generated by the Roslyn code analysis library.</param>
        /// <returns>The ancestor node kinds.</returns>
        private static AncestorNodeKinds GetAncestorKinds(SyntaxToken roslynToken)
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

            return new AncestorNodeKinds(builder.ToImmutable());
        }

        /// <summary>Returns the SyntaxKind of the ancestor nodes in a struct.</summary>
        /// <param name="roslynToken">The SyntaxToken generated by the Roslyn code analysis library.</param>
        /// <returns>The ancestor node kinds.</returns>
        private static NodeKindChainTest GetAncestorKindChain(SyntaxToken roslynToken)
        {
            SyntaxNode? currentNode = roslynToken.Parent;

            string? parentKind = null;
            string? grandParentKind = null;
            string? greatGrandParentKind = null;
            string? greatGreatGrandParentKind = null;

            if (currentNode is not null)
            {
                parentKind = currentNode.Kind().ToString();
                currentNode = currentNode.Parent;

                if (currentNode is not null)
                {
                    grandParentKind = currentNode.Kind().ToString();
                    currentNode = currentNode.Parent;

                    if (currentNode is not null)
                    {
                        greatGrandParentKind = currentNode.Kind().ToString();
                        currentNode = currentNode.Parent;

                        if (currentNode is not null)
                        {
                            greatGreatGrandParentKind = currentNode.Kind().ToString();
                        }
                    }
                }
            }

            return new NodeKindChainTest(
                parentKind,
                grandParentKind,
                greatGrandParentKind,
                greatGreatGrandParentKind);
        }

        /// <summary>Gets data for NavToken properites related to C# semantics.</summary>
        /// <param name="semanticModel">The semantic model generated from the source code.</param>
        /// <param name="node">The parent node of the token.</param>
        /// <returns>The semantic data for the token's parent node.</returns>
        private static TokenSemanticData? GetSemanticData(SemanticModel semanticModel, SyntaxNode? node, SyntaxTree syntaxTree)
        {
            if (node is null)
                return null;

            var data = new TokenSemanticData();

            // 1) DECLARATIONS: if this node declares something, GetDeclaredSymbol is the truth.
            //    This is the main fix for "field declaration token gives Alias" scenarios.
            data.DeclaredSymbol = TryGetDeclaredSymbol(semanticModel, node);

            // 2) REFERENCES / BINDING: SymbolInfo for the node (useful even if declared symbol exists).
            var symbolInfo = semanticModel.GetSymbolInfo(node);

            var boundSymbol = symbolInfo.Symbol;

            // Prefer declared symbol when present (definition site), otherwise bound symbol (reference site).
            // But still keep both.
            data.Symbol = data.DeclaredSymbol ?? boundSymbol;

            // 3) ALIAS UNWRAP (global::, using-alias, extern alias, etc.)
            //    If the chosen symbol is an alias, unwrap to its target.
            if (data.Symbol is IAliasSymbol alias)
            {
                data.IsAlias = true;
                data.AliasName = alias.Name;

                data.AliasTargetSymbol = alias.Target;
                data.AliasTargetName = alias.Target.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

                // Often you want the *target* treated as the "real" symbol for roles/classification.
                // Keep the alias info, but also swap Symbol to target so downstream logic sees Field/Type/etc.
                data.Symbol = alias.Target;
            }

            // 4) Fill symbol properties if we have any symbol (after alias unwrapping)
            if (data.Symbol is ISymbol symbol)
            {
                var locations = symbol.Locations;

                // True for symbols declared in *any* source file in this compilation
                data.IsInSourceCompilation = locations.Any(l => l.IsInSource);

                // True for symbols coming from metadata (referenced assemblies)
                data.IsInReferencedAssemblies = locations.Any(l => l.IsInMetadata);

                // True only when declared in the uploaded file’s syntax tree
                data.IsInUploadedFile = locations.Any(l => l.IsInSource && ReferenceEquals(l.SourceTree, syntaxTree));

                // Optional: capture the file path when in source (first location)
                data.DeclaredInFilePath = locations.FirstOrDefault(l => l.IsInSource)?.SourceTree?.FilePath;

                data.SymbolName = symbol.Name;
                data.SymbolKind = symbol.Kind;

                data.ContainingType = symbol.ContainingType?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                data.ContainingNamespace = symbol.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                data.ContainingAssembly = symbol.ContainingAssembly?.Name;

                data.Accessibility = symbol.DeclaredAccessibility;
                data.IsImplicitlyDeclared = symbol.IsImplicitlyDeclared;

                // Some flags require ISymbol subtypes, but we can safely probe common ones:
                data.IsStatic = symbol.IsStatic;
                data.IsAbstract = symbol.IsAbstract;
                data.IsVirtual = symbol.IsVirtual;
                data.IsOverride = symbol.IsOverride;
                data.IsSealed = symbol.IsSealed;
                data.IsExtern = symbol.IsExtern;

                // "IsDefinition" is more about whether the symbol is an original definition
                // vs a constructed/reduced one. For methods/types it's often helpful:
                data.IsOriginalDefinition = SymbolEqualityComparer.Default.Equals(symbol, symbol.OriginalDefinition);

                // Grab member type / method signature when applicable
                switch (symbol)
                {
                    case IFieldSymbol f:
                        data.MemberType = f.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                        data.MemberTypeKind = f.Type.Kind;
                        break;

                    case IPropertySymbol p:
                        data.MemberType = p.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                        data.MemberTypeKind = p.Type.Kind;
                        break;

                    case ILocalSymbol l:
                        data.MemberType = l.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                        data.MemberTypeKind = l.Type.Kind;
                        break;

                    case IParameterSymbol par:
                        data.MemberType = par.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                        data.MemberTypeKind = par.Type.Kind;
                        break;

                    case IMethodSymbol m:
                        data.IsAsync = m.IsAsync;
                        data.MemberType = m.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                        data.MethodSignature = m.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
                        data.MethodKind = m.MethodKind;
                        data.IsGenericMethod = m.IsGenericMethod;
                        data.ReturnType = m.ReturnType;
                        data.IsReadOnly = m.IsReadOnly;
                        data.TypeParameters = m.TypeParameters;
                        break;
                }
            }

            // 5) TypeInfo (+ conversion) for the node
            var typeInfo = semanticModel.GetTypeInfo(node);

            data.TypeSymbol = typeInfo.Type;
            data.ConvertedTypeSymbol = typeInfo.ConvertedType;

            if (typeInfo.Type is ITypeSymbol type)
            {
                data.TypeKind = type.TypeKind;

                //data.NullabilityFlowState = typeInfo.Nullability.FlowState;
                //data.NullabilityAnnotation = type.NullableAnnotation;
            }

            if (typeInfo.ConvertedType is ITypeSymbol ctype)
            {
                data.ConvertedTypeKind = ctype.TypeKind;

                // flow state is for the expression; same object
                //data.ConvertedNullabilityFlowState = typeInfo.Nullability.FlowState;
                //data.ConvertedNullabilityAnnotation = ctype.NullableAnnotation;
            }

            // 6) Constant value (works on expressions where Roslyn can evaluate a constant)
            //var constant = semanticModel.GetConstantValue(node);
            //data.HasConstantValue = constant.HasValue;
            //if (constant.HasValue)
            //{
            //    data.ConstantValue = constant.Value is null ? "null" : constant.Value.ToString();
            //}

            // 7) Operations API (often *better* than SymbolInfo for expressions)
            //    This returns null on many declaration nodes; it’s still valuable when present.
            var op = semanticModel.GetOperation(node);
            data.Operation = op;
            if (op != null)
            {
                data.OperationKind = op.Kind;
                data.OperationResultType = op.Type?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            }

            return data;
        }

        private static ISymbol? TryGetDeclaredSymbol(SemanticModel semanticModel, SyntaxNode node)
        {
            // VariableDeclaratorSyntax covers fields + locals, etc.
            // This is usually the right node for identifiers in declarations.
            return node switch
            {
                VariableDeclaratorSyntax v => semanticModel.GetDeclaredSymbol(v),
                SingleVariableDesignationSyntax d => semanticModel.GetDeclaredSymbol(d),
                ParameterSyntax p => semanticModel.GetDeclaredSymbol(p),
                PropertyDeclarationSyntax p => semanticModel.GetDeclaredSymbol(p),
                EventDeclarationSyntax e => semanticModel.GetDeclaredSymbol(e),
                EventFieldDeclarationSyntax efd => semanticModel.GetDeclaredSymbol(efd.Declaration?.Variables.FirstOrDefault()!), // best effort
                MethodDeclarationSyntax m => semanticModel.GetDeclaredSymbol(m),
                ConstructorDeclarationSyntax c => semanticModel.GetDeclaredSymbol(c),
                ClassDeclarationSyntax c => semanticModel.GetDeclaredSymbol(c),
                StructDeclarationSyntax s => semanticModel.GetDeclaredSymbol(s),
                InterfaceDeclarationSyntax i => semanticModel.GetDeclaredSymbol(i),
                RecordDeclarationSyntax r => semanticModel.GetDeclaredSymbol(r),
                EnumDeclarationSyntax e => semanticModel.GetDeclaredSymbol(e),
                DelegateDeclarationSyntax d => semanticModel.GetDeclaredSymbol(d),
                NamespaceDeclarationSyntax n => semanticModel.GetDeclaredSymbol(n),
                FileScopedNamespaceDeclarationSyntax n => semanticModel.GetDeclaredSymbol(n),
                UsingDirectiveSyntax u when u.Alias != null => semanticModel.GetDeclaredSymbol(u.Alias),
                _ => null
            };
        }

        private static SyntaxNode? GetSemanticNodeForToken(SyntaxToken token)
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

            // 3) Operators / punctuation: climb to the expression node that owns the operator
            // This makes Operation / TypeInfo much more meaningful.
            if (token.IsKind(SyntaxKind.DotToken) || token.IsKind(SyntaxKind.QuestionToken))
            {
                // Handles `obj.Member`, `obj?.Member`, conditional access chains, etc.
                return parent.FirstAncestorOrSelf<ConditionalAccessExpressionSyntax>()
                    ?? parent.FirstAncestorOrSelf<MemberAccessExpressionSyntax>()
                    ?? parent;
            }

            //if (SyntaxFacts.IsUnaryExpression(parent.Kind()))
            //    return parent;

            if (SyntaxFacts.IsBinaryExpression(parent.Kind()))
                return parent;

            // Invocation: name, parentheses, commas, etc.
            if (parent.FirstAncestorOrSelf<InvocationExpressionSyntax>() is { } invoke)
                return invoke;

            // Object creation: `new Foo(...)`
            if (parent.FirstAncestorOrSelf<ObjectCreationExpressionSyntax>() is { } create)
                return create;

            // default literal / default(T)
            if (parent.FirstAncestorOrSelf<DefaultExpressionSyntax>() is { } defExpr)
                return defExpr;
            if (parent.FirstAncestorOrSelf<LiteralExpressionSyntax>() is { } litExpr)
                return litExpr;

            // 4) Fallback
            return parent;
        }

        /// <summary>Gets the contextual data for the passed in syntax token.</summary>
        /// <param name="semanticModel">The semantic model generated from the source code.</param>
        /// <param name="syntaxTree">The syntax tree generated from the source code.</param>
        /// <param name="roslynToken">The SyntaxToken generated by the Roslyn code analysis library.</param>
        /// <returns>A list of symbol references for the passed in syntax token.</returns>
        private static List<string> GetContextualData(SemanticModel semanticModel, SyntaxTree syntaxTree, SyntaxToken roslynToken)
        {
            if (roslynToken.Parent is not IdentifierNameSyntax identifierNode)
            {
                return [];
            }

            var root = syntaxTree.GetRoot();
            var symbol = semanticModel.GetSymbolInfo(identifierNode).Symbol;

            if (symbol is null)
            {
                return [];
            }

            var identifierNodes = root.DescendantNodes().OfType<IdentifierNameSyntax>();

            // find all nodes where the symbol matches
            return identifierNodes
                .Where(
                    node =>
                    {
                        var nodeSymbol = semanticModel.GetSymbolInfo(node).Symbol;
                        return SymbolEqualityComparer.Default.Equals(nodeSymbol, symbol);
                    }
                )
                .Select(reference => reference.ToString())
                .ToList();
        }
    }
}
