using csharp_cartographer_backend._01.Configuration;
using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;
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
            for (int i = 0; i < navTokens.Count; i++)
            {
                MapToken(navTokens[i]);
            }
        }

        private void MapToken(NavToken token)
        {
            token.PrimaryKind = GetPrimaryKind(token);
            token.SemanticRole = GetSemanticRole(token);
            token.Map = _semanticLibrary.GetSemanticMap(token);
        }

        private static PrimaryKind GetPrimaryKind(NavToken token)
        {
            /*
             *   Classification is pulled from Roslyn's semantic model used
             *   for syntax highlighting during NavToken generation and then
             *   corrected for various edge cases.
             */

            switch (token.Classification)
            {
                case "delimiter":
                    return PrimaryKind.Delimiter;
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

        private static SemanticRole GetSemanticRole(NavToken token)
        {
            var semanticRole = SemanticRole.Unknown;

            // --- Delimiters ---
            semanticRole = TryGetDelimiterRole(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Punctuation ---
            semanticRole = TryGetPunctuationRole(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Operators ---
            semanticRole = TryGetOperatorRole(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Keywords ---
            semanticRole = TryGetKeywordRole(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Literals ---
            semanticRole = TryGetLiteralRole(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Misc ---
            semanticRole = TryGetMiscRole(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            // --- Identifiers ---
            semanticRole = TryGetIdentifierRole(token);
            if (semanticRole != SemanticRole.Unknown)
                return semanticRole;

            return SemanticRole.Unknown;
        }

        #region Semantic Roles
        private static SemanticRole TryGetDelimiterRole(NavToken token)
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

                if (token.IsDeconstructionDelimiter())
                    return SemanticRole.DeconstructionBoundary;

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

                if (token.IsStatementControlDelimiter())
                    return SemanticRole.StatementControlBoundary;

                if (token.IsSwitchStatementConditionDelimiter())
                    return SemanticRole.SwitchStatementConditionBoundary;

                if (token.IsTupleExpressionDelimiter())
                    return SemanticRole.TupleExpressionBoundary;

                if (token.IsTupleTypeDelimiter())
                    return SemanticRole.TupleTypeBoundary;

                if (token.IsTypeOfExpressionDelimiter())
                    return SemanticRole.TypeOfExpressionBoundary;

                if (token.IsUsingResourceDeclarationDelimiter())
                    return SemanticRole.UsingResourceDeclarationBoundary;

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

                if (token.IsFixedBlockDelimiter())
                    return SemanticRole.FixedStatementBlockBoundary;

                if (token.IsForEachBlockDelimiter())
                    return SemanticRole.ForEachBlockBoundary;

                if (token.IsForLoopBlockDelimiter())
                    return SemanticRole.ForLoopBlockBoundary;

                if (token.IsIfBlockDelimiter())
                    return SemanticRole.IfBlockBoundary;

                if (token.IsInterfaceDelimiter())
                    return SemanticRole.InterfaceBoundary;

                if (token.IsInterpolatedValueDelimiter())
                    return SemanticRole.InterpolatedValueBoundary;

                if (token.IsLambdaExpressionBlockDelimiter())
                    return SemanticRole.LambdaExpressionBlockBoundary;

                if (token.IsLocalFunctionDelimiter())
                    return SemanticRole.LocalFunctionBoundary;

                if (token.IsLockBlockDelimiter())
                    return SemanticRole.LockStatementBlockBoundary;

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

                if (token.IsRecordStructDelimiter())
                    return SemanticRole.RecordStructBoundary;

                if (token.IsRemoveAccessorBlockDelimiter())
                    return SemanticRole.RemoveAccessorBlockBoundary;

                if (token.IsSetAccessorBlockDelimiter())
                    return SemanticRole.SetAccessorBlockBoundary;

                if (token.IsStructDelimiter())
                    return SemanticRole.StructBoundary;

                if (token.IsSwitchExpressionDelimiter())
                    return SemanticRole.SwitchExpressionBoundary;

                if (token.IsSwitchStatementDelimiter())
                    return SemanticRole.SwitchStatementBoundary;

                if (token.IsTryBlockDelimiter())
                    return SemanticRole.TryBlockBoundary;

                if (token.IsUncheckedStatementBlockDelimiter())
                    return SemanticRole.UncheckedStatementBlockBoundary;

                if (token.IsUnsafeBlockDelimiter())
                    return SemanticRole.UnsafeStatementBlockBoundary;

                if (token.IsUsingStatementBlockDelimiter())
                    return SemanticRole.UsingStatementBlockBoundary;

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

        private static SemanticRole TryGetPunctuationRole(NavToken token)
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

                if (token.IsArrayLengthSeparator())
                    return SemanticRole.ArrayLengthSeparator;

                if (token.IsArrayRankIndicator())
                    return SemanticRole.ArrayRankIndicator;

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

                if (token.IsDeconstructionVariableSeparator())
                    return SemanticRole.DeconstructionVariableSeparator;

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

        private static SemanticRole TryGetOperatorRole(NavToken token)
        {
            if (!token.IsOperator())
                return SemanticRole.Unknown;

            /*
             * Currently not covered are non-short-circuit
             * boolean logical operators (&, |, ^) which
             * overlap with Bitwise operators if the operands
             * are both type bool (no way to determine).
             */

            // Arithmetic: +, -, *, /, %, ++, --
            if (token.IsArithmeticOperator())
                return SemanticRole.Arithmetic;

            // Assignment: =, +=, -=, *=, /=, %=, &=, |=, ^=, <<=, >>=, >>>=
            if (token.IsAssignmentOperator())
                return SemanticRole.Assignment;

            // Bitwise: &, |, ^, ~
            if (token.IsBitwiseOperator())
                return SemanticRole.Bitwise;

            // Boolean logical: &&, ||, !
            if (token.IsBooleanLogicalOperator())
                return SemanticRole.BooleanLogical;

            // Comparison: ==, !=, >, <, >=, <=
            if (token.IsComparisonOperator())
                return SemanticRole.Comparison;

            // Expression body arrow: =>
            if (token.IsExpressionBodyArrow())
                return SemanticRole.ExpressionBodyArrow;

            // Index: ^
            if (token.IsIndexFromEndOperator())
                return SemanticRole.IndexFromEnd;

            // Indirection: *p / &x
            if (token.IsIndirectionOperator())
                return SemanticRole.Indirection;

            // Lambda: =>
            if (token.IsLambdaOperator())
                return SemanticRole.Lambda;

            // Member access: ., ?., [], ?[],
            if (token.IsMemberAccessOperator())
                return SemanticRole.MemberAccess;

            // Null-related: ??, ??=, !
            if (token.IsNullCoalescingOperator())
                return SemanticRole.NullCoalescing;

            if (token.IsNullCoalescingAssignmentOperator())
                return SemanticRole.NullCoalescingAssignment;

            if (token.IsNullForgivingOperator())
                return SemanticRole.NullForgiving;

            // Pattern matching: =>, 
            if (token.IsPatternMatchArrow())
                return SemanticRole.PatternMatchArrow;

            // Pointer type indicator: int*
            if (token.IsPointerTypeIndicator())
                return SemanticRole.PointerTypeIndicator;

            // Range: ..
            if (token.IsRangeOperator())
                return SemanticRole.Range;

            // Shift: <<, >>, >>>
            if (token.IsShiftOperator())
                return SemanticRole.Shift;

            // Ternary: c ? t : f
            //if (token.IsTernaryOperator())
            //    return SemanticRole.Ternary;

            if (token.IsTernaryOperatorColon())
                return SemanticRole.TernaryColon;

            if (token.IsTernaryOperatorQuestion())
                return SemanticRole.TernaryQuestion;

            return SemanticRole.Unknown;
        }

        private static SemanticRole TryGetKeywordRole(NavToken token)
        {
            if (!token.IsKeyword())
                return SemanticRole.Unknown;

            // --- Access modifiers ---
            if (token.IsAccessModifierKeyword())
                return SemanticRole.AccessModifier;

            // --- Accessor keywords ---
            if (token.IsAccessorKeyword())
                return SemanticRole.Accessor;

            // --- Argument modifier keywords ---
            if (token.IsArgumentModifierKeyword())
                return SemanticRole.ArgumentModifier;

            // --- Concurrency keywords ---
            if (token.IsConcurrencyKeyword())
                return SemanticRole.Concurrency;

            // --- Conditional branching keywords ---
            if (token.IsConditionalBranchingKeyword())
                return SemanticRole.ConditionalBranching;

            // --- Constraint keywords ---
            if (token.IsConstraintKeyword())
                return SemanticRole.Constraint;

            // --- Control flow keywords ---
            if (token.IsControlFlowKeyword())
                return SemanticRole.ControlFlow;

            // --- Default keyword ---
            if (token.IsDefaultOperatorKeyword())
                return SemanticRole.DefaultOperator;

            if (token.IsDefaultLiteralKeyword())
                return SemanticRole.DefaultValue;

            // --- Discard keywords ---
            if (token.IsDiscardKeyword())
                return SemanticRole.DiscardValue;

            if (token.IsDiscardPattern())
                return SemanticRole.DiscardPattern;

            // --- Event keywords ---
            if (GlobalConstants.EventKeywords.Contains(token.Text) && token.Kind == SyntaxKind.EventKeyword)
                return SemanticRole.MemberDeclaration;

            if (token.IsEventHandlingKeyword())
                return SemanticRole.EventHandling;

            // --- Exception handling ---
            if (token.IsExceptionHandlingKeyword())
                return SemanticRole.ExceptionHandling;

            // --- Implicit parameters ---
            if (token.IsImplicitParameterKeyword())
                return SemanticRole.ImplicitParameter;

            // --- Iterator keywords ---
            if (token.IsIteratorKeyword())
                return SemanticRole.Iterator;

            // --- Jump statement keywords ---
            if (token.IsJumpStatementKeyword())
                return SemanticRole.JumpStatement;

            // --- Loop statement keywords ---
            if (token.IsLoopStatementKeyword())
                return SemanticRole.LoopStatement;

            // --- Member modifiers ---
            if (token.IsMemberModifierKeyword())
                return SemanticRole.MemberModifier;

            // --- Compilation scope keywords ---
            if (token.IsCompilationScopeKeyword())
                return SemanticRole.CompilationScope;

            // --- Object construction keywords ---
            if (token.IsObjectConstructionKeyword())
                return SemanticRole.ObjectConstruction;

            if (token.IsObjectConstructionTypeKeyword())
                return SemanticRole.ObjectConstructionType;

            // --- Parameter modifier keywords ---
            if (token.IsParameterModifierKeyword())
                return SemanticRole.ParameterModifier;

            // --- Pattern matching keywords ---
            if (token.IsPatternMatchingKeyword())
                return SemanticRole.PatternMatching;

            // --- Query expressions ---
            if (token.IsQueryExpressionKeyword())
                return SemanticRole.QueryExpression;

            // --- Safety context keywords ---
            if (token.IsSafetyContextKeyword())
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

            // --- Type modifier keywords ---
            if (token.IsTypeModifierKeyword())
                return SemanticRole.TypeModifier;

            // --- Type pattern keywords ---
            if (token.IsTypePattern())
                return SemanticRole.TypePattern;

            // --- Type system keywords ---
            if (token.IsTypeSystemKeyword())
                return SemanticRole.TypeSystem;

            // --- With expression keyword ---
            if (token.IsWithExpressionKeyword())
                return SemanticRole.WithExpression;

            // --- Data type keywords ---
            if (token.IsArrayDataType())
                return SemanticRole.ArrayDataType;

            if (token.IsDeconstructionVariableType())
                return SemanticRole.DeconstructionVariableType;

            if (token.IsFieldType())
                return SemanticRole.FieldType;

            if (token.IsLocalVariableType())
                return SemanticRole.LocalVariableType;

            if (token.IsLoopIteratorType())
                return SemanticRole.LoopIteratorType;

            if (token.IsMethodReturnType())
                return SemanticRole.MethodReturnType;

            if (token.IsDelegateReturnType())
                return SemanticRole.DelegateReturnType;

            if (token.IsOutVariableType())
                return SemanticRole.OutVariableType;

            if (token.IsParameterType())
                return SemanticRole.ParameterType;

            if (token.IsPropertyType())
                return SemanticRole.PropertyType;

            if (token.IsTupleElementType())
                return SemanticRole.TupleElementType;

            // --- Generic type keywords ---
            if (token.IsGenericTypeArgument())
                return SemanticRole.GenericTypeArgument;

            if (token.IsTypeConstraintKeyword())
                return SemanticRole.TypeConstraint;

            return SemanticRole.Unknown;
        }

        private static SemanticRole TryGetLiteralRole(NavToken token)
        {
            if (!token.IsInterpolatedString())
                return SemanticRole.Unknown;

            /*
             *  Most literals (numeric, char, bool, etc.) fall under specific
             *  roles for their context. Interpolated strings are the exception
             *  because they are the only literals that are always split into
             *  multiple tokens and each get their own role.
             */

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

            return SemanticRole.Unknown;
        }

        private static SemanticRole TryGetMiscRole(NavToken token)
        {
            // Anonymous object elements
            if (token.IsAnonymousObjectElement())
                return SemanticRole.AnonymousObjectElement;

            // Arguments
            if (token.IsArgument())
                return SemanticRole.Argument;

            // Assignment values
            if (token.IsAssignmentValue())
                return SemanticRole.AssignmentValue;

            // Casting
            if (token.IsCastTarget())
                return SemanticRole.CastTarget;

            if (token.IsCastType())
                return SemanticRole.CastType;

            // Collections
            if (token.IsCollectionElement())
                return SemanticRole.CollectionElement;

            if (token.IsCollectionLength())
                return SemanticRole.CollectionLength;

            // Index values
            if (token.IsIndexValue())
                return SemanticRole.IndexValue;

            if (token.IsInterpolatedValue())
                return SemanticRole.InterpolatedValue;

            // Operands
            if (token.IsAddressOfOperand())
                return SemanticRole.AddressOfOperand;

            if (token.IsArithmeticOperand())
                return SemanticRole.ArithmeticOperand;

            if (token.IsBitwiseOperand())
                return SemanticRole.BitwiseOperand;

            if (token.IsComparisonOperand())
                return SemanticRole.ComparisonOperand;

            if (token.IsConcatenationOperand())
                return SemanticRole.ConcatenationOperand;

            if (token.IsConditionalAccessTarget())
                return SemanticRole.ConditionalAccessTarget;

            if (token.IsDereferenceOperand())
                return SemanticRole.DereferenceOperand;

            if (token.IsLogicalOperand())
                return SemanticRole.LogicalOperand;

            if (token.IsNullCoalescingAssignmentValue())
                return SemanticRole.NullCoalescingAssignmentValue;

            if (token.IsNullCoalescingFallback())
                return SemanticRole.NullCoalescingFallback;

            if (token.IsNullCoalescingTarget())
                return SemanticRole.NullCoalescingTarget;

            if (token.IsNullForgivingOperand())
                return SemanticRole.NullForgivingOperand;

            if (token.IsShiftOperand())
                return SemanticRole.ShiftOperand;

            // Pattern matching
            if (token.IsConstantPattern())
                return SemanticRole.ConstantPattern;

            if (token.IsPatternBindingVariable())
                return SemanticRole.PatternBindingVariable;

            if (token.IsPatternMatchTarget())
                return SemanticRole.PatternMatchTarget;

            if (token.IsPropertyPattern())
                return SemanticRole.PropertyPattern;

            if (token.IsRelationalPattern())
                return SemanticRole.RelationalPattern;

            if (token.IsVarPattern())
                return SemanticRole.VarPattern;

            // Pointers
            if (token.IsPointerBaseType())
                return SemanticRole.PointerBaseType;

            // Return values
            if (token.IsLocalFunctionReturnType())
                return SemanticRole.LocalFunctionReturnType;

            if (token.IsQueryReturnValue())
                return SemanticRole.QueryReturnValue;

            if (token.IsReturnValue())
                return SemanticRole.ReturnValue;

            // Switch roles
            if (token.IsSwitchArmValue())
                return SemanticRole.SwitchArmValue;

            if (token.IsSwitchMatchTarget())
                return SemanticRole.SwitchMatchTarget;

            // Ternary values
            if (token.IsTernaryFalseValue())
                return SemanticRole.TernaryFalseValue;

            if (token.IsTernaryTrueValue())
                return SemanticRole.TernaryTrueValue;

            // Tuple elements
            if (token.IsTupleElement())
                return SemanticRole.TupleElement;

            // Type qualifiers
            if (token.IsTypeQualifier())
                return SemanticRole.TypeQualifier;

            return SemanticRole.Unknown;
        }

        private static SemanticRole TryGetIdentifierRole(NavToken token)
        {
            if (!token.IsIdentifier())
                return SemanticRole.Unknown;

            // --- Identifiers: Declarations ---
            if (token.IsAttribute())
                return SemanticRole.Attribute;

            if (token.IsClassDeclaration())
                return SemanticRole.ClassDeclaration;

            if (token.IsConstructorDeclaration())
                return SemanticRole.ConstructorDeclaration;

            if (token.IsDeconstructionVariable())
                return SemanticRole.DeconstructionVariable;

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

            if (token.IsFixedPointerDeclaration())
                return SemanticRole.FixedPointerDeclaration;

            if (token.IsLambdaParameterDeclaration())
                return SemanticRole.LambdaParameter;

            if (token.IsLocalFunctionDeclaration())
                return SemanticRole.LocalFunctionDeclaration;

            if (token.IsLocalVariableDeclaration())
                return SemanticRole.LocalVariableDeclaration;

            if (token.IsInterfaceDeclaration())
                return SemanticRole.InterfaceDeclaration;

            if (token.IsLoopIteratorDeclaration())
                return SemanticRole.LoopIteratorDeclaration;

            if (token.IsMethodDeclaration())
                return SemanticRole.MethodDeclaration;

            if (token.IsOutVariableDeclaration())
                return SemanticRole.OutVariableDeclaration;

            if (token.IsParameterDeclaration())
                return SemanticRole.Parameter;

            if (token.IsPropertyDeclaration())
                return SemanticRole.PropertyDeclaration;

            if (token.IsRecordDeclaration())
                return SemanticRole.RecordDeclaration;

            if (token.IsRecordStructDeclaration())
                return SemanticRole.RecordStructDeclaration;

            if (token.IsStructDeclaration())
                return SemanticRole.StructDeclaration;

            // --- Identifiers: data types ---
            if (token.IsEventFieldType())
                return SemanticRole.EventFieldType;

            if (token.IsEventPropertyType())
                return SemanticRole.EventPropertyType;

            if (token.IsFieldType())
                return SemanticRole.FieldType;

            if (token.IsLocalVariableType())
                return SemanticRole.LocalVariableType;

            if (token.IsMethodReturnType())
                return SemanticRole.MethodReturnType;

            if (token.IsDeconstructionVariableType())
                return SemanticRole.DeconstructionVariableType;

            if (token.IsDelegateReturnType())
                return SemanticRole.DelegateReturnType;

            if (token.IsParameterType())
                return SemanticRole.ParameterType;

            if (token.IsPropertyType())
                return SemanticRole.PropertyType;

            if (token.IsForEachLoopCollectionIdentifier())
                return SemanticRole.ForEachLoopCollection;

            if (token.IsLoopIteratorType())
                return SemanticRole.LoopIteratorType;

            // --- Identifiers: invocations ---
            if (token.IsMethodInvocation())
                return SemanticRole.MethodInvocation;

            if (token.IsConstructorInvocation())
                return SemanticRole.ConstructorInvocation;

            // --- Identifiers: namespaces, aliases, qualifiers ---
            if (token.IsNamespaceQualifier())
                return SemanticRole.NamespaceQualifier;

            if (token.IsNamespaceAliasDeclarationIdentifier())
                return SemanticRole.NamespaceAliasDeclaration;

            if (token.IsTypeAliasDeclarationIdentifier())
                return SemanticRole.TypeAliasDeclaration;

            if (token.IsAliasQualifier())
                return SemanticRole.AliasQualifier;

            // --- Identifiers: misc types ---
            if (token.IsBaseType())
                return SemanticRole.BaseType;

            if (token.IsCatchExceptionType())
                return SemanticRole.CatchExceptionType;

            if (token.IsCatchExceptionVariable())
                return SemanticRole.CatchExceptionVariable;

            if (token.IsGenericTypeArgument())
                return SemanticRole.GenericTypeArgument;

            if (token.IsGenericTypeParameter())
                return SemanticRole.GenericTypeParameter;

            if (token.IsTupleElementName())
                return SemanticRole.TupleElementName;

            if (token.IsTupleElementType())
                return SemanticRole.TupleElementType;

            if (token.IsTypeParameterConstraint())
                return SemanticRole.TypeParameterConstraint;

            // Identifier - default operands
            if (token.IsDefaultOperand())
                return SemanticRole.DefaultOperand;

            if (token.IsDiscardValue())
                return SemanticRole.DiscardValue;

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
            if (token.IsTypePattern())
                return SemanticRole.TypePattern;

            // Identifier - param labels
            if (token.IsParameterLabel())
                return SemanticRole.ParameterLabel;

            // Identifier - arguments
            if (token.IsArgument())
                return SemanticRole.Argument;

            if (token.IsAttributeArgument())
                return SemanticRole.AttributeArgument;

            // Identifier - type references
            if (token.IsTypeReference())
                return SemanticRole.TypeReference;

            // Identifier - condition values
            if (token.IsConditionValue())
                return SemanticRole.ConditionValue;

            // Identifier - lock target
            if (token.IsLockTarget())
                return SemanticRole.LockTarget;

            // With expression source
            if (token.IsWithExpressionSource())
                return SemanticRole.WithExpressionSource;

            // --------------------------------------------------------- //

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

            if (token.IsGroupElement())
                return SemanticRole.GroupElement;

            if (token.IsJoinIntoRangeVariable())
                return SemanticRole.JoinIntoRangeVariable;

            if (token.IsInstanceQualifier())
                return SemanticRole.InstanceQualifier;

            if (token.IsTargetMember())
                return SemanticRole.TargetMember;

            if (token.IsAssignmentRecipient())
                return SemanticRole.AssignmentRecipient;

            if (token.IsNullCoalescingAssignmentRecipient())
                return SemanticRole.NullCoalescingAssignmentRecipient;

            return SemanticRole.Unknown;
        }
        #endregion

        #region Special Case Tokens
        private static SemanticRole GetSpecialCaseKeywordRole(NavToken token)
        {
            /*
             *  Keeping for testing only
             */

            string? parentKind = "token.ParentNodeKind";

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
                    "ForEachStatement" or "ForEachVariableStatement" => SemanticRole.LoopStatement,
                    "Parameter" => SemanticRole.ParameterModifier,
                    _ when !string.IsNullOrEmpty(parentKind) && parentKind.Contains("Clause") => SemanticRole.QueryExpression,
                    _ => SemanticRole.Unknown
                },

                // object creation, member hiding
                "new" => parentKind is "ObjectCreationExpression"
                    or "ImplicitObjectCreationExpression"
                    or "ImplicitArrayCreationExpression"
                    or "AnonymousObjectCreationExpression"
                        ? SemanticRole.ObjectConstruction
                        : SemanticRole.MemberModifier,

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
            /*
             *  Keeping for testing only
             */

            string? parentKind = "token.ParentNodeKind";
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
                        : SemanticRole.Bitwise,
                    "AddressOfExpression" => SemanticRole.Indirection,
                    _ => SemanticRole.Unknown
                },

                "|" => parentKind switch
                {
                    "BitwiseOrExpression" => isBool
                        ? SemanticRole.BooleanLogical
                        : SemanticRole.Bitwise,
                    _ => SemanticRole.Unknown
                },

                "^" => parentKind switch
                {
                    "ExclusiveOrExpression" => isBool
                        ? SemanticRole.BooleanLogical
                        : SemanticRole.Bitwise,
                    "IndexExpression" => SemanticRole.IndexFromEnd,
                    _ => SemanticRole.Unknown
                },

                "*" => parentKind switch
                {
                    "MultiplyExpression" => SemanticRole.Arithmetic,
                    "PointerType" => SemanticRole.PointerTypeIndicator,
                    "PointerIndirectionExpression" => SemanticRole.Indirection,
                    _ => SemanticRole.Unknown
                },

                _ => SemanticRole.Unknown
            };
        }

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
                var parentKind = token.AncestorKinds.Ancestors[0];

                if (token.Text is not "from" || parentKind != SyntaxKind.FromClause)
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

                var semanticRole = token.SemanticRole;

                if (decToRefDict.TryGetValue(semanticRole, out var referenceRole))
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
                    || token.SemanticRole != SemanticRole.Unknown)
                    continue;

                if (identifierToRefDict.TryGetValue(token.Text, out var referenceRole))
                    token.SemanticRole = referenceRole;
            }
        }
        #endregion
    }
}
