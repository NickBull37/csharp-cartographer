using csharp_cartographer_backend._01.Configuration.Enums;
using csharp_cartographer_backend._02.Utilities.Helpers;
using csharp_cartographer_backend._02.Utilities.Providers;
using csharp_cartographer_backend._03.Models.Shared;

namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    public interface ISemanticMap
    {
        PrimaryKind PrimaryKind { get; init; }
        GroupRole GroupRole { get; }
        SemanticRole SemanticRole { get; init; }


        bool HasFocusedElement => PrimaryKind
            is PrimaryKind.Keyword
            or PrimaryKind.KeywordOperator
            or PrimaryKind.Literal
            or PrimaryKind.Operator;

        bool HasGroupElement => PrimaryKind
            is PrimaryKind.Delimiter
            or PrimaryKind.Identifier;


        string KindLabel => PrimaryKind.GetSpacedLabel() ?? PrimaryKind.ToSpacedString();
        string GroupLabel => GroupRole.GetSpacedLabel() ?? GroupRole.ToSpacedString();
        string RoleLabel => SemanticRole.GetSpacedLabel() ?? SemanticRole.ToSpacedString();
        string FocusedLabel { get; init; }


        string KindAbrv { get; }
        string KindDefKey => PrimaryKind.ToString();
        string GroupDefKey => GroupRole.ToString();
        string RoleDefKey => SemanticRole.ToString();
        string FocusedKey { get; init; }


        StyledText KindDefinition =>
            DefinitionProvider.GetStyledText(KindDefKey) ?? StyledText.NotFound();
        StyledText RoleDefinition =>
            DefinitionProvider.GetStyledText(RoleDefKey) ?? StyledText.NotFound();
        StyledText? GroupDefinition =>
            HasGroupElement
                ? DefinitionProvider.GetStyledText(GroupDefKey) ?? StyledText.NotFound()
                : null;
        StyledText? FocusedDefinition =>
            HasFocusedElement
                ? DefinitionProvider.GetStyledText(FocusedKey) ?? StyledText.NotFound()
                : null;

        static string Key(string kindAbrv, string extension)
            => $"{kindAbrv}:{extension}";

        static string Key(string kindAbrv, string extension1, string extension2)
            => $"{kindAbrv}:{extension1}:{extension2}";
    }

    public abstract class SemanticMapTest
    {
        public PrimaryKind PrimaryKind { get; init; }
        public GroupRole GroupRole { get; }
        public SemanticRole SemanticRole { get; init; }


        public bool HasFocusedElement => PrimaryKind
            is PrimaryKind.Keyword
            or PrimaryKind.KeywordOperator
            or PrimaryKind.Literal
            or PrimaryKind.Operator;

        public bool HasGroupElement => PrimaryKind
            is PrimaryKind.Delimiter
            or PrimaryKind.Identifier;


        public string KindLabel => PrimaryKind.GetSpacedLabel() ?? PrimaryKind.ToSpacedString();
        public string GroupLabel => GroupRole.GetSpacedLabel() ?? GroupRole.ToSpacedString();
        public string RoleLabel => SemanticRole.GetSpacedLabel() ?? SemanticRole.ToSpacedString();
        public string FocusedLabel { get; init; }


        public string KindAbrv { get; }
        abstract public string KindAbrv2 { get; }
        public string KindDefKey => PrimaryKind.ToString();
        public string GroupDefKey => GroupRole.ToString();
        public string RoleDefKey => SemanticRole.ToString();
        public string FocusedKey { get; init; }


        public StyledText KindDefinition =>
            DefinitionProvider.GetStyledText(KindDefKey) ?? StyledText.NotFound();
        public StyledText RoleDefinition =>
            DefinitionProvider.GetStyledText(RoleDefKey) ?? StyledText.NotFound();
        public StyledText? GroupDefinition =>
            HasGroupElement
                ? DefinitionProvider.GetStyledText(GroupDefKey) ?? StyledText.NotFound()
                : null;
        public StyledText? FocusedDefinition =>
            HasFocusedElement
                ? DefinitionProvider.GetStyledText(FocusedKey) ?? StyledText.NotFound()
                : null;

        protected static string Key(string kindAbrv, string extension)
            => $"{kindAbrv}:{extension}";

        protected static string Key(string kindAbrv, string extension1, string extension2)
            => $"{kindAbrv}:{extension1}:{extension2}";
    }

    public sealed class KeywordMap : SemanticMapTest
    {
        public PrimaryKind PrimaryKind { get; init; }
        public GroupRole GroupRole { get; }
        public SemanticRole SemanticRole { get; init; }

        public string KindAbrv => "KW";
        public override string KindAbrv2 { get; } = "KW";
        public string FocusedLabel { get; init; }
        public string FocusedKey { get; init; }
        public bool FocusedKeyRoleExtensionRequired { get; }

        public KeywordMap(NavToken token, PrimaryKind kind, GroupRole groupRole, SemanticRole semanticRole)
        {
            PrimaryKind = kind;
            GroupRole = groupRole;
            SemanticRole = semanticRole;

            FocusedLabel = $"{kind}: {token.Text}";
            FocusedKey = GetKeywordKey(token);
            FocusedKeyRoleExtensionRequired = token.Text
                is "case"
                or "in"
                or "new"
                or "out"
                or "ref"
                or "static"
                or "using"
                or "where";
        }

        private string GetKeywordKey(NavToken token)
        {
            if (token.IsVarPatternKeyword())
                return Key(KindAbrv, token.Text, "PatternMatching");

            if (token.IsDefaultLiteral())
                return Key(KindAbrv, token.Text, "Literal");

            return FocusedKeyRoleExtensionRequired
                ? Key(KindAbrv, token.Text, token.SemanticRole.ToString())
                : Key(KindAbrv, token.Text);
        }
    }






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
