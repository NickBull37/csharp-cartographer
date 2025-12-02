using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._01.Configuration.Enums;
using csharp_cartographer_backend._01.Configuration.ReservedText;
using csharp_cartographer_backend._02.Utilities.Charts;
using csharp_cartographer_backend._02.Utilities.Logging;
using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Options;

namespace csharp_cartographer_backend._05.Services.SyntaxHighlighting
{
    public class SyntaxHighlighter : ISyntaxHighlighter
    {
        private readonly CartographerConfig _config;

        public SyntaxHighlighter(IOptions<CartographerConfig> config)
        {
            _config = config.Value;
        }

        public void AddSyntaxHighlightingToNavTokens(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                if (string.IsNullOrEmpty(token.Classification))
                {
                    continue;
                }

                // default keyword can be blue or purple
                if (token.Text == "default")
                {
                    token.HighlightColor = GetDefaultKeywordColor(token);
                    continue;
                }

                switch (token.Classification)
                {
                    case string classification when classification.Contains("keyword"):
                        AddClassificationHighlighting(token, ReservedTextElements.KeywordList);
                        break;
                    case "punctuation":
                        AddClassificationHighlighting(token, ReservedTextElements.PunctuatorList);
                        break;
                    case "delimiter":
                        AddClassificationHighlighting(token, ReservedTextElements.DelimiterList);
                        break;
                    case "operator":
                        AddClassificationHighlighting(token, ReservedTextElements.OperatorList);
                        break;
                    case "namespace name":
                    case "field name":
                    case "property name":
                        token.HighlightColor = "color-white";
                        break;
                    case "parameter name":
                    case "local name":
                        token.HighlightColor = "color-light-blue";
                        break;
                    case "method name":
                        token.HighlightColor = "color-yellow";
                        break;
                    case "class name":
                    case "record class name":
                        token.HighlightColor = "color-green";
                        break;
                    case "number":
                    case "type parameter name":
                        token.HighlightColor = "color-light-green";
                        break;
                    case "string":
                        token.HighlightColor = "color-orange";
                        break;
                    case string classification when classification.Contains("identifier"):
                        AddIdentifierHighlighting(token);
                        break;
                }
            }

            if (_config.ShowUnhighlightedTokens)
            {
                HighlightRemainingTokens(navTokens);
            }
        }

        private static void AddClassificationHighlighting(NavToken token, List<ReservedTextElement> list)
        {
            foreach (var element in list)
            {
                if (token.Text.Equals(element.Text))
                {
                    token.HighlightColor = element.HighlightColor;
                }
            }
        }

        private static void AddIdentifierHighlighting(NavToken token)
        {
            if (HighlightColorAlreadySet(token))
            {
                return;
            }

            if (ChartNavigator.IsUsingDirectiveIdentifier(token))
            {
                token.HighlightColor = "color-white";
                return;
            }

            // Color Interface identifier refs light green
            if (char.IsUpper(token.Text[0])
                && char.IsUpper(token.Text[1])
                && token.Text.StartsWith('I'))
            {
                token.HighlightColor = "color-light-green";
                return;
            }

            // regular method calls [works]
            if (token.NextToken?.Text == "(" && token.GrandParentNodeKind != "ObjectCreationExpression" && token.GrandParentNodeKind != "Attribute")
            {
                token.HighlightColor = "color-yellow";
                return;
            }
            // generic methods that need types applied
            // still have preceeding . token
            // next token is type argument opener <
            if (token.PrevToken?.Text == "." && token.NextToken?.Text == "<")
            {
                if (token.NextToken?.ParentNodeKind == "TypeArgumentList" || token.NextToken?.ParentNodeKind == "TypeParameterList")
                {
                    if (token.GrandParentNodeKind == "InvocationExpression" || token.GreatGrandParentNodeKind == "InvocationExpression")
                    {
                        token.HighlightColor = "color-yellow";
                        return;
                    }
                }
            }

            // color property identifier refs white
            if (token.PrevToken?.Text == "." && token.NextToken?.Text != "<")
            {
                token.HighlightColor = "color-white";
                return;
            }

            // color parameter prefixes light blue
            if (token.NextToken?.Text == ":" && token.GreatGrandParentNodeKind == "Argument")
            {
                token.HighlightColor = "color-light-blue";
                return;
            }

            // color inline assignment class property identifiers
            if (token.NextToken?.Text == "=")
            {
                token.HighlightColor = "color-white";
                return;
            }

            // color inline namespace identifiers white
            if (token.ParentNodeKind == "AliasQualifiedName"
                || token.GrandParentNodeKind == "AliasQualifiedName"
                || token.GreatGrandParentNodeKind == "AliasQualifiedName")
            {
                token.HighlightColor = "color-white";
                return;
            }

            // color class, enum, & struct refs green
            token.HighlightColor = "color-green";
            return;
        }

        //public void AddSyntaxHighlightingToNavTokens(List<NavToken> navTokens)
        //{

        //    // TODO: SyntaxHighlighter can't tell the difference between classes, enums, & structs (check NavToken props)

        //    // loop through tokens and set highlight color when possible
        //    foreach (var token in navTokens)
        //    {
        //        AddReservedTextHighlighting(token);
        //        AddLiteralHighlighting(token);
        //        AddIdentifierHighlighting(token);
        //    }

        //    // loop through again and set references for any identifiers
        //    foreach (var token in navTokens)
        //    {
        //        if (token.IsIdentifier)
        //        {
        //            AddIdentifierReferenceHighlighting(token, navTokens);
        //        }
        //    }

        //    foreach (var token in navTokens)
        //    {
        //        AddStaticClassHighlighting(token);
        //    }

        //    //AddHighlightingThatNeedsSurroundingTokens(navTokens);

        //    if (_config.ShowUnhighlightedTokens)
        //    {
        //        HighlightRemainingTokens(navTokens);
        //    }
        //}

        private static void AddReservedTextHighlighting(NavToken token)
        {
            // default keyword can be blue or purple
            if (token.Text == "default")
            {
                token.HighlightColor = GetDefaultKeywordColor(token);
                return;
            }

            LoopThroughListAndAddColor(ReservedTextElements.KeywordList);
            LoopThroughListAndAddColor(ReservedTextElements.DelimiterList);
            LoopThroughListAndAddColor(ReservedTextElements.OperatorList);
            LoopThroughListAndAddColor(ReservedTextElements.PunctuatorList);
            LoopThroughListAndAddColor(ReservedTextElements.SystemClassList);
            LoopThroughListAndAddColor(ReservedTextElements.SystemInterfaceList);
            LoopThroughListAndAddColor(ReservedTextElements.SystemStructList);

            void LoopThroughListAndAddColor(List<ReservedTextElement> reservedTextList)
            {
                if (HighlightColorAlreadySet(token))
                {
                    return;
                }

                foreach (var element in reservedTextList)
                {
                    if (token.Text.Equals(element.Text))
                    {
                        token.HighlightColor = element.HighlightColor;
                    }
                }
            }
        }

        private static void AddLiteralHighlighting(NavToken token)
        {
            if (HighlightColorAlreadySet(token))
            {
                return;
            }

            token.HighlightColor = token.Kind switch
            {
                // integer, decimal, float
                SyntaxKind.NumericLiteralToken => "color-light-green",

                // string, char
                SyntaxKind.StringLiteralToken or SyntaxKind.CharacterLiteralToken => "color-orange",

                // interpolated strings
                _ when ChartNavigator.IsInterpolatedStringStart(token)
                    || ChartNavigator.IsInterpolatedStringEnd(token)
                    || ChartNavigator.IsInterpolatedStringText(token) => "color-orange",

                // no color assignment
                _ => token.HighlightColor
            };
        }

        //private static void AddIdentifierHighlighting(NavToken token)
        //{
        //    if (HighlightColorAlreadySet(token))
        //    {
        //        return;
        //    }

        //    // interface delclaration identifier
        //    if (ChartNavigator.IsInterfaceDeclarationIdentifier(token))
        //    {
        //        token.HighlightColor = "color-light-green";
        //        return;
        //    }

        //    if (token.Index == 548)
        //    {

        //    }

        //    // namespace delclaration identifier
        //    // using directive identifier
        //    // field declaration identifier
        //    // property declaration identifier
        //    // property access
        //    if (ChartNavigator.IsNamespaceDeclaration(token)
        //        || ChartNavigator.IsUsingDirectiveIdentifier(token)
        //        || ChartNavigator.IsFieldDeclarationIdentifier(token)
        //        || ChartNavigator.IsPropertyDeclarationIdentifier(token)
        //        || ChartNavigator.IsPropertyAccess(token))
        //    {
        //        token.HighlightColor = "color-white";
        //        return;
        //    }

        //    // constructor declaration identifier
        //    // class delclaration identifier
        //    // exceptions
        //    // declaration pattern
        //    // object creation identifiers
        //    // record declaration
        //    // catch declarations
        //    if (ChartNavigator.IsConstructorDeclarationIdentifier(token)
        //        || ChartNavigator.IsClassDeclarationIdentifier(token)
        //        || ChartNavigator.IsException(token)
        //        || ChartNavigator.IsDeclarationPattern(token)
        //        || ChartNavigator.IsObjectCreationIdentifier(token)
        //        || ChartNavigator.IsRecordDeclaration(token)
        //        || ChartNavigator.IsCatchDeclaration(token))
        //    {
        //        token.HighlightColor = "color-green";
        //        return;
        //    }

        //    // method delclarations
        //    // method invocations
        //    if (ChartNavigator.IsMethodDeclaration(token)
        //        || ChartNavigator.IsMethodInvocation(token))
        //    {
        //        token.HighlightColor = "color-yellow";
        //        return;
        //    }

        //    // variable delclarations
        //    // parameter identifier
        //    // exception identifiers
        //    // name colons
        //    // single var designations
        //    // for loop identifier
        //    // foreach variable
        //    if (ChartNavigator.IsVariableDeclaration(token)
        //        || ChartNavigator.IsParameter(token)
        //        || ChartNavigator.IsExceptionIdentifier(token)
        //        || ChartNavigator.IsNameColon(token)
        //        || ChartNavigator.IsSingleVarDesignation(token)
        //        || ChartNavigator.IsForLoopIdentifier(token)
        //        || ChartNavigator.IsForEachVariable(token))
        //    {
        //        token.HighlightColor = "color-light-blue";
        //        return;
        //    }

        //    // parameter type identifier
        //    // type argument identifiers
        //    // base types
        //    // data types
        //    // attributes
        //    if (ChartNavigator.IsParameterType(token)
        //        || ChartNavigator.IsTypeArgument(token)
        //        || ChartNavigator.IsBaseType(token)
        //        || ChartNavigator.IsDataType(token)
        //        || ChartNavigator.IsAttribute(token))
        //    {
        //        token.HighlightColor = GetClassOrInterfaceColor(token.Text);
        //        return;
        //    }
        //}

        private static void AddIdentifierReferenceHighlighting(NavToken token, List<NavToken> navTokens)
        {
            switch (token)
            {
                case var _ when ChartNavigator.IsParameter(token):
                    UpdateParameterIdentifierReferences(navTokens, token.Index);
                    break;
                case var _ when ChartNavigator.IsVariableDeclaration(token):
                case var _ when ChartNavigator.IsSingleVarDesignation(token):
                    UpdateRefsInContainingBlock(navTokens, token.Text, token.Index, "color-light-blue");
                    break;
                case var _ when ChartNavigator.IsForEachVariable(token):
                case var _ when ChartNavigator.IsExceptionIdentifier(token):
                    UpdateRefsInNextBlock(navTokens, token.Text, token.Index, "color-light-blue");
                    break;
                case var _ when ChartNavigator.IsForLoopIdentifier(token):
                    UpdateForLoopIdentifierReferences(navTokens, token.Index);
                    break;
                case var _ when ChartNavigator.IsFieldDeclarationIdentifier(token):
                case var _ when ChartNavigator.IsPropertyDeclarationIdentifier(token):
                    UpdateRefsInContainingBlock(navTokens, token.Index);
                    break;
                default:
                    break;
            }
        }

        private static void AddStaticClassHighlighting(NavToken token)
        {
            if (HighlightColorAlreadySet(token))
            {
                return;
            }

            if (token.NextToken != null
                && ChartNavigator.IsMemberAccess(token)
                && ChartNavigator.IsInvocation(token))
            {
                token.HighlightColor = "color-green";
            }
        }

        private static void UpdateRefsInContainingBlock(List<NavToken> tokens, int startIndex)
        {
            var identifierName = tokens[startIndex].Text;
            int blockDepth = 1;

            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == "{")
                {
                    blockDepth++;
                }
                if (tokens[i].Text == "}")
                {
                    blockDepth--;
                    if (blockDepth == 0)
                    {
                        break;
                    }
                }
                if (tokens[i].Text == identifierName && !HighlightColorAlreadySet(tokens[i]))
                {
                    tokens[i].HighlightColor = "color-white";
                }
            }
        }

        private static void UpdateParameterIdentifierReferences(List<NavToken> tokens, int startIndex)
        {
            var parameterName = tokens[startIndex].Text;
            var bodyStructure = GetBodyStructure(tokens, startIndex);

            switch (bodyStructure)
            {
                case BodyStructure.BlockBody:
                    UpdateRefsInNextBlock(tokens, parameterName, startIndex, "color-light-blue");
                    break;
                case BodyStructure.ExpressionBody:
                    UpdateRefsInExpressionBody(tokens, parameterName, startIndex, "color-light-blue");
                    break;
                case BodyStructure.NoBody:  // interface
                default:
                    break;
            }
        }

        private static void UpdateForLoopIdentifierReferences(List<NavToken> tokens, int startIndex)
        {
            var identifierName = tokens[startIndex].Text;
            UpdateForLoopControlRefs(tokens, identifierName, startIndex, "color-light-blue");
            UpdateRefsInNextBlock(tokens, identifierName, startIndex, "color-light-blue");
        }

        private static void UpdateRefsInExpressionBody(List<NavToken> tokens, string reference, int startIndex, string color)
        {
            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == ";")
                {
                    break;
                }
                if (tokens[i].Text == reference)
                {
                    tokens[i].HighlightColor = color;
                }
            }
        }

        private static void UpdateRefsInNextBlock(List<NavToken> tokens, string reference, int startIndex, string color)
        {
            int blockDepth = 0;
            bool blockOpened = false;

            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == "{")
                {
                    blockDepth++;
                    blockOpened = true;
                }
                if (tokens[i].Text == "}")
                {
                    blockDepth--;
                    if (blockOpened && blockDepth == 0)
                    {
                        break;
                    }
                }
                if (blockOpened && blockDepth > 0 && tokens[i].Text == reference)
                {
                    tokens[i].HighlightColor = color;
                }
            }
        }

        private static void UpdateRefsInContainingBlock(List<NavToken> tokens, string reference, int startIndex, string color)
        {
            int blockDepth = 1;
            bool blockOpened = true;

            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == "{")
                {
                    blockDepth++;
                }
                if (tokens[i].Text == "}")
                {
                    blockDepth--;
                    if (blockOpened && blockDepth == 0)
                    {
                        break;
                    }
                }
                if (blockOpened && blockDepth > 0 && tokens[i].Text == reference)
                {
                    tokens[i].HighlightColor = color;
                }
            }
        }

        private static void UpdateForLoopControlRefs(List<NavToken> tokens, string reference, int startIndex, string color)
        {
            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == ")")
                {
                    break;
                }
                if (tokens[i].Text == reference)
                {
                    tokens[i].HighlightColor = color;
                }
            }
        }

        private static BodyStructure GetBodyStructure(List<NavToken> tokens, int startIndex)
        {
            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == ";")
                {
                    return BodyStructure.NoBody;
                }
                if (tokens[i].Text == "=>")
                {
                    return BodyStructure.ExpressionBody;
                }
                if (tokens[i].Text == "{")
                {
                    return BodyStructure.BlockBody;
                }
            }
            throw new Exception();
        }

        private static string GetDefaultKeywordColor(NavToken token) =>
            token.Text == "default" && token.Charts[1].Label == "DefaultSwitchLabel"
                ? "color-purple"
                : "color-blue";

        private static bool HighlightColorAlreadySet(NavToken token) =>
            !string.IsNullOrEmpty(token.HighlightColor);

        private static string GetClassOrInterfaceColor(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length < 2)
            {
                return "color-orange"; // color red for unidentified tokens
            }

            if (char.IsUpper(text[0])
                && char.IsUpper(text[1])
                && text.StartsWith('I'))
            {
                return "color-light-green";
            }

            return "color-green";
        }

        private static void HighlightRemainingTokens(List<NavToken> navTokens)
        {
            TokenLogger.ClearLogFile();

            foreach (var token in navTokens)
            {
                if (string.IsNullOrEmpty(token.HighlightColor))
                {
                    TokenLogger.LogToken(token);
                    token.HighlightColor = "color-red";
                }
            }
        }
    }
}
