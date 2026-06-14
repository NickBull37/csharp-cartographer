using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public partial class NavToken
    {
        public bool IsLiteral()
        {
            return Kind
                is SyntaxKind.CharacterLiteralToken
                or SyntaxKind.StringLiteralToken
                or SyntaxKind.InterpolatedStringStartToken
                or SyntaxKind.InterpolatedStringTextToken
                or SyntaxKind.InterpolatedStringEndToken
                or SyntaxKind.InterpolatedVerbatimStringStartToken
                or SyntaxKind.NumericLiteralToken
                or SyntaxKind.TrueKeyword
                or SyntaxKind.FalseKeyword;
        }

        public bool IsStringLiteral()
        {
            return Kind
                is SyntaxKind.StringLiteralToken
                or SyntaxKind.InterpolatedStringStartToken
                or SyntaxKind.InterpolatedStringTextToken
                or SyntaxKind.InterpolatedStringEndToken
                or SyntaxKind.InterpolatedVerbatimStringStartToken;
        }

        #region ------------------- Role Checks --------------------
        public bool IsInterpolatedStringStart()
        {
            return Kind == SyntaxKind.InterpolatedStringStartToken;
        }

        public bool IsInterpolatedStringText()
        {
            return Kind == SyntaxKind.InterpolatedStringTextToken
                && !Ancestors.HasAncestorAt(0, SyntaxKind.InterpolationFormatClause);
        }

        public bool IsInterpolatedStringEnd()
        {
            return Kind == SyntaxKind.InterpolatedStringEndToken;
        }

        public bool IsInterpolatedVerbatimStringStart()
        {
            return Kind == SyntaxKind.InterpolatedVerbatimStringStartToken;
        }

        public bool IsNumericFormatSpecifier()
        {
            return Kind == SyntaxKind.InterpolatedStringTextToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.InterpolationFormatClause);
        }
        #endregion

        #region ------------------- Key Checks --------------------
        public bool IsCharacterLiteral()
        {
            return Kind is SyntaxKind.CharacterLiteralToken;
        }

        public bool IsQuotedString()
        {
            return Kind == SyntaxKind.StringLiteralToken
                && Text.StartsWith('"');
        }

        public bool IsVerbatimString()
        {
            return Kind == SyntaxKind.StringLiteralToken
                && Text.StartsWith('@');
        }

        public bool IsInterpolatedString()
        {
            return IsInterpolatedStringStart()
                || IsInterpolatedStringText()
                || IsInterpolatedStringEnd()
                || IsInterpolatedVerbatimStringStart()
                || IsNumericFormatSpecifier()
                || IsInterpolationFormatSeparator();
        }

        public bool IsInterpolatedVerbatimString()
        {
            return IsInterpolatedVerbatimStringStart();
        }

        public bool IsDecimalValue()
        {
            return Kind is SyntaxKind.NumericLiteralToken
                && Text.Length > 0
                && Text[^1] is 'm' or 'M';
        }

        public bool IsFloatingPointValue()
        {
            return Kind == SyntaxKind.NumericLiteralToken
                && Text.Length > 0
                && Text[^1] is 'f' or 'F';
        }

        public bool IsNumericLiteral()
        {
            return Kind == SyntaxKind.NumericLiteralToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.NumericLiteralExpression);
        }
        #endregion

        #region ------------------- Focused Label Checks --------------------
        public bool TryGetLiteralFocusedLabel(out string label)
        {
            label = null;

            if (IsCharacterLiteral())
                label = "Character Literal";

            if (IsQuotedString())
                label = "Quoted String";

            if (IsVerbatimString())
                label = "Verbatim String";

            if (IsInterpolatedString())
                label = "Interpolated String";

            if (IsInterpolatedVerbatimString())
                label = "Interpolated Verbatim String";

            if (IsDecimalValue())
                label = "Decimal Literal";

            if (IsFloatingPointValue())
                label = "Floating Point Literal";

            if (IsNumericLiteral())
                label = "Numeric Literal";

            return label is not null;
        }
        #endregion
    }
}
