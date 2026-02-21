using csharp_cartographer_backend._01.Configuration;
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
        *  1. There is often not enough data to distinguish classes, enums, & structs when highlighting.
        *     Use hardcoded lists of common structs and enums to improve highlighting accuracy.
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
                // color manually
                if (GlobalConstants.CommonIdentifiers.Contains(token.Text))
                {
                    ColorManually(token);

                    if (token.HighlightColor is not null)
                        continue;
                }

                if (token.Map is null)
                    continue;

                // color by classification
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

            foreach (var token in navTokens)
            {
                if (token.Map.SemanticRole == SemanticRole.Unknown)
                    token.HighlightColor = Red;
            }
        }

        private static void ColorManually(NavToken token)
        {
            // avoids accidentally coloring a member identifier as a type
            if (token.Map.SemanticRole == SemanticRole.MemberAccess)
                return;

            // avoids accidentally coloring a declaration identifier as a type
            if (token.Map.SemanticRole.ToString().Contains("Declaration"))
                return;

            // avoids accidentally coloring a reference identifier as a type
            if (token.Map.SemanticRole.ToString().Contains("Reference"))
                return;

            if (GlobalConstants.CommonEnums.Contains(token.Text))
                token.HighlightColor = LightGreen;

            if (GlobalConstants.CommonStructs.Contains(token.Text))
                token.HighlightColor = Jade;
        }

        private static void ColorByKeyword(NavToken token)
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

        private static void ColorByRoslynClassification(NavToken token)
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

        private static void ColorBySemanticRole(NavToken token)
        {
            switch (token.Map!.SemanticRole)
            {
                case SemanticRole.AttributeArgument:
                case SemanticRole.EnumMemberDeclaration:
                case SemanticRole.EnumMemberReference:
                case SemanticRole.FieldDeclaration:
                case SemanticRole.FieldReference:
                case SemanticRole.MemberAccess:
                case SemanticRole.NamespaceAliasDeclaration:
                case SemanticRole.ObjectPropertyAssignment:
                case SemanticRole.PropertyAccess:
                case SemanticRole.PropertyDeclaration:
                case SemanticRole.PropertyReference:
                case SemanticRole.TupleElementName:
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
                    token.HighlightColor = Gray;
                    break;
                case SemanticRole.AttributeDeclaration:
                case SemanticRole.ClassDeclaration:
                case SemanticRole.ClassReference:
                case SemanticRole.ExceptionType:
                case SemanticRole.RecordDeclaration:
                case SemanticRole.RecordReference:
                case SemanticRole.TypeAliasDeclaration:
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
                case SemanticRole.RecordStructReference:
                case SemanticRole.StructDeclaration:
                case SemanticRole.StructReference:
                    token.HighlightColor = Jade;
                    break;
                case SemanticRole.LocalVariableDeclaration:
                case SemanticRole.LocalVariableReference:
                case SemanticRole.Parameter:
                case SemanticRole.ParameterLabel:
                case SemanticRole.ParameterReference:
                    token.HighlightColor = LightBlue;
                    break;
                case SemanticRole.MethodDeclaration:
                case SemanticRole.MethodInvocation:
                    token.HighlightColor = Yellow;
                    break;
                case SemanticRole.NamespaceQualifier:
                    token.HighlightColor = GetNamespaceQualifierColor(token);
                    break;
                // -------------------------------------------------------------- //
                case SemanticRole.CastType:
                case SemanticRole.CastTargetType:
                case SemanticRole.TypeConstraint:
                case SemanticRole.ConstructorInvocation:
                case SemanticRole.EventFieldType:
                case SemanticRole.FieldDataType:
                case SemanticRole.GenericTypeArgument:
                case SemanticRole.LocalVariableDataType:
                case SemanticRole.LoopIteratorDataType:
                case SemanticRole.MethodReturnType:
                case SemanticRole.ParameterDataType:
                case SemanticRole.PropertyDataType:
                case SemanticRole.BaseType:
                case SemanticRole.TypeQualifier:
                case SemanticRole.TupleElementType:
                case SemanticRole.TypePattern:
                case SemanticRole.TypeReference:
                    token.HighlightColor = GuessColor(token.Text);
                    break;
            }
        }

        private static string GetDefaultKeywordColor(NavToken token) =>
            token.Map?.SemanticRole == SemanticRole.DefaultOperator ||
            token.Map?.SemanticRole == SemanticRole.DefaultValue
                ? Blue
                : Purple;

        private static string GetInKeywordColor(NavToken token) =>
            token.Map?.SemanticRole == SemanticRole.LoopStatement
                ? Purple
                : Blue;
        private static string GetNamespaceQualifierColor(NavToken token)
        {
            if (token.IsUsingDirectiveQualifier() || token.IsNamespaceDeclarationQualifier())
                return White;

            return Gray;
        }

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
