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
        ArrayInitializationBoundary,
        ArgumentListBoundary,
        AttributeListBoundary,
        AttributeArgumentListBoundary,
        BlockBoundary,
        CastTypeBoundary,
        CatchArgumentBoundary,
        CatchBlockBoundary,
        CatchFilterBoundary,
        ClassBoundary,
        ConstructorBoundary,
        CollectionExpressionBoundary,
        ForEachBlockBoundary,
        ForBlockBoundary,
        IfBlockBoundary,
        IfConditionBoundary,
        ImplicitArrayCreation,
        InterpolatedValueBoundary,
        MethodBoundary,
        NamespaceBoundary,
        ObjectInitializerBoundary,
        ParameterListBoundary,
        TryBlockBoundary,
        TupleExpressionBoundary,
        TupleTypeBoundary,
        TypeArgumentListBoundary,
        TypeParameterListBoundary,


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
        ArgumentSeparator,
        BaseTypeSeparator,
        CollectionElementSeparator,
        EnumMemberSeparator,
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
        EventHandling,
        ExceptionHandling,
        InheritanceModifier,
        Iterator,
        JumpStatement,
        LiteralValue,
        LoopStatement,
        MemberDeclaration,
        MemberModifier,
        NamespaceImport,
        ObjectConstruction,
        ParameterModifier,
        PatternMatching,
        QueryExpression,
        SafetyContext,
        TypeDeclaration,
        TypeReference,
        TypeSystem,


        // ------------ IDENTIFIERS ------------ //

        AttributeArgument,
        ObjectPropertyAssignment,
        ParameterLabel,

        // Declarations
        AttributeDeclaration,
        ClassDeclaration,
        ClassConstructorDeclaration,
        DelegateDeclaration,
        EnumDeclaration,
        EnumMemberDeclaration,
        EventDeclaration,
        EventFieldDeclaration,
        FieldDeclaration,
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
        DefaultValue,
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
        CastType,
        CastTargetType,
        ConstraintType,
        DelegateReturnType,
        EventFieldType,
        ExceptionType,
        FieldDataType,
        GenericTypeArgument,
        GenericTypeParameter,
        LocalVariableDataType,
        MethodReturnType,
        ParameterDataType,
        PropertyDataType,
        SimpleBaseType,
        TypePatternType,
        //TypeReference,
        TupleElementName,
        TupleElementType,


        // ------------ NAMES / QUALIFIERS ------------ //

        AliasDeclaration,
        AliasQualifier,
        //QualifiedTarget,
        MemberName,
        NamespaceQualifer,
        NamespaceDeclarationQualifer,
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
        Generic,
        GenericTypeParameter,
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
