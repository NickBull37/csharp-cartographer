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

        private static TokenMap MapToken(NavToken token, NavToken? previous, NavToken? next)
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

        private static TokenPrimaryKind GetPrimaryKind(NavToken token)
        {
            // Special cases - update manually
            if (token.Text == "?" && token.RoslynClassification == "operator")
            {
                return TokenPrimaryKind.Punctuation;
            }

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

        #region Semantic Roles
        private static SemanticRole GetSemanticRole(NavToken token, NavToken? previous, NavToken? next)
        {
            var semanticRole = SemanticRole.None;

            // --- Delimiters ---
            semanticRole = GetSemanticRoleForDelimiters(token);
            if (semanticRole != SemanticRole.None)
                return semanticRole;

            // --- Punctuation ---
            semanticRole = GetSemanticRoleForPunctuation(token);
            if (semanticRole != SemanticRole.None)
                return semanticRole;

            // --- Operators ---
            semanticRole = GetSemanticRoleForOperators(token);
            if (semanticRole != SemanticRole.None)
                return semanticRole;

            // --- Keywords ---
            semanticRole = GetSemanticRoleForKeywords(token);
            if (semanticRole != SemanticRole.None)
                return semanticRole;

            // --- Identifiers ---
            semanticRole = GetSemanticRoleForIdentifiers(token);
            if (semanticRole != SemanticRole.None)
                return semanticRole;

            // --- Literals ---
            semanticRole = GetSemanticRoleForLiterals(token);
            if (semanticRole != SemanticRole.None)
                return semanticRole;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForDelimiters(NavToken token)
        {
            if (!token.IsDelimiter())
                return SemanticRole.None;

            if (token.IsAccessorListDelimiter())
                return SemanticRole.AccessorListBoundary;

            if (token.IsArgumentListDelimiter())
                return SemanticRole.ArgumentListBoundary;

            if (token.IsAttributeListDelimiter())
                return SemanticRole.AttributeListBoundary;

            if (token.IsAttributeArgumentListDelimiter())
                return SemanticRole.AttributeArgumentListBoundary;

            if (token.IsCastTypeDelimiter())
                return SemanticRole.CastTypeBoundary;

            if (token.IsCollectionExpressionDelimiter())
                return SemanticRole.CollectionExpressionBoundary;

            if (token.IsForEachBlockDelimiter())
                return SemanticRole.ForEachBlockBoundary;

            if (token.IsIfBlockDelimiter())
                return SemanticRole.IfBlockBoundary;

            if (token.IsIfConditionDelimiter())
                return SemanticRole.IfConditionBoundary;

            if (token.IsInterpolatedValueDelimiter())
                return SemanticRole.InterpolatedValueBoundary;

            if (token.IsObjectInitializerDelimiter())
                return SemanticRole.ObjectInitializerBoundary;

            if (token.IsParameterListDelimiter())
                return SemanticRole.ParameterListBoundary;

            if (token.IsTupleTypeDelimiter())
                return SemanticRole.TupleTypeBoundary;

            if (token.IsTypeArgumentListDelimiter())
                return SemanticRole.TypeArgumentListBoundary;

            if (token.IsTypeParameterListDelimiter())
                return SemanticRole.TypeParameterListBoundary;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForPunctuation(NavToken token)
        {
            if (!token.IsPunctuation())
                return SemanticRole.None;

            // --- Separators ---
            if (token.IsArgumentSeperator())
                return SemanticRole.ArgumentSeparation;

            if (token.IsBaseTypeSeperator())
                return SemanticRole.BaseTypeSeparation;

            if (token.IsEnumMemberSeparator())
                return SemanticRole.EnumMemberSeparation;

            if (token.IsTypeArgumentSeperator())
                return SemanticRole.TypeArgumentSeparation;

            if (token.IsTypeParameterSeparator())
                return SemanticRole.TypeParameterSeparation;

            if (token.IsParameterSeparator())
                return SemanticRole.ParameterSeparation;

            if (token.IsSwitchArmSeperator())
                return SemanticRole.SwitchArmSeparation;

            if (token.IsTupleElementSeperator())
                return SemanticRole.TupleElementSeparation;

            if (token.IsTypeParameterConstraintClauseSeperator())
                return SemanticRole.TypeParameterConstraintClauseSeparation;

            if (token.IsVariableDeclaratorSeparator())
                return SemanticRole.VariableDeclaratorSeparation;

            // --- Terminators ---
            if (token.IsStatementTerminator())
                return SemanticRole.StatementTermination;

            if (token.IsSwitchCaseLabelTerminator())
                return SemanticRole.CaseLabelTermination;

            if (token.IsLabelTerminator())
                return SemanticRole.ParameterLabelTermination;

            // --- Misc ---
            if (token.IsNullConditionalGuard())
                return SemanticRole.NullConditionalGuard;

            if (token.IsNullableTypeMarker() || token.IsNullableConstraintTypeMarker())
                return SemanticRole.NullableTypeMarker;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForOperators(NavToken token)
        {
            if (!token.IsOperator())
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
            if (token.IsMemberAccessOperator() || token.IsConditionalMemberAccessOperator())
                return SemanticRole.MemberAccessOperator;

            // Range
            if (token.IsRangeOperator())
                return SemanticRole.RangeOperator;

            // Namespace alias
            if (token.IsNamespaceAliasQualifier())
                return SemanticRole.NamespaceAliasQualifier;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForKeywords(NavToken token)
        {
            if (!token.IsKeyword())
                return SemanticRole.None;

            // --- Access modifiers ---
            if (GlobalConstants.AccessModifiers.Contains(token.Text))
                return SemanticRole.AccessModifier;

            // --- Accessor keywords ---
            if (GlobalConstants.AccessorKeywords.Contains(token.Text))
                return SemanticRole.Accessor;

            // --- Concurrency keywords ---
            if (GlobalConstants.ConcurrencyKeywords.Contains(token.Text))
                return SemanticRole.Concurrency;

            // --- Conditional branching keywords ---
            if (GlobalConstants.ConditionalBranchingKeywords.Contains(token.Text))
                return SemanticRole.ConditionalBranching;

            // --- Constraint keywords ---
            if (GlobalConstants.ConstraintKeywords.Contains(token.Text))
                return SemanticRole.Constraint;

            // --- Control flow keywords ---
            if (GlobalConstants.ControlFlowKeywords.Contains(token.Text))
                return SemanticRole.ControlFlow;

            // --- Event keywords ---
            if (GlobalConstants.EventKeywords.Contains(token.Text))
                return SemanticRole.EventHandling;

            // --- Exception handling ---
            if (GlobalConstants.ExceptionHandlingKeywords.Contains(token.Text))
                return SemanticRole.ExceptionHandling;

            // --- Inheritance modifier keywords ---
            if (GlobalConstants.InheritanceModifiers.Contains(token.Text))
                return SemanticRole.InheritanceModifier;

            // --- Iterator keywords ---
            if (GlobalConstants.IteratorKeywords.Contains(token.Text))
                return SemanticRole.Iterator;

            // --- Jump statement keywords ---
            if (GlobalConstants.JumpStatementKeywords.Contains(token.Text))
                return SemanticRole.JumpStatement;

            // --- Literal keywords ---
            if (GlobalConstants.LiteralKeywords.Contains(token.Text))
                return SemanticRole.LiteralValue;

            // --- Loop statement keywords ---
            if (GlobalConstants.LoopStatementKeywords.Contains(token.Text))
                return SemanticRole.LoopStatement;

            // --- Member modifiers ---
            if (GlobalConstants.MemberModifiers.Contains(token.Text))
                return SemanticRole.MemberModifier;

            // --- Source scope keywords ---
            if (GlobalConstants.SourceScopeKeywords.Contains(token.Text))
                return SemanticRole.SourceScope;

            // --- Object construction keywords ---
            if (GlobalConstants.ObjectConstructionKeywords.Contains(token.Text))
                return SemanticRole.ObjectConstruction;

            // --- Parameter modifiers ---
            if (GlobalConstants.ParameterModifiers.Contains(token.Text))
                return SemanticRole.ParameterModifier;

            // --- Pattern matching keywords ---
            if (GlobalConstants.PatternMatchingKeywords.Contains(token.Text))
                return SemanticRole.PatternMatching;

            // --- Query expressions ---
            if (GlobalConstants.QueryExpressionKeywords.Contains(token.Text))
                return SemanticRole.QueryExpression;

            // --- Safety context keywords ---
            if (GlobalConstants.SafetyContextKeywords.Contains(token.Text))
                return SemanticRole.SafetyContext;

            // --- Type declaration keywords ---
            if (GlobalConstants.TypeDeclarationKeywords.Contains(token.Text))
                return SemanticRole.TypeDeclaration;

            // --- Type reference keywords ---
            if (GlobalConstants.PredefinedTypes.Contains(token.Text) && token.IsAccessStaticMember())
                return SemanticRole.TypeReference;

            // --- Type system keywords ---
            if (GlobalConstants.TypeSystemKeywords.Contains(token.Text))
                return SemanticRole.TypeSystem;


            // --- Keyword: data types ---
            if (token.IsFieldDataType())
                return SemanticRole.FieldDataType;

            if (token.IsLocalVariableDataType())
                return SemanticRole.LocalVariableDataType;

            if (token.IsMethodReturnType())
                return SemanticRole.MethodReturnType;

            if (token.IsParameterDataType())
                return SemanticRole.ParameterDataType;

            if (token.IsPropertyDataType())
                return SemanticRole.PropertyDataType;

            // --- Keyword: generic types ---
            if (token.IsGenericTypeArgument())
                return SemanticRole.GenericTypeArgument;

            return SemanticRole.None;
        }

        private static SemanticRole GetSemanticRoleForIdentifiers(NavToken token)
        {
            if (!token.IsIdentifier())
                return SemanticRole.None;

            // --- Identifiers: Declarations ---
            if (token.IsAttributeDeclaration())
                return SemanticRole.AttributeDeclaration;

            if (token.IsClassDeclaration())
                return SemanticRole.ClassDeclaration;

            if (token.IsClassConstructorDeclaration())
                return SemanticRole.ClassConstructorDeclaration;

            if (token.IsDelegateDeclaration())
                return SemanticRole.DelegateDeclaration;

            if (token.IsEnumDeclaration())
                return SemanticRole.EnumDeclaration;

            if (token.IsEnumMemberDeclaration())
                return SemanticRole.EnumMemberDeclaration;

            if (token.IsEventDeclaration())
                return SemanticRole.EventDeclaration;

            if (token.IsEventFieldDeclaration())
                return SemanticRole.EventFieldDeclaration;

            if (token.IsFieldDeclaration())
                return SemanticRole.FieldDeclaration;

            if (token.IsLocalVariableDeclaration())
                return SemanticRole.LocalVariableDeclaration;

            if (token.IsLocalForLoopVariableDeclaration())
                return SemanticRole.LocalVariableDeclaration;

            if (token.IsLocalForeachLoopVariableDeclaration())
                return SemanticRole.LocalVariableDeclaration;

            if (token.IsMethodDeclaration())
                return SemanticRole.MethodDeclaration;

            if (token.IsParameterDeclaration())
                return SemanticRole.ParameterDeclaration;

            if (token.IsPropertyDeclaration())
                return SemanticRole.PropertyDeclaration;

            if (token.IsRecordDeclaration())
                return SemanticRole.RecordDeclaration;

            if (token.IsRecordConstructorDeclaration())
                return SemanticRole.RecordConstructorDeclaration;

            if (token.IsRecordStructDeclaration())
                return SemanticRole.RecordStructDeclaration;

            if (token.IsRecordStructConstructorDeclaration())
                return SemanticRole.RecordStructConstructorDeclaration;

            if (token.IsStructDeclaration())
                return SemanticRole.StructDeclaration;

            if (token.IsStructConstructorDeclaration())
                return SemanticRole.StructConstructorDeclaration;

            // --- Identifiers: data types ---
            if (token.IsFieldDataType())
                return SemanticRole.FieldDataType;

            if (token.IsLocalVariableDataType())
                return SemanticRole.LocalVariableDataType;

            if (token.IsMethodReturnType())
                return SemanticRole.MethodReturnType;

            if (token.IsParameterDataType())
                return SemanticRole.ParameterDataType;

            if (token.IsPropertyDataType())
                return SemanticRole.PropertyDataType;

            // Identifier references
            if (token.RoslynClassification == "enum name")
                return SemanticRole.EnumReference;

            if (token.RoslynClassification == "enum member name")
                return SemanticRole.EnumMemberReference;

            if (token.RoslynClassification == "field name")
                return SemanticRole.FieldReference;

            if (token.RoslynClassification == "local name")
                return SemanticRole.LocalVariableReference;

            if (token.RoslynClassification == "parameter name")
                return SemanticRole.ParameterReference;

            if (token.RoslynClassification == "property name")
                return SemanticRole.PropertyReference;

            // Identifier invocations
            if (token.IsMethodInvocation())
                return SemanticRole.MethodInvocation;

            if (token.RoslynClassification == "class name" && token.IsObjectCreationExpression())
                return SemanticRole.ClassConstructorInvocation;

            if (token.RoslynClassification == "record class name" && token.IsObjectCreationExpression())
                return SemanticRole.RecordConstructorInvocation;

            if (token.RoslynClassification == "record struct name" && token.IsObjectCreationExpression())
                return SemanticRole.RecordStructConstructorInvocation;

            if (token.RoslynClassification == "struct name" && token.IsObjectCreationExpression())
                return SemanticRole.StructConstructorInvocation;

            // Identifier - namespaces
            if (token.IsUsingDirectiveSegment())
                return SemanticRole.UsingDirective;

            if (token.IsNamespaceSegment())
                return SemanticRole.NamespaceDeclaration;

            // Identifier - generic type args
            if (token.IsGenericTypeArgument())
                return SemanticRole.GenericTypeArgument;

            // Identifier - generic type params
            if (token.IsGenericTypeParameter())
                return SemanticRole.GenericTypeParameter;

            // Identifier - tuple types
            if (token.IsTupleElementName())
                return SemanticRole.TupleElementName;

            if (token.IsTupleElementType())
                return SemanticRole.TupleElementType;

            // Identifier - base types
            if (token.IsBaseType())
                return SemanticRole.SimpleBaseType;

            // Identifier - cast types
            if (token.IsCastType())
                return SemanticRole.CastType;

            if (token.IsCastTargetType())
                return SemanticRole.CastTargetType;

            // Identifier - exception types
            if (token.IsExceptionType())
                return SemanticRole.ExceptionType;

            // Identifier - constructors
            if (token.IsExternallyDefinedObjectCreationExpression())
                return SemanticRole.ConstructorInvocation;

            // Identifier - property assignment on obj creation
            if (token.IsObjCreationPropertyAssignment())
                return SemanticRole.ObjectPropertyAssignment;

            // Identifier - type constraints
            if (token.IsTypeConstraint())
                return SemanticRole.ConstraintType;

            // Identifier - type pattern types
            if (token.IsTypePatternType())
                return SemanticRole.TypePatternType;

            // Identifier - param labels
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
            //if (token.IsGenericTypeParameter())
            //    modifiers.Add(SemanticModifiers.GenericTypeParameter);


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

            // Operator modifiers
            if (token.IsConditionalMemberAccessOperator())
                modifiers.Add(SemanticModifiers.Conditional);

            return modifiers;
        }
        #endregion
    }
}
