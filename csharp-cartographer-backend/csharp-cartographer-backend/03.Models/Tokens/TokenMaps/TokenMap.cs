namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    // A general class based on the original Roslyn classification
    public enum TokenPrimaryKind
    {
        Unknown,
        Keyword,
        Delimiter,
        Operator,
        Punctuation,
        Identifier,
        Literal
    }

    // What the token actually being used for
    public enum SemanticRole
    {
        None,

        // ------------ DELIMITERS ------------ //

        AccessorListBoundary,
        ArgumentListBoundary,
        AttributeListBoundary,
        AttributeArgumentListBoundary,
        BlockBoundary,
        CastTypeBoundary,
        CollectionExpressionBoundary,
        ForEachBlockBoundary,
        IfBlockBoundary,
        IfConditionBoundary,
        InterpolatedValueBoundary,
        ObjectInitializerBoundary,
        ParameterListBoundary,
        TupleTypeBoundary,
        TypeArgumentListBoundary,
        TypeParameterListBoundary,


        // ------------ OPERATORS ------------ //
        ArithmeticOperator,
        AssignmentOperator,
        ComparisonOperator,
        ConditionalMemberAccessOperator,
        ConditionalOperator,
        LogicalOperator,
        MemberAccessOperator,
        RangeOperator,


        // ------------ PUNCTUATION ------------ //

        // Separation
        ArgumentSeparation,
        BaseTypeSeparation,
        EnumMemberSeparation,
        ParameterSeparation,
        SwitchArmSeparation,
        TupleElementSeparation,
        TypeArgumentSeparation,
        TypeParameterConstraintClauseSeparation,
        TypeParameterSeparation,
        VariableDeclaratorSeparation,

        // Termination
        CaseLabelTermination,
        ParameterLabelTermination,
        StatementTermination,

        // Misc
        NamespaceAliasQualifier,
        NullableTypeMarker,
        NullConditionalGuard,


        // ------------ KEYWORDS ------------ //

        AccessModifier,
        Accessor,
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
        MemberModifier,
        NamespaceImport,
        ObjectConstruction,
        ParameterModifier,
        PatternMatching,
        QueryExpression,
        SafetyContext,
        SourceScope,
        TypeDeclaration,
        TypeReference,
        TypeSystem,


        // ------------ IDENTIFIERS ------------ //

        ObjectPropertyAssignment,
        ParameterLabel,
        UsingDirective,

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
        NamespaceDeclaration,
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
        NamespaceReference,
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


        // ------------ LITERALS ------------ //

        // Literals & values
        BooleanLiteral,
        CharacterLiteral,
        DefaultValue,
        NullValue,
        NumericLiteral,
        StringLiteral,


        // ------------ TYPES ------------ //

        // Type positions (where a type appears)
        CastType,
        CastTargetType,
        ConstraintType,
        ExceptionType,
        FieldDataType,
        GenericTypeArgument,
        GenericTypeParameter,
        LocalVariableDataType,
        ParameterDataType,
        PropertyDataType,
        MethodReturnType,
        SimpleBaseType,
        TypePatternType,
        TupleElementName,
        TupleElementType,
    }

    public enum SemanticModifiers
    {
        None,

        // Accessibility & scope
        Public,
        Private,
        Protected,
        Internal,
        FileScoped,

        // Member behavior
        Abstract,
        Async,
        Const,
        Override,
        Partial,
        Readonly,
        Required,
        Sealed,
        Static,
        Virtual,
        Volatile,

        // Identifier types
        Class,
        Delegate,
        Enum,
        Interface,
        PredefinedType,
        Record,
        RecordStruct,
        Struct,

        // Type form modifiers
        Generic,
        Nullable,
        Array,
        Pointer,
        GenericTypeParameter,

        // Property / accessor modifiers
        Getter,
        Setter,
        InitOnly,

        // Variable behavior
        ImplicitlyTyped,
        Ref,
        Out,
        In,

        // Literal modifiers
        Numeric,
        QuotedString,
        VerbatimString,
        InterpolatedString,
        InterpolatedVerbatimString,
        Boolean,
        Character,

        // Control-flow modifiers
        ShortCircuit,
        FallThrough,
        ConditionalExecution,

        // Operator modifiers
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

    public enum ColorAs
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

    public sealed record TokenMap
    {
        public TokenPrimaryKind PrimaryKind { get; set; }

        public string PrimaryKindString { get; set; }

        public SemanticRole SemanticRole { get; set; }

        public string SemanticRoleString { get; set; }

        public IReadOnlySet<string> Modifiers { get; set; }

        public TokenMap(TokenPrimaryKind primaryKind, SemanticRole semanticRole, HashSet<SemanticModifiers>? modifiers)
        {
            HashSet<string> modifierStrings = [];
            if (modifiers is not null)
            {
                foreach (var modifier in modifiers)
                {
                    modifierStrings.Add(modifier.ToString());
                }
            }

            PrimaryKind = primaryKind;
            SemanticRole = semanticRole;
            PrimaryKindString = primaryKind.ToString();
            SemanticRoleString = semanticRole.ToString();
            Modifiers = modifierStrings;
        }
    }
}
