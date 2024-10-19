using csharp_cartographer._01.Configuration.CSharpElements;
using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._05.Services.TokenTags
{
    public class TokenTagWizard : ITokenTagWizard
    {
        private static readonly string _newMethodInvocationLabel = "MethodIdentifier";
        private static readonly string _newMethodInvocationColor = "color-yellow";

        public TokenTagWizard()
        {
        }

        public void UpdateNavTokenTags(List<NavToken> navTokens)
        {
            for (int i = 0; i < navTokens.Count; i++)
            {
                var token = navTokens[i];

                MakeMethodInvocationUpdatesIfNeeded(token, navTokens, i);
                MakeFieldReferenceUpdatesIfNeeded(token, navTokens, i);
                MakeClassReferenceUpdatesIfNeeded(token, navTokens, i);
                MakePropertyReferenceUpdatesIfNeeded(token, navTokens, i);
                MakeVariableReferenceUpdatesIfNeeded(token, navTokens, i);

                MakeDecimalLiteralUpdatesIfNeeded(token);
                MakeFloatLiteralUpdatesIfNeeded(token);
                MakeMethodReturnTypeUpdatesIfNeeded(token);
                MakeParameterDataTypeUpdatesIfNeeded(token);
                MakeFieldDeclarationUpdatesIfNeeded(token);
                MakePropertyDeclarationUpdatesIfNeeded(token);
                MakeVariableDeclarationUpdatesIfNeeded(token);
                MakeTypeArgumentUpdatesIfNeeded(token);
                MakeBaseTypeUpdatesIfNeeded(token);
            }
        }

        private static void MakeMethodInvocationUpdatesIfNeeded(NavToken token, List<NavToken> navTokens, int index)
        {
            if (token.Tags.Count >= 4
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "SimpleMemberAccessExpression"
                && token.Tags[3].Label == "InvocationExpression"
                && char.IsUpper(token.Text[0])
                && navTokens[index + 1].Text == "(")
            {
                token.Tags[1].Label = _newMethodInvocationLabel;
                token.HighlightColor = _newMethodInvocationColor;
            }

            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "InvocationExpression"
                && char.IsUpper(token.Text[0])
                && navTokens[index + 1].Text == "(")
            {
                token.Tags[1].Label = _newMethodInvocationLabel;
                token.HighlightColor = _newMethodInvocationColor;
            }

            AddFactsAndInsights(token);
        }

        // TODO: Only works on invocations
        // field, property, variable & parameter references
        private static void MakeFieldReferenceUpdatesIfNeeded(NavToken token, List<NavToken> navTokens, int index)
        {
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "SimpleMemberAccessExpression"
                && token.Text.StartsWith('_')
                && navTokens[index + 1].Text == ".")
            {
                token.Tags[1].Label = "FieldReferenceIdentifier";
                token.HighlightColor = "color-white";
            }

            AddFactsAndInsights(token);
        }

        private static void MakeClassReferenceUpdatesIfNeeded(NavToken token, List<NavToken> navTokens, int index)
        {
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "SimpleMemberAccessExpression"
                && char.IsUpper(token.Text[0])
                && navTokens[index + 1].Text == ".")
            {
                token.Tags[1].Label = "ClassReferenceIdentifier";
                token.HighlightColor = "color-green";
            }

            AddFactsAndInsights(token);
        }
        private static void MakePropertyReferenceUpdatesIfNeeded(NavToken token, List<NavToken> navTokens, int index)
        {
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "SimpleMemberAccessExpression"
                && char.IsUpper(token.Text[0])
                && navTokens[index - 1].Text == ".")
            {
                token.Tags[1].Label = "PropertyReferenceIdentifier";
                token.HighlightColor = "color-white";
            }

            AddFactsAndInsights(token);
        }
        private static void MakeVariableReferenceUpdatesIfNeeded(NavToken token, List<NavToken> navTokens, int index)
        {
            // TODO: stop this from highlighting parameter refs
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "SimpleMemberAccessExpression"
                && !char.IsUpper(token.Text[0])
                && !token.Text.StartsWith('_')
                && navTokens[index + 1].Text == ".")
            {
                token.Tags[1].Label = "VariableReferenceIdentifier";
                token.HighlightColor = "color-light-blue";
            }

            AddFactsAndInsights(token);
        }

        private static void MakeDecimalLiteralUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 1
                && token.Tags[0].Label.Equals("NumericLiteralToken")
                && token.Text.EndsWith('m'))
            {
                token.Tags[0].Label = "DecimalLiteralToken";
            }

            AddFactsAndInsights(token);
        }

        private static void MakeFloatLiteralUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 1
                && token.Tags[0].Label.Equals("NumericLiteralToken")
                && token.Text.EndsWith('f'))
            {
                token.Tags[0].Label = "FloatLiteralToken";
            }

            AddFactsAndInsights(token);
        }

        private static void MakeMethodReturnTypeUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "MethodDeclaration")
            {
                token.Tags[1].Label = "MethodReturnType";
                token.Tags[1].Facts = ["MethodReturnType fact"];
                token.Tags[1].Insights = ["MethodReturnType insight"];
                token.HighlightColor = GetClassOrInterfaceColor(token.Text);
            }

            AddFactsAndInsights(token);
        }

        private static void MakeParameterDataTypeUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "Parameter")
            {
                token.Tags[1].Label = "ParameterType";
                token.Tags[1].Facts = ["ParameterType fact"];
                token.Tags[1].Insights = ["ParameterType insight"];
                token.HighlightColor = GetClassOrInterfaceColor(token.Text);
            }

            AddFactsAndInsights(token);
        }

        private static void MakeFieldDeclarationUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 4
                && token.Tags[3].Label == "FieldDeclaration")
            {
                if (token.Tags[1].Label == "IdentifierName")
                {
                    token.Tags[1].Label = "DataType";
                    token.Tags[1].Label = "DataType";
                    token.Tags.RemoveAt(2);
                }

                if (token.Tags[1].Label == "VariableDeclarator")
                {
                    token.Tags.RemoveAt(2);
                    token.Tags.RemoveAt(1);
                }
            }

            AddFactsAndInsights(token);
        }

        private static void MakePropertyDeclarationUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 3
                && token.Tags[2].Label == "PropertyDeclaration")
            {
                if (token.Tags[1].Label == "IdentifierName")
                {
                    token.Tags[1].Label = "DataType";
                    token.Tags[1].Label = "DataType";
                }
            }

            AddFactsAndInsights(token);
        }

        private static void MakeVariableDeclarationUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 4
                && (token.Tags[2].Label == "VariableDeclaration" || token.Tags[3].Label == "VariableDeclaration"))
            {
                if (token.Tags[1].Label == "VariableDeclarator")
                {
                    token.Tags.RemoveAt(1);
                    token.HighlightColor = "color-light-blue";
                }

                if (token.Tags[1].Label == "IdentifierName")
                {
                    token.Tags[1].Label = "DataType";
                    token.Tags[1].Label = "DataType";
                    token.HighlightColor = GetClassOrInterfaceColor(token.Text);
                }
            }

            AddFactsAndInsights(token);
        }

        private static void MakeTypeArgumentUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 3
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "TypeArgumentList")
            {
                token.Tags[1].Label = "GenericType";
                token.Tags[1].Label = "GenericType";
                token.Tags.RemoveAt(3);
                token.HighlightColor = GetClassOrInterfaceColor(token.Text);
            }

            AddFactsAndInsights(token);
        }

        private static void MakeBaseTypeUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 3
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "SimpleBaseType")
            {
                token.Tags[2].Label = "InheritedBaseType";
                token.Tags[2].Label = "InheritedBaseType";
                token.Tags.RemoveAt(1);
                token.HighlightColor = GetClassOrInterfaceColor(token.Text);
            }

            AddFactsAndInsights(token);
        }

        private static void AddFactsAndInsights(NavToken token)
        {
            foreach (var tag in token.Tags)
            {
                foreach (var element in CSharpElements.ElementList)
                {
                    if (tag.Label.Equals(element.Label))
                    {
                        tag.Facts = element.Facts;
                        tag.Insights = element.Insights;
                    }
                }
            }
        }

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
