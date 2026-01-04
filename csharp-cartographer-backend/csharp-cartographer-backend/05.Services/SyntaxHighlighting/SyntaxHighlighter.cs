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
                        HighlightReservedTextTokens(token, ReservedTextColors.Keywords);
                        break;
                    case string classification when classification.Contains("punctuation"):
                        HighlightReservedTextTokens(token, ReservedTextColors.Punctuators);
                        break;
                    case string classification when classification.Contains("delimiter"):
                        HighlightReservedTextTokens(token, ReservedTextColors.Delimiters);
                        break;
                    case string classification when classification.Contains("operator"):
                        HighlightReservedTextTokens(token, ReservedTextColors.Operators);
                        break;
                    case "identifier - namespace reference segment":
                        token.HighlightColor = "color-gray";
                        break;
                    case "identifier - attribute argument":
                    case "identifier - constant":
                    case "identifier - enum member declaration":
                    case "identifier - enum member reference":
                    case "identifier - field declaration":
                    case "identifier - field reference":
                    case "identifier - namespace declaration segment":
                    case "identifier - property access":
                    case "identifier - property declaration":
                    case "identifier - property initialization":
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
                    case "identifier - class constructor":
                    case "identifier - class declaration":
                    case "identifier - class reference":
                    case "identifier - constructor invocation":
                    case "identifier - constructor invocation - parameterless":
                    case "identifier - exception type":
                    case "identifier - field data type - class":
                    case "identifier - field data type - class - generic":
                    case "identifier - field data type - class - nullable":
                    case "identifier - field data type - class - generic - nullable":
                    case "identifier - generic type argument - class":
                    case "identifier - generic type argument - class - nullable":
                    case "identifier - generic type argument - record":
                    case "identifier - generic type argument - record - nullable":
                    case "identifier - generic type parameter constraint - class":
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
                    case "identifier - enum declaration":
                    case "identifier - enum reference":
                    case "identifier - field data type - interface":
                    case "identifier - field data type - interface - generic":
                    case "identifier - field data type - interface - nullable":
                    case "identifier - field data type - interface - generic - nullable":
                    case "identifier - generic type argument - interface":
                    case "identifier - generic type argument - interface - nullable":
                    case "identifier - generic type parameter":
                    case "identifier - generic type parameter - nullable":
                    case "identifier - generic type parameter constraint - interface":
                    case "identifier - interface declaration":
                    case "identifier - interface reference":
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
                    case "identifier - struct declaration":
                    case "identifier - struct constructor":
                    case "identifier - record struct declaration":
                    case "identifier - record struct constructor":
                        token.HighlightColor = "color-jade";
                        break;
                }
            }

            // TODO: add manual override of highlight color for common structs & enums

            HighlightUnidentifiedTokensRed(navTokens);
        }

        private static void HighlightReservedTextTokens(NavToken token, List<ReservedTextColor> list)
        {
            foreach (var element in list)
            {
                if (token.Text.Equals(element.Text))
                {
                    token.HighlightColor = element.HighlightColor;
                }
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
    }
}
