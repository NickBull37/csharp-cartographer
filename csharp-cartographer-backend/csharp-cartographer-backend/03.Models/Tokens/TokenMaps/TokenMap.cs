namespace csharp_cartographer_backend._03.Models.Tokens.TokenMaps
{
    // a general class based on the original Roslyn classification
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

        Accessor,
        AccessModifier,
        Modifier,

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

        TypeDeclaration,
        UsingDirective,

        // ConstructorInvocations
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
        RecordReference,
        RecordStructReference,
        StructReference,
        TypeReference,

        // Invocations & access
        MethodInvocation,
        ConstructorInvocation,
        PropertyAccess,
        FieldAccess,
        EventSubscription,
        EventUnsubscription,
        IndexerAccess,

        // Type positions (where a type appears)
        CastType,
        ConstraintType,
        FieldDataType,
        GenericTypeArgument,
        //GenericTypeParameter,
        LocalVariableDataType,
        ParameterDataType,
        PropertyDataType,
        MethodReturnType,

        // Control flow
        ControlFlow,
        Conditional,
        Loop,
        Jump,
        ExceptionHandling,

        // Expressions & operations
        Assignment,
        Comparison,
        Arithmetic,
        Logical,
        UnaryOperation,
        BinaryOperation,
        IncrementDecrement,

        // Literals & values
        NumericLiteral,
        StringLiteral,
        DefaultValue,
        NullValue,

        // Structural / non-semantic (still useful)
        Grouping,
        Separator,
        BlockBoundary,
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
        Static,
        Instance,
        Abstract,
        Virtual,
        Override,
        Sealed,
        Readonly,
        Const,
        Required,

        // Identifier types
        Class,
        Delegate,
        Enum,
        Interface,
        Record,
        RecordStruct,
        Struct,

        // Type form modifiers
        Generic,
        Nullable,
        Array,
        Pointer,
        Tuple,
        GenericTypeParameter,

        // Method-specific modifiers
        Async,
        Iterator,
        Extension,
        Partial,

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
        Boolean,
        Character,

        // Control-flow modifiers
        ShortCircuit,
        FallThrough,
        ConditionalExecution,
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
