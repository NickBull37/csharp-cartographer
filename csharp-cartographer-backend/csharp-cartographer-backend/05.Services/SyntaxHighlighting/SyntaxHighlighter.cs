using csharp_cartographer_backend._01.Configuration.ReservedText;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
using Microsoft.CodeAnalysis;

namespace csharp_cartographer_backend._05.Services.SyntaxHighlighting
{
    public class SyntaxHighlighter : ISyntaxHighlighter
    {
        const string Blue = "color-blue";
        const string Gray = "color-gray";
        const string Green = "color-green";
        const string Jade = "color-jade";
        const string LightBlue = "color-light-blue";
        const string LightGreen = "color-light-green";
        const string Orange = "color-orange";
        const string Pink = "color-pink";
        const string Purple = "color-purple";
        const string Red = "color-red";
        const string White = "color-white";
        const string Yellow = "color-yellow";

        /*
        *  Order for adding syntax highlighting
        *  
        *  [Keywords, Literals, ]
        *  1) Highlight keywords, literals, delimiters, operators & punctuation manually [very reliable]
        *  
        *  [Externally Defined Identifiers]
        *  1) Use Roslyn's classification which is specifically purposed for syntax highlighting
        *     Roslyn will only have a useful classification if the symbol definition is inside the uploaded file.
        *  
        *  2) Use semantic data in token map. Cannot distinguish classes, enums, structs, etc. defined outside of the uploaded file.,
        *  
        *  3) Color unidentified tokens red.
        *  
        */
        public void AddSyntaxHighlightingToNavTokens(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                if (token.Map is null)
                    continue;

                // Roslyn classified keywords
                if (token.RoslynClassification is "keyword" or "keyword - control")
                {
                    HighlightKeyword(token);
                    continue;
                }

                // Roslyn classified tokens
                if (token.RoslynClassification
                    is "class name"
                    or "constant name"
                    or "delegate name"
                    or "enum name"
                    or "enum member name"
                    or "event name"
                    or "field name"
                    or "interface name"
                    or "local name"
                    or "method name"
                    or "namespace name"
                    or "number"
                    or "operator"
                    or "parameter name"
                    or "property name"
                    or "punctuation"
                    or "record class name"
                    or "record struct name"
                    or "string"
                    or "string - verbatim"
                    or "struct name"
                    or "type parameter name")
                {
                    HighlightRoslynClassifiedToken(token);
                    continue;
                }

                // Color by semantic data
                HighlightUsingSemanticData(token);
                if (token.HighlightColor is not null)
                    continue;

                // Color by semantic role

                // No color found - color red
                token.HighlightColor = Red;
            }

            //HighlightUnidentifiedTokensRed(navTokens);
        }

        public static void HighlightKeyword(NavToken token)
        {
            foreach (var keyword in ReservedTextColors.Keywords)
            {
                // "default" can be blue or purple
                if (token.Text == "default")
                {
                    token.HighlightColor = GetDefaultKeywordColor(token);
                    continue;
                }

                // "in" can be blue or purple
                if (token.Text == "in")
                {
                    token.HighlightColor = GetInKeywordColor(token);
                    continue;
                }

                if (token.Text.Equals(keyword.Text))
                    token.HighlightColor = keyword.HighlightColor;
            }
        }

        public static void HighlightRoslynClassifiedToken(NavToken token)
        {
            switch (token.RoslynClassification)
            {
                case "operator":
                    token.HighlightColor = Gray;
                    break;
                case "class name":
                case "delegate name":
                case "record class name":
                    token.HighlightColor = Green;
                    break;
                case "struct name":
                case "record struct name":
                    token.HighlightColor = Jade;
                    break;
                case "local name":
                case "parameter name":
                    token.HighlightColor = LightBlue;
                    break;
                case "enum name":
                case "interface name":
                case "number":
                case "type parameter name":
                    token.HighlightColor = LightGreen;
                    break;
                case "string":
                case "string - verbatim":
                    token.HighlightColor = Orange;
                    break;
                case "enum member name":
                case "event name":
                case "field name":
                case "namespace name":
                case "property name":
                case "punctuation":
                    token.HighlightColor = White;
                    break;
                case "method name":
                    token.HighlightColor = Yellow;
                    break;
                default:
                    break;
            }
        }

        public static void HighlightExternalIdentifier(NavToken token)
        {
            switch (token.Map!.SemanticRole)
            {
                case SemanticRole.EnumMemberDeclaration:
                case SemanticRole.EnumMemberReference:
                case SemanticRole.FieldDeclaration:
                case SemanticRole.FieldReference:
                case SemanticRole.NamespaceDeclaration:
                case SemanticRole.ObjectPropertyAssignment:
                case SemanticRole.PropertyAccess:
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

        private static void HighlightUsingSemanticData(NavToken token)
        {
            switch (token.SemanticData?.SymbolKind)
            {
                case SymbolKind.Field:
                case SymbolKind.Property:
                case SymbolKind.Namespace:
                    token.HighlightColor = White;
                    return;
                case SymbolKind.Method:
                    token.HighlightColor = Yellow;
                    return;
                default:
                    break;
            }

            switch (token.SemanticData?.TypeKind)
            {
                case TypeKind.Class:
                case TypeKind.Delegate:
                    token.HighlightColor = Pink;
                    break;
                case TypeKind.Enum:
                case TypeKind.Interface:
                case TypeKind.TypeParameter:
                    token.HighlightColor = Pink;
                    break;
                case TypeKind.Struct:
                    token.HighlightColor = Jade;
                    return;
                default:
                    break;
            }
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
                    return "color-light-green";
                case "struct name":
                case "record struct name":
                    return "color-jade";
                default:
                    return GuessColor(token.Text);
            }
        }

        private static void HighlightReservedTextTokens(NavToken token, List<ReservedTextColor> list)
        {
            foreach (var element in list)
            {
                if (token.Text == "default")
                {
                    token.HighlightColor = GetDefaultKeywordColor(token);
                    continue;
                }

                if (token.Text == "in")
                {
                    token.HighlightColor = GetInKeywordColor(token);
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
            token.Map?.SemanticRole == SemanticRole.LiteralValue
                ? "color-blue"
                : "color-purple";

        private static string GetInKeywordColor(NavToken token) =>
            token.Map?.SemanticRole == SemanticRole.LoopStatement
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
