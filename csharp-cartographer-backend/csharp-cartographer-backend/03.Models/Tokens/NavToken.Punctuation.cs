using csharp_cartographer_backend._01.Configuration.Enums;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public partial class NavToken
    {
        public bool IsPunctuation() => Kind
            is SyntaxKind.DotToken
            or SyntaxKind.CommaToken
            or SyntaxKind.SemicolonToken
            or SyntaxKind.ColonToken
            or SyntaxKind.ColonColonToken
            or SyntaxKind.QuestionToken
            or SyntaxKind.UnderscoreToken;

        #region ------------------- Role Checks --------------------

        /// Misc
        public bool IsArrayRankIndicator()
        {
            return Kind == SyntaxKind.CommaToken
                && (IsExplictArrayType() || IsImplicitArrayType());

            /// int[,] grid = new int[3, 5];
            bool IsExplictArrayType()
            {
                return Ancestors.HasAncestorAt(0, SyntaxKind.ArrayRankSpecifier)
                    && Ancestors.HasAncestorAt(1, SyntaxKind.ArrayType);
            }

            /// var grid = new[,] { { new { Id = 1 } }, { new { Id = 3 } } };
            bool IsImplicitArrayType()
            {
                return Ancestors.HasAncestorAt(0, SyntaxKind.ImplicitArrayCreationExpression);
            }
        }

        public bool IsNullableTypeMarker()
        {
            bool validKind = Kind == SyntaxKind.QuestionToken;
            bool validAncestor =
                Ancestors.HasAncestorAt(0, SyntaxKind.NullableType) ||
                Ancestors.HasAncestorAt(1, SyntaxKind.TypeParameterConstraintClause);

            return validKind && validAncestor;
        }

        public bool IsPointerTypeIndicator()
        {
            return Kind == SyntaxKind.AsteriskToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.PointerType);
        }

        /// Separators
        public bool IsAnonymousObjectMemberDeclarationSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.AnonymousObjectCreationExpression);
        }

        public bool IsArgumentSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.GetParent()
                    is SyntaxKind.ArgumentList
                    or SyntaxKind.BracketedArgumentList;
        }

        public bool IsArrayInitializerElementSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ArrayInitializerExpression);
        }

        public bool IsArrayLengthSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ArrayRankSpecifier)
                && Ancestors.HasAncestorAt(1, SyntaxKind.ArrayType)
                && Ancestors.HasAncestorAt(2, SyntaxKind.ArrayCreationExpression);
        }

        public bool IsAttributeArgumentSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.AttributeArgumentList);
        }

        public bool IsBaseTypeSeparator()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.BaseList);
        }

        public bool IsCollectionElementSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.GetParent()
                    is SyntaxKind.CollectionExpression
                    or SyntaxKind.CollectionInitializerExpression;
        }

        public bool IsComplexElementSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ComplexElementInitializerExpression);
        }

        public bool IsConstraintSeparator()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.TypeParameterConstraintClause);
        }

        public bool IsDeconstructionVariableSeparator()
        {
            bool validKind = Kind == SyntaxKind.CommaToken;

            bool validPrev = PrevToken?.SemanticRole
                is SemanticRole.DeconstructionVariable
                or SemanticRole.DiscardValue;

            bool validParent = Ancestors.GetParent()
                is SyntaxKind.TupleExpression
                or SyntaxKind.ParenthesizedVariableDesignation;

            return validKind && validPrev && validParent;
        }

        public bool IsEnumMemberSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.EnumDeclaration);
        }

        public bool IsInterpolationFormatSeparator()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.InterpolationFormatClause);
        }

        public bool IsMemberPatternSeparator()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.NameColon)
                && Ancestors.HasAncestorAt(1, SyntaxKind.Subpattern);
        }

        public bool IsOrderByClauseSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.OrderByClause);
        }

        public bool IsQualifiedNameSeparator()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.QualifiedName);
        }

        public bool IsParameterSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ParameterList);
        }

        public bool IsPatternElementSeparator()
        {
            bool validKind = Kind == SyntaxKind.CommaToken;

            bool validParent = Ancestors.GetParent()
                is SyntaxKind.ListPattern
                or SyntaxKind.PositionalPatternClause;

            return validKind && validParent;
        }

        public bool IsPropertyInitializationSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ObjectInitializerExpression);
        }

        public bool IsTypeArgumentSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.TypeArgumentList);
        }

        public bool IsTypeParameterSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.TypeParameterList);
        }

        public bool IsSwitchArmSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.SwitchExpression);
        }

        public bool IsTupleElementSeparator()
        {
            /*
             *  Tuple and deconstruction expressions look identical
             *  Check prev token role to distinguish
             */
            if (PrevToken?.SemanticRole == SemanticRole.DeconstructionVariable)
                return false;

            bool validKind = Kind == SyntaxKind.CommaToken;

            bool validParent = Ancestors.GetParent()
                is SyntaxKind.TupleType
                or SyntaxKind.TupleExpression;

            return validKind && validParent;
        }

        public bool IsTypeParameterConstraintClauseSeparator()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.TypeParameterConstraintClause);
        }

        public bool IsVariableDeclaratorSeparator()
        {
            return Kind == SyntaxKind.CommaToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclaration);
        }

        /// Terminators
        public bool IsCaseSwitchLabelTerminator()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.CaseSwitchLabel);
        }

        public bool IsCasePatternSwitchLabelTerminator()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.CasePatternSwitchLabel);
        }

        public bool IsDefaultCaseLabelTerminator()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.DefaultSwitchLabel);
        }

        public bool IsParameterLabelTerminator()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.NameColon)
                && Ancestors.HasAncestorAt(1, SyntaxKind.Argument);
        }

        public bool IsStatementTerminator()
        {
            return Kind == SyntaxKind.SemicolonToken;
        }
        #endregion
    }
}
