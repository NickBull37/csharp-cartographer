using csharp_cartographer_backend._01.Configuration.ReservedText;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._05.Services.SyntaxHighlighting
{
    public class SyntaxHighlighter : ISyntaxHighlighter
    {
        public void AddSyntaxHighlightingToNavTokens(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                if (token.Map is null)
                    continue;

                if (token.RoslynClassification == "type parameter name")
                {
                    token.HighlightColor = "color-light-green";
                    continue;
                }

                if (token.Map.PrimaryKind == TokenPrimaryKind.Delimiter)
                {
                    token.HighlightColor = "color-white";
                    continue;
                }

                if (token.Map.PrimaryKind == TokenPrimaryKind.Operator)
                {
                    if (token.Text == ".." || token.Text == ".")
                        token.HighlightColor = "color-white";
                    else
                        token.HighlightColor = "color-gray";
                    continue;
                }

                if (token.Map.PrimaryKind == TokenPrimaryKind.Punctuation)
                {
                    if (token.Text == "?")
                        token.HighlightColor = "color-gray";
                    else
                        token.HighlightColor = "color-white";
                    continue;
                }

                if (token.Map.PrimaryKind == TokenPrimaryKind.Keyword)
                {
                    HighlightReservedTextTokens(token, ReservedTextColors.Keywords);
                    continue;
                }

                switch (token.Map.SemanticRole)
                {
                    case SemanticRole.EnumMemberDeclaration:
                    case SemanticRole.EnumMemberReference:
                    case SemanticRole.FieldDeclaration:
                    case SemanticRole.FieldReference:
                    case SemanticRole.NamespaceDeclaration:
                    case SemanticRole.ObjectPropertyAssignment:
                    case SemanticRole.PropertyDeclaration:
                    case SemanticRole.PropertyReference:
                    case SemanticRole.TupleElementName:
                    case SemanticRole.UsingDirective:
                        token.HighlightColor = "color-white";
                        break;
                    case SemanticRole.AttributeDeclaration:
                    case SemanticRole.ClassConstructorDeclaration:
                    case SemanticRole.ClassConstructorInvocation:
                    case SemanticRole.ClassDeclaration:
                    case SemanticRole.ClassReference:
                    case SemanticRole.ExceptionType:
                    case SemanticRole.RecordConstructorDeclaration:
                    case SemanticRole.RecordConstructorInvocation:
                    case SemanticRole.RecordDeclaration:
                    case SemanticRole.RecordReference:
                        token.HighlightColor = "color-green";
                        break;
                    case SemanticRole.EnumDeclaration:
                    case SemanticRole.EnumReference:
                    //case SemanticRole.GenericTypeParameter:
                    case SemanticRole.InterfaceDeclaration:
                    case SemanticRole.InterfaceReference:
                    case SemanticRole.NumericLiteral:
                        token.HighlightColor = "color-light-green";
                        break;
                    case SemanticRole.RecordStructDeclaration:
                    case SemanticRole.RecordStructConstructorDeclaration:
                    case SemanticRole.RecordStructConstructorInvocation:
                    case SemanticRole.RecordStructReference:
                    case SemanticRole.StructDeclaration:
                    case SemanticRole.StructConstructorDeclaration:
                    case SemanticRole.StructConstructorInvocation:
                    case SemanticRole.StructReference:
                        token.HighlightColor = "color-jade";
                        break;
                    case SemanticRole.LocalVariableDeclaration:
                    case SemanticRole.LocalVariableReference:
                    case SemanticRole.ParameterDeclaration:
                    case SemanticRole.ParameterLabel:
                    case SemanticRole.ParameterReference:
                        token.HighlightColor = "color-light-blue";
                        break;
                    case SemanticRole.MethodDeclaration:
                    case SemanticRole.MethodInvocation:
                        token.HighlightColor = "color-yellow";
                        break;
                    case SemanticRole.CharacterLiteral:
                    case SemanticRole.StringLiteral:
                        token.HighlightColor = "color-orange";
                        break;
                    //case SemanticRole.ConstructorInvocation:
                    //    token.HighlightColor = "color-cart-green";
                    //    break;
                    case SemanticRole.CastType:
                    case SemanticRole.CastTargetType:
                    case SemanticRole.ConstraintType:
                    case SemanticRole.ConstructorInvocation:
                    case SemanticRole.FieldDataType:
                    case SemanticRole.GenericTypeArgument:
                    case SemanticRole.LocalVariableDataType:
                    case SemanticRole.MethodReturnType:
                    case SemanticRole.ParameterDataType:
                    case SemanticRole.PropertyDataType:
                    case SemanticRole.SimpleBaseType:
                    case SemanticRole.TupleElementType:
                    case SemanticRole.TypePatternType:
                        token.HighlightColor = GetColorForDataTypeIdentifiers(token);
                        break;
                }
            }

            // TODO: add manual override of highlight color for common structs & enums

            HighlightUnidentifiedTokensRed(navTokens);
        }

        private static string GetColorForDataTypeIdentifiers(NavToken token)
        {
            switch (token.RoslynClassification)
            {
                case "class name":
                case "record class name":
                    return "color-green";
                case "enum name":
                case "interface name":
                    //case "type parameter name":
                    return "color-light-green";
                case "struct name":
                case "record struct name":
                    return "color-jade";
                default:
                    return GuessColor(token.Text);
            }
        }

        private static string GetColorForExternallyDefinedToken(NavToken token)
        {
            throw new NotImplementedException();
        }

        private static void HighlightReservedTextTokens(NavToken token, List<ReservedTextColor> list)
        {
            foreach (var element in list)
            {
                if (token.IsKeyword("default"))
                {
                    token.HighlightColor = GetDefaultKeywordColor(token);
                    continue;
                }

                if (token.Text.Equals(element.Text))
                    token.HighlightColor = element.HighlightColor;
            }
        }

        private static void HighlightUnidentifiedTokensRed(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                if (string.IsNullOrEmpty(token.HighlightColor))
                {
                    token.HighlightColor = "color-red";
                }
            }
        }

        private static string GetDefaultKeywordColor(NavToken token) =>
            token.Text == "default" && token.Charts[1].Label == "DefaultSwitchLabel"
                ? "color-purple"
                : "color-blue";

        private static string GuessColor(string text)
        {
            if (text.Length >= 2
                && text[0] == 'I'
                && char.IsUpper(text[0])
                && char.IsUpper(text[1]))
            {
                return "color-light-green";
            }

            return "color-green";
        }
    }
}
