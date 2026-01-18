using csharp_cartographer_backend._01.Configuration;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    public class TokenMapper : ITokenMapper
    {
        public void MapNavTokens(List<NavToken> navTokens)
        {
            for (int i = 0; i < navTokens.Count; i++)
            {
                var token = navTokens[i];
                var previous = i > 0 ? navTokens[i - 1] : null;
                var next = i < navTokens.Count - 1 ? navTokens[i + 1] : null;

                token.Map = MapToken(token, previous, next);
            }
        }

        private TokenMap MapToken(NavToken token, NavToken? previous, NavToken? next)
        {
            var primaryKind = GetPrimaryKind(token);
            var role = GetSemanticRole(token, previous, next);
            var modifiers = GetSemanticModifiers(token);

            return new TokenMap(
                primaryKind: primaryKind,
                semanticRole: role,
                modifiers: modifiers.Count > 0 ? modifiers : null
            );
        }

        #region Primary Kind
        private static TokenPrimaryKind GetPrimaryKind(NavToken token)
        {
            switch (token.RoslynClassification)
            {
                case "keyword":
                case "keyword - control":
                    return TokenPrimaryKind.Keyword;
                case "operator":
                    return TokenPrimaryKind.Operator;
                case "punctuation":
                    return TokenPrimaryKind.Punctuation;
                case "string":
                case "string - verbatim":
                case "number":
                    return TokenPrimaryKind.Literal;
                case "delimiter":
                    return TokenPrimaryKind.Delimiter;
                case "class name":
                case "constant name":
                case "delegate name":
                case "enum name":
                case "enum member name":
                case "event name":
                case "field name":
                case "interface name":
                case "local name":
                case "method name":
                case "namespace name":
                case "parameter name":
                case "property name":
                case "record class name":
                case "record struct name":
                case "struct name":
                case "type parameter name":
                case "identifier":
                    return TokenPrimaryKind.Identifier;
                default:
                    return TokenPrimaryKind.Unknown;
            }
        }
        #endregion

        #region Semantic Roles
        private static SemanticRole GetSemanticRole(NavToken token, NavToken? previous, NavToken? next)
        {
            // --- Punctuation ---
            var punctuationRole = GetSemanticRoleForPunctuation(token);
            if (punctuationRole != SemanticRole.None)
                return punctuationRole;

            // --- Operators ---
            var operatorRole = GetSemanticRoleForOperators(token);
            if (operatorRole != SemanticRole.None)
                return operatorRole;

            // --- Delimiters ---
            var delimiterRole = GetSemanticRoleForDelimiters(token);
            if (delimiterRole != SemanticRole.None)
                return delimiterRole;

            // --- Identifier Declarations ---
            var declarationRole = GetSemanticRoleForDeclarations(token);
            if (declarationRole != SemanticRole.None)
                return declarationRole;

            // --- Identifier Data Types ---
            var dataTypeRole = GetSemanticRoleForIdentifierTypes(token);
            if (dataTypeRole != SemanticRole.None)
                return dataTypeRole;

            // --- Identifier References ---
            var referenceRole = GetSemanticRoleForIdentifierReferences(token);
            if (referenceRole != SemanticRole.None)
                return referenceRole;

            // --- Identifier Invocations ---
            var invocationRole = GetSemanticRoleForInvocations(token);
            if (invocationRole != SemanticRole.None)
                return invocationRole;

            // --- Identifier Using Directives & Namespaces ---
            var namespaceRole = GetSemanticRoleForNamespaces(token);
            if (namespaceRole != SemanticRole.None)
                return namespaceRole;

            // --- Identifier Generic Type Arguments ---
            var genTypeArgRole = GetSemanticRoleForGenericTypeArguments(token);
            if (genTypeArgRole != SemanticRole.None)
                return genTypeArgRole;

            // --- Identifier Base Types ---
            var baseTypeRole = GetSemanticRoleForBaseTypes(token);
            if (baseTypeRole != SemanticRole.None)
                return baseTypeRole;

            // --- Identifier Cast Types ---
            var castTypeRole = GetSemanticRoleForCastTypes(token);
            if (castTypeRole != SemanticRole.None)
                return castTypeRole;

            // --- Identifier Exception Types ---
            var exceptionTypeRole = GetSemanticRoleForExceptionTypes(token);
            if (exceptionTypeRole != SemanticRole.None)
                return exceptionTypeRole;

            // --- Identifier Constraint Types ---
            var typeConstraintRole = GetSemanticRoleForTypeConstraints(token);
            if (typeConstraintRole != SemanticRole.None)
                return typeConstraintRole;

            // --- Identifier Parameter Labels ---
            var parameterLabelRole = GetSemanticRoleForParameterLabels(token);
            if (parameterLabelRole != SemanticRole.None)
                return parameterLabelRole;

            // --- Literals ---
            var literalRole = GetSemanticRoleForLiterals(token);
            if (literalRole != SemanticRole.None)
                return literalRole;


            // --- Accessor keywords ---
            if (GlobalConstants.Accessors.Contains(token.Text))
                return SemanticRole.Accessor;

            // --- Access modifier keywords ---
            if (GlobalConstants.AccessModifiers.Contains(token.Text))
                return SemanticRole.AccessModifier;

            // --- Modifier keywords ---
            if (GlobalConstants.Modifiers.Contains(token.Text))
                return SemanticRole.Modifier;

            // --- Control flow keywords ---
            if (GlobalConstants.ConditionalKeywords.Contains(token.Text))
                return SemanticRole.Conditional;

            if (GlobalConstants.LoopKeywords.Contains(token.Text))
                return SemanticRole.Loop;

            if (GlobalConstants.JumpKeywords.Contains(token.Text))
                return SemanticRole.Jump;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForPunctuation(NavToken token)
        {
            if (!GlobalConstants.Punctuators.Contains(token.Text))
                return SemanticRole.None;

            // --- Seperators ---
            if (token.IsArgumentSeperator())
                return SemanticRole.ArgumentSeperator;

            if (token.IsTypeArgumentSeperator())
                return SemanticRole.TypeArgumentSeperator;

            if (token.IsBaseTypeSeperator())
                return SemanticRole.BaseTypeSeperation;

            //if (token.IsCaseLabelSeperator())
            //    return SemanticRole.CaseLabelSeperator;

            if (token.IsEnumMemberSeparator())
                return SemanticRole.EnumMemberSeparator;

            //if (token.IsGenericTypeSeperator())
            //    return SemanticRole.GenericTypeSeperator;

            if (token.IsParameterSeparator())
                return SemanticRole.ParameterSeparator;

            //if (token.IsPatternSeparator())
            //    return SemanticRole.PatternSeparator;

            if (token.IsSwitchClauseSeperator())
                return SemanticRole.SwitchClauseSeperator;

            //if (token.IsTupleElementSeperator())
            //    return SemanticRole.TupleElementSeperator;

            if (token.IsTypeParameterConstraintClauseSeperator())
                return SemanticRole.TypeParameterConstraintClauseSeperator;

            if (token.IsVariableDeclaratorSeparator())
                return SemanticRole.VariableDeclaratorSeparator;

            //if (token.IsVariableSeperator())
            //    return SemanticRole.VariableSeperator;

            // --- Terminators ---
            if (token.IsStatementTerminator())
                return SemanticRole.StatementTermination;

            if (token.IsLabelTerminator())
                return SemanticRole.ParameterLabelTermination;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForOperators(NavToken token)
        {
            if (!GlobalConstants.Operators.Contains(token.Text))
                return SemanticRole.None;

            // Arithmetic
            if (token.IsArithmeticOperator())
                return SemanticRole.ArithmeticOperator;

            // Assignment
            if (token.IsAssignmentOperator())
                return SemanticRole.AssignmentOperator;

            // Comparison
            if (token.IsComparisonOperator())
                return SemanticRole.ComparisonOperator;

            // Conditional
            if (token.IsConditionalOperator())
                return SemanticRole.ConditionalOperator;

            // Logical
            if (token.IsLogicalOperator())
                return SemanticRole.LogicalOperator;

            // Member access
            if (token.IsMemberAccessOperator())
                return SemanticRole.MemberAccessOperator;

            // Member access - conditional
            if (token.IsConditionalMemberAccessOperator())
                return SemanticRole.ConditionalMemberAccess;

            // Range
            if (token.IsRangeOperator())
                return SemanticRole.RangeOperator;

            // Namespace alias
            if (token.IsNamespaceAliasQualifier())
                return SemanticRole.NamespaceAliasQualifier;

            // Null-conditional guard
            if (token.IsNullConditionalGuard())
                return SemanticRole.NullConditionalGuard;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForDelimiters(NavToken token)
        {
            if (!GlobalConstants.Delimiters.Contains(token.Text))
                return SemanticRole.None;

            if (token.IsArgumentListDelimiter())
                return SemanticRole.ArgumentListBoundary;

            if (token.IsAttributeListDelimiter())
                return SemanticRole.AttributeListBoundary;

            if (token.IsAttributeArgumentListDelimiter())
                return SemanticRole.AttributeArgumentListBoundary;

            if (token.IsCollectionExpressionDelimiter())
                return SemanticRole.CollectionExpressionBoundary;

            if (token.IsForEachBlockDelimiter())
                return SemanticRole.ForEachBlockBoundary;

            if (token.IsIfBlockDelimiter())
                return SemanticRole.IfBlockBoundary;

            if (token.IsIfConditionDelimiter())
                return SemanticRole.IfConditionBoundary;

            if (token.IsTypeArgumentListDelimiter())
                return SemanticRole.TypeArgumentListBoundary;

            if (token.IsTypeParameterListDelimiter())
                return SemanticRole.TypeParameterListBoundary;

            if (token.IsParameterListDelimiter())
                return SemanticRole.ParameterListBoundary;

            if (token.IsObjectInitializerDelimiter())
                return SemanticRole.ObjectInitializerBoundary;

            if (token.IsInterpolatedValueDelimiter())
                return SemanticRole.InterpolatedValueBoundary;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForDeclarations(NavToken token)
        {
            if (!token.IsIdentifier())
                return SemanticRole.None;

            // Attribute declarations: [MyAttr] or [MyAttr(...)]
            if (token.IsAttributeDeclaration())
                return SemanticRole.AttributeDeclaration;

            // Class & class constructor declarations
            if (token.IsClassDeclaration())
                return SemanticRole.ClassDeclaration;

            if (token.IsClassConstructorDeclaration())
                return SemanticRole.ClassConstructorDeclaration;

            // Delegate declarations
            if (token.IsDelegateDeclaration())
                return SemanticRole.DelegateDeclaration;

            // Enum declarations
            if (token.IsEnumDeclaration())
                return SemanticRole.EnumDeclaration;

            // Enum member declarations
            if (token.IsEnumMemberDeclaration())
                return SemanticRole.EnumMemberDeclaration;

            // Event declarations
            if (token.IsEventDeclaration())
                return SemanticRole.EventDeclaration;

            // Event field declarations
            if (token.IsEventFieldDeclaration())
                return SemanticRole.EventFieldDeclaration;

            // Field declarations
            if (token.IsFieldDeclaration())
                return SemanticRole.FieldDeclaration;

            // Local variable declarations
            if (token.IsLocalVariableDeclaration())
                return SemanticRole.LocalVariableDeclaration;

            // Local for-loop variable declarations
            if (token.IsLocalForLoopVariableDeclaration())
                return SemanticRole.LocalVariableDeclaration;

            // Local foreach-loop variable declarations
            if (token.IsLocalForeachLoopVariableDeclaration())
                return SemanticRole.LocalVariableDeclaration;

            // Method declarations
            if (token.IsMethodDeclaration())
                return SemanticRole.MethodDeclaration;

            // Parameter declarations
            if (token.IsParameterDeclaration())
                return SemanticRole.ParameterDeclaration;

            // Property declarations
            if (token.IsPropertyDeclaration())
                return SemanticRole.PropertyDeclaration;

            // Records & record constructor declarations
            if (token.IsRecordDeclaration())
                return SemanticRole.RecordDeclaration;

            if (token.IsRecordConstructorDeclaration())
                return SemanticRole.RecordConstructorDeclaration;

            // Record structs & record struct declarations
            if (token.IsRecordStructDeclaration())
                return SemanticRole.RecordStructDeclaration;

            if (token.IsRecordStructConstructorDeclaration())
                return SemanticRole.RecordStructConstructorDeclaration;

            // Structs & struct constructor declarations
            if (token.IsStructDeclaration())
                return SemanticRole.StructDeclaration;

            if (token.IsStructConstructorDeclaration())
                return SemanticRole.StructConstructorDeclaration;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForIdentifierTypes(NavToken token)
        {
            // Only let through identifiers & pre-defined types; otherwise other
            // tokens get incorrect semantic roles assigned.
            if (!token.IsIdentifier() && !GlobalConstants.PredefinedTypes.Contains(token.Text))
                return SemanticRole.None;

            // Field data type
            if (token.IsFieldDataType())
                return SemanticRole.FieldDataType;

            // Local variable data type
            if (token.IsLocalVariableDataType())
                return SemanticRole.LocalVariableDataType;

            // Method return type
            if (token.IsMethodReturnType())
                return SemanticRole.MethodReturnType;

            // Parameter data types
            if (token.IsParameterDataType())
                return SemanticRole.ParameterDataType;

            // Property data types
            if (token.IsPropertyDataType())
                return SemanticRole.PropertyDataType;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForIdentifierReferences(NavToken token)
        {
            if (!token.IsIdentifier())
                return SemanticRole.None;

            // Enum references
            if (token.RoslynClassification == "enum name")
                return SemanticRole.EnumReference;

            // Enum member references
            if (token.RoslynClassification == "enum member name")
                return SemanticRole.EnumMemberReference;

            // Field references
            if (token.RoslynClassification == "field name")
                return SemanticRole.FieldReference;

            // Local variable references
            if (token.RoslynClassification == "local name")
                return SemanticRole.LocalVariableReference;

            // Parameter references
            if (token.RoslynClassification == "parameter name")
                return SemanticRole.ParameterReference;

            // Property references
            if (token.RoslynClassification == "property name")
                return SemanticRole.PropertyReference;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForInvocations(NavToken token)
        {
            if (!token.IsIdentifier())
                return SemanticRole.None;

            // Method invocations
            if (token.IsMethodInvocation())
                return SemanticRole.MethodInvocation;

            // Class constructor invocations
            if (token.RoslynClassification == "class name" && token.IsObjectCreationExpression())
                return SemanticRole.ClassConstructorInvocation;

            // Record constructor invocations
            if (token.RoslynClassification == "record class name" && token.IsObjectCreationExpression())
                return SemanticRole.RecordConstructorInvocation;

            // Record struct constructor invocations
            if (token.RoslynClassification == "record struct name" && token.IsObjectCreationExpression())
                return SemanticRole.RecordStructConstructorInvocation;

            // Struct constructor invocations
            if (token.RoslynClassification == "struct name" && token.IsObjectCreationExpression())
                return SemanticRole.StructConstructorInvocation;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForNamespaces(NavToken token)
        {
            // Using directives
            if (token.IsUsingDirectiveSegment())
                return SemanticRole.UsingDirective;

            // Namespaces
            if (token.IsNamespaceSegment())
                return SemanticRole.NamespaceDeclaration;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForGenericTypeArguments(NavToken token)
        {
            // Generic type arguments
            if (token.IsGenericTypeArgument())
                return SemanticRole.GenericTypeArgument;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForBaseTypes(NavToken token)
        {
            // Base types
            if (token.IsBaseType())
                return SemanticRole.SimpleBaseType;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForCastTypes(NavToken token)
        {
            // Cast types
            if (token.IsCastType())
                return SemanticRole.CastType;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForExceptionTypes(NavToken token)
        {
            // Exception types
            if (token.IsExceptionType())
                return SemanticRole.ExceptionType;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForTypeConstraints(NavToken token)
        {
            // Type constraints
            if (token.IsTypeConstraint())
                return SemanticRole.ConstraintType;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForParameterLabels(NavToken token)
        {
            // Parameter labels
            if (token.IsParameterLabel())
                return SemanticRole.ParameterLabel;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForLiterals(NavToken token)
        {
            // Numeric literals
            if (token.IsNumericLiteral())
                return SemanticRole.NumericLiteral;

            // String literals
            if (token.IsQuotedString() || token.IsVerbatimString() || token.IsInterpolatedString() || token.IsInterpolatedVerbatimString())
                return SemanticRole.StringLiteral;

            // Char literals
            if (token.IsCharacterLiteral())
                return SemanticRole.CharacterLiteral;

            // Boolean literals
            if (token.IsBooleanLiteral())
                return SemanticRole.BooleanLiteral;

            // Null literals
            if (token.IsNullLiteral())
                return SemanticRole.NullValue;

            return SemanticRole.None;
        }
        #endregion

        #region Semantic Modifiers
        private static HashSet<SemanticModifiers> GetSemanticModifiers(NavToken token)
        {
            var modifiers = new HashSet<SemanticModifiers>();

            // Generic type parameters
            if (token.IsGenericTypeParameter())
                modifiers.Add(SemanticModifiers.GenericTypeParameter);


            // Accessor modifiers
            if (token.IsKeyword("get"))
                modifiers.Add(SemanticModifiers.Getter);

            if (token.IsKeyword("set"))
                modifiers.Add(SemanticModifiers.Setter);

            if (token.IsKeyword("init"))
                modifiers.Add(SemanticModifiers.InitOnly);


            // Access modifiers
            if (token.IsKeyword("public"))
                modifiers.Add(SemanticModifiers.Public);

            if (token.IsKeyword("private"))
                modifiers.Add(SemanticModifiers.Private);

            if (token.IsKeyword("protected"))
                modifiers.Add(SemanticModifiers.Protected);

            if (token.IsKeyword("internal"))
                modifiers.Add(SemanticModifiers.Internal);


            // Member modifiers
            if (token.IsKeyword("abstract"))
                modifiers.Add(SemanticModifiers.Abstract);

            if (token.IsKeyword("async"))
                modifiers.Add(SemanticModifiers.Async);

            if (token.IsKeyword("const"))
                modifiers.Add(SemanticModifiers.Const);

            if (token.IsKeyword("override"))
                modifiers.Add(SemanticModifiers.Override);

            if (token.IsKeyword("partial"))
                modifiers.Add(SemanticModifiers.Partial);

            if (token.IsKeyword("readonly"))
                modifiers.Add(SemanticModifiers.Readonly);

            if (token.IsKeyword("required"))
                modifiers.Add(SemanticModifiers.Required);

            if (token.IsKeyword("sealed"))
                modifiers.Add(SemanticModifiers.Sealed);

            if (token.IsKeyword("static"))
                modifiers.Add(SemanticModifiers.Static);

            if (token.IsKeyword("virtual"))
                modifiers.Add(SemanticModifiers.Virtual);

            if (token.IsKeyword("volatile"))
                modifiers.Add(SemanticModifiers.Volatile);


            // Literal modifiers
            if (token.IsQuotedString())
                modifiers.Add(SemanticModifiers.QuotedString);

            if (token.IsVerbatimString())
                modifiers.Add(SemanticModifiers.VerbatimString);

            if (token.IsInterpolatedString())
                modifiers.Add(SemanticModifiers.InterpolatedString);

            if (token.IsInterpolatedVerbatimString())
                modifiers.Add(SemanticModifiers.InterpolatedVerbatimString);


            // Type modifiers
            if (GlobalConstants.PredefinedTypes.Contains(token.Text))
                modifiers.Add(SemanticModifiers.PredefinedType);

            if (token.IsNullableType() || token.IsNullableConstraintType())
                modifiers.Add(SemanticModifiers.Nullable);

            if (token.IsKeyword("var"))
                modifiers.Add(SemanticModifiers.ImplicitlyTyped);

            if (token.IsGenericType())
                modifiers.Add(SemanticModifiers.Generic);

            return modifiers;
        }
        #endregion
    }
}
