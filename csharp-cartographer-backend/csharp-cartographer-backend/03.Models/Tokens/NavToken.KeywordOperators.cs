using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public partial class NavToken
    {
        public bool IsKeywordOperator() => IsAsKeywordOperator()
            || IsDefaultKeywordOperator()
            || IsIsKeywordOperator()
            || IsNameOfKeywordOperator()
            || IsSizeOfKeywordOperator()
            || IsTypeOfKeywordOperator();

        public bool IsAsKeywordOperator()
        {
            return Kind == SyntaxKind.AsKeyword
                && Ancestors.HasAncestorAt(0, SyntaxKind.AsExpression);
        }

        public bool IsIsKeywordOperator()
        {
            var validParent = Ancestors.GetParent()
                is SyntaxKind.IsExpression
                or SyntaxKind.IsPatternExpression;

            return Kind == SyntaxKind.IsKeyword && validParent;
        }

        public bool IsTypeOfKeywordOperator()
        {
            return Kind == SyntaxKind.TypeOfKeyword
                && Ancestors.HasAncestorAt(0, SyntaxKind.TypeOfExpression);
        }

        #region ------------------- Role Checks --------------------
        public bool IsDefaultKeywordOperator()
        {
            return Kind == SyntaxKind.DefaultKeyword
                && Ancestors.HasAncestorAt(0, SyntaxKind.DefaultExpression);
        }

        public bool IsNameOfKeywordOperator()
        {
            return Text == "nameof"
                && Kind == SyntaxKind.IdentifierToken;
        }

        public bool IsSizeOfKeywordOperator()
        {
            /// sizeof

            return Kind == SyntaxKind.SizeOfKeyword
                && Ancestors.HasAncestorAt(0, SyntaxKind.SizeOfExpression);
        }

        public bool IsTypeTestingKeywordOperator()
        {
            /// is, as, typeof

            var validKind = Kind
                is SyntaxKind.IsKeyword
                or SyntaxKind.AsKeyword
                or SyntaxKind.TypeOfKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.IsExpression
                or SyntaxKind.AsExpression
                or SyntaxKind.TypeOfExpression;

            return validKind && validParent;
        }
        #endregion
    }
}
