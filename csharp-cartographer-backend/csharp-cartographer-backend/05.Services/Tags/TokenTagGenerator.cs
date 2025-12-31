using csharp_cartographer_backend._01.Configuration;
using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Tags
{
    public class TokenTagGenerator : ITokenTagGenerator
    {
        /// <summary>Iterates through the list of NavTokens and adds tags for each.</summary>
        /// <param name="navTokens">The list of NavTokens.</param>
        public void GenerateTokenTags(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                AddTokenTags(token);
            }
        }

        private static void AddTokenTags(NavToken token)
        {
            TryAddIdentifierTag(token);
            TryAddKeywordTag(token);
            TryAddAccessModifierTag(token);
            TryAddModifierTag(token);
            TryAddPredefinedTypeTag(token);
            TryAddParameterTag(token);
            TryAddStringLiteralTag(token);
            TryAddNumericLiteralTag(token);
            TryAddGenericTypeArgumentTag(token);
            TryAddAttributeTag(token);
            TryAddPunctuationTag(token);
            TryAddDelimiterTag(token);
            TryAddPropertyTag(token);
            TryAddFieldTag(token);
            TryAddBaseTypeTag(token);
            TryAddConstructorTag(token);
            TryAddMethodTag(token);
            TryAddClassTag(token);
            TryAddAccessorTag(token);
            TryAddOperatorTag(token);
        }

        private static void TryAddAccessorTag(NavToken token)
        {
            if (token.RoslynKind == "GetKeyword" || token.RoslynKind == "SetKeyword" || token.RoslynKind == "InitKeyword")
            {
                token.Tags.Add(new AccessorTag(token.Text));
            }
        }

        private static void TryAddAccessModifierTag(NavToken token)
        {
            if (GlobalConstants.AccessModifiers.Contains(token.Text))
            {
                token.Tags.Add(new AccessModifierTag(token.Text));
            }
        }

        private static void TryAddClassTag(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "ClassDeclaration")
            {
                token.Tags.Add(new ClassTag());
            }
        }

        private static void TryAddOperatorTag(NavToken token)
        {
            if (token.UpdatedClassification == "operator")
            {
                token.Tags.Add(new OperatorTag());
            }
        }

        private static void TryAddModifierTag(NavToken token)
        {
            if (GlobalConstants.Modifiers.Contains(token.Text))
            {
                token.Tags.Add(new ModifierTag(token.Text));
            }
        }

        private static void TryAddIdentifierTag(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken")
            {
                token.Tags.Add(new IdentifierTag());
            }
        }

        private static void TryAddKeywordTag(NavToken token)
        {
            if (token.RoslynKind.Contains("Keyword"))
            {
                token.Tags.Add(new KeywordTag());
            }
        }

        private static void TryAddStringLiteralTag(NavToken token)
        {
            //if (token.RoslynKind == "StringLiteralToken")
            //{
            //    token.Tags.Add(new StringLiteralTag());
            //}

            if (token.UpdatedClassification is not null && token.UpdatedClassification.Contains("string"))
            {
                token.Tags.Add(new StringLiteralTag(token.UpdatedClassification));
            }
        }

        private static void TryAddNumericLiteralTag(NavToken token)
        {
            if (token.RoslynKind == "NumericLiteralToken")
            {
                token.Tags.Add(new NumericLiteralTag());
            }
        }

        private static void TryAddAttributeTag(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "IdentifierName"
                && token.GrandParentNodeKind == "Attribute")
            {
                token.Tags.Add(new AttributeTag());
            }
        }

        private static void TryAddGenericTypeArgumentTag(NavToken token)
        {
            bool isGenericType = false;

            // handles pre-defined types
            if (GlobalConstants.PredefinedTypes.Contains(token.Text)
                && token.ParentNodeKind == "PredefinedType"
                && token.GrandParentNodeKind == "TypeArgumentList"
                && token.GreatGrandParentNodeKind == "GenericName")
            {
                isGenericType = true;
            }

            // handles non pre-defined types
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "IdentifierName"
                && token.GrandParentNodeKind == "TypeArgumentList"
                && token.GreatGrandParentNodeKind == "GenericName")
            {
                isGenericType = true;
            }

            if (isGenericType)
            {
                token.Tags.Add(new GenericTypeArgumentTag());
            }
        }

        private static void TryAddParameterTag(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "Parameter"
                && token.GrandParentNodeKind == "ParameterList")
            {
                token.Tags.Add(new ParameterTag());
            }
        }

        private static void TryAddPredefinedTypeTag(NavToken token)
        {
            if (GlobalConstants.PredefinedTypes.Contains(token.Text))
            {
                token.Tags.Add(new PredefinedTypeTag(token.Text));
            }
        }

        private static void TryAddPunctuationTag(NavToken token)
        {
            if (token.UpdatedClassification == "punctuation")
            {
                token.Tags.Add(new PunctuationTag());
            }
        }

        private static void TryAddDelimiterTag(NavToken token)
        {
            if (token.UpdatedClassification == "delimiter")
            {
                token.Tags.Add(new DelimiterTag());
            }
        }

        private static void TryAddPropertyTag(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "PropertyDeclaration")
            {
                token.Tags.Add(new PropertyTag());
            }
        }

        private static void TryAddMethodTag(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "MethodDeclaration")
            {
                token.Tags.Add(new MethodTag());
            }
        }

        private static void TryAddConstructorTag(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "ConstructorDeclaration")
            {
                token.Tags.Add(new ConstructorTag());
            }
        }

        private static void TryAddFieldTag(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "VariableDeclarator"
                && token.GrandParentNodeKind == "VariableDeclaration"
                && token.GreatGrandParentNodeKind == "FieldDeclaration")
            {
                token.Tags.Add(new FieldTag());
            }
        }

        private static void TryAddBaseTypeTag(NavToken token)
        {
            if (token.UpdatedClassification is not null && token.UpdatedClassification.Contains("base type - interface"))
            {
                token.Tags.Add(new InterfaceBaseTypeTag());
            }
            if (token.UpdatedClassification is not null && token.UpdatedClassification.Contains("base type - class"))
            {
                token.Tags.Add(new ClassBaseTypeTag());
            }

            //if (token.RoslynKind == "IdentifierToken"
            //    && token.ParentNodeKind == "IdentifierName"
            //    && token.GrandParentNodeKind == "SimpleBaseType"
            //    && token.GreatGrandParentNodeKind == "BaseList")
            //{
            //    if (char.IsUpper(token.Text[0])
            //        && char.IsUpper(token.Text[1])
            //        && token.Text.StartsWith('I'))
            //    {
            //        token.Tags.Add(new InterfaceBaseTypeTag());
            //    }
            //    else
            //    {
            //        token.Tags.Add(new ClassBaseTypeTag());
            //    }
            //}
        }
    }
}
