using csharp_cartographer_backend._01.Configuration.Enums;
using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public class ClassificationWizard : IClassificationWizard
    {
        // attributes
        private static readonly HashSet<string> AttributeClassifications =
        [
            "identifier"
        ];

        // base types
        private static readonly HashSet<string> BaseTypeClassifications =
        [
            "identifier"
        ];

        // classes
        private static readonly HashSet<string> ClassClassifications =
        [
            "class name",
            "static symbol"
        ];

        // interfaces
        private static readonly HashSet<string> InterfaceClassifications =
        [
            "interface name"
        ];

        // methods
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

        private static readonly HashSet<string> MethodReturnTypeClassifications =
        [
            "class name",
            "interface name",
            "record class name",
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

        // generic type args
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

        // namespaces
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

        // records
        private static readonly HashSet<string> RecordClassifications =
        [
            "record class name"
        ];

        // using directives
        private static readonly HashSet<string> UsingDirectiveClassifications =
        [
            /*
             *  Roslyn will classify some using directive segments as namespace name
             *  if that namespace is referenced in the uploaded file
             */

            "namespace name",
            "identifier"
        ];

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
            // constant identifiers
            if (token.FieldSymbol?.IsConst == true)
            {
                return "identifier - constant";
            }

            // class identifiers
            if (TryGetClassCorrection(token) is { } classCorrection)
            {
                return classCorrection;
            }

            // interface identifiers
            if (TryGetInterfaceCorrection(token) is { } interfaceCorrection)
            {
                return interfaceCorrection;
            }

            // record identifiers
            if (TryGetRecordCorrection(token) is { } recordCorrection)
            {
                return recordCorrection;
            }

            // property identifiers
            if (TryGetPropertyCorrection(token) is { } propertyCorrection)
            {
                return propertyCorrection;
            }

            // parameter identifiers
            if (TryGetParameterCorrection(token) is { } parameterCorrection)
            {
                return parameterCorrection;
            }

            // local identifiers
            if (TryGetLocalVariableCorrection(token) is { } localVarCorrection)
            {
                return localVarCorrection;
            }

            // field identifiers
            if (TryGetFieldCorrection(token) is { } fieldCorrection)
            {
                return fieldCorrection;
            }

            // namespace identifiers
            if (TryGetNamespaceCorrection(token) is { } namespaceCorrection)
            {
                return namespaceCorrection;
            }

            // attribute identifiers
            if (TryGetAttributeCorrection(token) is { } attributeCorrection)
            {
                return attributeCorrection;
            }

            // base type identifiers
            if (TryGetBaseTypeCorrection(token) is { } baseTypeCorrection)
            {
                return baseTypeCorrection;
            }

            // method identifiers
            if (TryGetMethodCorrection(token) is { } methodCorrection)
            {
                return methodCorrection;
            }

            // using directive identifiers
            if (TryGetUsingDirectiveCorrection(token) is { } usingDirectiveCorrection)
            {
                return usingDirectiveCorrection;
            }

            // generic type parameters
            // TODO: move this out of identifier highlighting (maybe to methods for return types)
            if (TryGetGenericTypeParameterCorrection(token) is { } genericTypeParameterCorrection)
            {
                return genericTypeParameterCorrection;
            }

            // generic type arguments
            if (TryGetGenericTypeArgumentCorrection(token) is { } genericTypeArgumentCorrection)
            {
                return genericTypeArgumentCorrection;
            }

            return token.RoslynClassification;
        }

        // [x] attributes
        private static string? TryGetAttributeCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;

            var hasAttributeAncestor = token.GrandParentNodeKind == RoslynKind.Attribute.ToString();

            if (classification is null
                || !hasAttributeAncestor
                || !AttributeClassifications.Contains(classification))
            {
                return null;
            }

            return $"identifier - attribute";
        }

        // [x] base types
        private static string? TryGetBaseTypeCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;

            var hasBaseTypeAncestor =
                token.GrandParentNodeKind == RoslynKind.SimpleBaseType.ToString() &&
                token.GreatGrandParentNodeKind == RoslynKind.BaseList.ToString();

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

        // [x] class declarations
        // [x] class constructors
        private static string? TryGetClassCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            bool isClassRelated = classification is not null
                && ClassClassifications.Contains(classification);

            if (!isClassRelated)
            {
                return null;
            }

            // declarations
            if (token.ParentNodeKind == RoslynKind.ClassDeclaration.ToString())
            {
                return $"identifier - class declaration";
            }

            // constructors
            if (token.ParentNodeKind == RoslynKind.ConstructorDeclaration.ToString())
            {
                return $"identifier - class constructor";
            }

            return null;
        }

        // [x] field declarations
        // [x] field references
        // [x] field types
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

            var genericExtension = token.ParentNodeKind == RoslynKind.GenericName.ToString()
                ? " - generic"
                : string.Empty;

            var nullableExtension = token.GrandParentNodeKind == RoslynKind.NullableType.ToString()
                ? " - nullable"
                : string.Empty;

            return $"identifier - field data type{typeExtenstion}{genericExtension}{nullableExtension}";
        }

        // [x] generic type arguments
        private static string? TryGetGenericTypeArgumentCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;

            var hasTypeArgumentListAncestor =
                token.GrandParentNodeKind == RoslynKind.TypeArgumentList.ToString() ||
                token.GreatGrandParentNodeKind == RoslynKind.TypeArgumentList.ToString();

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
                "identifier" => LooksLikeInterface(token.Text)
                    ? " - interface"
                    : " - class",
                _ => string.Empty
            };

            var nullableExtension = token.NextToken?.Text == "?"
                ? " - nullable"
                : string.Empty;

            return $"identifier - generic type argument{typeExtension}{nullableExtension}";
        }

        // [x] generic type parameters
        private static string? TryGetGenericTypeParameterCorrection(NavToken token)
        {
            if (token.RoslynClassification != "type parameter name")
                return null;

            var constraintExtension = token.GrandParentNodeKind == RoslynKind.TypeParameterConstraintClause.ToString()
                ? " - constraint"
                : string.Empty;

            var nullableExtension = token.GrandParentNodeKind == RoslynKind.NullableType.ToString()
                ? " - nullable"
                : string.Empty;

            return $"identifier - generic type parameter{constraintExtension}{nullableExtension}";
        }

        // [x] interface declarations
        private static string? TryGetInterfaceCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            bool isInterfaceRelated = classification is not null
                && InterfaceClassifications.Contains(classification);

            if (!isInterfaceRelated)
            {
                return null;
            }

            // declarations
            if (classification == "interface name"
                && token.ParentNodeKind == RoslynKind.InterfaceDeclaration.ToString())
            {
                return $"identifier - interface declaration";
            }

            return null;
        }

        // [x] local var declarations
        // [x] local var references
        // [x] local var - for
        // [x] local var - foreach
        // [x] local var types
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

            bool hasVariableDeclarationAncestor = token.GrandParentNodeKind == RoslynKind.VariableDeclaration.ToString()
                || token.GreatGrandParentNodeKind == RoslynKind.VariableDeclaration.ToString();

            bool hasLocalDeclarationStatementAncestor = token.GreatGrandParentNodeKind == RoslynKind.LocalDeclarationStatement.ToString()
                || token.GreatGreatGrandParentNodeKind == RoslynKind.LocalDeclarationStatement.ToString();

            if (!hasVariableDeclarationAncestor || !hasLocalDeclarationStatementAncestor)
            {
                return null;
            }

            // types
            var typeExtenstion = LooksLikeInterface(token.Text)
                ? " - interface"
                : " - class";

            var genericExtension = token.ParentNodeKind == RoslynKind.GenericName.ToString()
                ? " - generic"
                : string.Empty;

            var nullableExtension = token.GrandParentNodeKind == RoslynKind.NullableType.ToString()
                ? " - nullable"
                : string.Empty;

            return $"identifier - local variable type{typeExtenstion}{genericExtension}{nullableExtension}";
        }

        // [x] method declarations
        // [x] method invocations
        // [x] method return types
        // TODO: look into converting generic type params into return types
        private static string? TryGetMethodCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            bool isMethodRelated = classification is not null &&
                (
                    MethodDeclarationClassifications.Contains(classification) ||
                    MethodInvocationClassifications.Contains(classification) ||
                    MethodReturnTypeClassifications.Contains(classification)
                );

            if (!isMethodRelated)
            {
                return null;
            }

            // declarations
            if (token.ParentNodeKind == RoslynKind.MethodDeclaration.ToString() &&
                (
                    classification == "method name" ||
                    classification == "static symbol")
                )
            {
                string genExtension = string.Empty;
                if (token.NextToken?.Text == "<")
                {
                    genExtension = " - generic";
                }
                return $"identifier - method declaration{genExtension}";
            }

            // invocations
            var nextTokenText = token.NextToken?.Text;
            var hasPermittedNextToken = nextTokenText == "(" || nextTokenText == "<";
            var hasInvocationAncestor = token.GrandParentNodeKind == RoslynKind.InvocationExpression.ToString()
                || token.GreatGrandParentNodeKind == RoslynKind.InvocationExpression.ToString();

            bool isInvocation = hasInvocationAncestor
                && hasPermittedNextToken
                && MethodInvocationClassifications.Contains(classification);

            var hasGenericAncestor = token.NextToken?.ParentNodeKind == RoslynKind.TypeArgumentList.ToString()
                || token.NextToken?.ParentNodeKind == RoslynKind.TypeParameterList.ToString();

            string genericExtension = hasGenericAncestor && nextTokenText == "<"
                ? " - generic"
                : string.Empty;

            if (isInvocation)
            {
                return $"identifier - method invocation{genericExtension}";
            }

            // return types
            bool hasDeclarationAncestor = token.GrandParentNodeKind == RoslynKind.MethodDeclaration.ToString()
                || token.GreatGrandParentNodeKind == RoslynKind.MethodDeclaration.ToString();

            bool isReturnType = hasDeclarationAncestor && MethodReturnTypeClassifications.Contains(classification);
            if (isReturnType)
            {
                var typeExtenstion = LooksLikeInterface(token.Text)
                    ? " - interface"
                    : " - class";

                var nullableExtension = token.GrandParentNodeKind == RoslynKind.NullableType.ToString()
                    ? " - nullable"
                    : string.Empty;

                return $"identifier - method return type{typeExtenstion}{genericExtension}{nullableExtension}";
            }

            return null;
        }

        // [x] namespace segments
        private static string? TryGetNamespaceCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            var prevTokenText = token.PrevToken?.Text;

            var hasQualifiedNameAncestor = token.GrandParentNodeKind == RoslynKind.QualifiedName.ToString();
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

        // [x] parameter declarations
        // [x] parameter references
        // [x] parameter types
        // [x] parameter prefixes
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

            var genericExtension = token.ParentNodeKind == RoslynKind.GenericName.ToString()
                ? " - generic"
                : string.Empty;

            var nullableExtension = token.GrandParentNodeKind == RoslynKind.NullableType.ToString()
                && token.GreatGrandParentNodeKind == RoslynKind.Parameter.ToString()
                    ? " - nullable"
                    : string.Empty;

            return $"identifier - parameter data type{typeExtenstion}{genericExtension}{nullableExtension}";
        }

        // [x] property declarations
        // [x] property references
        // [x] property types
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
                token.GrandParentNodeKind == RoslynKind.PropertyDeclaration.ToString() ||
                token.GreatGrandParentNodeKind == RoslynKind.PropertyDeclaration.ToString();

            if (!hasPropertyDeclarationAncestor
                || !PropertyDeclarationTypeClassifications.Contains(classification))
            {
                return null;
            }

            var typeExtenstion = LooksLikeInterface(token.Text)
                ? " - interface"
                : " - class";

            var genericExtension = token.ParentNodeKind == RoslynKind.GenericName.ToString()
                ? " - generic"
                : string.Empty;

            var nullableExtension = token.GrandParentNodeKind == RoslynKind.NullableType.ToString()
                && token.GreatGrandParentNodeKind == RoslynKind.PropertyDeclaration.ToString()
                    ? " - nullable"
                    : string.Empty;

            return $"identifier - property data type{typeExtenstion}{genericExtension}{nullableExtension}";
        }

        // [x] record declarations
        // [x] record constructors
        private static string? TryGetRecordCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            bool isRecordRelated = classification is not null
                && RecordClassifications.Contains(classification);

            if (!isRecordRelated)
            {
                return null;
            }

            // declarations
            if (classification == "record class name"
                && token.ParentNodeKind == RoslynKind.RecordDeclaration.ToString())
            {
                return $"identifier - record declaration";
            }

            // constructors
            if (classification == "record class name"
                && token.ParentNodeKind == RoslynKind.ConstructorDeclaration.ToString())
            {
                return $"identifier - record constructor";
            }

            return null;
        }

        // [x] using directive segments
        private static string? TryGetUsingDirectiveCorrection(NavToken token)
        {
            var classification = token.RoslynClassification;
            var prevTokenText = token.PrevToken?.Text;
            var nextTokenText = token.NextToken?.Text;

            var hasQualifiedNameAncestor = token.GrandParentNodeKind == RoslynKind.QualifiedName.ToString();
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

        // correct any tokens Roslyn classifies as punctuation
        private static string? GetPunctuationCorrection(NavToken token)
        {
            // range operator
            if (token.Text == "..")
            {
                return $"operator - {token.Text}";
            }

            // delimiters
            if (IsDelimiterTokenText(token.Text))
            {
                return $"delimiter - {token.Text}";
            }

            // angle brackets (can be delimiters or operators)
            if (IsAngleBracketTokenText(token.Text))
            {
                var parent = token.RoslynToken.Parent;
                if (parent is null) return null;

                // generic type delimiters: List<T>
                if (parent.IsKind(SyntaxKind.TypeArgumentList) || parent.IsKind(SyntaxKind.TypeParameterList))
                    return $"delimiter - {token.Text}";

                // comparison operators: a < b, a > b
                if (parent.IsKind(SyntaxKind.GreaterThanExpression) || parent.IsKind(SyntaxKind.LessThanExpression))
                    return $"operator - {token.Text}";
            }

            return $"{token.RoslynClassification} - {token.Text}";
        }

        // correct any tokens Roslyn classifies as operator (none found yet)
        private static string? GetOperatorCorrection(NavToken token)
        {
            return $"{token.RoslynClassification} - {token.Text}";
        }

        // correct any tokens Roslyn classifies as string
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
