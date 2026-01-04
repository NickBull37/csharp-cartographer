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

        // Declarations
        NamespaceDeclaration,
        TypeDeclaration,
        MethodDeclaration,
        ConstructorDeclaration,
        PropertyDeclaration,
        FieldDeclaration,
        EventDeclaration,
        ParameterDeclaration,
        LocalVariableDeclaration,
        EnumMemberDeclaration,
        DelegateDeclaration,
        AttributeDeclaration,

        // References
        NamespaceReference,
        TypeReference,
        MethodReference,
        ConstructorReference,
        PropertyReference,
        FieldReference,
        EventReference,
        ParameterReference,
        LocalVariableReference,
        EnumMemberReference,
        DelegateReference,

        // Invocations & access
        MethodInvocation,
        ConstructorInvocation,
        PropertyAccess,
        FieldAccess,
        EventSubscription,
        EventUnsubscription,
        IndexerAccess,

        // Type positions (where a type appears)
        ReturnType,
        ParameterType,
        LocalVariableType,
        FieldType,
        PropertyType,
        GenericTypeArgument,
        GenericTypeParameter,
        ConstraintType,
        CastType,

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
        LiteralValue,
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

    public sealed record TokenMap(
        TokenPrimaryKind PrimaryKind,                       // Keyword / Identifier / Operator / etc.
        SemanticRole Role = SemanticRole.None,              // ParameterDeclaration / MethodInvocation / etc.
        IReadOnlySet<SemanticModifiers>? Modifiers = null,
        Color Color = Color.Red,
        TokenTypeInfo? TypeInfo = null,
        string? Raw = null
    );
}
