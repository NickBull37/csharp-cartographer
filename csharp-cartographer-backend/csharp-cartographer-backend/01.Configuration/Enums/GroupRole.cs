namespace csharp_cartographer_backend._01.Configuration.Enums
{
    public enum GroupRole
    {
        None,

        // Delimiter groups
        AccessorBlockBoundary,
        ConditionBoundary,
        ContextBlockBoundary,
        DeclarationBoundary,
        InitializerBoundary,
        LoopBlockBoundary,
        LoopControlBoundary,
        PatternBoundary,
        StatementControlBoundary,
        SwitchBlockBoundary,

        // Identifier groups
        Invocation,
        LocalDeclaration,
        MemberDeclaration,
        ParameterDeclaration,
        TypeDeclaration,
    }
}
