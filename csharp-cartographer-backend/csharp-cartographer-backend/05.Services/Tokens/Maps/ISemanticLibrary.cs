using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    /// <summary>
    /// The job of the SemanticLibrary is to add all of the labels and definitions
    /// to the Token.Map to display in the UI. The DefinitionProvider retrieves the
    /// actual definition text but the SemanticLibrary determines which key to pass
    /// to the provider when keywords, operators, etc., can have multiple definitions.
    /// </summary>
    public interface ISemanticLibrary
    {
        SemanticMap GetSemanticMap(NavToken token);
    }
}
