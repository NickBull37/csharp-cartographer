using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._05.Services.SyntaxHighlighting
{
    public interface ISyntaxHighlighter
    {
        void HighlightNavTokens(List<NavToken> navTokens);
    }
}
