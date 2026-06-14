using csharp_cartographer_backend._01.Configuration.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public partial class NavToken
    {
        public bool IsGenericType()
        {
            return !IsMethodInvocation()
                && Ancestors.HasAncestorAt(0, SyntaxKind.GenericName);
        }

        #region ------------------- Role Checks --------------------

        /// non-grouped
        public bool IsAnonymousObjectElement()
        {
            bool validPrev = PrevToken?.Text is "{" or ",";
            bool validNext = NextToken?.Text is "," or "}";
            bool validAncestors = Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName)
                && Ancestors.HasAncestorAt(1, SyntaxKind.AnonymousObjectMemberDeclarator)
                && Ancestors.HasAncestorAt(2, SyntaxKind.AnonymousObjectCreationExpression);

            return validPrev && validNext && validAncestors;
        }

        public bool IsArgument()
        {
            // check if covered by other role first
            if (IsIndexValue() || IsDefaultOperand() || IsNameOfOperand() || IsSizeOfOperand() || IsTypeOfOperand())
                return false;

            // single token identifiers, literals
            bool validParent = Ancestors.GetParent()
                is SyntaxKind.IdentifierName
                or SyntaxKind.NumericLiteralExpression
                or SyntaxKind.StringLiteralExpression
                or SyntaxKind.CharacterLiteralExpression
                or SyntaxKind.TrueLiteralExpression
                or SyntaxKind.FalseLiteralExpression
                or SyntaxKind.NullLiteralExpression
                or SyntaxKind.DefaultLiteralExpression;

            bool validGrandParent = Ancestors.HasAncestorAt(1, SyntaxKind.Argument);
            bool validGreatGrandParent = !Ancestors.HasAncestorAt(2, SyntaxKind.TupleExpression);

            return validParent && validGrandParent && validGreatGrandParent;
        }

        public bool IsAssignmentValue()
        {
            return PrevToken?.IsAssignmentOperator() == true
                && (IsStandardAssignmentValue() || IsNullForgivenValue() || IsAttributePropertyAssignmentValue());

            bool IsStandardAssignmentValue()
            {
                bool validKind = Kind is not SyntaxKind.RefKeyword;
                bool validGrandParent = Ancestors.GetGrandParent()
                    is SyntaxKind.EqualsValueClause
                    or SyntaxKind.SimpleAssignmentExpression
                    or SyntaxKind.AddAssignmentExpression
                    or SyntaxKind.SubtractAssignmentExpression
                    or SyntaxKind.MultiplyAssignmentExpression
                    or SyntaxKind.DivideAssignmentExpression
                    or SyntaxKind.ModuloAssignmentExpression
                    or SyntaxKind.AndAssignmentExpression
                    or SyntaxKind.OrAssignmentExpression
                    or SyntaxKind.ExclusiveOrAssignmentExpression
                    or SyntaxKind.LeftShiftAssignmentExpression
                    or SyntaxKind.RightShiftAssignmentExpression
                    or SyntaxKind.UnsignedRightShiftAssignmentExpression
                    or SyntaxKind.AnonymousObjectMemberDeclarator;

                return validKind && validGrandParent;
            }

            bool IsNullForgivenValue()
            {
                bool validGrandParent = Ancestors.HasAncestorAt(1, SyntaxKind.SuppressNullableWarningExpression);
                bool validGreatGrandParent = Ancestors.GetGreatGrandParent()
                    is SyntaxKind.EqualsValueClause
                    or SyntaxKind.SimpleAssignmentExpression
                    or SyntaxKind.AddAssignmentExpression
                    or SyntaxKind.SubtractAssignmentExpression
                    or SyntaxKind.MultiplyAssignmentExpression
                    or SyntaxKind.DivideAssignmentExpression
                    or SyntaxKind.ModuloAssignmentExpression
                    or SyntaxKind.AndAssignmentExpression
                    or SyntaxKind.OrAssignmentExpression
                    or SyntaxKind.ExclusiveOrAssignmentExpression
                    or SyntaxKind.LeftShiftAssignmentExpression
                    or SyntaxKind.RightShiftAssignmentExpression
                    or SyntaxKind.UnsignedRightShiftAssignmentExpression
                    or SyntaxKind.AnonymousObjectMemberDeclarator;

                return validGrandParent && validGreatGrandParent;
            }

            bool IsAttributePropertyAssignmentValue()
            {
                return Ancestors.HasAncestorAt(1, SyntaxKind.AttributeArgument);
            }
        }

        public bool IsAttributeArgument()
        {
            bool validPrev = PrevToken?.Kind
                is SyntaxKind.CommaToken
                or SyntaxKind.OpenParenToken;

            return validPrev
                && Ancestors.HasAncestorAt(1, SyntaxKind.AttributeArgument);
        }

        public bool IsCastTarget()
        {
            return IsExplicitCast() || IsSafeCast();

            bool IsExplicitCast()
            {
                return PrevToken?.Kind == SyntaxKind.CloseParenToken
                    && Ancestors.HasAncestorAt(1, SyntaxKind.CastExpression);
            }

            bool IsSafeCast()
            {
                return NextToken?.Kind == SyntaxKind.AsKeyword
                    && Ancestors.HasAncestorAt(1, SyntaxKind.AsExpression);
            }
        }

        public bool IsCollectionElement()
        {
            bool validKind = IsKeyword() || IsLiteral() || IsIdentifier();
            bool validSyntax = IsArrayInitializerSyntax()
                || IsCollectionExpressionSyntax()
                || IsCollectionInitializerSyntax()
                || IsComplexCollectionInitializerSyntax();

            return validKind && validSyntax;

            /// int[] nums = { 10, 20, 30, 40, 50 };
            /// int[] nums = new[] { 10, 20, 30, 40, 50 };
            /// int[] nums = new int[] { 10, 20, 30, 40, 50 };
            bool IsArrayInitializerSyntax()
            {
                return Ancestors.HasAncestorAt(1, SyntaxKind.ArrayInitializerExpression)
                    && Ancestors.GetGreatGrandParent()
                        is SyntaxKind.EqualsValueClause
                        or SyntaxKind.ArrayCreationExpression
                        or SyntaxKind.ImplicitArrayCreationExpression;
            }

            /// int[] nums = [10, 20, 30, 40, 50];
            bool IsCollectionExpressionSyntax()
            {
                return Ancestors.HasAncestorAt(1, SyntaxKind.ExpressionElement)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.CollectionExpression);
            }

            /// var list = new() { 1, 2, 3 };
            /// var list = new List<int> { 1, 2, 3 };
            /// var list = new List<int> { { 1 }, { 2 }, { 3 } };
            bool IsCollectionInitializerSyntax()
            {
                return Ancestors.HasAncestorAt(1, SyntaxKind.CollectionInitializerExpression)
                    && Ancestors.GetGreatGrandParent()
                        is SyntaxKind.ObjectCreationExpression
                        or SyntaxKind.ImplicitObjectCreationExpression
                        or SyntaxKind.CollectionInitializerExpression;
            }

            /// var list = new List<int> { { 1 }, { 2 }, { 3 } };
            bool IsComplexCollectionInitializerSyntax()
            {
                bool validNeighbors =
                    PrevToken?.Kind != SyntaxKind.CommaToken &&
                    NextToken?.Kind != SyntaxKind.CommaToken;

                return validNeighbors
                    && Ancestors.HasAncestorAt(1, SyntaxKind.ComplexElementInitializerExpression)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.CollectionInitializerExpression);
            }
        }

        public bool IsCollectionElementKey()
        {
            /// var list = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };

            return NextToken?.Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.ComplexElementInitializerExpression)
                && Ancestors.HasAncestorAt(2, SyntaxKind.CollectionInitializerExpression);
        }

        public bool IsCollectionElementValue()
        {
            /// var list = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };

            return PrevToken?.Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.ComplexElementInitializerExpression)
                && Ancestors.HasAncestorAt(2, SyntaxKind.CollectionInitializerExpression);
        }

        public bool IsCollectionLength()
        {
            return IsArrayLength() || IsStackallocLength();

            /// int[] ar = new int[5];
            bool IsArrayLength()
            {
                return Ancestors.HasAncestorAt(1, SyntaxKind.ArrayRankSpecifier)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.ArrayType)
                    && Ancestors.HasAncestorAt(3, SyntaxKind.ArrayCreationExpression);
            }

            /// Span<int> span = stackalloc int[5];
            bool IsStackallocLength()
            {
                return Ancestors.HasAncestorAt(1, SyntaxKind.ArrayRankSpecifier)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.ArrayType)
                    && Ancestors.HasAncestorAt(3, SyntaxKind.StackAllocArrayCreationExpression);
            }
        }

        public bool IsIndexValue()
        {
            return IsSingleTokenIndex()
                || IsIndexWithIndexFromEnd()
                || IsIndexWithRange();

            /// int firstIndex = numbersArray[0];
            bool IsSingleTokenIndex()
            {
                return PrevToken?.Text == "["
                    && NextToken?.Text == "]"
                    && Ancestors.HasAncestorAt(1, SyntaxKind.Argument);
            }

            // TODO: factor out the below methods into new role (don't fit here)

            /// int indexDemo = numbersArray[^1];
            bool IsIndexWithIndexFromEnd()
            {
                return PrevToken?.Text == "^"
                    && Ancestors.HasAncestorAt(1, SyntaxKind.IndexExpression);
            }

            /// int[] toEnd = numbersArray[1..];
            bool IsIndexWithRange()
            {
                return NextToken?.Text == ".."
                    && Ancestors.HasAncestorAt(1, SyntaxKind.RangeExpression);
            }
        }

        public bool IsInterpolatedValue()
        {
            return PrevToken?.Text == "{"
                && NextToken?.Text == "}"
                && Ancestors.HasAncestorAt(1, SyntaxKind.Interpolation);
        }

        public bool IsNullCoalescingAssignmentValue()
        {
            return PrevToken?.Text == "??="
                && Ancestors.HasAncestorAt(1, SyntaxKind.CoalesceAssignmentExpression);
        }

        public bool IsQueryReturnValue()
        {
            bool validPrev = PrevToken?.Text == "select";
            bool validNext = NextToken?.Text is ";" or ")";
            bool validGrandParent = Ancestors.HasAncestorAt(1, SyntaxKind.SelectClause);

            return validPrev && validNext && validGrandParent;
        }

        public bool IsReturnValue()
        {
            bool validToken = Kind == SyntaxKind.IdentifierToken
                || Kind == SyntaxKind.DefaultKeyword
                || SyntaxFacts.IsLiteralExpression(Kind);

            bool validGrandParent = Ancestors.GetGrandParent()
                is SyntaxKind.ArrowExpressionClause
                or SyntaxKind.ReturnStatement
                or SyntaxKind.YieldReturnStatement;

            return validToken && validGrandParent;
        }

        public bool IsSwitchArmValue()
        {
            // covers switch expressions
            return Ancestors.HasAncestorAt(1, SyntaxKind.SwitchExpressionArm);
        }

        public bool IsSwitchMatchTarget()
        {
            return IsExpressionSyntaxTarget() || IsStatementSyntaxTarget();

            /// value switch {
            bool IsExpressionSyntaxTarget()
            {
                return NextToken?.Kind == SyntaxKind.SwitchKeyword
                    && Ancestors.HasAncestorAt(1, SyntaxKind.SwitchExpression);
            }

            /// switch (input) {
            bool IsStatementSyntaxTarget()
            {
                return PrevToken?.Text == "("
                    && NextToken?.Text == ")"
                    && Ancestors.HasAncestorAt(1, SyntaxKind.SwitchStatement);
            }
        }

        /// Operands
        public bool IsAddressOfOperand()
        {
            return PrevToken?.Text == "&"
                && Ancestors.HasAncestorAt(1, SyntaxKind.AddressOfExpression);
        }

        public bool IsArithmeticOperand()
        {
            // covered by string concatenation
            if (IsStringLiteral())
                return false;

            return Ancestors.GetGrandParent()
                is SyntaxKind.AddExpression
                or SyntaxKind.SubtractExpression
                or SyntaxKind.MultiplyExpression
                or SyntaxKind.DivideExpression
                or SyntaxKind.ModuloExpression
                or SyntaxKind.UnaryPlusExpression
                or SyntaxKind.UnaryMinusExpression
                or SyntaxKind.PostIncrementExpression
                or SyntaxKind.PostDecrementExpression
                or SyntaxKind.PreIncrementExpression
                or SyntaxKind.PreDecrementExpression;
        }

        public bool IsBitwiseOperand()
        {
            return Ancestors.GetGrandParent()
                is SyntaxKind.BitwiseAndExpression
                or SyntaxKind.BitwiseOrExpression
                or SyntaxKind.ExclusiveOrExpression
                or SyntaxKind.BitwiseNotExpression;
        }

        public bool IsComparisonOperand()
        {
            return Ancestors.GetGrandParent()
                is SyntaxKind.EqualsExpression
                or SyntaxKind.NotEqualsExpression
                or SyntaxKind.GreaterThanExpression
                or SyntaxKind.LessThanExpression
                or SyntaxKind.GreaterThanOrEqualExpression
                or SyntaxKind.LessThanOrEqualExpression;
        }

        public bool IsConcatenationOperand()
        {
            return Kind == SyntaxKind.StringLiteralToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.AddExpression);
        }

        public bool IsDefaultOperand()
        {
            return Ancestors.HasAncestorAt(1, SyntaxKind.DefaultExpression);
        }

        public bool IsDereferenceOperand()
        {
            return PrevToken?.Kind == SyntaxKind.AsteriskToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.PointerIndirectionExpression);
        }

        public bool IsLogicalOperand()
        {
            return Ancestors.GetGrandParent()
                is SyntaxKind.LogicalAndExpression
                or SyntaxKind.LogicalOrExpression
                or SyntaxKind.LogicalNotExpression;
        }

        public bool IsNameOfOperand()
        {
            return PrevToken?.PrevToken?.Text == "nameof"
                && Ancestors.HasAncestorAt(1, SyntaxKind.Argument);
        }

        public bool IsNullCoalescingFallback()
        {
            return PrevToken?.Text == "??"
                && Ancestors.HasAncestorAt(1, SyntaxKind.CoalesceExpression);
        }

        public bool IsNullCoalescingTarget()
        {
            return NextToken?.Text == "??"
                && Ancestors.HasAncestorAt(1, SyntaxKind.CoalesceExpression);
        }

        public bool IsNullForgivingOperand()
        {
            return NextToken?.Text == "!"
                && Ancestors.HasAncestorAt(1, SyntaxKind.SuppressNullableWarningExpression);
        }

        public bool IsShiftOperand()
        {
            return Ancestors.GetGrandParent()
                is SyntaxKind.LeftShiftExpression
                or SyntaxKind.RightShiftExpression
                or SyntaxKind.UnsignedRightShiftExpression;
        }

        public bool IsSizeOfOperand()
        {
            return Ancestors.HasAncestorAt(1, SyntaxKind.SizeOfExpression);
        }

        public bool IsTernaryFalseValue()
        {
            return PrevToken?.Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.ConditionalExpression);
        }

        public bool IsTernaryTrueValue()
        {
            return PrevToken?.Kind == SyntaxKind.QuestionToken
                && NextToken?.Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.ConditionalExpression);
        }

        public bool IsTypeOfOperand()
        {
            return Ancestors.HasAncestorAt(1, SyntaxKind.TypeOfExpression);
        }

        /// Pattern matching
        public bool IsConstantPattern()
        {
            return IsStandardPattern() || IsSwitchConstantPattern();

            bool IsStandardPattern()
            {
                return Ancestors.HasAncestorAt(1, SyntaxKind.ConstantPattern);
            }

            bool IsSwitchConstantPattern()
            {
                bool validAncestor = Ancestors.HasAncestorAt(1, SyntaxKind.CaseSwitchLabel);

                bool isLiteralConstant = Kind
                    is SyntaxKind.NullKeyword
                    or SyntaxKind.TrueKeyword
                    or SyntaxKind.FalseKeyword
                    or SyntaxKind.NumericLiteralToken
                    or SyntaxKind.StringLiteralToken
                    or SyntaxKind.CharacterLiteralToken;

                bool isIdentifierConstant = Kind is SyntaxKind.IdentifierToken
                    && PrevToken?.Kind == SyntaxKind.CaseKeyword
                    && NextToken?.Kind == SyntaxKind.ColonToken;

                return validAncestor && (isLiteralConstant || isIdentifierConstant);
            }
        }

        public bool IsPatternBindingVariable()
        {
            bool validParent = Ancestors.HasAncestorAt(0, SyntaxKind.SingleVariableDesignation);
            bool validGrandParent = Ancestors.GetGrandParent()
                is SyntaxKind.IsPatternExpression
                or SyntaxKind.RecursivePattern
                or SyntaxKind.DeclarationPattern
                or SyntaxKind.VarPattern;

            return validParent && validGrandParent;
        }

        public bool IsPatternMatchTarget()
        {
            bool validNext = NextToken?.Kind == SyntaxKind.IsKeyword;

            bool validGrandParent = Ancestors.GetGrandParent()
                is SyntaxKind.IsExpression
                or SyntaxKind.IsPatternExpression;

            return validNext && validGrandParent;
        }

        public bool IsPropertyPattern()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName)
                && Ancestors.HasAncestorAt(1, SyntaxKind.NameColon)
                && Ancestors.HasAncestorAt(2, SyntaxKind.Subpattern)
                && Ancestors.HasAncestorAt(3, SyntaxKind.PropertyPatternClause);
        }

        public bool IsRelationalPattern()
        {
            bool validPrev = PrevToken?.IsOperator() ?? false;
            return validPrev && Ancestors.HasAncestorAt(1, SyntaxKind.RelationalPattern);
        }

        public bool IsTypePattern()
        {
            return IsStandardTypePattern() || IsSwitchTypePattern();

            bool IsStandardTypePattern()
            {
                /// _ = obj is string;
                /// _ = numericObj is int or long;
                /// _ = obj is string s;
                /// items is [int, string, double]
                /// if (tuple is (int a2, int b2) t)

                bool validPrev = PrevToken?.Kind
                    is SyntaxKind.IsKeyword
                    or SyntaxKind.OrKeyword
                    or SyntaxKind.OpenBraceToken
                    or SyntaxKind.OpenBracketToken
                    or SyntaxKind.OpenParenToken
                    or SyntaxKind.CommaToken;

                bool validGrandParent = Ancestors.GetGrandParent()
                    is SyntaxKind.IsExpression
                    or SyntaxKind.TypePattern
                    or SyntaxKind.DeclarationPattern;

                return validPrev && validGrandParent;
            }

            bool IsSwitchTypePattern()
            {
                /// case int:
                /// case Person { Age: >= 18 } adult:
                /// case Person p when p.Age < 18:

                return PrevToken?.Kind == SyntaxKind.CaseKeyword
                    && Ancestors.HasAncestorAt(2, SyntaxKind.CasePatternSwitchLabel);
            }
        }

        public bool IsVarPattern()
        {
            return Kind == SyntaxKind.VarKeyword
                && Ancestors.HasAncestorAt(0, SyntaxKind.VarPattern);
        }

        /// Qualifiers
        public bool IsLiteralQualifier()
        {
            // under misc because "true" and "false" are classified as keywords
            bool validNext = NextToken?.Kind
                is SyntaxKind.DotToken
                or SyntaxKind.ExclamationToken
                or SyntaxKind.QuestionToken;

            return IsLiteral() && validNext;
        }

        public bool IsTypeQualifier()
        {
            if (IsInstanceQualifier())
                return false;

            /// [standard]   Guid.NewGuid();
            /// [standard]   Console.WriteLine(text);
            /// [standard]   !string.IsNullOrEmpty(parentKind)
            /// [generic]    ActionResponse<Artifact>.Failure("Error")
            /// [namespace]  System.Console.WriteLine(text);
            /// [expression] bool IsIdentifier() => Kind is SyntaxKind.IdentifierToken;

            return IsStandardTypeQualifier()
                || IsGenericTypeQualifier()
                || IsNamespaceQualifiedType()
                || IsExpressionBodiedTypeQualifier();

            bool IsStandardTypeQualifier()
            {
                bool validNext = NextToken?.Kind == SyntaxKind.DotToken;
                bool validParent = Ancestors.GetParent()
                    is SyntaxKind.IdentifierName
                    or SyntaxKind.PredefinedType;
                bool validGrandParent = Ancestors.HasAncestorAt(1, SyntaxKind.SimpleMemberAccessExpression);
                bool validSemanticData = SemanticData?.SymbolKind != SymbolKind.Namespace;
                bool validClassification = Classifications.Corrected != PropertyName;

                return validNext
                    && validParent
                    && validGrandParent
                    && validSemanticData
                    && validClassification;
            }

            bool IsExpressionBodiedTypeQualifier()
            {
                bool validNext = NextToken?.Kind == SyntaxKind.DotToken;
                bool validSemanticData = SemanticData?.SymbolKind != SymbolKind.Namespace;
                bool validClassification = Classifications.Corrected != PropertyName;
                bool validAncestors = Ancestors.GetParent() is SyntaxKind.IdentifierName or SyntaxKind.PredefinedType
                    && Ancestors.GetGrandParent() is SyntaxKind.QualifiedName
                    && Ancestors.GetLastAncestor() is not SyntaxKind.UsingDirective;

                return validNext
                    && validAncestors
                    && validSemanticData
                    && validClassification;
            }

            bool IsGenericTypeQualifier()
            {
                return NextToken?.Kind == SyntaxKind.LessThanToken
                    && Ancestors.HasAncestorAt(0, SyntaxKind.GenericName)
                    && Ancestors.HasAncestorAt(1, SyntaxKind.SimpleMemberAccessExpression);
            }

            bool IsNamespaceQualifiedType()
            {
                return PrevToken?.Kind == SyntaxKind.DotToken
                    && Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName)
                    && Ancestors.HasAncestorAt(1, SyntaxKind.SimpleMemberAccessExpression)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.SimpleMemberAccessExpression)
                    && SemanticData?.SymbolKind != SymbolKind.Namespace
                    && PrevToken?.PrevToken?.SemanticRole
                        is SemanticRole.NamespaceQualifier
                        or SemanticRole.AliasQualifier;
            }
        }

        /// Types
        public bool IsArrayBaseType()
        {
            return Ancestors.HasAncestorAt(1, SyntaxKind.ArrayType);
        }

        public bool IsDeconstructionVariableType()
        {
            /// var (id, name) = GetUser();
            /// (int id2, string name2) = GetUser();

            bool validPrev = PrevToken?.Kind is SyntaxKind.OpenParenToken or SyntaxKind.CommaToken;
            bool validNext = NextToken?.Kind is SyntaxKind.OpenParenToken;
            bool validGrandParent = Ancestors.HasAncestorAt(1, SyntaxKind.DeclarationExpression);

            return (validPrev || validNext) && validGrandParent;
        }

        public bool IsCastType()
        {
            return IsSafeCast() || IsExplicitCast();

            bool IsSafeCast()
            {
                return PrevToken?.Kind == SyntaxKind.AsKeyword
                    && Ancestors.HasAncestorAt(1, SyntaxKind.AsExpression);
            }

            bool IsExplicitCast()
            {
                return PrevToken?.Kind == SyntaxKind.OpenParenToken
                    && NextToken?.Kind == SyntaxKind.CloseParenToken
                    && Ancestors.HasAncestorAt(1, SyntaxKind.CastExpression);
            }
        }

        public bool IsConversionTargetType()
        {
            return Ancestors.HasAncestorAt(1, SyntaxKind.ConversionOperatorDeclaration);
        }

        public bool IsDelegateReturnType()
        {
            if (IsTupleElementName() || IsTupleElementType())
                return false;

            return Ancestors.HasAncestorAt(1, SyntaxKind.DelegateDeclaration)
                || Ancestors.HasAncestorAt(2, SyntaxKind.DelegateDeclaration);
        }

        public bool IsFieldType()
        {
            bool isFieldDecl =
                Ancestors.HasAncestorAt(2, SyntaxKind.FieldDeclaration) ||
                Ancestors.HasAncestorAt(3, SyntaxKind.FieldDeclaration);

            return isFieldDecl && !IsPointerBaseType();
        }

        public bool IsForLoopIteratorType()
        {
            return IsForLoopIteratorType() || IsNullableLoopIteratorType();

            bool IsForLoopIteratorType()
            {
                var validPrev = PrevToken?.Kind == SyntaxKind.OpenParenToken;
                var validParent = Ancestors.GetParent()
                    is SyntaxKind.PredefinedType
                    or SyntaxKind.IdentifierName;
                var validGrandParent = Ancestors.HasAncestorAt(1, SyntaxKind.VariableDeclaration);
                var validGreatGrandParent = Ancestors.HasAncestorAt(2, SyntaxKind.ForStatement);

                return validPrev
                    && validParent
                    && validGrandParent
                    && validGreatGrandParent;
            }

            bool IsNullableLoopIteratorType()
            {
                return PrevToken?.Kind == SyntaxKind.OpenParenToken
                    && Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName)
                    && Ancestors.HasAncestorAt(1, SyntaxKind.NullableType)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.VariableDeclaration)
                    && Ancestors.HasAncestorAt(3, SyntaxKind.ForStatement);
            }
        }

        public bool IsForEachLoopIteratorType()
        {
            return IsForEachLoopIteratorType() || IsNullableForEachLoopIteratorType();

            bool IsForEachLoopIteratorType()
            {
                return NextToken?.Kind != SyntaxKind.CloseParenToken
                    && Ancestors.HasAncestorAt(1, SyntaxKind.ForEachStatement);
            }

            bool IsNullableForEachLoopIteratorType()
            {
                return NextToken?.Kind != SyntaxKind.CloseParenToken
                    && Ancestors.HasAncestorAt(1, SyntaxKind.NullableType)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.ForEachStatement);
            }
        }

        public bool IsGenericTypeArgument()
        {
            return IsNonNullableTypeArgument() || IsNullableTypeArgument();

            /// List<string> LeadingTrivia
            /// Func<int, int> square = x =>
            /// new Dictionary<string, List<int>>
            /// Task<ActionResponse<Artifact>> GenerateUserArtifact
            bool IsNonNullableTypeArgument()
            {
                bool validPrev = PrevToken?.Kind
                    is SyntaxKind.LessThanToken
                    or SyntaxKind.CommaToken;

                bool validNext = NextToken?.Kind
                    is SyntaxKind.LessThanToken
                    or SyntaxKind.GreaterThanToken
                    or SyntaxKind.CommaToken;

                bool validAncestor = Ancestors.HasAncestorAt(1, SyntaxKind.TypeArgumentList);

                return validPrev && validNext && validAncestor;
            }

            /// List<string?> LeadingTrivia
            /// Func<int?, int> square = x =>
            /// Func<int, int?> square = x =>
            bool IsNullableTypeArgument()
            {
                bool validPrev = PrevToken?.Kind
                    is SyntaxKind.LessThanToken
                    or SyntaxKind.CommaToken;

                bool validNext = NextToken?.Kind
                    is SyntaxKind.QuestionToken
                    or SyntaxKind.CommaToken;

                bool validAncestor = Ancestors.HasAncestorAt(2, SyntaxKind.TypeArgumentList);

                return validPrev && validNext && validAncestor;
            }
        }

        public bool IsGenericTypeParameter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.TypeParameter);
        }

        public bool IsLocalFunctionReturnType()
        {
            return Ancestors.HasAncestorAt(1, SyntaxKind.LocalFunctionStatement);
        }

        public bool IsLocalVariableType()
        {
            // skip tuple types
            if (Ancestors.HasAncestorAt(0, SyntaxKind.TupleElement))
                return false;

            if (IsArrayBaseType())
                return false;

            if (IsPointerBaseType())
                return false;

            if (IsLocalModifierKeyword())
                return false;

            return IsLocalVariableType()
                || IsUsingStatementVariableType()
                || IsOutArgumentVariableType();

            /// [standard]      string test = "Test String.";
            /// [with modifier] const int Max = 10;
            /// [with modifier] ref int item = ref Ages[0];
            /// [with modifier] ref readonly int item3 = ref Ages[0];
            /// [with modifier] scoped Span<int> buffer = stackalloc int[10];
            bool IsLocalVariableType()
            {
                bool invalidDeclaratorAncestor = Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclarator);

                bool validLocDeclAncestor =
                    Ancestors.HasAncestorAt(2, SyntaxKind.LocalDeclarationStatement) ||
                    Ancestors.HasAncestorAt(3, SyntaxKind.LocalDeclarationStatement);

                return validLocDeclAncestor && !invalidDeclaratorAncestor;
            }

            /// using (var reader = new StreamReader(path))
            bool IsUsingStatementVariableType()
            {
                bool validAncestor =
                    Ancestors.HasAncestorAt(2, SyntaxKind.UsingStatement) ||
                    Ancestors.HasAncestorAt(3, SyntaxKind.UsingStatement);

                return validAncestor;
            }

            /// if (items.TryGetValue(key, out var value))
            bool IsOutArgumentVariableType()
            {
                bool validKind = Kind
                    is SyntaxKind.IdentifierToken
                    or SyntaxKind.BoolKeyword
                    or SyntaxKind.ByteKeyword
                    or SyntaxKind.SByteKeyword
                    or SyntaxKind.CharKeyword
                    or SyntaxKind.DecimalKeyword
                    or SyntaxKind.DoubleKeyword
                    or SyntaxKind.FloatKeyword
                    or SyntaxKind.IntKeyword
                    or SyntaxKind.UIntKeyword
                    or SyntaxKind.LongKeyword
                    or SyntaxKind.ULongKeyword
                    or SyntaxKind.ObjectKeyword
                    or SyntaxKind.ShortKeyword
                    or SyntaxKind.UShortKeyword
                    or SyntaxKind.StringKeyword
                    or SyntaxKind.VarKeyword;

                bool validPrev = PrevToken?.Kind is SyntaxKind.OutKeyword;
                bool validGrandParent = Ancestors.HasAncestorAt(1, SyntaxKind.DeclarationExpression);

                return validKind && validPrev && validGrandParent;
            }
        }

        public bool IsMethodReturnType()
        {
            if (IsTupleElementName() || IsTupleElementType())
                return false;

            var validPrev = PrevToken?.Kind != SyntaxKind.EqualsGreaterThanToken;
            var validNext = NextToken?.Kind != SyntaxKind.SemicolonToken;
            if (!validPrev || !validNext)
                return false;

            var parent = Ancestors.GetParent();
            var grandParent = Ancestors.GetGrandParent();

            if (parent is SyntaxKind.Parameter or SyntaxKind.TypeParameter or SyntaxKind.YieldReturnStatement)
                return false;

            if (grandParent is SyntaxKind.TypeParameterConstraintClause)
                return false;

            return Ancestors.HasAncestorAt(1, SyntaxKind.MethodDeclaration)
                || Ancestors.HasAncestorAt(2, SyntaxKind.MethodDeclaration);
        }

        public bool IsOperatorReturnType()
        {
            return Ancestors.HasAncestorAt(1, SyntaxKind.OperatorDeclaration)
                && NextToken?.Kind == SyntaxKind.OperatorKeyword;
        }

        public bool IsParameterType()
        {
            bool isParamDecl =
                Ancestors.HasAncestorAt(1, SyntaxKind.Parameter) ||
                Ancestors.HasAncestorAt(2, SyntaxKind.Parameter);

            return isParamDecl
                && !IsPointerBaseType()
                && !IsArrayBaseType();
        }

        public bool IsPointerBaseType()
        {
            return NextToken?.Kind == SyntaxKind.AsteriskToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.PointerType);
        }

        public bool IsPropertyType()
        {
            bool isPropDecl =
                Ancestors.HasAncestorAt(1, SyntaxKind.PropertyDeclaration) ||
                Ancestors.HasAncestorAt(2, SyntaxKind.PropertyDeclaration);

            return isPropDecl && !IsPointerBaseType();
        }

        public bool IsTupleElement()
        {
            bool validPrev =
                PrevToken?.Kind
                    is SyntaxKind.OpenParenToken
                    or SyntaxKind.CommaToken
                    && PrevToken?.SemanticRole
                        is SemanticRole.TupleExpressionBoundary
                        or SemanticRole.TupleElementSeparator;

            bool validNext = NextToken?.Kind
                is SyntaxKind.CommaToken
                or SyntaxKind.CloseParenToken;

            bool validAncestors =
                Ancestors.HasAncestorAt(1, SyntaxKind.Argument) &&
                Ancestors.HasAncestorAt(2, SyntaxKind.TupleExpression);

            return validPrev && validNext && validAncestors;
        }

        public bool IsTupleElementType()
        {
            return Ancestors.HasAncestorAt(1, SyntaxKind.TupleElement);
        }

        public bool IsTypeConstraint()
        {
            return IsTypeConstraintIdentifier() || IsTypeConstraintKeyword();

            bool IsTypeConstraintIdentifier()
            {
                bool hasValidAncestors =
                    Ancestors.HasAncestorAt(1, SyntaxKind.TypeParameterConstraintClause) ||
                    Ancestors.HasAncestorAt(2, SyntaxKind.TypeParameterConstraintClause);

                return Kind == SyntaxKind.IdentifierToken && hasValidAncestors;
            }

            bool IsTypeConstraintKeyword()
            {
                return PrimaryKind == PrimaryKind.Keyword
                    && Ancestors.HasAncestorAt(1, SyntaxKind.TypeParameterConstraintClause);
            }
        }

        public bool IsTypeParameterConstraint()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName)
                && Ancestors.HasAncestorAt(1, SyntaxKind.TypeParameterConstraintClause);
        }
        #endregion
    }
}
