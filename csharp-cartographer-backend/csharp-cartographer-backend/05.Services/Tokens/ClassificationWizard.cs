using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public class ClassificationWizard : IClassificationWizard
    {
        /// <summary>Updates classifications that are misleading or don't provide enough info to be helpful.</summary>
        public void CorrectTokenClassifications(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                token.UpdatedClassification = GetCorrectedClassification(token);
            }
        }

        private static string? GetCorrectedClassification(NavToken token)
        {
            // correct keyword classifications
            if (token.RoslynClassification is not null && token.RoslynClassification.Contains("keyword"))
            {
                return GetKeywordCorrection(token);
            }

            // correct identifier classifications
            if (token.RoslynKind == "IdentifierToken")
            {
                return GetIdentifierCorrection(token);
            }

            // correct punctuation classifications
            if (token.RoslynClassification == "punctuation")
            {
                return GetPunctuationCorrection(token);
            }

            // correct literal classifications
            if (token.RoslynClassification == "string" || token.RoslynClassification == "number")
            {
                return GetLiteralCorrection(token);
            }

            return token.RoslynClassification;
        }

        private static string? GetKeywordCorrection(NavToken token)
        {
            if (token.Text is "public" or "protected" or "private" or "internal")
            {
                return $"keyword - access modifier - {token.Text}";
            }

            if (token.Text is "static" or "abstract" or "sealed" or "void")
            {
                return $"keyword - modifier - {token.Text}";
            }

            if (token.Text is "int" or "bool" or "double" or "char" or "decimal" or "string")
            {
                return $"keyword - predefined type - {token.Text}";
            }

            if (token.RoslynClassification == "keyword - control")
            {
                return $"keyword - control - {token.Text}";
            }

            return token.RoslynClassification;
        }

        private static string? GetIdentifierCorrection(NavToken token)
        {
            // correct constant identifiers
            if (token.FieldSymbol?.IsConst == true)
            {
                return "identifier - constant";
            }

            // correct static symbol identifiers
            if (token.RoslynClassification == "static symbol")
            {
                if (token.NextToken?.Text == "(")
                {
                    return "method identifier - invocation";
                }
                if (token.PrevToken?.Text == "class")
                {
                    return "static class name identifier";
                }
            }

            // correct parameter prefix identifiers
            if (token.NextToken?.RoslynClassification == "punctuation"
                && token.GrandParentNodeKind == "NameColon"
                && token.GreatGrandParentNodeKind == "Argument")
            {
                return "identifier - parameter prefix";
            }

            // correct class identifiers
            if (token.RoslynClassification == "class name" && token.ParentNodeKind == "ClassDeclaration")
            {
                return "identifier - class declaration";
            }
            if (token.RoslynClassification == "class name" && token.ParentNodeKind == "ConstructorDeclaration")
            {
                return "identifier - constructor";
            }
            if (token.RoslynClassification == "class name")
            {
                return "identifier - class name";
            }

            // correct property identifiers
            if (token.RoslynClassification == "property name" && token.ParentNodeKind == "PropertyDeclaration")
            {
                return "identifier - property declaration";
            }
            if (token.RoslynClassification == "property name")
            {
                return "identifier - property reference";
            }

            // correct parameter identifiers
            if (token.RoslynClassification == "parameter name")
            {
                return "identifier - parameter";
            }

            // correct method identifiers
            if (token.RoslynClassification == "method name" && token.ParentNodeKind == "MethodDeclaration")
            {
                return "identifier - method declaration";
            }
            if (token.RoslynClassification == "method name" && token.ParentNodeKind == "InvocationExpression")
            {
                return "identifier - method invocation";
            }
            if (token.RoslynClassification == "method name")
            {
                return "identifier - method invocation";
            }

            // correct local identifiers
            if (token.RoslynClassification == "local name")
            {
                return "identifier - local variable";
            }

            // correct field identifiers
            if (token.RoslynClassification == "field name" && token.GreatGrandParentNodeKind == "FieldDeclaration")
            {
                return "identifier - field declaration";
            }
            if (token.RoslynClassification == "field name")
            {
                return "identifier - field reference";
            }

            // correct namespace identifiers
            if (token.RoslynClassification == "namespace name")
            {
                return "identifier - namespace";
            }

            // correct attribute identifiers
            if (token.RoslynClassification == "identifier" && token.GrandParentNodeKind == "Attribute")
            {
                return "identifier - attribute";
            }

            // correct record identifiers
            if (token.RoslynClassification == "record class name" && token.ParentNodeKind == "RecordDeclaration")
            {
                return "identifier - record declaration";
            }

            return token.RoslynClassification;
        }

        private static string? GetPunctuationCorrection(NavToken token)
        {
            // correct range operator
            if (token.Text == "..")
            {
                return "operator";
            }

            // correct delimiters
            if (IsDelimiterTokenText(token.Text))
            {
                return "delimiter";
            }

            // correct angle brackets
            if (IsAngleBracketTokenText(token.Text))
            {
                var parent = token.RoslynToken.Parent;
                if (parent is null) return null;

                // Generic type delimiters: List<T>
                if (parent.IsKind(SyntaxKind.TypeArgumentList) || parent.IsKind(SyntaxKind.TypeParameterList))
                    return "delimiter";

                // Comparison operators: a < b, a > b
                if (parent.IsKind(SyntaxKind.GreaterThanExpression) || parent.IsKind(SyntaxKind.LessThanExpression))
                    return "operator";
            }

            return token.RoslynClassification;
        }

        private static string? GetLiteralCorrection(NavToken token)
        {
            if (token.RoslynClassification == "string")
            {
                return "string literal";
            }
            if (token.RoslynClassification == "number")
            {
                return "numeric literal";
            }
            return token.RoslynClassification;
        }

        private static bool IsDelimiterTokenText(string text) =>
            text is "(" or ")" or "{" or "}" or "[" or "]";

        private static bool IsAngleBracketTokenText(string text) =>
            text is "<" or ">";
    }
}
