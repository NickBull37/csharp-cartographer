using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._05.Services.TokenTags
{
    public interface ITagIndexer
    {
        void AddHighlightIndicesToTags(List<NavToken> navTokens);
    }
}
