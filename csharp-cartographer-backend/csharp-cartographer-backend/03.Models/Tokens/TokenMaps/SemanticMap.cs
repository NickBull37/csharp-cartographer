using csharp_cartographer_backend._02.Utilities.Helpers;

namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    /// <summary>
    /// Describes the general syntax category the token falls under.
    /// </summary>
    public enum PrimaryKind
    {
        Unknown,
        Keyword,
        Delimiter,
        Operator,
        Punctuation,
        Identifier,
        Literal,
        Other
    }

    /// <summary>
    /// Describes what the token is actually being used for in it's current context.
    /// </summary>
    public enum SemanticRole
    {
        Unknown,

        #region DELIMITERS
        // ------------ DELIMITERS ------------ //

        // Definitions
        [Label("DefinitionBoundary")]
        ClassBoundary,
        [Label("DefinitionBoundary")]
        ConstructorBoundary,
        [Label("DefinitionBoundary")]
        EnumBoundary,
        [Label("DefinitionBoundary")]
        InterfaceBoundary,
        [Label("DefinitionBoundary")]
        LocalFunctionBoundary,
        [Label("DefinitionBoundary")]
        MethodBoundary,
        [Label("DefinitionBoundary")]
        RecordBoundary,
        [Label("DefinitionBoundary")]
        RecordStructBoundary,
        [Label("DefinitionBoundary")]
        StructBoundary,

        // Condition
        [Label("ConditionBoundary")]
        IfConditionBoundary,
        [Label("ConditionBoundary")]
        SwitchStatementConditionBoundary,
        [Label("ConditionBoundary")]
        WhileLoopConditionBoundary,

        // Context
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

        // Control
        [Label("LoopControlBoundary")]
        ForEachControlBoundary,
        [Label("LoopControlBoundary")]
        ForLoopControlBoundary,
        StatementControlBoundary,

        AccessorListBoundary,
        AddAccessorBlockBoundary,
        AnonymousObjectCreationExpressionBoundary,
        ArgumentListBoundary,
        ArrayInitializationBoundary,
        ArrayType,
        AttributeListBoundary,
        AttributeArgumentListBoundary,
        BlockBoundary,
        BracketedArgumentListBoundary,
        CastTypeBoundary,
        CatchArgumentBoundary,
        CatchBlockBoundary,
        CatchFilterBoundary,
        CollectionInitializerExpressionBoundary,
        CollectionExpressionBoundary,
        DeconstructionBoundary,
        DefaultExpressionBoundary,
        ElseBlockBoundary,
        ForEachBlockBoundary,
        ForLoopBlockBoundary,
        IfBlockBoundary,
        ImplicitArrayCreation,
        InterpolatedValueBoundary,
        LambdaExpressionBlockBoundary,
        NamespaceBoundary,
        ObjectInitializerBoundary,
        ParameterListBoundary,
        ParenthesizedExpressionBoundary,
        ParenthesizedPatternBoundary,
        PropertyPatternBoundary,
        RemoveAccessorBlockBoundary,
        SetAccessorBlockBoundary,
        SizeOfExpressionBoundary,
        SwitchExpressionBoundary,
        SwitchStatementBoundary,
        TryBlockBoundary,
        TupleExpressionBoundary,
        TupleTypeBoundary,
        TypeArgumentListBoundary,
        TypeOfExpressionBoundary,
        TypeParameterListBoundary,
        UsingResourceDeclarationBoundary,
        WhileLoopBlockBoundary,
        WithInitializerExpressionBoundary,
        #endregion

        #region OPERATORS
        // ------------ OPERATORS ------------ //
        Arithmetic,
        Assignment,
        Bitwise,
        BooleanLogical,
        Comparison,
        ExpressionBodyArrow,
        IndexFromEnd,
        Indirection,
        Lambda,
        MemberAccess,
        NullCoalescing,
        NullCoalescingAssignment,
        [Label("Null Conditional (1/2)")]
        NullConditionalQuestion,
        [Label("Null Conditional (2/2)")]
        NullConditionalDot,
        NullForgiving,
        PatternMatchArrow,
        PointerTypeIndicator,
        Range,
        Shift,
        Ternary,
        [Label("Ternary (2/2)")]
        TernaryColon,
        [Label("Ternary (1/2)")]
        TernaryQuestion,
        #endregion

        #region PUNCTUATION
        // ------------ PUNCTUATION ------------ //

        // Separation
        AnonymousObjectMemberDeclarationSeparator,
        ArgumentSeparator,
        ArrayInitializerElementSeparator,
        ArrayLengthSeparator,
        ArrayRankIndicator,
        AttributeArgumentSeparator,
        BaseTypeSeparator,
        CollectionExpressionElementSeparator,
        CollectionInitializerElementSeparator,
        ConstraintSeparator,
        DeconstructionVariableSeparator,
        EnumMemberSeparator,
        InterpolationFormatSeparator,
        MemberPatternSeparator,
        OrderByClauseSeparator,
        ParameterSeparator,
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

        // Misc
        NullableTypeMarker,
        #endregion

        #region ------------ KEYWORDS ------------

        AccessModifier,
        Accessor,
        ArgumentModifier,
        CompilationScope,
        Concurrency,
        ConditionalBranching,
        Constraint,
        ControlFlow,
        [Label("default Operator")]
        DefaultOperator,
        DefaultValue,
        DiscardValue,
        DiscardPattern,
        EventHandling,
        ExceptionHandling,
        ImplicitParameter,
        Iterator,
        JumpStatement,
        LiteralValue,
        LoopStatement,
        MemberDeclaration,
        MemberModifier,
        [Label("nameof Operator")]
        NameOfOperator,
        NamespaceImport,
        ObjectConstruction,
        ObjectConstructionType,
        ParameterModifier,
        PatternMatching,
        QueryExpression,
        SafetyContext,
        [Label("sizeof Operator")]
        SizeOfOperator,
        TypeDeclaration,
        TypeModifier,
        [Label("typeof Operator")]
        TypeOfOperator,
        TypeReference,
        TypeSystem,
        WithExpression,
        #endregion

        #region ------------ IDENTIFIERS ------------

        AttributeArgument,
        ConditionValue,
        ForEachLoopCollection,
        LockTarget,
        ParameterLabel,
        TernaryCondition,

        // Declarations
        Attribute,
        ClassDeclaration,
        ConstructorDeclaration,
        DeconstructionVariable,
        DelegateDeclaration,
        EnumDeclaration,
        EnumMemberDeclaration,
        EventFieldDeclaration,
        EventPropertyDeclaration,
        FieldDeclaration,
        FixedPointerDeclaration,
        InterfaceDeclaration,
        LambdaParameter,
        LocalFunctionDeclaration,
        LocalVariableDeclaration,
        LoopIteratorDeclaration,
        MethodDeclaration,
        OutVariableDeclaration,
        Parameter,
        PropertyDeclaration,
        RecordDeclaration,
        RecordStructDeclaration,
        StructDeclaration,

        // References
        ClassReference,
        ConstantReference,
        DelegateReference,
        EnumReference,
        EnumMemberReference,
        EventReference,
        FieldReference,
        InterfaceReference,
        LocalVariableReference,
        ParameterReference,
        PropertyReference,
        PropertyOrEnumMemberReference,
        RecordReference,
        RecordStructReference,
        StructReference,

        // Invocations & access
        AssignmentRecipient,
        NullCoalescingAssignmentRecipient,
        ConstructorInvocation,
        MethodInvocation,
        EventSubscription,
        EventUnsubscription,
        FieldAccess,
        IndexerAccess,
        InstanceQualifier,
        TargetMember,
        PropertyAccess,
        #endregion

        #region LITERALS
        // ------------ LITERALS ------------ //

        BooleanLiteral,
        CharacterLiteral,
        NullValue,
        NumericFormatSpecifier,
        NumericLiteral,
        QuotedString,
        VerbatimString,
        InterpolatedStringStart,
        InterpolatedStringText,
        InterpolatedStringEnd,
        InterpolatedVerbatimStringStart,
        #endregion

        #region TYPES
        // ------------ TYPES ------------ //

        ArrayDataType,
        BaseType,
        CatchExceptionType,
        CatchExceptionVariable,
        DeconstructionVariableType,
        DelegateReturnType,
        EventFieldType,
        EventPropertyType,
        FieldType,
        GenericTypeArgument,
        GenericTypeParameter,
        TypeParameterConstraint,
        LocalFunctionReturnType,
        LocalVariableType,
        LoopIteratorType,
        MethodReturnType,
        OutVariableType,
        ParameterType,
        PointerBaseType,
        PropertyType,
        TupleElement,
        TupleElementName,
        TupleElementType,
        TypeConstraint,
        #endregion

        #region PATTERN MATCHING
        // ------------ PATTERN MATCHING ------------ //
        ConstantPattern,
        PatternBindingVariable,
        PatternMatchTarget,
        PropertyPattern,
        RelationalPattern,
        TypePattern,
        VarPattern,
        #endregion

        #region NAMES / QUALIFIERS
        // ------------ NAMES / QUALIFIERS ------------ //

        AliasQualifier,
        NamespaceAliasDeclaration,
        NamespaceQualifier,
        TypeAliasDeclaration,
        TypeQualifier,
        #endregion

        #region QUERY EXPRESSIONS
        // ------------ QUERY EXPRESSIONS ------------ //

        GroupContinuationRangeVariable,
        GroupElement,
        JoinIntoRangeVariable,
        JoinRangeVariable,
        JoinSource,
        LetVariable,
        QuerySource,
        QueryVariableReference,
        RangeVariable,
        GroupContinuationRangeVariableReference,
        JoinIntoRangeVariableReference,
        JoinRangeVariableReference,
        LetVariableReference,
        RangeVariableReference,
        #endregion

        #region MISC
        // ------------ MISC ------------ //

        AddressOfOperand,
        AnonymousObjectElement,
        ArithmeticOperand,
        AssignmentValue,
        Argument,
        BitwiseOperand,
        CastType,
        CastTarget,
        CollectionElement,
        CollectionLength,
        ComparisonOperand,
        ConcatenationOperand,
        ConditionalAccessTarget,
        DefaultOperand,
        DereferenceOperand,
        ExpressionOperand,
        IndexValue,
        InterpolatedValue,
        LogicalOperand,
        NameOfOperand,
        NullCoalescingAssignmentValue,
        NullCoalescingFallback,
        NullCoalescingTarget,
        NullForgivingOperand,
        QueryReturnValue,
        ShiftOperand,
        SizeOfOperand,
        SwitchArmValue,
        SwitchMatchTarget,
        ReturnValue,
        TernaryTrueValue,
        TernaryFalseValue,
        TypeOfOperand,
        WithExpressionSource,
        #endregion
    }

    public enum TypeSymbols
    {
        Class,
        Enum,
        Delegate,
        Interface,
        Record,
        RecordStruct,
        Struct,
        TypeParameter,
    }

    public enum MemberSymbols
    {
        Constructor,
        ConversionOperator,
        Event,
        Field,
        Indexer,
        Method,
        Operator,
        Property,
    }

    public enum NonMemberSymbols
    {
        EnumMember,
        Label,
        LocalVariable,
        Namespace,
        Parameter,
        RangeVariable,
    }

    public enum SymbolReference
    {
        LocalVariableReference,
        FieldReference,
        ParameterReference,
        PropertyReference,
    }

    //public enum SemanticModifiers
    //{
    //    None,

    //    // Operator modifiers
    //    Concatenation,
    //    ConditionalMemberAccess,

    //    // Keyword modifiers
    //    ImplicitCreation,
    //    ShortCircuit,

    //    // Identifier modifiers
    //    ImportedNamespace,
    //    UsingStatementResource,

    //    // Type form modifiers
    //    Anonymous,
    //    Argument,
    //    Array,
    //    For,
    //    ForEach,
    //    GenericMethod,
    //    GenericType,
    //    TypeParameter,
    //    Nullable,
    //    Pointer,
    //    ReturnValue,

    //    // Variable behavior
    //    ImplicitlyTyped,
    //    Ref,
    //    Out,
    //    In,

    //    // Control-flow modifiers
    //    FallThrough,
    //    ConditionalExecution,

    //    // Literal modifiers
    //    BooleanLiteral,
    //    CharacterLiteral,
    //    NullValue,
    //    NumericLiteral,
    //    QuotedString,
    //    VerbatimString,
    //}

    public sealed record SemanticMap
    {
        public string PKLabel { get; init; }

        public string SRLabel { get; init; }

        public MapText RoleDefinition { get; set; }

        public string FDLabel { get; init; }

        public MapText? FocusedDefinition { get; set; }

        public SemanticMap(
            PrimaryKind kind,
            SemanticRole role,
            MapText roleDefinition,
            string focusedDefinitionLabel,
            MapText? focusedDefinition)
        {
            PKLabel = kind.ToString();
            SRLabel = role.GetSpacedLabel() ?? role.ToSpacedString();
            FDLabel = focusedDefinitionLabel;
            RoleDefinition = roleDefinition;
            FocusedDefinition = focusedDefinition;
        }
    }
}
