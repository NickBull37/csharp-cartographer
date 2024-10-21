using csharp_cartographer._01.Configuration.ReservedText;
using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._05.Services.SyntaxHighlighting
{
    public class SyntaxHighlighter : ISyntaxHighlighter
    {
        public SyntaxHighlighter()
        {
        }

        public void HighlightNavTokens(List<NavToken> navTokens)
        {
            /*
             *  SyntaxHighlighter only called after all token tags have
             *  been updated. Do all token tag edits before highlighting.
             */

            AddReservedTextHighlighting(navTokens);

            AddLiteralHighlighting(navTokens);

            AddIdentifierHighlighting(navTokens);

            AddIdentifierReferenceHighlighting(navTokens);
        }

        private static void AddReservedTextHighlighting(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                // default keyword can be blue or purple
                if (token.Text == "default" && token.Tags[1].Label == "DefaultSwitchLabel")
                {
                    token.HighlightColor = "color-purple";
                    continue;
                }
                else if (token.Text == "default" && token.Tags[1].Label == "DefaultLiteralExpression")
                {
                    token.HighlightColor = "color-blue";
                    continue;
                }

                foreach (var element in ReservedTextElements.ElementList)
                {
                    if (token.Text.Equals(element.Text))
                    {
                        token.HighlightColor = element.HighlightColor;
                    }
                }
            }
        }

        private static void AddLiteralHighlighting(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                if (HighlightColorAlreadySet(token))
                {
                    continue;
                }

                // string literals
                if (token.RoslynKind == "StringLiteralToken")
                {
                    token.HighlightColor = "color-orange";
                }
                // numeric literals (integer, decimal, float)
                if (token.RoslynKind == "NumericLiteralToken"
                    || token.RoslynKind == "DecimalLiteralToken"
                    || token.RoslynKind == "FloatLiteralToken")
                {
                    token.HighlightColor = "color-light-green";
                }
            }
        }

        private static void AddIdentifierHighlighting(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                if (HighlightColorAlreadySet(token))
                {
                    continue;
                }

                // using directive identifier
                if (token.Tags.Count > 1)
                {
                    if (token.Tags[1].Label == "Using Directive")
                    {
                        token.HighlightColor = "color-white";
                        continue;
                    }
                }

                // interface delclaration identifier
                if (token.Tags.Count > 0)
                {
                    if (token.Tags[0].Label == "Interface Declaration")
                    {
                        token.HighlightColor = "color-light-green";
                        continue;
                    }
                }

                // class delclaration identifier
                if (token.Tags.Count > 0)
                {
                    if (token.Tags[0].Label == "Class Declaration")
                    {
                        token.HighlightColor = "color-green";
                        continue;
                    }
                }

                // constructor delclaration identifier
                if (token.Tags.Count > 0)
                {
                    if (token.Tags[0].Label == "Constructor Declaration")
                    {
                        token.HighlightColor = "color-green";
                        continue;
                    }
                }

                // method delclaration identifier
                if (token.Tags.Count > 0)
                {
                    if (token.Tags[0].Label == "Method Declaration" && token.TrailingTrivia.Count == 0)
                    {
                        token.HighlightColor = "color-yellow";
                        continue;
                    }
                }

                // variable delclaration identifier
                if (token.Tags.Count > 0)
                {
                    if (token.Tags[0].Label == "Variable Declaration")
                    {
                        token.HighlightColor = "color-light-blue";
                        continue;
                    }
                }

                // parameter identifier
                if (token.Tags.Count > 0)
                {
                    if (token.Tags[0].Label == "Parameter")
                    {
                        token.HighlightColor = "color-light-blue";
                        continue;
                    }
                }

                // field identifier
                if (token.Tags.Count > 0)
                {
                    if (token.Tags[0].Label == "Field Declaration")
                    {
                        token.HighlightColor = "color-white";
                        continue;
                    }
                }

                // property identifier
                if (token.Tags.Count > 0)
                {
                    if (token.Tags[0].Label == "Property Declaration")
                    {
                        token.HighlightColor = "color-white";
                        continue;
                    }
                }

                // namespace delclaration identifier
                if (token.Tags.Count > 1)
                {
                    if (token.Tags[1].Label == "Namespace Declaration")
                    {
                        token.HighlightColor = "color-white";
                        continue;
                    }
                }

                // data types
                //if (token.Tags.Count > 1)
                //{
                //    if (token.Tags[1].Label == "DataType")
                //    {
                //        token.HighlightColor = GetClassOrInterfaceColor(token.Text);
                //    }
                //}
            }
        }

        private static void AddIdentifierReferenceHighlighting(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                // parameter refs
                if (token.Tags.Count >= 1
                    && token.Tags[0].Label == "Parameter")
                {
                    UpdateParameterReferences(navTokens, token.Index);
                }

                // variable refs
                if (token.Tags.Count >= 1
                    && token.Tags[0].Label == "Variable Declaration")
                {
                    UpdateVariableReferences(navTokens, token.Index);
                }

                // foreach var refs
                if (token.Tags.Count >= 1
                    && token.Tags[0].Label == "ForEach Statement")
                {
                    //token.Tags[1].RoslynKind = "ForEachVariableIdentifier";
                    //token.Tags[1].Label = "ForEachVariableIdentifier";
                    token.HighlightColor = "color-light-blue";
                    UpdateForEachVariableReferences(navTokens, token.Index);
                }
            }
        }

        private static void UpdateVariableReferences(List<NavToken> tokens, int startIndex)
        {
            var parameterName = tokens[startIndex].Text;
            int zeroCount = 1;

            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == parameterName && zeroCount > 0)
                {
                    tokens[i].HighlightColor = "color-light-blue";
                }
                if (tokens[i].Text == "{")
                {
                    zeroCount++;
                }
                if (tokens[i].Text == "}")
                {
                    zeroCount--;
                }
                if (zeroCount is 0)
                {
                    break;
                }
            }
        }

        private static void UpdateForEachVariableReferences(List<NavToken> tokens, int startIndex)
        {
            var parameterName = tokens[startIndex].Text;
            int zeroCount = 0;
            bool blockOpened = false;

            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == parameterName && zeroCount > 0)
                {
                    tokens[i].HighlightColor = "color-light-blue";
                }
                if (tokens[i].Text == "{")
                {
                    blockOpened = true;
                    zeroCount++;
                }
                if (tokens[i].Text == "}")
                {
                    zeroCount--;
                }
                if (blockOpened && zeroCount is 0)
                {
                    break;
                }
            }
        }

        private static void UpdateParameterReferences(List<NavToken> tokens, int startIndex)
        {
            var parameterName = tokens[startIndex].Text;
            int zeroCount = 0;

            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                // interface param - nothing to hightlight
                if (tokens[i].Text == ";" && zeroCount == 0)
                {
                    break;
                }

                // expression-bodied method
                if (tokens[i].Text == "=>" && zeroCount == 0)
                {
                    UpdateExpressionBodiedParameterRefs(tokens, parameterName, i);
                    break;
                }

                // block-bodied method
                if (tokens[i].Text == "{" && zeroCount == 0)
                {
                    UpdateBlockBodiedParameterRefs(tokens, parameterName, i);
                    break;
                }
            }
        }

        private static void UpdateExpressionBodiedParameterRefs(List<NavToken> tokens, string parameterName, int startIndex)
        {
            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == parameterName)
                {
                    tokens[i].HighlightColor = "color-light-blue";
                }
                if (tokens[i].Text == ";")
                {
                    break;
                }
            }
        }

        private static void UpdateBlockBodiedParameterRefs(List<NavToken> tokens, string parameterName, int startIndex)
        {
            var zeroCount = 1;
            var blockClosed = false;

            for (int i = startIndex + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Text == "{")
                {
                    zeroCount++;
                }
                if (tokens[i].Text == "}")
                {
                    zeroCount--;
                    if (zeroCount == 0)
                    {
                        blockClosed = true;
                    }
                }
                if (tokens[i].Text == parameterName && !blockClosed)
                {
                    tokens[i].HighlightColor = "color-light-blue";
                }
            }

        }

        private static bool HighlightColorAlreadySet(NavToken token)
        {
            if (!string.IsNullOrEmpty(token.HighlightColor))
            {
                return true;
            }
            return false;
        }

        // TODO: Move to helper, dupped in tokentag wizard
        private static string GetClassOrInterfaceColor(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length < 2)
            {
                return "color-red";
            }

            if (char.IsUpper(text[0])
                && char.IsUpper(text[1])
                && text.StartsWith('I'))
            {
                return "color-light-green";
            }

            return "color-green";
        }
    }
}
