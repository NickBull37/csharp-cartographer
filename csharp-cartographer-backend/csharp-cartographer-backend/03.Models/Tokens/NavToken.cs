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

    public readonly record struct NodeKindChain(
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

        /// <summary>The token field symbol.</summary>
        [JsonIgnore]
        public IFieldSymbol? FieldSymbol { get; set; }

        /// <summary>The token classification.</summary>
        public string? RoslynClassification { get; set; }

        /// <summary>The updated token classification.</summary>
        public string? UpdatedClassification { get; set; }

        public TokenMap Map { get; set; }

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

        public NodeKindChain AncestorKindChain { get; set; }

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
            List<int>? test = [];
            TestEnum = TestEnum.Camaro;

            ID = Guid.NewGuid();
            Index = index;

            #region Lexical (token) data
            Text = roslynToken.Text;
            Kind = roslynToken.Kind();
            RoslynKind = roslynToken.Kind().ToString();
            FieldSymbol = TryGetFieldSymbol(semanticModel, roslynToken);
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

            #region Semantic data
            SemanticData = GetSemanticData(semanticModel, roslynToken.Parent);
            #endregion

            #region Contextual data
            References = GetContextualData(semanticModel, syntaxTree, roslynToken);
            #endregion
        }

        public bool IsKeyword(string keyword)
        {
            return RoslynClassification is not null
                && RoslynClassification.Contains("keyword")
                && string.Equals(Text, keyword, StringComparison.Ordinal);
        }

        public bool IsIdentifier()
        {
            return RoslynToken.IsKind(SyntaxKind.IdentifierToken);
        }

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

        public bool IsParameterSeparator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.ParameterList)
                && Text == ",";
        }

        public bool IsTypeArgumentSeperator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.TypeArgumentList)
                && Text == ",";
        }

        public bool IsStatementTerminator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && Text == ";";
            //&&
            //    (
            //        HasAncestorAt(0, SyntaxKind.ExpressionStatement) ||
            //        HasAncestorAt(0, SyntaxKind.LocalDeclarationStatement) ||
            //        HasAncestorAt(0, SyntaxKind.ReturnStatement) ||
            //        HasAncestorAt(0, SyntaxKind.FieldDeclaration)
            //    );
        }

        public bool IsLabelTerminator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.NameColon)
                && Text == ":";
        }

        public bool IsSwitchClauseSeperator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.CaseSwitchLabel)
                && Text == ":";
        }

        public bool IsTypeParameterConstraintClauseSeperator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.TypeParameterConstraintClause)
                && Text == ":";
        }
        #endregion

        #region Delimiter Checks
        public bool IsArgumentListDelimiter()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.ArgumentList)
                &&
                    (
                        Text == "(" ||
                        Text == ")"
                    );
        }

        public bool IsAttributeListDelimiter()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.AttributeList)
                &&
                    (
                        Text == "[" ||
                        Text == "]"
                    );
        }

        public bool IsAttributeArgumentListDelimiter()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.AttributeArgumentList)
                &&
                    (
                        Text == "(" ||
                        Text == ")"
                    );
        }

        public bool IsCollectionExpressionDelimiter()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.CollectionExpression)
                &&
                    (
                        Text == "[" ||
                        Text == "]"
                    );
        }

        public bool IsTypeArgumentListDelimiter()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.TypeArgumentList)
                &&
                    (
                        Text == "<" ||
                        Text == ">"
                    );
        }

        public bool IsTypeParameterListDelimiter()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.TypeParameterList)
                &&
                    (
                        Text == "<" ||
                        Text == ">"
                    );
        }

        public bool IsParameterListDelimiter()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.ParameterList)
                &&
                    (
                        Text == "(" ||
                        Text == ")"
                    );
        }

        public bool IsObjectInitializerDelimiter()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.ObjectInitializerExpression)
                &&
                    (
                        Text == "{" ||
                        Text == "}"
                    );
        }

        public bool IsInterpolatedValueDelimiter()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.Interpolation)
                &&
                    (
                        Text == "{" ||
                        Text == "}"
                    );
        }
        #endregion

        #region Operator Checks
        public bool IsMemberAccessOperator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "operator"
                && HasAncestorAt(0, SyntaxKind.SimpleMemberAccessExpression)
                && Text == ".";
        }

        public bool IsNamespaceAliasQualifier()
        {
            return RoslynClassification is not null
                && RoslynClassification == "operator"
                && HasAncestorAt(0, SyntaxKind.AliasQualifiedName)
                && Text == "::";
        }

        public bool IsRangeOperator()
        {
            return RoslynClassification is not null
                && RoslynClassification == "punctuation"
                && HasAncestorAt(0, SyntaxKind.RangeExpression)
                && Text == "..";
        }
        #endregion

        #region Identifier Checks
        public bool IsTypeConstraint()
        {
            return HasAncestorAt(1, SyntaxKind.TypeParameterConstraintClause)
                || HasAncestorAt(2, SyntaxKind.TypeParameterConstraintClause);
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

        public bool IsEventDeclaration() =>
            HasAncestorAt(0, SyntaxKind.EventDeclaration);

        public bool IsEventFieldDeclaration() =>
            HasAncestorAt(0, SyntaxKind.EventFieldDeclaration);

        public bool IsFieldDeclaration() =>
            HasAncestorAt(0, SyntaxKind.VariableDeclarator) &&
            HasAncestorAt(2, SyntaxKind.FieldDeclaration);

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
         *      DataType Identifiers
         *  -----------------------------------------------------------------------
         */
        public bool IsFieldDataType() =>
            HasAncestorAt(2, SyntaxKind.FieldDeclaration) ||
            HasAncestorAt(3, SyntaxKind.FieldDeclaration);

        public bool IsLocalVariableDataType() =>
            HasAncestorAt(2, SyntaxKind.LocalDeclarationStatement) ||
            HasAncestorAt(3, SyntaxKind.LocalDeclarationStatement);

        public bool IsMethodReturnType() =>
            HasAncestorAt(1, SyntaxKind.MethodDeclaration) ||
            HasAncestorAt(2, SyntaxKind.MethodDeclaration);

        public bool IsParameterDataType() =>
            HasAncestorAt(1, SyntaxKind.Parameter) ||
            HasAncestorAt(2, SyntaxKind.Parameter);

        public bool IsPropertyDataType() =>
            HasAncestorAt(1, SyntaxKind.PropertyDeclaration) ||
            HasAncestorAt(2, SyntaxKind.PropertyDeclaration);
        #endregion

        #region Type Checks
        public bool IsPredefinedType()
        {
            return SyntaxFacts.IsPredefinedType(Kind);
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

        public bool IsCharacterLiteral()
        {
            return RoslynClassification is not null
                && RoslynClassification == "string"
                && Kind == SyntaxKind.CharacterLiteralToken
                && HasAncestorAt(0, SyntaxKind.CharacterLiteralExpression);
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

        public bool IsOpenParen()
        {
            return Text.Equals("(");
        }

        public bool IsDot()
        {
            return Text.Equals(".");
        }

        public bool IsTypeKeywordOrIdentifier()
        {
            var kind = RoslynToken.Kind();

            // Predefined C# type keywords: int, string, bool, object, etc.
            if (SyntaxFacts.IsPredefinedType(kind))
                return true;

            // Identifiers that could be types: List, MyClass, IFoo
            if (kind == SyntaxKind.IdentifierToken)
                return true;

            return false;
        }

        /*
         *   Does this token logically end a declaration or separate a declarator?
         *   
         *   covers:
         *       variable declarations with initializers
         *       reassignment
         *       compound assignments
         * 
         *   int x = 5;         pass
         *   int x;             pass
         *   Foo(a, b)          pass
         *   Foo(a)             pass
         *   
         *   x + y              fail
         *   if (x == y)        fail
         *   
         */
        /// <summary>
        /// Determines whether this token logically ends a declaration
        /// or separates declarators.
        /// </summary>
        /// <remarks>
        /// <para>Covered scenarios:</para>
        /// <list type="bullet">
        ///   <item>
        ///     <description>Variable declarations with initializers</description>
        ///   </item>
        ///   <item>
        ///     <description>Reassignment expressions</description>
        ///   </item>
        ///   <item>
        ///     <description>Compound assignment expressions</description>
        ///   </item>
        /// </list>
        /// <para>Examples that return <c>true</c>:</para>
        /// <code>
        /// int x = 5;
        /// int x;
        /// Foo(a, b)
        /// Foo(a)
        /// </code>
        /// <para>Examples that return <c>false</c>:</para>
        /// <code>
        /// x + y
        /// if (x == y)
        /// </code>
        /// </remarks>
        public bool IsAssignmentOrDelimiter()
        {
            var kind = RoslynToken.Kind();

            if (SyntaxFacts.IsAssignmentExpression(kind))
                return true;

            return kind switch
            {
                SyntaxKind.SemicolonToken => true,
                SyntaxKind.CommaToken => true,
                SyntaxKind.CloseParenToken => true,
                SyntaxKind.CloseBracketToken => true,
                _ => false
            };
        }

        private bool HasAncestorAt(int index, SyntaxKind kind)
        {
            var ancestors = AncestorKinds.Ancestors;

            // Use Count or Length depending on the type
            return !ancestors.IsEmpty
                && index >= 0
                && index < ancestors.Length
                && ancestors[index] == kind;
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
            return HasAncestorAt(1, SyntaxKind.ObjectCreationExpression);
        }

        public bool IsUsingDirectiveSegment()
        {
            if (RoslynClassification is not ("namespace name" or "identifier"))
                return false;

            var ancestors = AncestorKinds.Ancestors;
            return ancestors.Length > 0 && ancestors.Last() == SyntaxKind.UsingDirective;
        }

        public bool IsNamespaceSegment()
        {
            return RoslynClassification is not null
                && RoslynClassification == "namespace name";
        }

        public bool IsGenericTypeArgument()
        {
            return HasAncestorAt(1, SyntaxKind.TypeArgumentList) ||
                HasAncestorAt(2, SyntaxKind.TypeArgumentList);
        }

        public bool IsBaseType()
        {
            return Kind == SyntaxKind.IdentifierToken &&
                HasAncestorAt(1, SyntaxKind.SimpleBaseType);
        }

        public bool IsGenericTypeParameter()
        {
            return RoslynClassification is not null && RoslynClassification == "type parameter name";
        }

        public bool IsNullableType() => HasAncestorAt(1, SyntaxKind.NullableType);

        public bool IsNullableConstraintType()
        {
            return IsTypeConstraint() && NextToken?.Text == "?";
        }

        public bool IsGenericType() => HasAncestorAt(0, SyntaxKind.GenericName);

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
        private static NodeKindChain GetAncestorKindChain(SyntaxToken roslynToken)
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

            return new NodeKindChain(
                parentKind,
                grandParentKind,
                greatGrandParentKind,
                greatGreatGrandParentKind);
        }

        /// <summary>Gets data for NavToken properites related to C# semantics.</summary>
        /// <param name="semanticModel">The semantic model generated from the source code.</param>
        /// <param name="node">The node the semantic data is reffering to.</param>
        /// <returns>The semantic data for the passed in node.</returns>
        private static TokenSemanticData? GetSemanticData(SemanticModel semanticModel, SyntaxNode? node)
        {
            if (node is null)
            {
                return null;
            }

            TokenSemanticData semanticData = new();
            var symbolInfo = semanticModel.GetSymbolInfo(node);

            if (symbolInfo.Symbol != null)
            {
                semanticData.Symbol = symbolInfo.Symbol;
                semanticData.SymbolName = symbolInfo.Symbol.Name;
                semanticData.SymbolKind = symbolInfo.Symbol.Kind.ToString();
                semanticData.ContainingType = symbolInfo.Symbol.ContainingType?.ToString() ?? null;
                semanticData.ContainingNamespace = symbolInfo.Symbol.ContainingNamespace?.ToString() ?? null;
            }

            var typeInfo = semanticModel.GetTypeInfo(node);
            if (typeInfo.Type != null)
            {
                semanticData.TypeName = typeInfo.Type.Name;
                semanticData.TypeKind = typeInfo.Type.Kind.ToString();
                semanticData.IsNullable = typeInfo.Nullability.FlowState == NullableFlowState.MaybeNull;
            }

            return semanticData;
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

        /// <summary>Tries to get the field symbol for the passed in syntax token.</summary>
        /// <param name="semanticModel">The semantic model generated from the source code.</param>
        /// <param name="roslynToken">The SyntaxToken generated by the Roslyn code analysis library.</param>
        /// <returns>The field symbol of the SyntaxToken.</returns>
        private static IFieldSymbol? TryGetFieldSymbol(SemanticModel semanticModel, SyntaxToken roslynToken)
        {
            var declarator = roslynToken.Parent?.AncestorsAndSelf()
                .OfType<VariableDeclaratorSyntax>()
                .FirstOrDefault();

            if (declarator != null && declarator.Identifier == roslynToken)
            {
                return semanticModel.GetDeclaredSymbol(declarator) as IFieldSymbol;
            }

            var identifierName = roslynToken.Parent?.AncestorsAndSelf()
                .OfType<IdentifierNameSyntax>()
                .FirstOrDefault();

            if (identifierName != null && identifierName.Identifier == roslynToken)
            {
                return semanticModel.GetSymbolInfo(identifierName).Symbol as IFieldSymbol;
            }

            var memberAccess = roslynToken.Parent?.AncestorsAndSelf()
                .OfType<MemberAccessExpressionSyntax>()
                .FirstOrDefault();

            if (memberAccess != null
                && memberAccess.Name is IdentifierNameSyntax nameId
                && nameId.Identifier == roslynToken)
            {
                return semanticModel.GetSymbolInfo(memberAccess).Symbol as IFieldSymbol;
            }

            return null;
        }
    }
}
