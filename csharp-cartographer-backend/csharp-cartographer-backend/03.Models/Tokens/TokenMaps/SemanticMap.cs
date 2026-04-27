namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    /// <summary>
    /// Describes the general syntax category the token falls under.
    /// </summary>
    public enum PrimaryKind
    {
        Unknown,
        Delimiter,
        Identifier,
        Keyword,
        Literal,
        Operator,
        Punctuation,

        [Label("Keyword / Literal")]
        KeywordLiteral,
        [Label("Keyword / Operator")]
        KeywordOperator,
        [Label("Identifier / Keyword")]
        IdentifierKeyword
    }

    /// <summary>
    /// Describes what the token is actually being used for in it's current context.
    /// </summary>
    public enum SemanticRole
    {
        Unknown,

        #region ------------ DELIMITERS ------------

        AccessorListBoundary,
        AnonymousObjectCreationExpressionBoundary,
        ArrayInitializationBoundary,
        ArrayType,
        AttributeListBoundary,
        CastTypeBoundary,
        CatchArgumentBoundary,
        CatchFilterBoundary,
        CollectionInitializerExpressionBoundary,
        CollectionExpressionBoundary,
        DeconstructionBoundary,
        DefaultExpressionBoundary,
        ImplicitArrayCreation,
        InterpolatedValueBoundary,
        NamespaceBoundary,
        ObjectInitializerBoundary,
        ParenthesizedExpressionBoundary,
        ParenthesizedPatternBoundary,
        PropertyPatternBoundary,
        SizeOfExpressionBoundary,
        TupleExpressionBoundary,
        TupleTypeBoundary,
        TypeOfExpressionBoundary,
        UsingResourceDeclarationBoundary,
        WithInitializerExpressionBoundary,

        // Arg/Param Lists
        ArgumentListBoundary,
        AttributeArgumentListBoundary,
        BracketedArgumentListBoundary,
        ParameterListBoundary,
        TypeArgumentListBoundary,
        TypeParameterListBoundary,

        // Blocks
        AddAccessorBlockBoundary,
        CatchBlockBoundary,
        ElseBlockBoundary,
        ForEachBlockBoundary,
        ForLoopBlockBoundary,
        IfBlockBoundary,
        LambdaExpressionBlockBoundary,
        RemoveAccessorBlockBoundary,
        SetAccessorBlockBoundary,
        SwitchExpressionBlockBoundary,
        SwitchStatementBlockBoundary,
        TryBlockBoundary,
        WhileLoopBlockBoundary,

        // Condition
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

        // Control
        [Label("LoopControlBoundary")]
        ForEachControlBoundary,
        [Label("LoopControlBoundary")]
        ForLoopControlBoundary,
        [Label("StatementControlBoundary")]
        FixedStatementControlBoundary,
        [Label("StatementControlBoundary")]
        LockStatementControlBoundary,

        // Pattern Matching
        [Label("PatternBoundary")]
        ListPatternBoundary,
        [Label("PatternBoundary")]
        PositionalPatternBoundary,

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
        OperatorSymbol,
        PatternMatchArrow,
        RangeSlice,
        Shift,
        Ternary,
        TypeTesting,

        // multi-token
        [Label("Null Conditional (1/2)")]
        NullConditionalQuestion,
        [Label("Null Conditional (2/2)")]
        NullConditionalDot,
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
        CollectionExpressionElementSeparator,
        CollectionInitializerElementSeparator,
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
        Iterator,
        MemberDeclaration,
        MemberModifier,
        ObjectConstruction,
        ParameterModifier,
        QueryExpression,
        ResourceManagement,
        SafetyContext,
        TypeDeclaration,
        TypeModifier,
        TypeSystem,
        UsingDirectiveModifier,
        WithExpression,

        // purple
        JumpStatement,
        ConditionalBranching,
        ExceptionHandling,
        ControlFlow,
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
        CatchExceptionVariable,
        ConditionValue,
        EventFieldType,
        EventPropertyType,
        ForEachLoopCollection,
        GenericTypeParameter,
        LockTarget,
        NullCoalescingAssignmentRecipient,
        ParameterLabel,
        TernaryCondition,
        TupleElementName,
        WithExpressionSource,

        // Qualifiers
        AliasQualifier,
        ContainingTypeMemberQualifer,
        ElementAccessQualifer,
        InstanceQualifier,
        NamespaceQualifier,

        // Declarations
        OutVariableDeclaration,

        // Alias Declarations
        NamespaceAliasDeclaration,
        TypeAliasDeclaration,

        // Local Declarations
        [Label("Local Declaration")]
        DeconstructionVariable,
        [Label("Local Declaration")]
        FixedPointerDeclaration,
        [Label("Local Declaration")]
        LocalVariableDeclaration,
        [Label("Local Declaration")]
        LoopIteratorDeclaration,

        // Member Declarations
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
        LocalFunctionDeclaration,
        [Label("Member Declaration")]
        MethodDeclaration,
        [Label("Member Declaration")]
        OperatorDeclaration,
        [Label("Member Declaration")]
        PropertyDeclaration,

        // Parameter Declarations
        [Label("Parameter Declaration")]
        LambdaParameter,
        [Label("Parameter Declaration")]
        Parameter,

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

        // Invocations & access
        ConstructorInvocation,
        EventSubscription,
        EventUnsubscription,
        FieldAccess, // 
        GenericMethodInvocation,
        IndexerAccess, //
        MethodInvocation,
        PropertyAccess, //
        TargetMember,

        // Query expressions
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
        #endregion

        #region ------------ LITERALS ------------

        //BooleanLiteral,
        //CharacterLiteral,
        //NullValue,
        NumericFormatSpecifier,
        //NumericLiteral,
        //QuotedString,
        //VerbatimString,
        InterpolatedStringStart,
        InterpolatedStringText,
        InterpolatedStringEnd,
        InterpolatedVerbatimStringStart,
        #endregion

        #region ------------ MISC ------------

        AnonymousObjectElement,
        Argument,
        AssignmentValue,
        AttributeArgument,
        CastTarget,
        CollectionElement,
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

        // Types
        ArrayDataType,
        CastType,
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
        //LoopIteratorType,
        MethodReturnType,
        OutVariableType,
        ParameterType,
        PointerBaseType,
        PropertyType,
        TupleElement,
        TupleElementType,
        TypeConstraint,
        TypeParameterConstraint,
        TypeQualifier,
        #endregion

        #region ------------ KEYWORD LITERALS ------------

        #endregion

        #region ------------ KEYWORD OPERATORS ------------

        DefaultOperator,
        NameOfOperator,
        SizeOfOperator,
        TypeOfOperator,
        #endregion

        #region ------------ IDENTIFIER KEYWORDS ------------

        #endregion
    }

    public enum SecondaryRole
    {
        ConditionalAccessTarget,
        NullForgivingOperand,
        TargetMember,
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

        public string FDLabel { get; init; }

        // TODO: deprecate MapText class
        public MapText? RoleDefinition { get; set; }

        public MapText? FocusedDefinition { get; set; }

        public SemanticMap(
            string kindLabel,
            string roleLabel,
            string focusedLabel,
            MapText? roleDefinition,
            MapText? focusedDefinition)
        {
            PKLabel = kindLabel;
            SRLabel = roleLabel;
            FDLabel = focusedLabel;
            RoleDefinition = roleDefinition;
            FocusedDefinition = focusedDefinition;
        }
    }
}
