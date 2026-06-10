using csharp_cartographer_backend._03.Models.Shared;

namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    public sealed record SemanticMap
    {
        public string KindLabel { get; init; }

        public string RoleLabel { get; init; }

        public string? FocusedLabel { get; init; }

        public StyledText RoleDefinition { get; init; }

        public StyledText? FocusedDefinition { get; init; }

        public SemanticMap(
            string kindLabel,
            string roleLabel,
            string? focusedLabel,
            StyledText roleDefinition,
            StyledText? focusedDefinition)
        {
            KindLabel = kindLabel;
            RoleLabel = roleLabel;
            FocusedLabel = focusedLabel;
            RoleDefinition = roleDefinition;
            FocusedDefinition = focusedDefinition;
        }
    }
}
