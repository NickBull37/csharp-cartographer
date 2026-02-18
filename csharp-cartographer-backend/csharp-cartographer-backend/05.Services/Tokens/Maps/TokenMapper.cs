using csharp_cartographer_backend._01.Configuration;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Tokens.Maps
{
    public class TokenMapper : ITokenMapper
    {
        private readonly ISemanticLibrary _semanticLibrary;

        public TokenMapper(ISemanticLibrary semanticLibrary)
        {
            _semanticLibrary = semanticLibrary;
        }

        public void MapNavTokens(List<NavToken> navTokens)
        {
            // for tokens that have enough data to be classified on the first attempt
            for (int i = 0; i < navTokens.Count; i++)
            {
                var token = navTokens[i];
                token.Map = MapToken(token);
            }

            MapSpecialCaseSemanticRoles(navTokens);

            _semanticLibrary.AddSemanticInfo(navTokens);
        }

        private static SemanticMap MapToken(NavToken token)
        {
            var primaryKind = GetPrimaryKind(token);
            var role = GetSemanticRole(token);
            var modifiers = GetSemanticModifiers(token);

            return new SemanticMap(
                primaryKind: primaryKind,
                semanticRole: role,
                modifiers: modifiers
            );
        }

        private static PrimaryKind GetPrimaryKind(NavToken token)
        {
            /*
             *   Roslyn does a lot of the heavy lifting with their classification. But their
             *   classification value may only be used for VS syntax highlighting and isn't
             *   100% reliable for PrimaryKind mapping.
             */

            // Special cases - update manually
            if (token.Text == "." && token.IsQualifiedNameSeparator())
            {
                return PrimaryKind.Punctuation;
            }
            if (token.Text == "?" && token.IsNullableTypeMarker())
            {
                return PrimaryKind.Punctuation;
            }
            if (token.Text == ".." && token.RoslynClassification == "punctuation")
            {
                return PrimaryKind.Operator;
            }
            if (GlobalConstants.Delimiters.Contains(token.Text) && token.RoslynClassification == "punctuation")
            {
                return PrimaryKind.Delimiter;
            }

            switch (token.RoslynClassification)
            {
                case "keyword":
                case "keyword - control":
                    return PrimaryKind.Keyword;
                case "operator":
                    return PrimaryKind.Operator;
                case "punctuation":
                    return PrimaryKind.Punctuation;
                case "string":
                case "string - verbatim":
                case "number":
                    return PrimaryKind.Literal;
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
                case "static symbol":
                    return PrimaryKind.Identifier;
                default:
                    return PrimaryKind.Unknown;
            }
        }

        #region Semantic Roles
        private static SemanticRole GetSemanticRole(NavToken token)
        {
            var semanticRole = SemanticRole.Unknown;

            // --- Delimiters ---
            semanticRole = GetSemanticRoleForDelimiters(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Punctuation ---
            semanticRole = GetSemanticRoleForPunctuation(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Operators ---
            semanticRole = GetSemanticRoleForOperators(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Keywords ---
            semanticRole = GetSemanticRoleForKeywords(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Literals ---
            semanticRole = GetSemanticRoleForLiterals(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Identifiers ---
            semanticRole = GetSemanticRoleForIdentifiers(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Members ---
            //return SemanticRole.MemberAccess;

            return SemanticRole.Unknown;
        }

        private static SemanticRole GetSemanticRoleForDelimiters(NavToken token)
        {
            if (!token.IsDelimiter())
                return SemanticRole.Unknown;

            if (token.Text is "(" or ")")
            {
                if (token.IsArgumentListDelimiter())
                    return SemanticRole.ArgumentListBoundary;

                if (token.IsAttributeArgumentListDelimiter())
                    return SemanticRole.AttributeArgumentListBoundary;

                if (token.IsCastTypeDelimiter())
                    return SemanticRole.CastTypeBoundary;

                if (token.IsCatchArgumentDelimiter())
                    return SemanticRole.CatchArgumentBoundary;

                if (token.IsCatchFilterDelimiter())
                    return SemanticRole.CatchFilterBoundary;

                if (token.IsDefaultExpressionDelimiter())
                    return SemanticRole.DefaultExpressionBoundary;

                if (token.IsForEachControlDelimiter())
                    return SemanticRole.ForEachControlBoundary;

                if (token.IsForLoopControlDelimiter())
                    return SemanticRole.ForLoopControlBoundary;

                if (token.IsIfConditionDelimiter())
                    return SemanticRole.IfConditionBoundary;

                if (token.IsParameterListDelimiter())
                    return SemanticRole.ParameterListBoundary;

                if (token.IsParenthesizedExpressionDelimiter())
                    return SemanticRole.ParenthesizedExpressionBoundary;

                if (token.IsParenthesizedPatternDelimiter())
                    return SemanticRole.ParenthesizedPatternBoundary;

                if (token.IsSizeOfExpressionDelimiter())
                    return SemanticRole.SizeOfExpressionBoundary;

                if (token.IsSwitchStatementConditionDelimiter())
                    return SemanticRole.SwitchStatementConditionBoundary;

                if (token.IsTupleExpressionDelimiter())
                    return SemanticRole.TupleExpressionBoundary;

                if (token.IsTupleTypeDelimiter())
                    return SemanticRole.TupleTypeBoundary;

                if (token.IsTypeOfExpressionDelimiter())
                    return SemanticRole.TypeOfExpressionBoundary;

                if (token.IsWhileLoopConditionDelimiter())
                    return SemanticRole.WhileLoopConditionBoundary;
            }

            if (token.Text is "{" or "}")
            {
                if (token.IsAccessorListDelimiter())
                    return SemanticRole.AccessorListBoundary;

                if (token.IsAddAccessorBlockDelimiter())
                    return SemanticRole.AddAccessorBlockBoundary;

                if (token.IsAnonymousObjectCreationExpressionDelimiter())
                    return SemanticRole.AnonymousObjectCreationExpressionBoundary;

                if (token.IsArrayInitializationDelimiter())
                    return SemanticRole.ArrayInitializationBoundary;

                if (token.IsCatchBlockDelimiter())
                    return SemanticRole.CatchBlockBoundary;

                if (token.IsCheckedStatementBlockDelimiter())
                    return SemanticRole.CheckedStatementBlockBoundary;

                if (token.IsClassDelimiter())
                    return SemanticRole.ClassBoundary;

                if (token.IsCollectionInitializerExpressionDelimiter())
                    return SemanticRole.CollectionInitializerExpressionBoundary;

                if (token.IsConstructorDelimiter())
                    return SemanticRole.ConstructorBoundary;

                if (token.IsElseBlockDelimiter())
                    return SemanticRole.ElseBlockBoundary;

                if (token.IsEnumDelimiter())
                    return SemanticRole.EnumBoundary;

                if (token.IsForEachBlockDelimiter())
                    return SemanticRole.ForEachBlockBoundary;

                if (token.IsForLoopBlockDelimiter())
                    return SemanticRole.ForLoopBlockBoundary;

                if (token.IsIfBlockDelimiter())
                    return SemanticRole.IfBlockBoundary;

                if (token.IsInterpolatedValueDelimiter())
                    return SemanticRole.InterpolatedValueBoundary;

                if (token.IsMethodDelimiter())
                    return SemanticRole.MethodBoundary;

                if (token.IsNamespaceDelimiter())
                    return SemanticRole.NamespaceBoundary;

                if (token.IsObjectInitializerDelimiter())
                    return SemanticRole.ObjectInitializerBoundary;

                if (token.IsPropertyPatternDelimiter())
                    return SemanticRole.PropertyPatternBoundary;

                if (token.IsRecordDelimiter())
                    return SemanticRole.RecordBoundary;

                if (token.IsRemoveAccessorBlockDelimiter())
                    return SemanticRole.RemoveAccessorBlockBoundary;

                if (token.IsSetAccessorBlockDelimiter())
                    return SemanticRole.SetAccessorBlockBoundary;

                if (token.IsSwitchExpressionDelimiter())
                    return SemanticRole.SwitchExpressionBoundary;

                if (token.IsSwitchStatementDelimiter())
                    return SemanticRole.SwitchStatementBoundary;

                if (token.IsTryBlockDelimiter())
                    return SemanticRole.TryBlockBoundary;

                if (token.IsUncheckedStatementBlockDelimiter())
                    return SemanticRole.UncheckedStatementBlockBoundary;

                if (token.IsWhileLoopBlockDelimiter())
                    return SemanticRole.WhileLoopBlockBoundary;

                if (token.IsWithInitializerExpressionDelimiter())
                    return SemanticRole.WithInitializerExpressionBoundary;
            }

            if (token.Text is "[" or "]")
            {
                if (token.IsArrayTypeDelimiter())
                    return SemanticRole.ArrayType;

                if (token.IsAttributeListDelimiter())
                    return SemanticRole.AttributeListBoundary;

                if (token.IsBracketedArgumentListDelimiter())
                    return SemanticRole.BracketedArgumentListBoundary;

                if (token.IsCollectionExpressionDelimiter())
                    return SemanticRole.CollectionExpressionBoundary;

                if (token.IsImplicitArrayCreationDelimiter())
                    return SemanticRole.ImplicitArrayCreation;
            }

            if (token.Text is "<" or ">")
            {
                if (token.IsTypeArgumentListDelimiter())
                    return SemanticRole.TypeArgumentListBoundary;

                if (token.IsTypeParameterListDelimiter())
                    return SemanticRole.TypeParameterListBoundary;
            }

            return SemanticRole.Unknown;
        }

        private static SemanticRole GetSemanticRoleForPunctuation(NavToken token)
        {
            if (!token.IsPunctuation())
                return SemanticRole.Unknown;

            // --- Separators ---
            if (token.Text is "," or ":")
            {
                if (token.IsAnonymousObjectMemberDeclarationSeparator())
                    return SemanticRole.AnonymousObjectMemberDeclarationSeparator;

                if (token.IsArgumentSeparator())
                    return SemanticRole.ArgumentSeparator;

                if (token.IsArrayInitializerElementSeparator())
                    return SemanticRole.ArrayInitializerElementSeparator;

                if (token.IsAttributeArgumentSeparator())
                    return SemanticRole.AttributeArgumentSeparator;

                if (token.IsBaseTypeSeparator())
                    return SemanticRole.BaseTypeSeparator;

                if (token.IsCollectionExpressionElementSeparator())
                    return SemanticRole.CollectionExpressionElementSeparator;

                if (token.IsCollectionInitializerElementSeparator())
                    return SemanticRole.CollectionInitializerElementSeparator;

                if (token.IsConstraintSeparator())
                    return SemanticRole.ConstraintSeparator;

                if (token.IsEnumMemberSeparator())
                    return SemanticRole.EnumMemberSeparator;

                if (token.IsInterpolationFormatSeparator())
                    return SemanticRole.InterpolationFormatSeparator;

                if (token.IsMemberPatternSeparator())
                    return SemanticRole.MemberPatternSeparator;

                if (token.IsOrderByClauseSeparator())
                    return SemanticRole.OrderByClauseSeparator;

                if (token.IsTypeArgumentSeperator())
                    return SemanticRole.TypeArgumentSeparator;

                if (token.IsTypeParameterSeparator())
                    return SemanticRole.TypeParameterSeparator;

                if (token.IsParameterSeparator())
                    return SemanticRole.ParameterSeparator;

                if (token.IsPropertyInitializationSeparator())
                    return SemanticRole.PropertyInitializationSeparator;

                if (token.IsSwitchArmSeperator())
                    return SemanticRole.SwitchArmSeparator;

                if (token.IsTupleElementSeperator())
                    return SemanticRole.TupleElementSeparator;

                if (token.IsTypeParameterConstraintClauseSeperator())
                    return SemanticRole.TypeParameterConstraintClauseSeparator;

                if (token.IsVariableDeclaratorSeparator())
                    return SemanticRole.VariableDeclaratorSeparator;
            }

            // --- Terminators ---
            if (token.Text is ";" or ":")
            {
                if (token.IsStatementTerminator())
                    return SemanticRole.StatementTerminator;

                if (token.IsSwitchCaseLabelTerminator())
                    return SemanticRole.CaseLabelTerminator;

                if (token.IsSwitchCasePatternLabelTerminator())
                    return SemanticRole.CasePatternLabelTerminator;

                if (token.IsDefaultCaseLabelTerminator())
                    return SemanticRole.DefaultLabelTerminator;

                if (token.IsParameterLabelTerminator())
                    return SemanticRole.ParameterLabelTerminator;
            }

            // --- Qualifiers ---
            if (token.Text is ".")
            {
                if (token.IsQualifiedNameSeparator())
                    return SemanticRole.QualifiedNameSeparator;
            }

            // --- Nullables ---
            if (token.Text is "?")
            {
                if (token.IsNullConditionalGuard())
                    return SemanticRole.NullConditionalGuard;

                if (token.IsNullableTypeMarker() || token.IsNullableConstraintTypeMarker())
                    return SemanticRole.NullableTypeMarker;
            }

            return SemanticRole.Unknown;
        }

        private static SemanticRole GetSemanticRoleForOperators(NavToken token)
        {
            if (!token.IsOperator())
                return SemanticRole.Unknown;

            // Handle operators that can fall into multiple semantic roles first
            if (GlobalConstants.SpecialCaseOperators.Contains(token.Text))
                return GetSpecialCaseOperatorRole(token);

            // Arithmetic
            if (token.IsArithmeticOperator())
                return SemanticRole.Arithmetic;

            // Assignment
            if (token.IsAssignmentOperator())
                return SemanticRole.Assignment;

            // Bitwise-shift
            if (token.IsBitwiseShiftOperator())
                return SemanticRole.BitwiseShift;

            // Boolean logical
            if (token.IsBooleanLogicalOperator())
                return SemanticRole.BooleanLogical;

            // Comparison
            if (token.IsComparisonOperator())
                return SemanticRole.Comparison;

            // Index & Range
            if (token.IsIndexOrRangeOperator())
                return SemanticRole.IndexRange;

            // Lambda
            if (token.IsLambdaOperator())
                return SemanticRole.Lambda;

            // Member access
            if (token.IsMemberAccessOperator() || token.IsConditionalMemberAccessOperator() || token.IsNamespaceAliasQualifier())
                return SemanticRole.MemberAccess;

            // Null-related
            if (token.IsNullCoalescingOperator())
                return SemanticRole.NullCoalescing;

            if (token.IsNullCoalescingAssignmentOperator())
                return SemanticRole.NullCoalescingAssignment;

            if (token.IsNullForgivingOperator())
                return SemanticRole.NullForgiving;

            // Pointer
            if (token.IsPointerOperator())
                return SemanticRole.Pointer;

            // Ternary
            if (token.IsTernaryOperator())
                return SemanticRole.Ternary;

            return SemanticRole.Unknown;
        }

        private static SemanticRole GetSemanticRoleForKeywords(NavToken token)
        {
            if (!token.IsKeyword())
                return SemanticRole.Unknown;

            // Handle keywords that can fall into multiple semantic roles first
            if (GlobalConstants.SpecialCaseKeywords.Contains(token.Text))
                return GetSpecialCaseKeywordRole(token);

            // --- Access modifiers ---
            if (GlobalConstants.AccessModifiers.Contains(token.Text))
                return SemanticRole.AccessModifier;

            // --- Accessor keywords ---
            if (GlobalConstants.AccessorKeywords.Contains(token.Text))
                return SemanticRole.Accessor;

            // --- Cast target type keywords ---
            if (token.IsCastTargetType())
                return SemanticRole.CastTargetType;

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

            // --- Default keyword ---
            if (token.IsDefaultOperatorKeyword())
                return SemanticRole.DefaultOperator;

            if (token.IsDefaultValueKeyword())
                return SemanticRole.DefaultValue;

            // --- Discard contextual keyword ---
            if (token.IsDiscard())
                return SemanticRole.Discard;

            // --- Event keywords ---
            if (GlobalConstants.EventKeywords.Contains(token.Text) && token.Kind == SyntaxKind.EventKeyword)
                return SemanticRole.MemberDeclaration;

            if (GlobalConstants.EventKeywords.Contains(token.Text))
                return SemanticRole.EventHandling;

            // --- Exception handling ---
            if (GlobalConstants.ExceptionHandlingKeywords.Contains(token.Text))
                return SemanticRole.ExceptionHandling;

            // --- Implicit parameters ---
            if (token.IsImplicitParameterKeyword())
                return SemanticRole.ImplicitParameter;

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
            //if (GlobalConstants.LiteralKeywords.Contains(token.Text))
            //    return SemanticRole.LiteralValue;

            // --- Loop statement keywords ---
            if (GlobalConstants.LoopStatementKeywords.Contains(token.Text))
                return SemanticRole.LoopStatement;

            // --- Member modifiers ---
            if (GlobalConstants.MemberModifiers.Contains(token.Text))
                return SemanticRole.MemberModifier;

            // --- Compilation scope keywords ---
            if (GlobalConstants.CompilationScopeKeywords.Contains(token.Text))
                return SemanticRole.CompilationScope;

            // --- Object construction keywords ---
            if (GlobalConstants.ObjectConstructionKeywords.Contains(token.Text))
                return SemanticRole.ObjectConstruction;

            if (token.IsObjectConstructionTypeKeyword())
                return SemanticRole.ObjectConstructionType;

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

            // --- Default operator operand keywords ---
            if (token.IsDefaultOperand())
                return SemanticRole.DefaultOperand;

            // --- NameOf operator / operand keywords ---
            if (token.IsNameOfOperator())
                return SemanticRole.NameOfOperator;

            if (token.IsNameOfOperand())
                return SemanticRole.NameOfOperand;

            // --- SizeOf operator / operand keywords ---
            if (token.IsSizeOfOperator())
                return SemanticRole.SizeOfOperator;

            if (token.IsSizeOfOperand())
                return SemanticRole.SizeOfOperand;

            // --- TypeOf operator / operand keywords ---
            if (token.IsTypeOfOperator())
                return SemanticRole.TypeOfOperator;

            if (token.IsTypeOfOperand())
                return SemanticRole.TypeOfOperand;

            // --- Type declaration keywords ---
            if (token.IsTypeDeclarationKeyword())
                return SemanticRole.TypeDeclaration;

            // --- Type pattern keywords ---
            if (token.IsTypePatternType())
                return SemanticRole.TypePattern;

            // --- Type reference keywords ---
            if (GlobalConstants.PredefinedTypes.Contains(token.Text) && token.IsAccessStaticMember())
                return SemanticRole.TypeReference;

            // --- Type system keywords ---
            if (GlobalConstants.TypeSystemKeywords.Contains(token.Text))
                return SemanticRole.TypeSystem;

            // --- With expression keyword ---
            if (token.IsWithExpressionKeyword())
                return SemanticRole.WithExpression;


            // --- Keyword: data types ---
            if (token.IsArrayDataType())
                return SemanticRole.ArrayDataType;

            if (token.IsFieldDataType())
                return SemanticRole.FieldDataType;

            if (token.IsForLoopIteratorDataType())
                return SemanticRole.ForLoopIteratorDataType;

            if (token.IsForEachLoopIteratorDataType())
                return SemanticRole.ForEachLoopIteratorDataType;

            if (token.IsLocalVariableDataType())
                return SemanticRole.LocalVariableDataType;

            if (token.IsMethodReturnType())
                return SemanticRole.MethodReturnType;

            if (token.IsDelegateReturnType())
                return SemanticRole.DelegateReturnType;

            if (token.IsOutVariableDataType())
                return SemanticRole.OutVariableDataType;

            if (token.IsParameterDataType())
                return SemanticRole.ParameterDataType;

            if (token.IsPropertyDataType())
                return SemanticRole.PropertyDataType;

            if (token.IsTupleElementType())
                return SemanticRole.TupleElementType;

            // --- Keyword: generic types ---
            if (token.IsGenericTypeArgument())
                return SemanticRole.GenericTypeArgument;

            if (token.IsTypeConstraintKeyword())
                return SemanticRole.TypeConstraint;

            return SemanticRole.Unknown;
        }

        private static SemanticRole GetSpecialCaseKeywordRole(NavToken token)
        {
            string? parentKind = token.ParentNodeKind;

            return token.Text switch
            {
                // switch case label, pattern case label
                "case" => parentKind switch
                {
                    "CaseSwitchLabel" => SemanticRole.ControlFlow,
                    "CasePatternSwitchLabel" => SemanticRole.PatternMatching,
                    _ => SemanticRole.Unknown
                },

                // switch label, default literal
                "default" => parentKind switch
                {
                    "DefaultLiteralExpression" => SemanticRole.DefaultValue,
                    "DefaultExpression" => SemanticRole.DefaultOperator,
                    "DefaultSwitchLabel" => SemanticRole.ControlFlow,
                    _ => SemanticRole.Unknown
                },

                // foreach loops, query expressions, param modifiers
                "in" => parentKind switch
                {
                    "ForEachStatement" => SemanticRole.LoopStatement,
                    "Parameter" => SemanticRole.ParameterModifier,
                    _ when !string.IsNullOrEmpty(parentKind) && parentKind.Contains("Clause") => SemanticRole.QueryExpression,
                    _ => SemanticRole.Unknown
                },

                // object creation, member hiding
                "new" => parentKind is "ObjectCreationExpression" or "ImplicitArrayCreationExpression" or "AnonymousObjectCreationExpression"
                    ? SemanticRole.ObjectConstruction
                    : SemanticRole.InheritanceModifier,

                // query expressions, generic constraints
                "where" => parentKind switch
                {
                    "WhereClause" => SemanticRole.QueryExpression,
                    "TypeParameterConstraintClause" => SemanticRole.TypeConstraint,
                    _ => SemanticRole.Unknown
                },

                _ => SemanticRole.Unknown
            };
        }

        private static SemanticRole GetSpecialCaseOperatorRole(NavToken token)
        {
            string? parentKind = token.ParentNodeKind;
            var containingType = token.SemanticData?.ContainingType;
            bool isBool = containingType == "bool";

            return token.Text switch
            {
                "!" => parentKind switch
                {
                    "LogicalNotExpression" => SemanticRole.BooleanLogical,
                    "SuppressNullableWarningExpression" => SemanticRole.NullForgiving,
                    _ => SemanticRole.Unknown
                },

                "&" => parentKind switch
                {
                    "BitwiseAndExpression" => isBool
                        ? SemanticRole.BooleanLogical
                        : SemanticRole.BitwiseShift,
                    "AddressOfExpression" => SemanticRole.Pointer,
                    _ => SemanticRole.Unknown
                },

                "|" => parentKind switch
                {
                    "BitwiseOrExpression" => isBool
                        ? SemanticRole.BooleanLogical
                        : SemanticRole.BitwiseShift,
                    _ => SemanticRole.Unknown
                },

                "^" => parentKind switch
                {
                    "ExclusiveOrExpression" => isBool
                        ? SemanticRole.BooleanLogical
                        : SemanticRole.BitwiseShift,
                    "IndexExpression" => SemanticRole.IndexRange,
                    _ => SemanticRole.Unknown
                },

                "*" => parentKind switch
                {
                    "MultiplyExpression" => SemanticRole.Arithmetic,
                    "PointerType" or "PointerIndirectionExpression" => SemanticRole.Pointer,
                    _ => SemanticRole.Unknown
                },

                _ => SemanticRole.Unknown
            };
        }

        private static SemanticRole GetSemanticRoleForLiterals(NavToken token)
        {
            // Numeric literals
            if (token.IsNumericLiteral())
                return SemanticRole.NumericLiteral;

            // String literals
            if (token.IsStringLiteral())
            {
                if (token.IsQuotedString())
                    return SemanticRole.QuotedString;

                if (token.IsVerbatimString())
                    return SemanticRole.VerbatimString;

                if (token.IsInterpolatedStringStart())
                    return SemanticRole.InterpolatedStringStart;

                if (token.IsInterpolatedStringText())
                    return SemanticRole.InterpolatedStringText;

                if (token.IsInterpolatedStringEnd())
                    return SemanticRole.InterpolatedStringEnd;

                if (token.IsInterpolatedVerbatimStringStart())
                    return SemanticRole.InterpolatedVerbatimStringStart;

                if (token.IsNumericFormatSpecifier())
                    return SemanticRole.NumericFormatSpecifier;
            }

            // Char literals
            if (token.IsCharacterLiteral())
                return SemanticRole.CharacterLiteral;

            // Boolean literals
            if (token.IsBooleanLiteral())
                return SemanticRole.BooleanLiteral;

            // Null literals
            if (token.IsNullLiteral())
                return SemanticRole.NullValue;

            return SemanticRole.Unknown;
        }

        private static SemanticRole GetSemanticRoleForIdentifiers(NavToken token)
        {
            if (!token.IsIdentifier())
                return SemanticRole.Unknown;

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

            if (token.IsEventPropertyDeclaration())
                return SemanticRole.EventPropertyDeclaration;

            if (token.IsEventFieldDeclaration())
                return SemanticRole.EventFieldDeclaration;

            if (token.IsFieldDeclaration())
                return SemanticRole.FieldDeclaration;

            if (token.IsLocalVariableDeclaration())
                return SemanticRole.LocalVariableDeclaration;

            if (token.IsForLoopIteratorDeclaration())
                return SemanticRole.ForLoopIteratorDeclaration;

            if (token.IsForEachLoopIteratorDeclaration())
                return SemanticRole.ForEachLoopIteratorDeclaration;

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
            if (token.IsEventFieldType())
                return SemanticRole.EventFieldType;

            if (token.IsEventPropertyDataType())
                return SemanticRole.EventPropertyDataType;

            if (token.IsFieldDataType())
                return SemanticRole.FieldDataType;

            if (token.IsLocalVariableDataType())
                return SemanticRole.LocalVariableDataType;

            if (token.IsMethodReturnType())
                return SemanticRole.MethodReturnType;

            if (token.IsDelegateReturnType())
                return SemanticRole.DelegateReturnType;

            if (token.IsParameterDataType())
                return SemanticRole.ParameterDataType;

            if (token.IsPropertyDataType())
                return SemanticRole.PropertyDataType;

            if (token.IsForLoopIteratorDataType())
                return SemanticRole.ForLoopIteratorDataType;

            if (token.IsForEachLoopIteratorDataType())
                return SemanticRole.ForEachLoopIteratorDataType;

            if (token.IsForEachLoopCollectionIdentifier())
                return SemanticRole.ForEachLoopCollectionIdentifier;

            // Identifier references
            if (token.RoslynClassification == "constant name")
                return SemanticRole.ConstantReference;

            if (token.RoslynClassification == "enum name")
                return SemanticRole.EnumReference;

            if (token.RoslynClassification == "enum member name")
                return SemanticRole.EnumMemberReference;

            if (token.RoslynClassification == "event name")
                return SemanticRole.EventReference;

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

            if (token.IsObjectCreationExpression())
                return SemanticRole.ConstructorInvocation;

            // Identifier - namespaces, aliases, qualifiers
            if (token.IsUsingDirectiveQualifier())
                return SemanticRole.UsingDirectiveQualifier;

            if (token.IsNamespaceDeclarationQualifier())
                return SemanticRole.NamespaceDeclarationQualifer;

            if (token.IsNamespaceQualifier())
                return SemanticRole.NamespaceQualifer;

            if (token.IsNamespaceAliasDeclarationIdentifier())
                return SemanticRole.NamespaceAliasDeclaration;

            if (token.IsTypeAliasDeclarationIdentifier())
                return SemanticRole.TypeAliasDeclaration;

            if (token.IsAliasQualifier())
                return SemanticRole.AliasQualifier;

            // Identifier - generic type args
            if (token.IsGenericTypeArgument())
                return SemanticRole.GenericTypeArgument;

            // Identifier - generic type params
            if (token.IsGenericTypeParameter())
                return SemanticRole.GenericTypeParameter;

            if (token.IsTypeParameterConstraint())
                return SemanticRole.TypeParameterConstraint;

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

            // Identifier - property assignment on obj creation
            if (token.IsObjCreationPropertyAssignment())
                return SemanticRole.ObjectPropertyAssignment;

            // Identifier - default operands
            if (token.IsDefaultOperand())
                return SemanticRole.DefaultOperand;

            // Identifier - NameOf operands
            if (token.IsNameOfOperand())
                return SemanticRole.NameOfOperand;

            // Identifier - SizeOf operands
            if (token.IsSizeOfOperand())
                return SemanticRole.SizeOfOperand;

            // Identifier - TypeOf operands
            if (token.IsTypeOfOperand())
                return SemanticRole.TypeOfOperand;

            // Identifier - type constraints
            if (token.IsTypeConstraint())
                return SemanticRole.TypeConstraint;

            // Identifier - type pattern types
            if (token.IsTypePatternType())
                return SemanticRole.TypePattern;

            // Identifier - param labels
            if (token.IsParameterLabel())
                return SemanticRole.ParameterLabel;

            // Identifier - attribute arguments
            if (token.IsAttributeArgument())
                return SemanticRole.AttributeArgument;

            // Identifier - type references
            if (token.IsTypeReference())
                return SemanticRole.TypeReference;

            // --------------------------------------------------------- //

            // Identifier - type qualifiers
            if (token.IsTypeQualifier())
                return SemanticRole.TypeQualifier;

            // Identifier - query expressions
            if (token.IsRangeVariable())
                return SemanticRole.RangeVariable;

            if (token.IsQuerySource())
                return SemanticRole.QuerySource;

            if (token.IsJoinRangeVariable())
                return SemanticRole.JoinRangeVariable;

            if (token.IsJoinSource())
                return SemanticRole.JoinSource;

            if (token.IsLetVariable())
                return SemanticRole.LetVariable;

            if (token.IsGroupContinuationRangeVariable())
                return SemanticRole.GroupContinuationRangeVariable;

            if (token.IsJoinIntoRangeVariable())
                return SemanticRole.JoinIntoRangeVariable;

            if (token.PrevToken?.Text == ".")
                return SemanticRole.MemberAccess;

            return SemanticRole.Unknown;
        }
        #endregion

        #region Semantic Modifiers
        private static List<SemanticModifiers> GetSemanticModifiers(NavToken token)
        {
            List<SemanticModifiers> modifiers = [];

            // --- Keyword modifiers ---
            if (token.IsKeyword() || token.IsContextualKeyword())
            {
                if (token.Text == "var" || token.Parent.IsKind(SyntaxKind.DefaultLiteralExpression))
                    modifiers.Add(SemanticModifiers.ImplicitlyTyped);

                if (token.IsAnonymousObjectCreation())
                    modifiers.Add(SemanticModifiers.Anonymous);
            }

            // --- Operator modifiers ---
            if (GlobalConstants.Operators.Contains(token.Text))
            {
                if (token.IsConditionalMemberAccessOperator())
                    modifiers.Add(SemanticModifiers.Conditional);

                if (token.IsConcatenationAddOperator())
                    modifiers.Add(SemanticModifiers.Concatenation);
            }

            // --- Identifier modifiers
            if (token.IsIdentifier())
            {
                if (token.IsInterpolatedValue())
                    modifiers.Add(SemanticModifiers.InterpolatedValue);

                if (token.IsGenericMethodDeclaration() || token.IsGenericMethodInvocation())
                    modifiers.Add(SemanticModifiers.GenericMethod);

                if (token.IsTypeParameter())
                    modifiers.Add(SemanticModifiers.TypeParameter);
            }

            // --- General modifiers
            if (token.IsArgument())
                modifiers.Add(SemanticModifiers.Argument);

            if (token.IsGenericType())
                modifiers.Add(SemanticModifiers.GenericType);

            if (token.IsNullableType() || token.IsNullableConstraintType())
                modifiers.Add(SemanticModifiers.Nullable);

            if (token.IsReturnValue())
                modifiers.Add(SemanticModifiers.ReturnValue);

            return modifiers;
        }
        #endregion

        #region Special Case Tokens
        private static void MapSpecialCaseSemanticRoles(List<NavToken> navTokens)
        {
            MapQueryExpressionVariableRefs(navTokens);
        }

        private static void MapQueryExpressionVariableRefs(List<NavToken> navTokens)
        {
            // Iterate through tokens until a query expression is found, then map it.
            // The "from" will always be the first token in a query expression.
            for (int i = 0; i < navTokens.Count; i++)
            {
                var token = navTokens[i];

                if (token.Text is not "from" || !token.Parent.IsKind(SyntaxKind.FromClause))
                    continue;

                MapQueryExpression(navTokens, i);
            }
        }

        private static void MapQueryExpression(List<NavToken> navTokens, int startIndex)
        {
            var decToRefDict = new Dictionary<SemanticRole, SemanticRole>
            {
                [SemanticRole.RangeVariable] = SemanticRole.RangeVariableReference,
                [SemanticRole.LetVariable] = SemanticRole.LetVariableReference,
                [SemanticRole.GroupContinuationRangeVariable] = SemanticRole.GroupContinuationRangeVariableReference,
                [SemanticRole.JoinRangeVariable] = SemanticRole.JoinRangeVariableReference,
                [SemanticRole.JoinIntoRangeVariable] = SemanticRole.JoinIntoRangeVariableReference
            };

            var identifierToRefDict = new Dictionary<string, SemanticRole>(StringComparer.Ordinal);

            // 1) Collect query expression declaration identifiers. Break out of loop on first
            //    ";" token to avoid mapping identifiers from other query expressions that may
            //    share the same identifier name.
            for (int i = startIndex; i < navTokens.Count; i++)
            {
                var token = navTokens[i];

                if (token.Text == ";")
                    break;

                var semanticRole = token.Map?.SemanticRole;
                if (semanticRole is null)
                    continue;

                if (decToRefDict.TryGetValue(semanticRole.Value, out var referenceRole))
                    identifierToRefDict.TryAdd(token.Text, referenceRole);
            }

            // 2) Find matches to declaration identifiers & set references roles. Break out of
            //    loop on first ";" token to avoid mapping identifiers from other query expressions
            //    that may share the same identifier name.
            for (int i = startIndex; i < navTokens.Count; i++)
            {
                var token = navTokens[i];

                if (token.Text == ";")
                    break;

                // don't update declarations (if semantic role is already set)
                if (!token.IsQueryExpressionVariable()
                    || token.Map is null
                    || token.Map.SemanticRole != SemanticRole.Unknown)
                    continue;

                if (identifierToRefDict.TryGetValue(token.Text, out var referenceRole))
                    token.Map.SemanticRole = referenceRole;
            }
        }
        #endregion
    }
}
