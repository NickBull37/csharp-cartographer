using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.SyntaxHighlighting
{
    public interface ISyntaxHighlighter
    {
        void AddSyntaxHighlightingToNavTokens(List<NavToken> navTokens);
    }

    public interface ITestModel
    {
        int Index { get; set; }
    }

    public interface ITestModelTwo
    {
        List<ITestModel> ModelList { get; set; }
    }
}
