using csharp_cartographer_backend._03.Models.Shared;

namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    public sealed record SemanticMap
    {
        public string PKLabel { get; init; }

        public string SRLabel { get; init; }

        public string? FDLabel { get; init; }

        public StyledText RoleDefinition { get; init; }

        public StyledText? FocusedDefinition { get; init; }

        public SemanticMap(
            string kindLabel,
            string roleLabel,
            string? focusedLabel,
            StyledText roleDefinition,
            StyledText? focusedDefinition)
        {
            PKLabel = kindLabel;
            SRLabel = roleLabel;
            FDLabel = focusedLabel;
            RoleDefinition = roleDefinition;
            FocusedDefinition = focusedDefinition;
        }
    }
}
