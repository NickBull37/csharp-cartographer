using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._05.Services.TokenTags
{
    public interface ITokenTagGenerator
    {
        void AddTokenTag(NavToken token, string label, int level);

        void GenerateTokenTags(List<NavToken> navTokens);
    }
}
