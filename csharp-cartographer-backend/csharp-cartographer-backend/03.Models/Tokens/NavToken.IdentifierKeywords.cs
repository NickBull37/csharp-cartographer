using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public partial class NavToken
    {
        public bool IsIdentifierKeyword() => throw new NotImplementedException();

        #region ------------------- Role Checks --------------------

        #endregion

        #region ------------------- Correction Checks --------------------
        public bool IsArgsIdentifierKeyword()
        {
            return Text is "args"
                && Kind is SyntaxKind.IdentifierToken;
        }
        #endregion
    }
}
