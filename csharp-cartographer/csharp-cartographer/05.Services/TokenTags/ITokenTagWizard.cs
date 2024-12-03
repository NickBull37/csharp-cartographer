using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._05.Services.TokenTags
{
    public interface ITokenTagWizard
    {
        void AddFactsAndInsightsToTags(List<NavToken> navTokens);
        void CleanUpTokenTags(List<NavToken> navTokens);
        void UpdateNavTokenTags(List<NavToken> navTokens);
    }
}
