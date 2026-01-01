using csharp_cartographer_backend._01.Configuration.ReservedText;
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
                    case string classification when classification.Contains("punctuation"):
                        AddClassificationHighlighting(token, ReservedTextColors.Punctuators);
                        break;
                    case string classification when classification.Contains("delimiter"):
                        AddClassificationHighlighting(token, ReservedTextColors.Delimiters);
                        break;
                    case string classification when classification.Contains("operator"):
                        AddClassificationHighlighting(token, ReservedTextColors.Operators);
                        break;
                    case "identifier - constant":
                    case "identifier - field declaration":
                    case "identifier - field reference":
                    case "identifier - namespace segment":
                    case "identifier - property declaration":
                    case "identifier - property reference":
                    case "identifier - using directive segment":
                        token.HighlightColor = "color-white";
                        break;
                    case "identifier - local variable declaration":
                    case "identifier - local variable reference":
                    case "identifier - local variable - for":
                    case "identifier - local variable - foreach":
                    case "identifier - parameter declaration":
                    case "identifier - parameter reference":
                    case "identifier - parameter prefix":
                        token.HighlightColor = "color-light-blue";
                        break;
                    case "identifier - method declaration":
                    case "identifier - method declaration - generic":
                    case "identifier - method invocation":
                    case "identifier - method invocation - generic":
                        token.HighlightColor = "color-yellow";
                        break;
                    case "identifier - attribute":
                    case "identifier - base type - class":
                    case "identifier - class declaration":
                    case "identifier - class constructor":
                    case "identifier - field data type - class":
                    case "identifier - field data type - class - generic":
                    case "identifier - field data type - class - nullable":
                    case "identifier - field data type - class - generic - nullable":
                    case "identifier - generic type argument - class":
                    case "identifier - generic type argument - class - nullable":
                    case "identifier - generic type argument - record":
                    case "identifier - generic type argument - record - nullable":
                    case "identifier - local variable type - class":
                    case "identifier - local variable type - class - generic":
                    case "identifier - local variable type - class - nullable":
                    case "identifier - local variable type - class - generic - nullable":
                    case "identifier - method return type - class":
                    case "identifier - method return type - class - generic":
                    case "identifier - method return type - class - nullable":
                    case "identifier - method return type - class - generic - nullable":
                    case "identifier - parameter data type - class":
                    case "identifier - parameter data type - class - generic":
                    case "identifier - parameter data type - class - nullable":
                    case "identifier - parameter data type - class - generic - nullable":
                    case "identifier - property data type - class":
                    case "identifier - property data type - class - generic":
                    case "identifier - property data type - class - nullable":
                    case "identifier - property data type - class - generic - nullable":
                    case "identifier - record declaration":
                    case "identifier - record constructor":
                        token.HighlightColor = "color-green";
                        break;
                    case "identifier - base type - interface":
                    case "identifier - field data type - interface":
                    case "identifier - field data type - interface - generic":
                    case "identifier - field data type - interface - nullable":
                    case "identifier - field data type - interface - generic - nullable":
                    case "identifier - generic type argument - interface":
                    case "identifier - generic type argument - interface - nullable":
                    case "identifier - generic type parameter":
                    case "identifier - generic type parameter - constraint":
                    case "identifier - generic type parameter - nullable":
                    case "identifier - interface declaration":
                    case "identifier - local variable type - interface":
                    case "identifier - local variable type - interface - generic":
                    case "identifier - local variable type - interface - nullable":
                    case "identifier - local variable type - interface - generic - nullable":
                    case "identifier - method return type - interface - generic":
                    case "identifier - method return type - interface - nullable":
                    case "identifier - method return type - interface - generic - nullable":
                    case "identifier - parameter data type - interface":
                    case "identifier - parameter data type - interface - generic":
                    case "identifier - parameter data type - interface - nullable":
                    case "identifier - parameter data type - interface - generic - nullable":
                    case "identifier - property data type - interface":
                    case "identifier - property data type - interface - generic":
                    case "identifier - property data type - interface - nullable":
                    case "identifier - property data type - interface - generic - nullable":
                    case "identifier - type parameter":
                    case "literal - numeric":
                        token.HighlightColor = "color-light-green";
                        break;
                    case "literal - char":
                    case "literal - quoted string":
                    case "literal - verbatim string":
                    case "literal - interpolated string - start":
                    case "literal - interpolated string - text":
                    case "literal - interpolated string - end":
                    case "literal - interpolated verbatim string - start":
                    case "literal - interpolated verbatim string - text":
                    case "literal - interpolated verbatim string - end":
                        token.HighlightColor = "color-orange";
                        break;
                    //case "identifier - property type":
                    //case "identifier - property type - nullable":
                    //    token.HighlightColor = GetFinalColorChoice(token);
                    //    break;
                    case string classification when classification.Contains("identifier"):
                        //AddIdentifierHighlighting(token);
                        break;
                }
            }

            HighlightUnidentifiedTokensRed(navTokens);
        }

        private static string GetFinalColorChoice(NavToken token)
        {
            // Color Interface identifier refs light green
            if (char.IsUpper(token.Text[0])
                && char.IsUpper(token.Text[1])
                && token.Text.StartsWith('I'))
            {
                return "color-light-green";
            }

            return "color-red";
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
                token.UpdatedClassification = "identifier - method invocation";
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
