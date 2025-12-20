using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public interface IClassificationWizard
    {
        void CorrectTokenClassifications(List<NavToken> navTokens);
    }
}
