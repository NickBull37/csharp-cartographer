using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._01.Configuration.ReservedText;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;

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
        const string Teal = "color-teal";
        const string White = "color-white";
        const string Yellow = "color-yellow";

        private readonly CartographerConfig _config;

        public SyntaxHighlighter(IOptions<CartographerConfig> config)
        {
            _config = config.Value;
        }

        /*
        *  Order for adding syntax highlighting
        *  
        *  1a. Highlight Keywords manually (most reliable), use Classification to identify them
        *  
        *  1b. Use Classification directly to highlight delimiters, operators, punctuation, literals,
        *      and any identifiers defined in the uploaded file (highly reliable).
        *  
        *  2. Use Roslyn semantic data to highlight tokens defined in referenced assembiles (works rarely).
        *  
        *  3. Use SemanticRole to highlight remaining tokens (not fully reliable until unit tests are in place). 
        *  
        *  3. Color unidentified tokens red.
        *  
        */
        public void AddSyntaxHighlightingToNavTokens(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                if (token.Map is null)
                    continue;

                // TODO: color by classification
                if (token.RoslynClassification is "keyword" or "keyword - control")
                {
                    ColorByKeyword(token);
                    continue;
                }

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
                    //or "namespace name"
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
                    ColorByRoslynClassification(token);
                    continue;
                }

                // Is subject to change and could break in the future
                if (_config.SemanticDataHighlightingEnabled)
                {
                    ColorBySemanticData(token);
                    if (token.HighlightColor is not null)
                        continue;
                }

                ColorBySemanticRole(token);
                if (token.HighlightColor is not null)
                    continue;

                // No color found - color red
                token.HighlightColor = Red;
            }
        }

        public static void ColorByKeyword(NavToken token)
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

        public static void ColorByRoslynClassification(NavToken token)
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
                //case "namespace name":
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

        private static void ColorBySemanticData(NavToken token)
        {
            switch (token.SemanticData?.SymbolKind)
            {
                case SymbolKind.Field:
                case SymbolKind.Property:
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
                    token.HighlightColor = Green;
                    return;
                case TypeKind.Enum:
                case TypeKind.Interface:
                case TypeKind.TypeParameter:
                    token.HighlightColor = LightGreen;
                    return;
                case TypeKind.Struct:
                    token.HighlightColor = Jade;
                    return;
                default:
                    break;
            }
        }

        public static void ColorBySemanticRole(NavToken token)
        {
            switch (token.Map!.SemanticRole)
            {
                case SemanticRole.AliasDeclaration:
                case SemanticRole.AttributeArgument:
                case SemanticRole.EnumMemberDeclaration:
                case SemanticRole.EnumMemberReference:
                case SemanticRole.FieldDeclaration:
                case SemanticRole.FieldReference:
                case SemanticRole.MemberAccess:
                case SemanticRole.NamespaceDeclarationQualifer:
                case SemanticRole.ObjectPropertyAssignment:
                case SemanticRole.PropertyAccess:
                case SemanticRole.PropertyDeclaration:
                case SemanticRole.PropertyReference:
                case SemanticRole.TupleElementName:
                case SemanticRole.UsingDirectiveQualifier:
                // query expression vars
                case SemanticRole.GroupContinuationRangeVariable:
                case SemanticRole.JoinIntoRangeVariable:
                case SemanticRole.JoinRangeVariable:
                case SemanticRole.JoinSource:
                case SemanticRole.LetVariable:
                case SemanticRole.QuerySource:
                case SemanticRole.QueryVariableReference:
                case SemanticRole.RangeVariable:
                case SemanticRole.GroupContinuationRangeVariableReference:
                case SemanticRole.JoinIntoRangeVariableReference:
                case SemanticRole.JoinRangeVariableReference:
                case SemanticRole.LetVariableReference:
                case SemanticRole.RangeVariableReference:
                    token.HighlightColor = White;
                    break;
                case SemanticRole.AliasQualifier:
                case SemanticRole.NamespaceQualifer:
                    token.HighlightColor = Gray;
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
                    token.HighlightColor = Green;
                    break;
                case SemanticRole.EnumDeclaration:
                case SemanticRole.EnumReference:
                case SemanticRole.InterfaceDeclaration:
                case SemanticRole.InterfaceReference:
                case SemanticRole.NumericLiteral:
                    token.HighlightColor = LightGreen;
                    break;
                case SemanticRole.RecordStructDeclaration:
                case SemanticRole.RecordStructConstructorDeclaration:
                case SemanticRole.RecordStructConstructorInvocation:
                case SemanticRole.RecordStructReference:
                case SemanticRole.StructDeclaration:
                case SemanticRole.StructConstructorDeclaration:
                case SemanticRole.StructConstructorInvocation:
                case SemanticRole.StructReference:
                    token.HighlightColor = Jade;
                    break;
                case SemanticRole.LocalVariableDeclaration:
                case SemanticRole.LocalVariableReference:
                case SemanticRole.ParameterDeclaration:
                case SemanticRole.ParameterLabel:
                case SemanticRole.ParameterReference:
                    token.HighlightColor = LightBlue;
                    break;
                case SemanticRole.MethodDeclaration:
                case SemanticRole.MethodInvocation:
                    token.HighlightColor = Yellow;
                    break;
                case SemanticRole.CharacterLiteral:
                case SemanticRole.StringLiteral:
                    token.HighlightColor = Orange;
                    break;
                // -------------------------------------------------------------- //
                case SemanticRole.CastType:
                case SemanticRole.CastTargetType:
                case SemanticRole.ConstraintType:
                case SemanticRole.ConstructorInvocation:
                case SemanticRole.EventFieldType:
                case SemanticRole.FieldDataType:
                case SemanticRole.GenericTypeArgument:
                case SemanticRole.LocalVariableDataType:
                case SemanticRole.MethodReturnType:
                case SemanticRole.ParameterDataType:
                case SemanticRole.PropertyDataType:
                case SemanticRole.SimpleBaseType:
                case SemanticRole.TypeQualifier:
                case SemanticRole.TupleElementType:
                case SemanticRole.TypePatternType:
                case SemanticRole.TypeReference:
                    token.HighlightColor = GuessColor(token.Text);
                    break;
            }
        }

        private static string GetDefaultKeywordColor(NavToken token) =>
            token.Map?.SemanticRole == SemanticRole.LiteralValue
                ? Blue
                : Purple;

        private static string GetInKeywordColor(NavToken token) =>
            token.Map?.SemanticRole == SemanticRole.LoopStatement
                ? Purple
                : Blue;

        private static string GuessColor(string text)
        {
            if (text.Length >= 2
                && text[0] == 'I'
                && char.IsUpper(text[0])
                && char.IsUpper(text[1]))
            {
                return LightGreen;
            }

            return Green;
        }
    }
}
