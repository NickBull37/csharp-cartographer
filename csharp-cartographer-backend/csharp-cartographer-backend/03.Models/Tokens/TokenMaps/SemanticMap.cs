using csharp_cartographer_backend._02.Utilities.Helpers;

namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    /// <summary>
    /// Describes the general syntax category the token falls under.
    /// </summary>
    public enum SyntaxCategory
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

        // ------------ DELIMITERS ------------ //

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
        CheckedStatementBlockBoundary,
        ClassBoundary,
        CollectionInitializerExpressionBoundary,
        ConstructorBoundary,
        CollectionExpressionBoundary,
        DeconstructionBoundary,
        DefaultExpressionBoundary,
        ElseBlockBoundary,
        EnumBoundary,
        ForEachBlockBoundary,
        ForEachControlBoundary,
        ForLoopBlockBoundary,
        ForLoopControlBoundary,
        IfBlockBoundary,
        IfConditionBoundary,
        ImplicitArrayCreation,
        InterfaceBoundary,
        InterpolatedValueBoundary,
        LambdaExpressionBlockBoundary,
        MethodBoundary,
        NamespaceBoundary,
        ObjectInitializerBoundary,
        ParameterListBoundary,
        ParenthesizedExpressionBoundary,
        ParenthesizedPatternBoundary,
        PropertyPatternBoundary,
        RecordBoundary,
        RecordStructBoundary,
        RemoveAccessorBlockBoundary,
        SetAccessorBlockBoundary,
        SizeOfExpressionBoundary,
        StructBoundary,
        SwitchExpressionBoundary,
        SwitchStatementBoundary,
        SwitchStatementConditionBoundary,
        TryBlockBoundary,
        TupleExpressionBoundary,
        TupleTypeBoundary,
        TypeArgumentListBoundary,
        TypeOfExpressionBoundary,
        TypeParameterListBoundary,
        UncheckedStatementBlockBoundary,
        UsingResourceDeclarationBoundary,
        UsingStatementBlockBoundary,
        WhileLoopConditionBoundary,
        WhileLoopBlockBoundary,
        WithInitializerExpressionBoundary,


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
        NullForgiving,
        PatternMatchArrow,
        //Pointer,
        PointerTypeIndicator,
        Range,
        Shift,
        Ternary,
        [Label("Ternary (2/2)")]
        TernaryColon,
        [Label("Ternary (1/2)")]
        TernaryQuestion,


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
        DeconstructionValueSeparator,
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
        NullConditionalGuard,


        // ------------ KEYWORDS ------------ //

        AccessModifier,
        Accessor,
        ArgumentModifier,
        CompilationScope,
        Concurrency,
        ConcurrencyModifier,
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
        PolymorphismModifier,
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


        // ------------ IDENTIFIERS ------------ //

        AttributeArgument,
        ForEachLoopCollection,
        ParameterLabel,

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
        InterfaceDeclaration,
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


        // ------------ TYPES ------------ //

        ArrayDataType,
        BaseType,
        CatchExceptionType,
        CatchExceptionVariable,
        DeconstructionVariableDataType,
        DelegateReturnType,
        EventFieldType,
        EventPropertyDataType,
        FieldDataType,
        GenericTypeArgument,
        GenericTypeParameter,
        TypeParameterConstraint,
        [Label("Local Variable Type")]
        LocalVariableDataType,
        LoopIteratorDataType,
        MethodReturnType,
        OutVariableDataType,
        ParameterDataType,
        PointerBaseType,
        PropertyDataType,
        TupleElementName,
        TupleElementType,
        TypeConstraint,


        // ------------ PATTERN MATCHING ------------ //
        ConstantPattern,
        PatternBindingVariable,
        PatternMatchTarget,
        PropertyPattern,
        RelationalPattern,
        TypePattern,
        VarPattern,


        // ------------ NAMES / QUALIFIERS ------------ //

        AliasQualifier,
        NamespaceAliasDeclaration,
        NamespaceQualifier,
        TypeAliasDeclaration,
        TypeQualifier,


        // ------------ QUERY EXPRESSIONS ------------ //

        GroupContinuationRangeVariable,
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

        // ------------ Misc ------------ //

        AddressOfOperand,
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
        ShiftOperand,
        SizeOfOperand,
        SwitchArmValue,
        SwitchMatchTarget,
        ReturnValue,
        TernaryTrueValue,
        TernaryFalseValue,
        TypeOfOperand,
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

    public enum SemanticModifiers
    {
        None,

        // Operator modifiers
        Concatenation,
        ConditionalMemberAccess,

        // Keyword modifiers
        ImplicitCreation,
        ShortCircuit,

        // Identifier modifiers
        ImportedNamespace,
        UsingStatementResource,

        // Type form modifiers
        Anonymous,
        Argument,
        Array,
        For,
        ForEach,
        GenericMethod,
        GenericType,
        TypeParameter,
        Nullable,
        Pointer,
        ReturnValue,

        // Variable behavior
        ImplicitlyTyped,
        Ref,
        Out,
        In,

        // Control-flow modifiers
        FallThrough,
        ConditionalExecution,

        // Literal modifiers
        BooleanLiteral,
        CharacterLiteral,
        NullValue,
        NumericLiteral,
        QuotedString,
        VerbatimString,
    }

    public enum Color
    {
        Blue,
        Gray,
        Green,
        Jade,
        LightBlue,
        LightGreen,
        Orange,
        Purple,
        Red,
        White,
        Yellow,
    }

    public enum ColorAsType
    {
        // white
        Field,
        Namespace,
        Property,

        // light blue
        LocalVaraible,
        Parameter,

        // yellow
        Method,

        // green
        Attribute,
        BaseType,
        Class,
        Delegate,
        Event,
        GenericType,
        Record,

        // light green
        Interface,
        Enum,
        GenericTypeParameter,
        NumericLiteral,

        // blue
        Keyword,

        // purple
        KeywordControl,

        // jade
        Struct,
        RecordStruct,

        // orange
        StringLiteral,

        // red
        Unknown,
    }

    public sealed record TokenTypeInfo(
        bool IsPredefinedType = false,
        bool IsNullable = false,
        bool IsGeneric = false
    );

    public sealed record SemanticMap
    {
        // Used for classifying a token
        public SyntaxCategory SyntaxCategory { get; set; } = SyntaxCategory.Unknown;

        public SemanticRole SemanticRole { get; set; } = SemanticRole.Unknown;

        public IEnumerable<SemanticModifiers> Modifiers { get; set; } = [];

        public string? SymbolReference { get; set; }

        // UI elements to show to the user
        public string RoleLabel { get; set; } = string.Empty;

        public MapText RoleDefinition { get; set; }

        public string CategoryLabel { get; set; } = string.Empty;

        public MapText FocusedDefinition { get; set; }

        public IEnumerable<string> ModifierStrings { get; set; } = [];

        public SemanticMap(
            SyntaxCategory primaryKind,
            SemanticRole semanticRole,
            List<SemanticModifiers> modifiers,
            SymbolReference? symbolReference)
        {
            List<string> modifierStrings = [];
            if (modifiers is not null)
            {
                foreach (var modifier in modifiers)
                {
                    modifierStrings.Add(modifier.ToString());
                }
            }

            SyntaxCategory = primaryKind;
            SemanticRole = semanticRole;
            ModifierStrings = modifierStrings;
            SymbolReference = symbolReference?.ToSpacedString() ?? null;
        }
    }
}
