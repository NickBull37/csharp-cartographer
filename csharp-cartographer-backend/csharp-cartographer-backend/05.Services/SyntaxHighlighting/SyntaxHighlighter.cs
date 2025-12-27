using csharp_cartographer_backend._01.Configuration.ReservedText;
using csharp_cartographer_backend._02.Utilities.Charts;
using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.SyntaxHighlighting
{
    public class SyntaxHighlighter : ISyntaxHighlighter
    {
        public void AddSyntaxHighlightingToNavTokens(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                if (string.IsNullOrEmpty(token.UpdatedClassification))
                {
                    continue;
                }

                // default keyword can be blue or purple
                if (token.Text == "default")
                {
                    token.HighlightColor = GetDefaultKeywordColor(token);
                    continue;
                }

                switch (token.UpdatedClassification)
                {
                    case string classification when classification.Contains("keyword"):
                        AddClassificationHighlighting(token, ReservedTextColors.Keywords);
                        break;
                    case "punctuation":
                        AddClassificationHighlighting(token, ReservedTextColors.Punctuators);
                        break;
                    case "delimiter":
                        AddClassificationHighlighting(token, ReservedTextColors.Delimiters);
                        break;
                    case "operator":
                        AddClassificationHighlighting(token, ReservedTextColors.Operators);
                        break;
                    case "identifier - namespace":
                    case "identifier - field declaration":
                    case "identifier - field reference":
                    case "identifier - property declaration":
                    case "identifier - property reference":
                    case "identifier - constant":
                        token.HighlightColor = "color-white";
                        break;
                    case "identifier - parameter":
                    case "identifier - parameter prefix":
                    case "identifier - local variable":
                        token.HighlightColor = "color-light-blue";
                        break;
                    case "identifier - method declaration":
                    case "identifier - method invocation":
                        token.HighlightColor = "color-yellow";
                        break;
                    case "identifier - class declaration":
                    case "identifier - class name":
                    case "identifier - constructor":
                    case "identifier - record declaration":
                        token.HighlightColor = "color-green";
                        break;
                    case "numeric literal":
                    case "type parameter name":
                        token.HighlightColor = "color-light-green";
                        break;
                    case "string literal":
                        token.HighlightColor = "color-orange";
                        break;
                    case string classification when classification.Contains("identifier"):
                        AddIdentifierHighlighting(token);
                        break;
                }
            }

            HighlightUnidentifiedTokensRed(navTokens);
        }

        private static void AddClassificationHighlighting(NavToken token, List<ReservedTextColor> list)
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
            // Roslyn cannot tell the difference between class, struct, or enum identifier
            // references when they are defined outside of the uploaded file. 

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
                token.UpdatedClassification = "method identifier - invocation";
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

        private static string GetDefaultKeywordColor(NavToken token) =>
            token.Text == "default" && token.Charts[1].Label == "DefaultSwitchLabel"
                ? "color-purple"
                : "color-blue";

        private static bool HighlightColorAlreadySet(NavToken token) =>
            !string.IsNullOrEmpty(token.HighlightColor);

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
    }
}
