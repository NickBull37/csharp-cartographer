namespace csharp_cartographer_backend._01.Configuration.Enums
{
    public enum GroupRole
    {
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
        LocalDeclaration,
        MemberDeclaration,
        ParameterDeclaration,
        TypeDeclaration,
    }
}
