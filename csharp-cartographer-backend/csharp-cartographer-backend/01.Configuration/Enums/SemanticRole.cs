using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

namespace csharp_cartographer_backend._01.Configuration.Enums
{
    /// <summary>
    /// Describes what the token is actually being used for in it's current context.
    /// </summary>
    public enum SemanticRole
    {
        Unknown,

        #region ------------ DELIMITERS ------------

        AccessorListBoundary,
        AttributeListBoundary,
        CastTypeBoundary,
        CatchArgumentBoundary,
        CollectionExpressionBoundary,
        DeconstructionBoundary,
        InterpolationBoundary,
        ParenthesizedExpressionBoundary,
        TupleExpressionBoundary,
        TupleTypeBoundary,

        // Array types
        ArrayTypeFragment,
        ImplicitArrayTypeFragment,

        // Accessor Blocks
        [Label("AccessorBlockBoundary")]
        AddAccessorBlockBoundary,
        [Label("AccessorBlockBoundary")]
        GetAccessorBlockBoundary,
        [Label("AccessorBlockBoundary")]
        InitAccessorBlockBoundary,
        [Label("AccessorBlockBoundary")]
        RemoveAccessorBlockBoundary,
        [Label("AccessorBlockBoundary")]
        SetAccessorBlockBoundary,

        // Arg/Param Lists
        ArgumentListBoundary,
        AttributeArgumentListBoundary,
        IndexArgumentListBoundary,
        ParameterListBoundary,
        TypeArgumentListBoundary,
        TypeParameterListBoundary,

        // Blocks
        CatchBlockBoundary,
        ElseBlockBoundary,
        IfBlockBoundary,
        LambdaExpressionBlockBoundary,
        TryBlockBoundary,

        // Condition
        [Label("ConditionBoundary")]
        CatchFilterClauseConditionBoundary,
        [Label("ConditionBoundary")]
        DoWhileConditionBoundary,
        [Label("ConditionBoundary")]
        IfConditionBoundary,
        [Label("ConditionBoundary")]
        SwitchStatementConditionBoundary,
        [Label("ConditionBoundary")]
        WhileLoopConditionBoundary,

        // Context Blocks
        [Label("ContextBlockBoundary")]
        CheckedStatementBlockBoundary,
        [Label("ContextBlockBoundary")]
        FixedStatementBlockBoundary,
        [Label("ContextBlockBoundary")]
        LockStatementBlockBoundary,
        [Label("ContextBlockBoundary")]
        UncheckedStatementBlockBoundary,
        [Label("ContextBlockBoundary")]
        UnsafeStatementBlockBoundary,
        [Label("ContextBlockBoundary")]
        UsingStatementBlockBoundary,

        // Declarations
        [Label("DeclarationBoundary")]
        ClassBoundary,
        [Label("DeclarationBoundary")]
        ConstructorBoundary,
        [Label("DeclarationBoundary")]
        EnumBoundary,
        [Label("DeclarationBoundary")]
        InterfaceBoundary,
        [Label("DeclarationBoundary")]
        LocalFunctionBoundary,
        [Label("DeclarationBoundary")]
        MethodBoundary,
        [Label("DeclarationBoundary")]
        NamespaceBoundary,
        [Label("DeclarationBoundary")]
        RecordBoundary,
        [Label("DeclarationBoundary")]
        RecordStructBoundary,
        [Label("DeclarationBoundary")]
        StructBoundary,

        // Expressions
        DefaultExpressionBoundary,
        SizeOfExpressionBoundary,
        TypeOfExpressionBoundary,

        // Initializers
        [Label("InitializerBoundary")]
        AnonymousObjectInitializerBoundary,
        [Label("InitializerBoundary")]
        ArrayInitializerBoundary,
        [Label("InitializerBoundary")]
        CollectionElementInitializerBoundary,
        [Label("InitializerBoundary")]
        CollectionInitializerBoundary,
        [Label("InitializerBoundary")]
        ObjectInitializerBoundary,
        [Label("InitializerBoundary")]
        WithInitializerExpressionBoundary,

        // Loop Blocks
        [Label("LoopBlockBoundary")]
        DoWhileLoopBlockBoundary,
        [Label("LoopBlockBoundary")]
        ForEachLoopBlockBoundary,
        [Label("LoopBlockBoundary")]
        ForLoopBlockBoundary,
        [Label("LoopBlockBoundary")]
        WhileLoopBlockBoundary,

        // Loop Control
        [Label("LoopControlBoundary")]
        ForEachControlBoundary,
        [Label("LoopControlBoundary")]
        ForLoopControlBoundary,

        // Pattern Matching
        [Label("PatternBoundary")]
        ListPatternBoundary,
        [Label("PatternBoundary")]
        ParenthesizedPatternBoundary,
        [Label("PatternBoundary")]
        PositionalPatternBoundary,
        [Label("PatternBoundary")]
        PropertyPatternBoundary,

        // Statement Control
        [Label("StatementControlBoundary")]
        FixedStatementControlBoundary,
        [Label("StatementControlBoundary")]
        LockStatementControlBoundary,
        [Label("StatementControlBoundary")]
        UsingStatementControlBoundary,

        // Switch Blocks
        [Label("SwitchBlockBoundary")]
        SwitchExpressionBlockBoundary,
        [Label("SwitchBlockBoundary")]
        SwitchStatementBlockBoundary,
        #endregion

        #region ------------ OPERATORS ------------

        Arithmetic,
        Assignment,
        Bitwise,
        BooleanLogical,
        Comparison,
        Equality,
        ExpressionBodyArrow, // move to punc?
        IndexFromEnd,
        Indirection,
        Lambda,
        MemberAccess,
        NamespaceAlias,
        NullCoalescing,
        NullCoalescingAssignment,
        NullForgiving,
        OperatorDeclaration,
        PatternMatchArrow,
        RangeSlice,
        Shift,

        // multi-token
        [Label("Null Conditional (2/2)")]
        NullConditionalDot,
        [Label("Null Conditional (1/2)")]
        NullConditionalQuestion,
        [Label("Ternary (2/2)")]
        TernaryColon,
        [Label("Ternary (1/2)")]
        TernaryQuestion,
        #endregion

        #region ------------ PUNCTUATION ------------

        // Misc
        ArrayRankIndicator,
        NullableTypeMarker,
        PointerTypeIndicator,

        // Separators
        AnonymousObjectMemberDeclarationSeparator,
        ArgumentSeparator,
        ArrayInitializerElementSeparator,
        ArrayLengthSeparator,
        AttributeArgumentSeparator,
        BaseTypeSeparator,
        CollectionElementSeparator,
        ComplexElementSeparator,
        ConstraintSeparator,
        DeconstructionVariableSeparator,
        EnumMemberSeparator,
        InterpolationFormatSeparator,
        MemberPatternSeparator,
        OrderByClauseSeparator,
        ParameterSeparator,
        PatternElementSeparator,
        PropertyInitializationSeparator,
        QualifiedNameSeparator,
        SwitchArmSeparator,
        TupleElementSeparator,
        TypeArgumentSeparator,
        TypeParameterConstraintClauseSeparator,
        TypeParameterSeparator,
        VariableDeclaratorSeparator,

        // Termination
        CaseLabelTerminator,
        CasePatternLabelTerminator,
        DefaultLabelTerminator,
        ParameterLabelTerminator,
        StatementTerminator,
        #endregion

        #region ------------ KEYWORDS ------------

        // blue
        AccessModifier,
        Accessor,
        ArgumentModifier,
        CompilationScope,
        Constraint,
        DiscardPattern,
        DiscardValue,
        InitializerModifier,
        Iterator,
        LocalModifier,
        MemberDeclarator,
        MemberModifier,
        ObjectConstruction,
        ParameterModifier,
        QueryExpression,
        ResourceManagement,
        SafetyContext,
        TypeDeclarator,
        TypeModifier,
        TypeSystem,
        UsingDirectiveModifier,

        // purple
        ConditionalBranching,
        ControlFlow,
        ExceptionHandling,
        JumpStatement,
        LoopStatement,

        // blue | purple
        Concurrency,
        PatternMatching,
        #endregion

        #region ------------ IDENTIFIERS ------------

        AssignmentRecipient,
        Attribute,
        BaseType,
        CatchExceptionType,
        ConditionValue,
        EventFieldType,
        EventPropertyType,
        ForEachLoopCollection,
        GenericTypeParameter,
        LockTarget,
        ParameterLabel,
        TargetMember,
        TernaryCondition,
        TupleElementName,
        Type,
        WithExpressionSource,

        // Alias Declarations
        NamespaceAliasDeclaration,
        TypeAliasDeclaration,

        // Events
        //EventSubscription,
        //EventUnsubscription,

        // Invocations
        [Label("Invocation")]
        ConstructorInvocation,
        [Label("Invocation")]
        GenericMethodInvocation,
        [Label("Invocation")]
        MethodInvocation,

        // Local Declarations
        [Label("Local Declaration")]
        CatchExceptionVariable,
        [Label("Local Declaration")]
        DeconstructionVariable,
        [Label("Local Declaration")]
        FixedPointerDeclaration,
        [Label("Local Declaration")]
        LocalConstantDeclaration,
        [Label("Local Declaration")]
        LocalFunctionDeclaration,
        [Label("Local Declaration")]
        LocalVariableDeclaration,
        [Label("Local Declaration")]
        LoopIteratorDeclaration,
        [Label("Local Declaration")]
        OutVariableDeclaration,
        [Label("Local Declaration")]
        UsingResourceDeclaration,

        // Member Declarations
        [Label("Member Declaration")]
        ConstantDeclaration,
        [Label("Member Declaration")]
        ConstructorDeclaration,
        [Label("Member Declaration")]
        EnumMemberDeclaration,
        [Label("Member Declaration")]
        EventFieldDeclaration,
        [Label("Member Declaration")]
        EventPropertyDeclaration,
        [Label("Member Declaration")]
        FieldDeclaration,
        [Label("Member Declaration")]
        GenericMethodDeclaration,
        [Label("Member Declaration")]
        MethodDeclaration,
        [Label("Member Declaration")]
        PropertyDeclaration,

        // Parameter Declarations
        [Label("Parameter Declaration")]
        LambdaParameter,
        [Label("Parameter Declaration")]
        Parameter,

        // Qualifiers
        AliasQualifier,
        CollectionInstanceQualifer,
        ImplicitInstanceQualifier,
        InstanceQualifier,
        NamespaceQualifier,

        // Type Declarations
        [Label("Type Declaration")]
        ClassDeclaration,
        [Label("Type Declaration")]
        DelegateDeclaration,
        [Label("Type Declaration")]
        EnumDeclaration,
        [Label("Type Declaration")]
        InterfaceDeclaration,
        [Label("Type Declaration")]
        RecordDeclaration,
        [Label("Type Declaration")]
        RecordStructDeclaration,
        [Label("Type Declaration")]
        StructDeclaration,

        // Query Expressions
        GroupContinuationRangeVariable,
        GroupElement,
        JoinIntoRangeVariable,
        JoinRangeVariable,
        JoinSource,
        LetVariable,
        QuerySource,
        RangeVariable,
        #endregion

        #region ------------ LITERALS ------------

        InterpolatedStringStart,
        InterpolatedStringText,
        InterpolatedStringEnd,
        InterpolatedVerbatimStringStart,
        NumericFormatSpecifier,
        #endregion

        #region ------------ MISC ------------

        AnonymousObjectElement,
        Argument,
        AssignmentValue,
        AttributeArgument,
        CastTarget,
        CollectionElement,
        CollectionElementKey,
        CollectionElementValue,
        CollectionLength,
        IndexValue,
        InterpolatedValue,
        NullCoalescingAssignmentValue,
        QueryReturnValue,
        ReturnValue,
        SwitchArmValue,
        SwitchMatchTarget,

        // Operands
        AddressOfOperand,
        ArithmeticOperand,
        BitwiseOperand,
        ComparisonOperand,
        ConcatenationOperand,
        DefaultOperand,
        DereferenceOperand,
        LogicalOperand,
        NameOfOperand,
        NullCoalescingFallback,
        NullCoalescingTarget,
        NullForgivingOperand,
        ShiftOperand,
        SizeOfOperand,
        TernaryTrueValue,
        TernaryFalseValue,
        TypeOfOperand,

        // Pattern matching
        ConstantPattern,
        PatternBindingVariable,
        PatternMatchTarget,
        PropertyPattern,
        RelationalPattern,
        TypePattern,
        VarPattern,

        // Qualifiers
        LiteralQualifier,
        TypeQualifier,

        // Types
        ArrayBaseType,
        CastType,
        ConversionTargetType,
        DeconstructionVariableType,
        DelegateReturnType,
        FieldType,
        [Label("Loop Iterator Type")]
        ForEachLoopIteratorType,
        [Label("Loop Iterator Type")]
        ForLoopIteratorType,
        GenericTypeArgument,
        LocalFunctionReturnType,
        LocalVariableType,
        MethodReturnType,
        OperatorReturnType,
        ParameterType,
        PointerBaseType,
        PropertyType,
        TupleElement,
        TupleElementType,
        TypeConstraint,
        TypeParameterConstraint,
        #endregion

        #region ------------ KEYWORD OPERATORS ------------

        DefaultOperator,
        NameOfOperator,
        SizeOfOperator,
        TypeOfOperator,
        TypeTesting,
        #endregion
    }
}
