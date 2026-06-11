using csharp_cartographer_backend._01.Configuration.Enums;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public partial class NavToken
    {
        public bool IsDelimiter() => Kind
            is SyntaxKind.OpenBraceToken
            or SyntaxKind.OpenBracketToken
            or SyntaxKind.OpenParenToken
            or SyntaxKind.LessThanToken
            or SyntaxKind.CloseBraceToken
            or SyntaxKind.CloseBracketToken
            or SyntaxKind.CloseParenToken
            or SyntaxKind.GreaterThanToken;

        public bool IsBrace() => Kind
            is SyntaxKind.OpenBraceToken
            or SyntaxKind.CloseBraceToken;

        public bool IsBracket() => Kind
            is SyntaxKind.OpenBracketToken
            or SyntaxKind.CloseBracketToken;

        public bool IsClip() => Kind
            is SyntaxKind.LessThanToken
            or SyntaxKind.GreaterThanToken;

        public bool IsParen() => Kind
            is SyntaxKind.OpenParenToken
            or SyntaxKind.CloseParenToken;

        public bool IsOpenDelimiter() => Kind
            is SyntaxKind.OpenBraceToken
            or SyntaxKind.OpenBracketToken
            or SyntaxKind.OpenParenToken
            or SyntaxKind.LessThanToken;

        public bool IsCloseDelimiter() => Kind
            is SyntaxKind.CloseBraceToken
            or SyntaxKind.CloseBracketToken
            or SyntaxKind.CloseParenToken
            or SyntaxKind.GreaterThanToken;

        #region ------------------- Group Checks --------------------
        public bool IsAccessorBlockDelimiter()
        {
            return SemanticRole
                is SemanticRole.AddAccessorBlockBoundary
                or SemanticRole.GetAccessorBlockBoundary
                or SemanticRole.InitAccessorBlockBoundary
                or SemanticRole.RemoveAccessorBlockBoundary
                or SemanticRole.SetAccessorBlockBoundary;
        }

        public bool IsConditionDelimiter()
        {
            return SemanticRole
                is SemanticRole.CatchFilterClauseConditionBoundary
                or SemanticRole.DoWhileConditionBoundary
                or SemanticRole.IfConditionBoundary
                or SemanticRole.SwitchStatementConditionBoundary
                or SemanticRole.WhileLoopConditionBoundary;
        }

        public bool IsContextBlockDelimiter()
        {
            return SemanticRole
                is SemanticRole.CheckedStatementBlockBoundary
                or SemanticRole.FixedStatementBlockBoundary
                or SemanticRole.LockStatementBlockBoundary
                or SemanticRole.UncheckedStatementBlockBoundary
                or SemanticRole.UnsafeStatementBlockBoundary
                or SemanticRole.UsingStatementBlockBoundary;
        }

        public bool IsDeclarationDelimiter()
        {
            return SemanticRole
                is SemanticRole.ClassBoundary
                or SemanticRole.ConstructorBoundary
                or SemanticRole.EnumBoundary
                or SemanticRole.InterfaceBoundary
                or SemanticRole.LocalFunctionBoundary
                or SemanticRole.MethodBoundary
                or SemanticRole.NamespaceBoundary
                or SemanticRole.RecordBoundary
                or SemanticRole.RecordStructBoundary
                or SemanticRole.StructBoundary;
        }

        public bool IsInitializerDelimiter()
        {
            return SemanticRole
                is SemanticRole.AnonymousObjectInitializerBoundary
                or SemanticRole.ArrayInitializerBoundary
                or SemanticRole.CollectionElementInitializerBoundary
                or SemanticRole.CollectionInitializerBoundary
                or SemanticRole.ObjectInitializerBoundary
                or SemanticRole.WithInitializerExpressionBoundary;
        }

        public bool IsLoopBlockDelimiter()
        {
            return SemanticRole
                is SemanticRole.DoWhileLoopBlockBoundary
                or SemanticRole.ForEachLoopBlockBoundary
                or SemanticRole.ForLoopBlockBoundary
                or SemanticRole.WhileLoopBlockBoundary;
        }

        public bool IsLoopControlDelimiter()
        {
            return SemanticRole
                is SemanticRole.ForEachControlBoundary
                or SemanticRole.ForLoopControlBoundary;
        }

        public bool IsPatternMatchingDelimiter()
        {
            return SemanticRole
                is SemanticRole.ListPatternBoundary
                or SemanticRole.ParenthesizedPatternBoundary
                or SemanticRole.PositionalPatternBoundary
                or SemanticRole.PropertyPatternBoundary;
        }

        public bool IsStatementControlDelimiter()
        {
            return SemanticRole
                is SemanticRole.FixedStatementControlBoundary
                or SemanticRole.LockStatementControlBoundary
                or SemanticRole.UsingStatementControlBoundary;
        }

        public bool IsSwitchBlockDelimiter()
        {
            return SemanticRole
                is SemanticRole.SwitchExpressionBlockBoundary
                or SemanticRole.SwitchStatementBlockBoundary;
        }
        #endregion

        #region ------------------- Role Checks --------------------
        public bool IsAccessorListDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.AccessorList);
        }

        public bool IsArrayTypeFragmentDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ArrayRankSpecifier)
                && Ancestors.HasAncestorAt(1, SyntaxKind.ArrayType);
        }

        public bool IsAttributeListDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.AttributeList);
        }

        public bool IsCastTypeDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.CastExpression);
        }

        public bool IsCatchArgumentDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.CatchDeclaration);
        }

        public bool IsCollectionExpressionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.CollectionExpression);
        }

        public bool IsDeconstructionDelimiter()
        {
            return IsNormalDeconstruction() || IsLoopDeconstruction();

            /// var (x, y) = (10, 20);
            bool IsNormalDeconstruction()
            {
                bool isLeftHandSide = false;
                while (NextToken is not null && NextToken.Text != ";")
                {
                    // left-hand side of statement -> deconstruction
                    if (NextToken.Text == "=")
                        isLeftHandSide = true;

                    NextToken = NextToken.NextToken;
                }

                bool validParent = Ancestors.GetParent()
                    is SyntaxKind.ParenthesizedVariableDesignation
                    or SyntaxKind.TupleExpression;

                return isLeftHandSide
                    && validParent;
            }

            /// foreach ((string k, int v) in d)
            /// foreach (var (key, valuetwo) in dict)
            bool IsLoopDeconstruction()
            {
                bool hasValidAncestors1 =
                    Ancestors.HasAncestorAt(0, SyntaxKind.TupleExpression) &&
                    Ancestors.HasAncestorAt(1, SyntaxKind.ForEachVariableStatement);

                bool hasValidAncestors2 =
                    Ancestors.HasAncestorAt(0, SyntaxKind.ParenthesizedVariableDesignation) &&
                    Ancestors.HasAncestorAt(1, SyntaxKind.DeclarationExpression);

                return hasValidAncestors1 || hasValidAncestors2;
            }
        }

        public bool IsDefaultExpressionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.DefaultExpression);
        }

        public bool IsImplicitArrayTypeFragmentDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ImplicitArrayCreationExpression);
        }

        public bool IsInterpolationDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Interpolation);
        }

        public bool IsParenthesizedExpressionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ParenthesizedExpression);
        }

        public bool IsSizeOfExpressionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.SizeOfExpression);
        }

        public bool IsTupleExpressionDelimiter()
        {
            /*
             *  Tuples and deconstruction have a lot of overlap.
             */

            while (NextToken is not null && NextToken.Text != ";")
            {
                // means tokens are on left-hand side of statement -> deconstruction
                if (NextToken.Text == "=")
                    return false;

                NextToken = NextToken.NextToken;
            }

            return Ancestors.HasAncestorAt(0, SyntaxKind.TupleExpression);
        }

        public bool IsTupleTypeDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.TupleType);
        }

        public bool IsTypeOfExpressionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.TypeOfExpression);
        }

        /// AccessorBlock delimiters group
        public bool IsAddAccessorBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.AddAccessorDeclaration);
        }

        public bool IsGetAccessorBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.GetAccessorDeclaration);
        }

        public bool IsInitAccessorBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.InitAccessorDeclaration);
        }

        public bool IsRemoveAccessorBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.RemoveAccessorDeclaration);
        }

        public bool IsSetAccessorBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.SetAccessorDeclaration);
        }

        /// Argument/ParameterList delimiters group
        public bool IsArgumentListDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ArgumentList);
        }

        public bool IsAttributeArgumentListDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.AttributeArgumentList);
        }

        public bool IsIndexArgumentListDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.BracketedArgumentList);
        }

        public bool IsParameterListDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ParameterList);
        }

        public bool IsTypeArgumentListDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.TypeArgumentList);
        }

        public bool IsTypeParameterListDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.TypeParameterList);
        }

        /// Block delimiters group
        public bool IsCatchBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.CatchClause);
        }

        public bool IsElseBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.ElseClause);
        }

        public bool IsIfBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.IfStatement);
        }

        public bool IsLambdaExpressionBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && (Ancestors.HasAncestorAt(1, SyntaxKind.SimpleLambdaExpression) || Ancestors.HasAncestorAt(1, SyntaxKind.ParenthesizedLambdaExpression));
        }

        public bool IsTryBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.TryStatement);
        }

        /// Condition delimiters group
        public bool IsCatchFilterClauseConditionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.CatchFilterClause);
        }

        public bool IsDoWhileConditionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.DoStatement);
        }

        public bool IsIfConditionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.IfStatement);
        }

        public bool IsSwitchStatementConditionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.SwitchStatement);
        }

        public bool IsWhileLoopConditionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.WhileStatement);
        }

        /// ContextBlock delimiters group
        public bool IsCheckedStatementBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.CheckedStatement);
        }

        public bool IsFixedBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.FixedStatement);
        }

        public bool IsLockBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.LockStatement);
        }

        public bool IsUncheckedStatementBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.UncheckedStatement);
        }

        public bool IsUnsafeBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.UnsafeStatement);
        }

        public bool IsUsingStatementBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.UsingStatement);
        }

        /// Control delimiters group
        public bool IsFixedStatementControlDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.FixedStatement);
        }

        public bool IsForEachControlDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ForEachStatement)
                || Ancestors.HasAncestorAt(0, SyntaxKind.ForEachVariableStatement);
        }

        public bool IsForLoopControlDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ForStatement);
        }

        public bool IsLockStatementControlDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.LockStatement);
        }

        public bool IsUsingStatementControlDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.UsingStatement);
        }

        /// Declaration delimiters group
        public bool IsClassDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ClassDeclaration);
        }

        public bool IsConstructorDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.ConstructorDeclaration);
        }

        public bool IsEnumDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.EnumDeclaration);
        }

        public bool IsInterfaceDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.InterfaceDeclaration);
        }

        public bool IsLocalFunctionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.LocalFunctionStatement);
        }

        public bool IsMethodDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.MethodDeclaration);
        }

        public bool IsNamespaceDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.NamespaceDeclaration);
        }

        public bool IsRecordDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.RecordDeclaration);
        }

        public bool IsRecordStructDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.RecordStructDeclaration);
        }

        public bool IsStructDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.StructDeclaration);
        }

        /// Initializer delimiters group
        public bool IsAnonymousObjectInitializerDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.AnonymousObjectCreationExpression);
        }

        public bool IsArrayInitializerDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ArrayInitializerExpression);
        }

        public bool IsCollectionElementInitializerDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ComplexElementInitializerExpression);
        }

        public bool IsCollectionInitializerDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.CollectionInitializerExpression);
        }

        public bool IsObjectInitializerDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ObjectInitializerExpression);
        }

        public bool IsWithInitializerExpressionDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.WithInitializerExpression)
                && Ancestors.HasAncestorAt(1, SyntaxKind.WithExpression);
        }

        /// LoopBlock delimiters group
        public bool IsDoWhileLoopBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.DoStatement);
        }

        public bool IsForEachLoopBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && (Ancestors.HasAncestorAt(1, SyntaxKind.ForEachStatement) || Ancestors.HasAncestorAt(1, SyntaxKind.ForEachVariableStatement));
        }

        public bool IsForLoopBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.ForStatement);
        }

        public bool IsWhileLoopBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Block)
                && Ancestors.HasAncestorAt(1, SyntaxKind.WhileStatement);
        }

        /// PatternMatching delimiters group
        public bool IsListPatternDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ListPattern);
        }

        public bool IsParenthesizedPatternDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ParenthesizedPattern);
        }

        public bool IsPositionalPatternDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.PositionalPatternClause);
        }

        public bool IsPropertyPatternDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.PropertyPatternClause);
        }

        /// SwitchBlock delimiters group
        public bool IsSwitchExpressionBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.SwitchExpression);
        }

        public bool IsSwitchStatementBlockDelimiter()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.SwitchStatement);
        }
        #endregion

        #region ------------------- Key Checks --------------------

        /// Array Types
        public bool IsStandardArrayTypeFragment()
        {
            /// int[] data = [1, 2, 3];

            if (Kind == SyntaxKind.OpenBracketToken)
            {
                return NextToken?.Kind == SyntaxKind.CloseBracketToken
                    && NextToken?.NextToken?.Kind != SyntaxKind.OpenBracketToken
                    && PrevToken?.Kind != SyntaxKind.CloseBracketToken;
            }

            if (Kind == SyntaxKind.CloseBracketToken)
            {
                return PrevToken?.Kind == SyntaxKind.OpenBracketToken
                    && NextToken?.Kind != SyntaxKind.OpenBracketToken
                    && PrevToken?.PrevToken?.Kind != SyntaxKind.CloseBracketToken;
            }

            return false;
        }

        public bool IsJaggedArrayTypeFragment()
        {
            /// int[][] jagged = new int[][];

            if (Kind == SyntaxKind.OpenBracketToken)
            {
                // first
                if (PrevToken?.Kind != SyntaxKind.CloseBracketToken)
                {
                    return NextToken?.Kind == SyntaxKind.CloseBracketToken
                        && NextToken?.NextToken?.Kind == SyntaxKind.OpenBracketToken;
                }

                // second
                if (PrevToken?.Kind == SyntaxKind.CloseBracketToken)
                {
                    return NextToken?.Kind == SyntaxKind.CloseBracketToken
                        && PrevToken?.PrevToken?.Kind == SyntaxKind.OpenBracketToken;
                }
            }

            if (Kind == SyntaxKind.CloseBracketToken)
            {
                return (PrevToken?.Kind == SyntaxKind.OpenBracketToken
                            && NextToken?.Kind == SyntaxKind.OpenBracketToken)
                    || (PrevToken?.Kind == SyntaxKind.OpenBracketToken
                            && PrevToken?.PrevToken?.Kind == SyntaxKind.CloseBracketToken);
            }

            return false;
        }

        public bool IsRectangularArrayTypeFragment()
        {
            /// int[,] grid = new int[3, 4];
            /// int[,,] cube = new int[x, y, z];

            if (Kind == SyntaxKind.OpenBracketToken)
                return NextToken?.Kind == SyntaxKind.CommaToken;

            if (Kind == SyntaxKind.CloseBracketToken)
                return PrevToken?.Kind == SyntaxKind.CommaToken;

            return false;
        }
        #endregion
    }
}
