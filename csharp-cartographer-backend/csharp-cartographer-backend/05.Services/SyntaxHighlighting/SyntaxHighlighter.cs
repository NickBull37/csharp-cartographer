using csharp_cartographer_backend._01.Configuration.Configs;
using csharp_cartographer_backend._01.Configuration.Enums;
using csharp_cartographer_backend._01.Configuration.ReservedText;
using csharp_cartographer_backend._02.Utilities.Charts;
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
                AddReservedTextHighlighting(token);
                AddLiteralHighlighting(token);
                AddIdentifierHighlighting(token);
                AddIdentifierReferenceHighlighting(token, navTokens);
            }

            AddHighlightingThatNeedsSurroundingTokens(navTokens);

            if (_config.ShowUnhighlightedTokens)
            {
                HighlightRemainingTokens(navTokens);
            }
        }

        private static void AddReservedTextHighlighting(NavToken token)
        {
            if (HighlightColorAlreadySet(token))
            {
                return;
            }

            // default keyword can be blue or purple
            if (token.Text == "default")
            {
                token.HighlightColor = GetDefaultKeywordColor(token);
                return;
            }

            foreach (var element in ReservedTextElements.ElementList)
            {
                if (token.Text.Equals(element.Text))
                {
                    token.HighlightColor = element.HighlightColor;
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
                // numeric literals (integer, decimal, float)
                SyntaxKind.NumericLiteralToken => "color-light-green",

                // string and char literals
                SyntaxKind.StringLiteralToken or SyntaxKind.CharacterLiteralToken => "color-orange",

                // interpolated strings
                _ when ChartNavigator.IsInterpolatedStringStart(token)
                    || ChartNavigator.IsInterpolatedStringEnd(token)
                    || ChartNavigator.IsInterpolatedStringText(token) => "color-orange",

                // no color assignment
                _ => token.HighlightColor
            };
        }

        private static void AddIdentifierHighlighting(NavToken token)
        {
            if (HighlightColorAlreadySet(token))
            {
                return;
            }

            // interface delclaration identifier
            if (ChartNavigator.IsInterfaceDeclaration(token))
            {
                token.HighlightColor = "color-light-green";
                return;
            }

            // namespace delclaration identifier
            // using directive identifier
            // field identifier
            // property
            // property access
            if (ChartNavigator.IsNamespaceDeclaration(token)
                || ChartNavigator.IsUsingDirective(token)
                || ChartNavigator.IsField(token)
                || ChartNavigator.IsProperty(token)
                || ChartNavigator.IsPropertyAccess(token))
            {
                token.HighlightColor = "color-white";
                return;
            }

            // constructor declaration identifier
            // class delclaration identifier
            // exceptions
            // declaration pattern
            // object creation identifiers
            // record declaration
            // catch declarations
            if (ChartNavigator.IsConstructorDeclarationIdentifier(token)
                || ChartNavigator.IsClassDeclaration(token)
                || ChartNavigator.IsException(token)
                || ChartNavigator.IsDeclarationPattern(token)
                || ChartNavigator.IsObjectCreationIdentifier(token)
                || ChartNavigator.IsRecordDeclaration(token)
                || ChartNavigator.IsCatchDeclaration(token))
            {
                token.HighlightColor = "color-green";
                return;
            }

            // method delclarations
            // expressions
            if (ChartNavigator.IsMethodDeclaration(token)
                || ChartNavigator.IsExpression(token))
            {
                token.HighlightColor = "color-yellow";
                return;
            }

            // variable delclarations
            // parameter identifier
            // exception identifiers
            // name colons
            // single var designations
            // for loop identifier
            // foreach variable
            if (ChartNavigator.IsVariableDeclaration(token)
                || ChartNavigator.IsParameter(token)
                || ChartNavigator.IsExceptionIdentifier(token)
                || ChartNavigator.IsNameColon(token)
                || ChartNavigator.IsSingleVarDesignation(token)
                || ChartNavigator.IsForLoopIdentifier(token)
                || ChartNavigator.IsForEachVariable(token))
            {
                token.HighlightColor = "color-light-blue";
                return;
            }

            // parameter type identifier
            // type argument identifiers
            // base types
            // data types
            // attributes
            if (ChartNavigator.IsParameterType(token)
                || ChartNavigator.IsTypeArgument(token)
                || ChartNavigator.IsBaseType(token)
                || ChartNavigator.IsDataType(token)
                || ChartNavigator.IsAttribute(token))
            {
                token.HighlightColor = GetClassOrInterfaceColor(token.Text);
                return;
            }
        }

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
                default:
                    break;
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
                return "color-red"; // color red for unidentified tokens
            }

            if (char.IsUpper(text[0])
                && char.IsUpper(text[1])
                && text.StartsWith('I'))
            {
                return "color-light-green";
            }

            return "color-green";
        }

        private static void AddHighlightingThatNeedsSurroundingTokens(List<NavToken> navTokens)
        {
            for (int i = 0; i < navTokens.Count; i++)
            {
                var token = navTokens[i];
                bool onLastToken = token.Index == navTokens.Count - 1;
                var nextToken = onLastToken ? null : navTokens[i + 1];

                if (HighlightColorAlreadySet(token))
                {
                    continue;
                }

                // supposed to be for enum & static class member access
                if (nextToken != null
                    && ChartNavigator.IsMemberAccess(token)
                    && !ChartNavigator.IsInvocation(token)
                    && nextToken.Text == "."
                    && !token.Text.StartsWith('_'))
                {
                    token.HighlightColor = "color-green";
                }

                if (!ChartNavigator.IsInvocation(token))
                {
                    continue;
                }

                // method invocations
                if (nextToken.Text == "(")
                {
                    token.HighlightColor = "color-yellow";
                }

                // static class invocations
                if (nextToken.Text == "." && token.SemanticData?.SymbolKind != "Field")
                {
                    token.HighlightColor = "color-green";
                }
            }
        }

        private static void HighlightRemainingTokens(List<NavToken> navTokens)
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
