using csharp_cartographer_backend._01.Configuration;
using csharp_cartographer_backend._01.Configuration.Configs;
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
        *  Syntax Highlighting Steps
        *  
        *  1. Color Manually (highly reliable)
        *  
        *     There is no way to distinguish classes, enums, & structs when they are defined
        *     outside of the uploaded file. Use hardcoded lists of common structs and enums
        *     to handle the most common scenarios. Skip any roles that could also share a name
        *     but should never be colored as a type.
        *  
        *  2. Color by Roslyn Classification (highly reliable)
        *  
        *     The roslyn classification property is generated specifically for syntax highlighting.
        *     Use it whenever its available (only tokens defined in the uploaded file).
        *     
        *  3. Color by Roslyn SemanticData (unreliable)
        *  
        *     Additional semantic data is gathered for identifier tokens. But which tokens actually
        *     receive data and what that data says is inconsistent. Mostly it just helps to highlight
        *     a small handful of externally defined identifiers from referenced assembiles.
        *     
        *     The roslyn semantic data is also subject to change and could break in the future. This
        *     highlighting step can be toggled by the appsetting SemanticDataHighlightingEnabled.
        *  
        *  4. Color by SemanticRole (not fully reliable until unit tests are in place)
        *  
        *     A semantic role should be generated for every token by this point. Use this role to 
        *     highlight remaining tokens. This method will not be able to distinguish externally
        *     defined classes, enums, structs, etc. In those cases, a best guess is used.
        *  
        *  5. Color unidentified tokens red.
        *  
        */

        public void AddSyntaxHighlightingToNavTokens(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                // color manually
                if (token.IsCommonIdentifier())
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

                // color by semantic data (subject to change & could break in the future)
                if (_config.SemanticDataHighlightingEnabled)
                {
                    ColorBySemanticData(token);
                    if (token.HighlightColor is not null)
                        continue;
                }

                // color by semantic role
                ColorBySemanticRole(token);
                if (token.HighlightColor is not null)
                    continue;

                // color by ancestors
                ColorByAncestors(token);
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
            // early-outs for local var, parameter, or property
            // identifier that will never be colored as a type
            if (token.Map.SemanticRole is SemanticRole.MemberAccess or SemanticRole.TargetMember)
                return;

            if (token.Map.SemanticRole.ToString().Contains("Declaration"))
                return;

            if (token.Map.SemanticRole.ToString().Contains("Reference"))
                return;

            if (token.Map.SemanticRole == SemanticRole.AssignmentRecipient)
                return;

            // if it looks like a common type name, color it accordingly
            if (GlobalConstants.CommonEnums.Contains(token.Text))
                token.HighlightColor = LightGreen;

            if (GlobalConstants.CommonStructs.Contains(token.Text))
                token.HighlightColor = Jade;
        }

        private static void ColorByKeyword(NavToken token)
        {
            // "default" can be blue or purple
            if (token.Text == "default")
            {
                token.HighlightColor = GetDefaultKeywordColor(token);
                return;
            }

            // "in" can be blue or purple
            if (token.Text == "in")
            {
                token.HighlightColor = GetInKeywordColor(token);
                return;
            }

            if (token.RoslynClassification == "keyword")
            {
                token.HighlightColor = Blue;
                return;
            }

            if (token.RoslynClassification == "keyword - control")
            {
                token.HighlightColor = Purple;
                return;
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
                case SemanticRole.TargetMember:
                case SemanticRole.NamespaceAliasDeclaration:
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
                case SemanticRole.Attribute:
                case SemanticRole.ClassDeclaration:
                case SemanticRole.ClassReference:
                case SemanticRole.CatchExceptionType:
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
                case SemanticRole.CastTarget:
                case SemanticRole.TypeConstraint:
                case SemanticRole.ConstructorInvocation:
                case SemanticRole.DeconstructionVariableType:
                case SemanticRole.EventFieldType:
                case SemanticRole.FieldType:
                case SemanticRole.GenericTypeArgument:
                case SemanticRole.LocalVariableType:
                case SemanticRole.LoopIteratorType:
                case SemanticRole.MethodReturnType:
                case SemanticRole.ParameterType:
                case SemanticRole.PropertyType:
                case SemanticRole.BaseType:
                case SemanticRole.TypeQualifier:
                case SemanticRole.TupleElementType:
                case SemanticRole.TypePattern:
                case SemanticRole.TypeReference:
                    token.HighlightColor = GuessColor(token.Text);
                    break;
            }
        }

        private static void ColorByAncestors(NavToken token)
        {
            // query expression variable refs
            if (token.IsQueryExpressionVariable())
            {
                token.HighlightColor = White;
                return;
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
            return token.IsUsingDirectiveQualifier() || token.IsNamespaceDeclarationQualifier()
                ? White
                : Gray;
        }

        private static string GuessColor(string text)
        {
            bool looksLikeInterface = text.Length >= 2
                && text[0] == 'I'
                && char.IsUpper(text[0])
                && char.IsUpper(text[1]);

            if (looksLikeInterface)
                return LightGreen;

            return Green;
        }
    }
}
