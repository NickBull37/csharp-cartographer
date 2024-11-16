using csharp_cartographer._01.Configuration.CSharpElements;
using csharp_cartographer._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Text.RegularExpressions;

namespace csharp_cartographer._05.Services.TokenTags
{
    public class TokenTagWizard : ITokenTagWizard
    {
        private static readonly string _newMethodInvocationLabel = "MethodIdentifier";
        private static readonly string _newFieldReferenceLabel = "FieldReferenceIdentifier";
        private static readonly string _newPropertyReferenceLabel = "PropertyReferenceIdentifier";
        private static readonly string _newVariableReferenceLabel = "VariableReferenceIdentifier";
        private static readonly string _newParameterReferenceLabel = "ParameterReferenceIdentifier";
        private static readonly string _newClassReferenceLabel = "ClassReferenceIdentifier";

        private static readonly string _newMethodInvocationColor = "color-yellow";
        private static readonly string _newFieldReferenceColor = "color-white";
        private static readonly string _newPropertyReferenceColor = "color-white";
        private static readonly string _newVariableReferenceColor = "color-light-blue";
        private static readonly string _newParameterReferenceColor = "color-light-blue";

        private static ITokenTagGenerator _tokenTagGenerator;

        public TokenTagWizard(ITokenTagGenerator tokenTagGenerator)
        {
            _tokenTagGenerator = tokenTagGenerator;
        }

        public void UpdateNavTokenTags(List<NavToken> navTokens)
        {
            /*
             * Steps to update token tags
             * 
             * Step 1. 
             * 
             * 
             * 
             */

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
                MakePropertyTypeUpdatesIfNeeded(token);
                MakeVariableDeclarationUpdatesIfNeeded(token);
                MakeTypeArgumentUpdatesIfNeeded(token);
                MakeBaseTypeUpdatesIfNeeded(token);
                MakeAttributeUpdatesIfNeeded(token);
                MakeArgumentPrefixUpdatesIfNeeded(token);
                MakeCatchDeclarationUpdatesIfNeeded(token);
                MakeNamespaceDeclarationUpdatesIfNeeded(token);
                MakeUsingDirectiveUpdatesIfNeeded(token);
            }

            RemoveRemainingIdentifierTags(navTokens);
            //AddTagExtenstions(navTokens);
            AddSpacesToTagLabels(navTokens);
            AddElementHighlightingIndicies(navTokens);
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
                token.Tags[1].Label = _newFieldReferenceLabel;
                token.HighlightColor = _newFieldReferenceColor;
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
                token.Tags[1].Label = _newClassReferenceLabel;
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
                token.Tags[1].Label = _newPropertyReferenceLabel;
                token.HighlightColor = _newPropertyReferenceColor;
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
                token.Tags[1].Label = _newVariableReferenceLabel;
                token.HighlightColor = _newVariableReferenceColor;
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
                token.Tags[1].Facts = ["The data type of the following parameter."];
                token.Tags[1].Insights = ["ParameterType insight"];
                token.HighlightColor = GetClassOrInterfaceColor(token.Text);
            }

            AddFactsAndInsights(token);
        }

        private static void MakeFieldDeclarationUpdatesIfNeeded(NavToken token)
        {
            // only applies to field names, not data types
            if (token.Tags.Count >= 4
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "VariableDeclarator"
                && token.Tags[2].Label == "VariableDeclaration"
                && token.Tags[3].Label == "FieldDeclaration")
            {
                if (token.Tags[1].Label == "IdentifierName")
                {
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

        private static void MakePropertyTypeUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "PropertyDeclaration")
            {
                if (token.Tags[1].Label == "IdentifierName")
                {
                    token.Tags[1].Label = "PropertyDataType";
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
                    token.Tags[1].Label = "VariableDataType";
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
                token.Tags.RemoveAt(1);
                token.HighlightColor = GetClassOrInterfaceColor(token.Text);
            }

            AddFactsAndInsights(token);
        }

        private static void MakeAttributeUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 2
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "Attribute")
            {
                // extend list for additional attributes
                if (token.Text == "ApiController")
                {
                    token.Tags[2].Label = "ApiController Attribute";
                }
                else if (token.Text == "Route")
                {
                    token.Tags[2].Label = "Route Attribute";
                }
                else if (token.Text == "HttpGet")
                {
                    token.Tags[2].Label = "HttpGet Attribute";
                }
                else if (token.Text == "HttpPost")
                {
                    token.Tags[2].Label = "HttpPost Attribute";
                }
                else if (token.Text == "FromQuery")
                {
                    token.Tags[2].Label = "FromQuery Attribute";
                }
                else if (token.Text == "FromBody")
                {
                    token.Tags[2].Label = "FromBody Attribute";
                }
                else
                {
                    token.Tags[2].Label = "AttributeIdentifier";
                }

                token.Tags.RemoveAt(1);
                token.HighlightColor = "color-green";
            }

            AddFactsAndInsights(token);
        }

        private static void MakeArgumentPrefixUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "NameColon"
                && token.Tags[3].Label == "Argument")
            {
                token.Tags[2].Label = "ArgumentPrefix";
                token.Tags.RemoveAt(1);
                token.HighlightColor = "color-light-blue";
            }

            AddFactsAndInsights(token);
        }

        private static void MakeCatchDeclarationUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 4
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "CatchDeclaration"
                && token.Tags[3].Label == "CatchClause")
            {
                token.Tags[2].Label = "CatchClauseDataType";
                token.Tags.RemoveAt(1);
                token.HighlightColor = GetClassOrInterfaceColor(token.Text);
            }

            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "CatchDeclaration"
                && token.Tags[2].Label == "CatchClause")
            {
                token.Tags[1].Label = "CatchClauseIdentifier";
                token.HighlightColor = "color-light-blue";
            }

            AddFactsAndInsights(token);
        }

        private static void MakeNamespaceDeclarationUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "NamespaceDeclaration")
            {
                token.Tags[1].Label = "NamespaceIdentifier";
                token.HighlightColor = "color-white";
            }

            AddFactsAndInsights(token);
        }

        private static void MakeUsingDirectiveUpdatesIfNeeded(NavToken token)
        {
            if (token.Tags.Count >= 3
                && token.Tags[0].Label == "IdentifierToken"
                && token.Tags[1].Label == "IdentifierName"
                && token.Tags[2].Label == "UsingDirective")
            {
                token.Tags[1].Label = "UsingDirectiveIdentifier";
                token.HighlightColor = "color-white";
            }

            AddFactsAndInsights(token);
        }

        public static void AddElementHighlightingIndicies(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                var tagCount = 1;
                foreach (var tag in token.Tags)
                {
                    if (tag.Tokens.Count == 0 || tagCount == 1)
                    {
                        tag.HighlightIndices.Add(token.Index);
                    }
                    else
                    {
                        GetElementIndices(navTokens, tag);
                    }
                    tagCount++;
                }
            }
        }

        private static void GetElementIndices(List<NavToken> navTokens, TokenTag tag)
        {
            if (tag.Label == "Simple Member Access Expression")
            {

            }

            List<int> highlightIndices = [];
            var elementTextStrings = GetElementStrings(tag);


            if (elementTextStrings.Count == 0 || navTokens.Count < elementTextStrings.Count)
            {
                return;
            }

            for (int i = 0; i <= navTokens.Count - elementTextStrings.Count; i++)
            {
                bool isMatch = true;

                for (int j = 0; j < elementTextStrings.Count; j++)
                {
                    if (navTokens[i + j].Text != elementTextStrings[j])
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                {
                    for (int j = 0; j < elementTextStrings.Count; j++)
                    {
                        highlightIndices.Add(i + j);
                    }
                    break;
                }
            }

            tag.HighlightIndices = highlightIndices;
        }

        private static List<string> GetElementStrings(TokenTag tag)
        {
            List<string> elementStrings = [];

            // trim endOfFile token from list
            if (tag.Tokens.Last().IsKind(SyntaxKind.EndOfFileToken))
            {
                tag.Tokens.RemoveAt(tag.Tokens.Count - 1);
            }
            // trim extra semicolon token from list
            if (tag.Tokens.Last().IsKind(SyntaxKind.SemicolonToken) && !tag.Label.EndsWith("Declaration"))
            {
                tag.Tokens.RemoveAt(tag.Tokens.Count - 1);
            }

            // correction - add lamda to element strings
            if (tag.Label == "Arrow Expression Clause")
            {
                elementStrings.Add("=>");
            }

            foreach (var roslynToken in tag.Tokens)
            {
                elementStrings.Add(roslynToken.Text);
            }
            return elementStrings;
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

        private static void RemoveRemainingIdentifierTags(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                if (token.Tags.Count > 0 && token.Tags[0].Label == "IdentifierToken")
                {
                    token.Tags.RemoveAt(0);
                }
            }
        }

        // TODO: stop adding spaces between words like DataType or ForEach
        private static void AddSpacesToTagLabels(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                foreach (var tag in token.Tags)
                {
                    if (tag.Label.Contains(' '))
                    {
                        continue;
                    }
                    tag.Label = Regex.Replace(tag.Label, "(?<!^)([A-Z])", " $1");
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
