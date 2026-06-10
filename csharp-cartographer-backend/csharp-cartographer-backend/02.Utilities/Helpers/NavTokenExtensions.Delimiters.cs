using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._02.Utilities.Helpers
{
    public static partial class NavTokenExtensions
    {
        public static bool IsDelimiter(this NavToken token) => token.Kind
            is SyntaxKind.OpenBraceToken
            or SyntaxKind.OpenBracketToken
            or SyntaxKind.OpenParenToken
            or SyntaxKind.LessThanToken
            or SyntaxKind.CloseBraceToken
            or SyntaxKind.CloseBracketToken
            or SyntaxKind.CloseParenToken
            or SyntaxKind.GreaterThanToken;

        public static bool IsBrace(this NavToken token) => token.Kind
            is SyntaxKind.OpenBraceToken
            or SyntaxKind.CloseBraceToken;

        public static bool IsBracket(this NavToken token) => token.Kind
            is SyntaxKind.OpenBracketToken
            or SyntaxKind.CloseBracketToken;

        public static bool IsClip(this NavToken token) => token.Kind
            is SyntaxKind.LessThanToken
            or SyntaxKind.GreaterThanToken;

        public static bool IsParen(this NavToken token) => token.Kind
            is SyntaxKind.OpenParenToken
            or SyntaxKind.CloseParenToken;

        public static bool IsOpenDelimiter(this NavToken token) => token.Kind
            is SyntaxKind.OpenBraceToken
            or SyntaxKind.OpenBracketToken
            or SyntaxKind.OpenParenToken
            or SyntaxKind.LessThanToken;

        public static bool IsCloseDelimiter(this NavToken token) => token.Kind
            is SyntaxKind.CloseBraceToken
            or SyntaxKind.CloseBracketToken
            or SyntaxKind.CloseParenToken
            or SyntaxKind.GreaterThanToken;


        /*
         *  -----------------------------------------------------------------------
         *                            Semantic Roles
         *  -----------------------------------------------------------------------
         */



        // ------------------- Argument & Parameter Delimiters ---------------------

        // ------------------- Block Delimiters ---------------------

        // ------------------- Condition Delimiters ---------------------

        // ------------------- Context Block Delimiters ---------------------

        // ------------------- Control Delimiters ---------------------

        // ------------------- Definition Delimiters ---------------------

        // ------------------- Initializer Delimiters ---------------------

        // ------------------- Pattern Matching Delimiters ---------------------


        /*
         *  -----------------------------------------------------------------------
         *                            Focused Roles
         *  -----------------------------------------------------------------------
         */

    }
}
