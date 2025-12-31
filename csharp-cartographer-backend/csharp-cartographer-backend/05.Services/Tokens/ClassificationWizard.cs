using csharp_cartographer_backend._01.Configuration.Enums;
using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public class ClassificationWizard : IClassificationWizard
    {
        private static readonly HashSet<string> AttributeClassifications =
        [
            "identifier"
        ];

        private static readonly HashSet<string> BaseTypeClassifications =
        [
            "identifier"
        ];

        private static readonly HashSet<string> MethodDeclarationClassifications =
        [
            "method name",
            "static symbol"
        ];

        private static readonly HashSet<string> MethodInvocationClassifications =
        [
            "method name",
            "static symbol",
            "identifier"
        ];

        // parameters
        private static readonly HashSet<string> ParameterClassifications =
        [
            "parameter name",
        ];

        private static readonly HashSet<string> ParameterPrefixClassifications =
        [
            "identifier",
        ];

        private static readonly HashSet<string> ParameterTypeClassifications =
        [
            "class name",
            "interface name",
            "record class name",
            "identifier",
        ];

        // fields
        private static readonly HashSet<string> FieldClassifications =
        [
            "field name"
        ];

        private static readonly HashSet<string> FieldTypeClassifications =
        [
            "class name",
            "interface name",
            "record class name",
            "identifier",
        ];

        private static readonly HashSet<string> GenericTypeArgumentClassifications =
        [
            "class name",
            "interface name",
            "record class name",
            "identifier"
        ];

        // local vars
        private static readonly HashSet<string> LocalVariableClassifications =
        [
            "local name"
        ];

        private static readonly HashSet<string> LocalVariableTypeClassifications =
        [
            "class name",
            "interface name",
            "record class name",
            "identifier",
        ];

        private static readonly HashSet<string> NamespaceClassifications =
        [
            "namespace name"
        ];

        // properties
        private static readonly HashSet<string> PropertyDeclarationIdentifierClassifications =
        [
            "property name",
        ];

        private static readonly HashSet<string> PropertyReferenceIdentifierClassifications =
        [
            "property name",
        ];

        private static readonly HashSet<string> PropertyDeclarationTypeClassifications =
        [
            "class name",
            "interface name",
            "record class name",
            "identifier"
        ];

        private static readonly HashSet<string> UsingDirectiveClassifications =
        [
            // Roslyn will classify some using directive segments as namespace
            // name if that namespace is referenced in the uploaded file
            "namespace name",
            "identifier"
        ];

        private const string AttributeKind = "Attribute";
        private const string BaseListKind = "BaseList";
        private const string SimpleBaseTypeKind = "SimpleBaseType";
        private const string PropertyDeclarationKind = "PropertyDeclaration";
        private const string TypeArgumentListKind = "TypeArgumentList";
        private const string IdentifierNameKind = "IdentifierName";
        private const string QualifiedNameKind = "QualifiedName";
        private const string UsingDirectiveKind = "UsingDirective";
        private const string FieldDeclarationKind = "FieldDeclaration";

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

            // correct operator classifications
            if (token.RoslynClassification == "operator")
            {
                return GetOperatorCorrection(token);
            }

            // correct string literal classifications
            if (token.RoslynClassification == "string" || token.RoslynClassification == "string - verbatim")
            {
                return GetStringLiteralCorrection(token);
            }

            // correct number literal classifications
            if (token.RoslynClassification == "number")
            {
                return "literal - numeric";
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

            if (token.RoslynClassification == "keyword")
            {
                return $"keyword - {token.Text}";
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

            // correct class identifiers
            if (token.RoslynClassification == "class name" && token.ParentNodeKind == "ClassDeclaration")
            {
                return "identifier - class declaration";
            }
            if (token.RoslynClassification == "class name" && token.ParentNodeKind == "ConstructorDeclaration")
            {
                return "identifier - class constructor";
            }

            // correct interface identifiers
            if (token.RoslynClassification == "interface name" && token.ParentNodeKind == "InterfaceDeclaration")
            {
                return "identifier - interface declaration";
            }

            // correct record identifiers
            if (token.RoslynClassification == "record class name" && token.ParentNodeKind == "RecordDeclaration")
            {
                return "identifier - record declaration";
            }
            if (token.RoslynClassification == "record class name" && token.ParentNodeKind == "ConstructorDeclaration")
            {
                return "identifier - record constructor";
            }

            // correct property identifiers
            var propertyCorrection = TryGetPropertyCorrection(token);
            if (propertyCorrection is not null)
            {
                return propertyCorrection;
            }

            // correct parameter identifiers
            var parameterCorrection = TryGetParameterCorrection(token);
            if (parameterCorrection is not null)
            {
                return parameterCorrection;
            }

            // correct local identifiers
            var localVarCorrection = TryGetLocalVariableCorrection(token);
            if (localVarCorrection is not null)
            {
                return localVarCorrection;
            }

            // correct field identifiers
            var fieldCorrection = TryGetFieldCorrection(token);
            if (fieldCorrection is not null)
            {
                return fieldCorrection;
            }

            // correct namespace identifiers
            var namespaceCorrection = TryGetNamespaceCorrection(token);
            if (namespaceCorrection is not null)
            {
                return namespaceCorrection;
            }

            // correct attribute identifiers
            var attributeCorrection = TryGetAttributeCorrection(token);
            if (attributeCorrection is not null)
            {
                return attributeCorrection;
            }

            // correct base type identifiers
            var baseTypeCorrection = TryGetBaseTypeCorrection(token);
            if (baseTypeCorrection is not null)
            {
                return baseTypeCorrection;
            }

            // correct method identifiers
            var methodCorrection = TryGetMethodCorrection(token);
            if (methodCorrection is not null)
            {
                return methodCorrection;
            }

            // correct using directive identifiers
            var usingDirectiveCorrection = TryGetUsingDirectiveCorrection(token);
            if (usingDirectiveCorrection is not null)
            {
                return usingDirectiveCorrection;
            }

            // correct generic type parameters
            var genericTypeParameterCorrection = TryGetGenericTypeParameterCorrection(token);
            if (genericTypeParameterCorrection is not null)
            {
                return genericTypeParameterCorrection;
            }

            // correct generic type arguments
            var genericTypeArgumentCorrection = TryGetGenericTypeArgumentCorrection(token);
            if (genericTypeArgumentCorrection is not null)
            {
                return genericTypeArgumentCorrection;
            }

            return token.RoslynClassification;
        }

        // attributes
        // [DONE]
        private static string? TryGetAttributeCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;

            var hasAttributeAncestor = token.GrandParentNodeKind == AttributeKind;

            if (classification is null
                || !hasAttributeAncestor
                || !AttributeClassifications.Contains(classification))
            {
                return null;
            }

            return $"identifier - attribute";
        }

        // base types
        // [DONE]
        private static string? TryGetBaseTypeCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;

            var hasBaseTypeAncestor =
                token.GrandParentNodeKind == SimpleBaseTypeKind &&
                token.GreatGrandParentNodeKind == BaseListKind;

            if (classification is null
                || !hasBaseTypeAncestor
                || !BaseTypeClassifications.Contains(classification))
            {
                return null;
            }

            var typeExtenstion = LooksLikeInterface(token.Text)
                ? " - interface"
                : " - class";

            return $"identifier - base type{typeExtenstion}";
        }

        // field declarations
        // field references
        // field types
        // [DONE]
        private static string? TryGetFieldCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            bool isFieldRelated = classification is not null &&
                (
                    FieldClassifications.Contains(classification) ||
                    FieldTypeClassifications.Contains(classification)
                );

            if (!isFieldRelated)
            {
                return null;
            }

            // declarations & references
            if (classification == "field name")
            {
                var extension = token.ParentNodeKind == RoslynKind.VariableDeclarator.ToString()
                    ? "declaration"
                    : "reference";

                return $"identifier - field {extension}";
            }

            if (token.GreatGrandParentNodeKind != RoslynKind.FieldDeclaration.ToString()
                && token.GreatGreatGrandParentNodeKind != RoslynKind.FieldDeclaration.ToString())
            {
                return null;
            }

            // types
            var typeExtenstion = LooksLikeInterface(token.Text)
                ? " - interface"
                : " - class";

            var nullableExtension = token.GrandParentNodeKind == RoslynKind.NullableType.ToString()
                ? " - nullable"
                : string.Empty;

            return $"identifier - field type{typeExtenstion}{nullableExtension}";
        }

        // generic type arguments
        // [DONE]
        private static string? TryGetGenericTypeArgumentCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;

            var hasTypeArgumentListAncestor =
                token.GrandParentNodeKind == TypeArgumentListKind ||
                token.GreatGrandParentNodeKind == TypeArgumentListKind;

            if (classification is null
                || !hasTypeArgumentListAncestor
                || !GenericTypeArgumentClassifications.Contains(classification))
            {
                return null;
            }

            var typeExtension = classification switch
            {
                "class name" => " - class",
                "interface name" => " - interface",
                "record class name" => " - record",
                "identifier" => LooksLikeInterface(token.Text) ? " - interface" : " - class",
                _ => string.Empty
            };

            var nullableExtension = token.NextToken?.Text == "?" ? " - nullable" : string.Empty;

            return $"identifier - generic type argument{typeExtension}{nullableExtension}";
        }

        // generic type parameters
        // [DONE]
        private static string? TryGetGenericTypeParameterCorrection(NavToken token)
        {
            if (token.RoslynClassification != "type parameter name")
                return null;

            var constraintExtension = token.GrandParentNodeKind == "TypeParameterConstraintClause"
                ? " - constraint"
                : string.Empty;

            var nullableExtension = token.GrandParentNodeKind == "NullableType"
                ? " - nullable"
                : string.Empty;

            return $"identifier - generic type parameter{constraintExtension}{nullableExtension}";
        }

        // local var declarations
        // local var references
        // local var - for
        // local var - foreach
        // local var types
        // TODO: tweak for & foreach variables
        private static string? TryGetLocalVariableCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            bool isLocalVarRelated = classification is not null &&
                (
                    LocalVariableClassifications.Contains(classification) ||
                    LocalVariableTypeClassifications.Contains(classification)
                );

            if (!isLocalVarRelated)
            {
                return null;
            }

            if (classification == "local name")
            {
                // for
                if (token.GreatGrandParentNodeKind == RoslynKind.ForStatement.ToString())
                {
                    return $"identifier - local variable - for";
                }

                // foreach
                if (token.ParentNodeKind == RoslynKind.ForEachStatement.ToString())
                {
                    return $"identifier - local variable - foreach";
                }

                // declarations & references
                var extension = token.ParentNodeKind == RoslynKind.VariableDeclarator.ToString()
                    ? "declaration"
                    : "reference";

                return $"identifier - local variable {extension}";
            }


            if (token.GrandParentNodeKind != RoslynKind.VariableDeclaration.ToString()
                && token.GreatGrandParentNodeKind != RoslynKind.VariableDeclaration.ToString())
            {
                return null;
            }

            // types
            var typeExtenstion = LooksLikeInterface(token.Text)
                ? " - interface"
                : " - class";

            var nullableExtension = token.GrandParentNodeKind == RoslynKind.NullableType.ToString()
                ? " - nullable"
                : string.Empty;

            return $"identifier - local variable type{typeExtenstion}{nullableExtension}";
        }

        // method declarations
        // method invocations
        // [DONE]
        private static string? TryGetMethodCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            if (classification is null)
            {
                return null;
            }

            // declarations
            var isDeclaration = token.ParentNodeKind == RoslynKind.MethodDeclaration.ToString();
            if (isDeclaration && MethodDeclarationClassifications.Contains(classification))
            {
                string genExtension = string.Empty;
                if (token.NextToken?.Text == "<")
                {
                    genExtension = " - generic";
                }
                return $"identifier - method declaration{genExtension}";
            }

            // invocations
            bool isInvocation = token.GrandParentNodeKind == RoslynKind.InvocationExpression.ToString()
                || token.GreatGrandParentNodeKind == RoslynKind.InvocationExpression.ToString();

            var nextTokenText = token.NextToken?.Text;
            var hasPermittedNextToken = nextTokenText == "(" || nextTokenText == "<";

            if (!isInvocation
                || !hasPermittedNextToken
                || !MethodInvocationClassifications.Contains(classification))
            {
                return null;
            }

            var hasGenericAncestor = token.NextToken?.ParentNodeKind == RoslynKind.TypeArgumentList.ToString()
                || token.NextToken?.ParentNodeKind == RoslynKind.TypeParameterList.ToString();

            string genericExtension = hasGenericAncestor && nextTokenText == "<"
                ? " - generic"
                : string.Empty;

            return $"identifier - method invocation{genericExtension}";
        }

        // namespace segments
        // [DONE]
        private static string? TryGetNamespaceCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            var prevTokenText = token.PrevToken?.Text;

            var hasQualifiedNameAncestor = token.GrandParentNodeKind == QualifiedNameKind;
            var isMistakingUsingDirForNamespace = prevTokenText == "using";

            if (classification is null
                || !hasQualifiedNameAncestor
                || isMistakingUsingDirForNamespace
                || !NamespaceClassifications.Contains(classification))
            {
                return null;
            }

            return $"identifier - namespace segment";
        }

        // parameter declarations
        // parameter references
        // parameter types
        // parameter prefixes
        // [DONE]
        private static string? TryGetParameterCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            bool isParameterRelated = classification is not null &&
                (
                    ParameterClassifications.Contains(classification) ||
                    ParameterPrefixClassifications.Contains(classification) ||
                    ParameterTypeClassifications.Contains(classification)
                );

            if (!isParameterRelated)
            {
                return null;
            }

            // declarations & references
            if (classification == "parameter name")
            {
                var extension = token.ParentNodeKind == RoslynKind.Parameter.ToString()
                    ? "declaration"
                    : "reference";

                return $"identifier - parameter {extension}";
            }

            // prefixes
            if (classification == "identifier"
                && token.GrandParentNodeKind == RoslynKind.NameColon.ToString()
                && token.GreatGrandParentNodeKind == RoslynKind.Argument.ToString())
            {
                return $"identifier - parameter prefix";
            }

            if (token.GrandParentNodeKind != RoslynKind.Parameter.ToString()
                && token.GreatGrandParentNodeKind != RoslynKind.Parameter.ToString())
            {
                return null;
            }

            // types
            var typeExtenstion = LooksLikeInterface(token.Text)
                ? " - interface"
                : " - class";

            var nullableExtension = token.GrandParentNodeKind == RoslynKind.NullableType.ToString()
                && token.GreatGrandParentNodeKind == RoslynKind.Parameter.ToString()
                    ? " - nullable"
                    : string.Empty;

            return $"identifier - parameter type{typeExtenstion}{nullableExtension}";
        }

        // property declarations
        // property references
        // property types
        // [DONE]
        private static string? TryGetPropertyCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            if (classification is null)
            {
                return null;
            }

            // declarations
            var isPropertyDeclaration = token.ParentNodeKind == RoslynKind.PropertyDeclaration.ToString();
            if (isPropertyDeclaration && PropertyDeclarationIdentifierClassifications.Contains(classification))
            {
                return "identifier - property declaration";
            }

            // references
            if (PropertyReferenceIdentifierClassifications.Contains(classification))
            {
                return "identifier - property reference";
            }

            // types
            var hasPropertyDeclarationAncestor =
                token.GrandParentNodeKind == PropertyDeclarationKind ||
                token.GreatGrandParentNodeKind == PropertyDeclarationKind;

            if (!hasPropertyDeclarationAncestor
                || !PropertyDeclarationTypeClassifications.Contains(classification))
            {
                return null;
            }

            var typeExtenstion = LooksLikeInterface(token.Text)
                ? " - interface"
                : " - class";

            var genericExtension = token.ParentNodeKind == "GenericName"
                ? " - generic"
                : string.Empty;

            var nullableExtension = token.GrandParentNodeKind == "NullableType"
                && token.GreatGrandParentNodeKind == PropertyDeclarationKind
                    ? " - nullable"
                    : string.Empty;

            return $"identifier - property type{typeExtenstion}{genericExtension}{nullableExtension}";
        }

        // using directive segments
        // [DONE]
        private static string? TryGetUsingDirectiveCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            var prevTokenText = token.PrevToken?.Text;
            var nextTokenText = token.NextToken?.Text;

            var hasQualifiedNameAncestor = token.GrandParentNodeKind == QualifiedNameKind;
            bool hasPermittedNextAndPrevTokens = (nextTokenText, prevTokenText) switch
            {
                (".", ".") => true,
                (".", "using") => true,
                (";", ".") => true,
                _ => false
            };

            if (classification is null
                || !hasQualifiedNameAncestor
                || !hasPermittedNextAndPrevTokens
                || !UsingDirectiveClassifications.Contains(classification))
            {
                return null;
            }

            return $"identifier - using directive segment";
        }

        private static string? GetPunctuationCorrection(NavToken token)
        {
            // correct range operator
            if (token.Text == "..")
            {
                return $"operator - {token.Text}";
            }

            // correct delimiters
            if (IsDelimiterTokenText(token.Text))
            {
                return $"delimiter - {token.Text}";
            }

            // correct angle brackets
            if (IsAngleBracketTokenText(token.Text))
            {
                var parent = token.RoslynToken.Parent;
                if (parent is null) return null;

                // Generic type delimiters: List<T>
                if (parent.IsKind(SyntaxKind.TypeArgumentList) || parent.IsKind(SyntaxKind.TypeParameterList))
                    return $"delimiter - {token.Text}";

                // Comparison operators: a < b, a > b
                if (parent.IsKind(SyntaxKind.GreaterThanExpression) || parent.IsKind(SyntaxKind.LessThanExpression))
                    return $"operator - {token.Text}";
            }

            return $"{token.RoslynClassification} - {token.Text}";
        }

        private static string? GetOperatorCorrection(NavToken token)
        {
            return $"{token.RoslynClassification} - {token.Text}";
        }

        private static string? GetStringLiteralCorrection(NavToken token)
        {
            if (token.RoslynClassification == "string" && token.RoslynKind == "CharacterLiteralToken")
            {
                return "literal - char";
            }

            return (token.RoslynClassification, token.RoslynKind) switch
            {
                ("string", "InterpolatedStringStartToken") => "literal - interpolated string - start",
                ("string", "InterpolatedStringTextToken") => "literal - interpolated string - text",
                ("string", "InterpolatedStringEndToken") => "literal - interpolated string - end",

                ("string - verbatim", "InterpolatedVerbatimStringStartToken") => "literal - interpolated verbatim string - start",
                ("string - verbatim", "InterpolatedStringTextToken") => "literal - interpolated verbatim string - text",
                ("string - verbatim", "InterpolatedStringEndToken") => "literal - interpolated verbatim string - end",

                ("string - verbatim", _) => "literal - verbatim string",
                ("string", _) => "literal - quoted string",

                _ => token.RoslynClassification
            };
        }

        private static bool IsDelimiterTokenText(string text) =>
            text is "(" or ")" or "{" or "}" or "[" or "]";

        private static bool IsAngleBracketTokenText(string text) =>
            text is "<" or ">";

        private static bool LooksLikeInterface(string text)
        {
            return text.Length >= 2
                && text[0] == 'I'
                && char.IsUpper(text[0])
                && char.IsUpper(text[1]);
        }
    }
}
