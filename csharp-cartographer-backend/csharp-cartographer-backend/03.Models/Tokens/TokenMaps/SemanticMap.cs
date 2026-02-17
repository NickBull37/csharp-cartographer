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
        Literal
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
        DefaultExpressionBoundary,
        ForEachBlockBoundary,
        ForEachControlBoundary,
        ForLoopBlockBoundary,
        ForLoopControlBoundary,
        IfBlockBoundary,
        IfConditionBoundary,
        ImplicitArrayCreation,
        InterpolatedValueBoundary,
        MethodBoundary,
        NamespaceBoundary,
        ObjectInitializerBoundary,
        ParameterListBoundary,
        ParenthesizedExpressionBoundary,
        ParenthesizedPatternBoundary,
        PropertyPatternBoundary,
        RecordBoundary,
        RemoveAccessorBlockBoundary,
        SetAccessorBlockBoundary,
        SizeOfExpressionBoundary,
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
        WhileLoopConditionBoundary,
        WhileLoopBlockBoundary,
        WithInitializerExpressionBoundary,


        // ------------ OPERATORS ------------ //
        Arithmetic,
        Assignment,
        BitwiseShift,
        BooleanLogical,
        Comparison,
        IndexRange,
        Lambda,
        MemberAccess,
        NullCoalescing,
        NullCoalescingAssignment,
        NullForgiving,
        Pointer,
        Ternary,


        // ------------ PUNCTUATION ------------ //

        // Separation
        AnonymousObjectMemberDeclarationSeparator,
        ArgumentSeparator,
        ArrayInitializerElementSeparator,
        AttributeArgumentSeparator,
        BaseTypeSeparator,
        CollectionExpressionElementSeparator,
        CollectionInitializerElementSeparator,
        ConstraintSeparator,
        EnumMemberSeparator,
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
        CompilationScope,
        Concurrency,
        ConditionalBranching,
        Constraint,
        ControlFlow,
        DefaultOperator,
        DefaultValue,
        Discard,
        EventHandling,
        ExceptionHandling,
        ImplicitParameter,
        InheritanceModifier,
        Iterator,
        JumpStatement,
        LiteralValue,
        LoopStatement,
        MemberDeclaration,
        MemberModifier,
        NameOfOperator,
        NamespaceImport,
        ObjectConstruction,
        ParameterModifier,
        PatternMatching,
        QueryExpression,
        SafetyContext,
        SizeOfOperator,
        TypeDeclaration,
        TypeOfOperator,
        TypeReference,
        TypeSystem,
        WithExpression,


        // ------------ IDENTIFIERS ------------ //

        AttributeArgument,
        ForEachLoopCollectionIdentifier,
        ObjectPropertyAssignment,
        ParameterLabel,

        // Declarations
        AttributeDeclaration,
        ClassDeclaration,
        ClassConstructorDeclaration,
        DelegateDeclaration,
        EnumDeclaration,
        EnumMemberDeclaration,
        EventFieldDeclaration,
        EventPropertyDeclaration,
        FieldDeclaration,
        ForLoopIteratorDeclaration,
        ForEachLoopIteratorDeclaration,
        InterfaceDeclaration,
        LocalVariableDeclaration,
        MethodDeclaration,
        ParameterDeclaration,
        PropertyDeclaration,
        RecordDeclaration,
        RecordConstructorDeclaration,
        RecordStructDeclaration,
        RecordStructConstructorDeclaration,
        StructDeclaration,
        StructConstructorDeclaration,

        // Constructor invocations
        ConstructorInvocation,
        ClassConstructorInvocation,
        RecordConstructorInvocation,
        RecordStructConstructorInvocation,
        StructConstructorInvocation,

        // References
        ClassReference,
        ConstantReference,
        ConstructorReference,
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
        MethodInvocation,
        FieldAccess,
        EventSubscription,
        EventUnsubscription,
        IndexerAccess,
        PropertyAccess,


        // ------------ LITERALS ------------ //

        // Literals & values
        BooleanLiteral,
        CharacterLiteral,
        NullValue,
        NumericLiteral,
        QuotedString,
        VerbatimString,
        InterpolatedStringStart,
        InterpolatedStringText,
        InterpolatedStringEnd,
        InterpolatedVerbatimStringStart,
        InterpolatedVerbatimStringText,
        InterpolatedVerbatimStringEnd,


        // ------------ TYPES ------------ //

        // Type positions
        ArrayDataType,
        CastType,
        CastTargetType,
        DefaultOperand,
        DelegateReturnType,
        EventFieldType,
        EventPropertyDataType,
        ExceptionType,
        FieldDataType,
        ForLoopIteratorDataType,
        ForEachLoopIteratorDataType,
        GenericTypeArgument,
        GenericTypeParameter,
        TypeParameterConstraint,
        LocalVariableDataType,
        MethodReturnType,
        NameOfOperand,
        OutVariableDataType,
        ParameterDataType,
        PropertyDataType,
        SimpleBaseType,
        SizeOfOperand,
        TupleElementName,
        TupleElementType,
        TypeConstraint,
        TypeOfOperand,
        TypePattern,


        // ------------ NAMES / QUALIFIERS ------------ //

        AliasQualifier,
        MemberName,
        NamespaceAliasDeclaration,
        NamespaceQualifer,
        NamespaceDeclarationQualifer,
        TypeAliasDeclaration,
        TypeQualifier,
        UsingDirectiveQualifier,


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

    public enum SemanticModifiers
    {
        None,

        // Accessibility & scope
        //Public,
        //Private,
        //Protected,
        //Internal,
        //FileScoped,

        // Member behavior
        //Abstract,
        //Async,
        //Const,
        //Override,
        //Partial,
        //Readonly,
        //Required,
        //Sealed,
        //Static,
        //Virtual,
        //Volatile,

        // Identifier types
        //Class,
        //Delegate,
        //Enum,
        //Interface,
        //PredefinedType,
        //Record,
        //RecordStruct,
        //Struct,

        // Type form modifiers
        Anonymous,
        Argument,
        Array,
        For,
        ForEach,
        GenericMethod,
        GenericType,
        TypeParameter,
        InterpolatedValue,
        Nullable,
        Pointer,
        ReturnValue,

        // Property / accessor modifiers
        //Getter,
        //Setter,
        //InitOnly,

        // Variable behavior
        ImplicitlyTyped,
        Ref,
        Out,
        In,

        // Literal modifiers
        //Numeric,
        //QuotedString,
        //VerbatimString,
        //InterpolatedString,
        //InterpolatedVerbatimString,
        //Boolean,
        //Character,

        // Control-flow modifiers
        ShortCircuit,
        FallThrough,
        ConditionalExecution,

        // Operator modifiers
        Concatenation,
        Conditional,
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
        public SemanticRole SemanticRole { get; set; } = SemanticRole.Unknown;

        public PrimaryKind PrimaryKind { get; set; } = PrimaryKind.Unknown;

        public IEnumerable<SemanticModifiers> Modifiers { get; set; } = [];

        // UI elements to show to the user
        public string PrimaryLabel { get; set; } = string.Empty;

        public MapText PrimaryDefinition { get; set; }

        public MapText? PrimaryFocusedDefinition { get; set; }

        public string? SecondaryLabel { get; set; } = string.Empty;

        public MapText? SecondaryDefinition { get; set; }

        // PK and SR only used for development
        public string SemanticRoleString => SemanticRole.ToString();
        public string PrimaryKindString => PrimaryKind.ToString();
        public IEnumerable<string> ModifierStrings { get; set; } = [];

        public SemanticMap(
            PrimaryKind primaryKind,
            SemanticRole semanticRole,
            List<SemanticModifiers> modifiers)
        {
            List<string> modifierStrings = [];
            if (modifiers is not null)
            {
                foreach (var modifier in modifiers)
                {
                    modifierStrings.Add(modifier.ToString());
                }
            }

            PrimaryKind = primaryKind;
            SemanticRole = semanticRole;
            ModifierStrings = modifierStrings;
        }
    }
}
