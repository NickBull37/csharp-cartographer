using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public partial class NavToken
    {
        public bool IsOperator() => Classifications.Corrected == Operator;

        public bool IsShortCircuitOperator()
        {
            /// &&, ||, ??, ??=

            return Kind
                is SyntaxKind.AmpersandAmpersandToken
                or SyntaxKind.BarBarToken
                or SyntaxKind.QuestionQuestionToken
                or SyntaxKind.QuestionQuestionEqualsToken;
        }

        #region ------------------- Role Checks --------------------
        public bool IsArithmeticOperator()
        {
            /// +, -, *, /, %, ++, --

            var validKind = Kind
                is SyntaxKind.PlusToken
                or SyntaxKind.MinusToken
                or SyntaxKind.AsteriskToken
                or SyntaxKind.SlashToken
                or SyntaxKind.PercentToken
                or SyntaxKind.PlusPlusToken
                or SyntaxKind.MinusMinusToken;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.AddExpression
                or SyntaxKind.SubtractExpression
                or SyntaxKind.MultiplyExpression
                or SyntaxKind.DivideExpression
                or SyntaxKind.ModuloExpression
                or SyntaxKind.PreIncrementExpression
                or SyntaxKind.PreDecrementExpression
                or SyntaxKind.PostIncrementExpression
                or SyntaxKind.PostDecrementExpression
                or SyntaxKind.UnaryPlusExpression
                or SyntaxKind.UnaryMinusExpression;

            return validKind && validParent;
        }

        public bool IsAssignmentOperator()
        {
            // =, +=, -=, *=, /=, %=, &=, |=, ^=, <<=, >>=, >>>=, ??=

            return Kind
                is SyntaxKind.EqualsToken
                or SyntaxKind.PlusEqualsToken
                or SyntaxKind.MinusEqualsToken
                or SyntaxKind.AsteriskEqualsToken
                or SyntaxKind.SlashEqualsToken
                or SyntaxKind.PercentEqualsToken
                or SyntaxKind.AmpersandEqualsToken
                or SyntaxKind.BarEqualsToken
                or SyntaxKind.CaretEqualsToken
                or SyntaxKind.LessThanLessThanEqualsToken
                or SyntaxKind.GreaterThanGreaterThanEqualsToken
                or SyntaxKind.GreaterThanGreaterThanGreaterThanEqualsToken
                or SyntaxKind.QuestionQuestionEqualsToken;
        }

        public bool IsBitwiseOperator()
        {
            /// &, |, ^, ~

            var validKind = Kind
                is SyntaxKind.AmpersandToken
                or SyntaxKind.BarToken
                or SyntaxKind.CaretToken
                or SyntaxKind.TildeToken;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.BitwiseAndExpression
                or SyntaxKind.BitwiseOrExpression
                or SyntaxKind.ExclusiveOrExpression
                or SyntaxKind.BitwiseNotExpression;

            return validKind && validParent;
        }

        public bool IsBooleanLogicalOperator()
        {
            // currently doesn't cover non-short-circuit
            // boolean logical operators (&, |, ^) that
            // overlap with bitwise operators

            /// !, &, |, ^, &&, ||

            var validKind = Kind
                is SyntaxKind.ExclamationToken
                or SyntaxKind.AmpersandToken
                or SyntaxKind.BarToken
                or SyntaxKind.CaretToken
                or SyntaxKind.AmpersandAmpersandToken
                or SyntaxKind.BarBarToken;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.LogicalNotExpression
                or SyntaxKind.LogicalAndExpression
                or SyntaxKind.LogicalOrExpression;

            return validKind && validParent;
        }

        public bool IsComparisonOperator()
        {
            /// <, >, <=, >=

            var validKind = Kind
                is SyntaxKind.LessThanToken
                or SyntaxKind.GreaterThanToken
                or SyntaxKind.LessThanEqualsToken
                or SyntaxKind.GreaterThanEqualsToken;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.LessThanExpression
                or SyntaxKind.GreaterThanExpression
                or SyntaxKind.LessThanOrEqualExpression
                or SyntaxKind.GreaterThanOrEqualExpression
                or SyntaxKind.RelationalPattern;

            return validKind && validParent;
        }

        public bool IsEqualityOperator()
        {
            /// ==, !=

            var validKind = Kind
                is SyntaxKind.EqualsEqualsToken
                or SyntaxKind.ExclamationEqualsToken;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.EqualsExpression
                or SyntaxKind.NotEqualsExpression;

            return validKind && validParent;
        }

        public bool IsExpressionBodyArrow()
        {
            /// =>

            return Kind == SyntaxKind.EqualsGreaterThanToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ArrowExpressionClause);
        }

        public bool IsIndexFromEndOperator()
        {
            /// ^

            return Kind == SyntaxKind.CaretToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.IndexExpression);
        }

        public bool IsIndirectionOperator()
        {
            /// &, *, ->

            var validKind = Kind
                is SyntaxKind.AmpersandToken
                or SyntaxKind.AsteriskToken
                or SyntaxKind.MinusGreaterThanToken;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.AddressOfExpression
                or SyntaxKind.PointerIndirectionExpression
                or SyntaxKind.PointerMemberAccessExpression;

            return validKind && validParent;
        }

        public bool IsLambdaOperator()
        {
            /// =>

            var validKind = Kind == SyntaxKind.EqualsGreaterThanToken;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.SimpleLambdaExpression
                or SyntaxKind.ParenthesizedLambdaExpression;

            return validKind && validParent;
        }

        public bool IsMemberAccessOperator()
        {
            /// .

            return Kind == SyntaxKind.DotToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.SimpleMemberAccessExpression);
        }

        public bool IsNamespaceAliasOperator()
        {
            /// ::

            return Kind == SyntaxKind.ColonColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.AliasQualifiedName);
        }

        public bool IsNullCoalescingAssignmentOperator()
        {
            /// ??=

            return Kind == SyntaxKind.QuestionQuestionEqualsToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.CoalesceAssignmentExpression);
        }

        public bool IsNullCoalescingOperator()
        {
            /// ??

            return Kind == SyntaxKind.QuestionQuestionToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.CoalesceExpression);
        }

        public bool IsNullConditionalOperatorDot()
        {
            return Kind == SyntaxKind.DotToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.MemberBindingExpression);
        }

        public bool IsNullConditionalOperatorQuestion()
        {
            return Kind == SyntaxKind.QuestionToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ConditionalAccessExpression);
        }

        public bool IsNullForgivingOperator()
        {
            /// !

            return Kind == SyntaxKind.ExclamationToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.SuppressNullableWarningExpression);
        }

        public bool IsOperatorDeclaration()
        {
            return Classifications.Corrected == Operator
                && Ancestors.HasAncestorAt(0, SyntaxKind.OperatorDeclaration);
        }

        public bool IsPatternMatchArrow()
        {
            /// =>

            return Kind == SyntaxKind.EqualsGreaterThanToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.SwitchExpressionArm);
        }

        public bool IsRangeSliceOperator()
        {
            /// ..

            return IsRangeOperator() || IsSliceOperator();
        }

        public bool IsShiftOperator()
        {
            /// <<, >>, >>>

            var validKind = Kind
                is SyntaxKind.LessThanLessThanToken
                or SyntaxKind.GreaterThanGreaterThanToken
                or SyntaxKind.GreaterThanGreaterThanGreaterThanToken;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.LeftShiftExpression
                or SyntaxKind.RightShiftExpression
                or SyntaxKind.UnsignedRightShiftExpression;

            return validKind && validParent;
        }

        public bool IsTernaryOperatorColon()
        {
            return Kind == SyntaxKind.ColonToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ConditionalExpression);
        }

        public bool IsTernaryOperatorQuestion()
        {
            return Kind == SyntaxKind.QuestionToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ConditionalExpression);
        }
        #endregion

        #region ------------------- Key Checks --------------------
        public bool IsAddressOfOperator()
        {
            return Kind == SyntaxKind.AmpersandToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.AddressOfExpression);
        }

        public bool IsBitwiseAndOperator()
        {
            return Kind == SyntaxKind.AmpersandToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.BitwiseAndExpression);
        }

        public bool IsBitwiseXorOperator()
        {
            return Kind == SyntaxKind.CaretToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.ExclusiveOrExpression);
        }

        public bool IsDereferenceOperator()
        {
            return Kind == SyntaxKind.AsteriskToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.PointerIndirectionExpression);
        }

        public bool IsLogicalNotOperator()
        {
            return Kind == SyntaxKind.ExclamationToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.LogicalNotExpression);
        }

        public bool IsMultiplicationOperator()
        {
            return Kind == SyntaxKind.AsteriskToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.MultiplyExpression);
        }

        public bool IsNullConditionalOperator()
        {
            return IsNullConditionalOperatorDot()
                || IsNullConditionalOperatorQuestion();
        }

        public bool IsTernaryOperator()
        {
            return IsTernaryOperatorColon()
                || IsTernaryOperatorQuestion();
        }

        public bool IsUnaryMinusOperator()
        {
            return Kind == SyntaxKind.MinusToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.UnaryMinusExpression);
        }

        public bool IsUnaryPlusOperator()
        {
            return Kind == SyntaxKind.PlusToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.UnaryPlusExpression);
        }
        #endregion

        #region ------------------- Not sure where to put Checks --------------------
        public bool IsRangeOperator()
        {
            return Kind == SyntaxKind.DotDotToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.RangeExpression);
        }

        public bool IsSliceOperator()
        {
            return Kind == SyntaxKind.DotDotToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.SlicePattern);
        }
        #endregion
    }
}
