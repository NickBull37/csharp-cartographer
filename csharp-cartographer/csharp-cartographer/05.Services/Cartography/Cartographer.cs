using csharp_cartographer._03.Models.Charts;
using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._05.Services.Cartography
{
    public class Cartographer : ICartographer
    {
        private static readonly List<string> _punctuationChars =
        [
            ".",
            ",",
            ";",
            ":",
            "?"
        ];

        private static readonly List<string> _delimiterChars =
        [
            "(",
            ")",
            "[",
            "]",
            "{",
            "}",
            "<",
            ">"
        ];

        private static readonly List<string> _primitiveTypes =
        [
            "StringKeyword",
            "DecimalKeyword",
            "DoubleKeyword",
            "IntKeyword",
            "CharKeyword",
            "FloatKeyword",
            "BoolKeyword"
        ];

        private static readonly List<string> _accessModifiers =
        [
            "PublicKeyword",
            "PrivateKeyword",
            "ProtectedKeyword",
            "InternalKeyword"
        ];

        private static readonly List<string> _modifiers =
        [
            "AbstractKeyword",
            "AsyncKeyword",
            "ConstKeyword",
            "OverrideKeyword",
            "ReadOnlyKeyword",
            "SealedKeyword",
            "StaticKeyword",
            "VirtualKeyword",
            "VolatileKeyword"
        ];

        private static readonly List<string> _literalKinds =
        [
            "NumericLiteralToken",
            "StringLiteralToken"
        ];

        private static readonly List<string> _genericCollections =
        [
            "List",

        ];

        public Cartographer()
        {
        }

        public void AddNavigationCharts(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                AddCharts(token);
            }
        }

        private static void AddCharts(NavToken token)
        {
            AddIdentifierChartIfNeeded(token);
            AddKeywordChartIfNeeded(token);

            AddAccessModifierChartIfNeeded(token);
            AddModifierChartIfNeeded(token);
            AddPrimitiveTypeChartIfNeeded(token);
            AddParameterChartIfNeeded(token);
            AddParameterTypeChartIfNeeded(token);
            AddStringLiteralChartIfNeeded(token);
            AddNumericLiteralChartIfNeeded(token);
            AddGenericTypeArgumentChartIfNeeded(token);
            AddGenericCollectionChartIfNeeded(token);
            AddAttributeChartIfNeeded(token);
            AddPunctuationChartIfNeeded(token);
            AddDelimiterChartIfNeeded(token);
            AddPropertyChartIfNeeded(token);
            AddFieldChartIfNeeded(token);
            AddBaseTypeChartIfNeeded(token);
        }

        private static void AddAccessModifierChartIfNeeded(NavToken token)
        {
            if (_accessModifiers.Contains(token.RoslynKind))
            {
                token.Charts.Add(new AccessModifierChart());
            }
        }

        private static void AddModifierChartIfNeeded(NavToken token)
        {
            if (_modifiers.Contains(token.RoslynKind))
            {
                token.Charts.Add(new ModifierChart());
            }
        }

        private static void AddIdentifierChartIfNeeded(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken")
            {
                token.Charts.Add(new IdentifierChart());
            }
        }

        private static void AddKeywordChartIfNeeded(NavToken token)
        {
            if (token.RoslynKind.Contains("Keyword"))
            {
                token.Charts.Add(new KeywordChart());
            }
        }

        private static void AddStringLiteralChartIfNeeded(NavToken token)
        {
            if (token.RoslynKind == "StringLiteralToken")
            {
                token.Charts.Add(new StringLiteralChart());
            }
        }

        private static void AddNumericLiteralChartIfNeeded(NavToken token)
        {
            if (token.RoslynKind == "NumericLiteralToken")
            {
                token.Charts.Add(new NumericLiteralChart());
            }
        }

        private static void AddGenericCollectionChartIfNeeded(NavToken token)
        {
            if (_genericCollections.Contains(token.Text)
                && token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "GenericName")
            {
                token.Charts.Add(new GenericCollectionChart());
            }
        }

        private static void AddAttributeChartIfNeeded(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "IdentifierName"
                && token.GrandParentNodeKind == "Attribute")
            {
                token.Charts.Add(new AttributeChart());
            }
        }

        private static void AddGenericTypeArgumentChartIfNeeded(NavToken token)
        {
            bool isGenericType = false;

            if (_primitiveTypes.Contains(token.RoslynKind)
                && token.ParentNodeKind == "PredefinedType"
                && token.GrandParentNodeKind == "TypeArgumentList"
                && token.GreatGrandParentNodeKind == "GenericName")
            {
                isGenericType = true;
            }

            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "IdentifierName"
                && token.GrandParentNodeKind == "TypeArgumentList"
                && token.GreatGrandParentNodeKind == "GenericName")
            {
                isGenericType = true;
            }

            if (isGenericType)
            {
                token.Charts.Add(new GenericTypeArgumentChart());
            }
        }

        private static void AddParameterChartIfNeeded(NavToken token)
        {
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "Parameter"
                && token.GrandParentNodeKind == "ParameterList")
            {
                token.Charts.Add(new ParameterChart());
            }
        }

        private static void AddParameterTypeChartIfNeeded(NavToken token)
        {
            bool isParamType = false;

            // non-primitive types
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "IdentifierName"
                && token.GrandParentNodeKind == "Parameter"
                && token.GreatGrandParentNodeKind == "ParameterList")
            {
                isParamType = true;
            }

            // primitive types
            if (_primitiveTypes.Contains(token.RoslynKind)
                && token.ParentNodeKind == "PredefinedType"
                && token.GrandParentNodeKind == "Parameter"
                && token.GreatGrandParentNodeKind == "ParameterList")
            {
                isParamType = true;
            }

            if (isParamType)
            {
                token.Charts.Add(new ParameterTypeChart());
            }
        }

        private static void AddPrimitiveTypeChartIfNeeded(NavToken token)
        {
            if (_primitiveTypes.Contains(token.RoslynKind)
                && token.ParentNodeKind == "PredefinedType")
            {
                token.Charts.Add(new PrimitiveTypeChart());
            }
        }

        private static void AddPunctuationChartIfNeeded(NavToken token)
        {
            // punctuation
            if (_punctuationChars.Contains(token.Text))
            {
                token.Charts.Add(new PunctuationChart());
            }
        }

        private static void AddDelimiterChartIfNeeded(NavToken token)
        {
            // delimiters
            bool isAligatorClip = false;

            if (token.Text == "<" || token.Text == ">")
            {
                isAligatorClip = true;
            }

            if (_delimiterChars.Contains(token.Text) && !isAligatorClip)
            {
                token.Charts.Add(new DelimiterChart());
            }
            else if (_delimiterChars.Contains(token.Text) && isAligatorClip)
            {
                if (token.ParentNodeKind != "LessThanExpression"
                    && token.ParentNodeKind != "GreaterThanExpression")
                {
                    token.Charts.Add(new DelimiterChart());
                }
            }
        }

        private static void AddPropertyChartIfNeeded(NavToken token)
        {
            // properties
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "PropertyDeclaration")
            {
                token.Charts.Add(new PropertyChart());
            }
        }

        private static void AddFieldChartIfNeeded(NavToken token)
        {
            // fields
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "VariableDeclarator"
                && token.GrandParentNodeKind == "VariableDeclaration"
                && token.GreatGrandParentNodeKind == "FieldDeclaration")
            {
                token.Charts.Add(new FieldChart());
            }
        }

        private static void AddBaseTypeChartIfNeeded(NavToken token)
        {
            // base types
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "IdentifierName"
                && token.GrandParentNodeKind == "SimpleBaseType"
                && token.GreatGrandParentNodeKind == "BaseList")
            {
                token.Charts.Add(new BaseTypeChart());
            }
        }

        private static void AddReturnTypeChartIfNeeded(NavToken token)
        {
            // return types
            if (token.RoslynKind == "IdentifierToken"
                && token.ParentNodeKind == "IdentifierName"
                && token.GrandParentNodeKind == "SimpleBaseType"
                && token.GreatGrandParentNodeKind == "BaseList")
            {
                token.Charts.Add(new BaseTypeChart());
            }
        }
    }
}
