using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._02.Utilities.Logging
{
    public enum LogType
    {
        ArtifactLog,
        ExceptionLog,
        TextLog,
        TokenLog,
    }

    public static class CartographerLogger
    {
        private static readonly string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        private static readonly string _artifactLogPath = @"02.Utilities\Logging\Logs\ArtifactLog.txt";
        private static readonly string _exceptionLogPath = @"02.Utilities\Logging\Logs\ExceptionLog.txt";
        private static readonly string _textLogPath = @"02.Utilities\Logging\Logs\TextLog.txt";
        private static readonly string _tokenLogPath = @"02.Utilities\Logging\Logs\TokenLog.txt";

        public static void LogArtifact(Artifact artifact)
        {
            ClearLogFile(LogType.ArtifactLog);

            LogMessage(" ", _artifactLogPath);
            LogMessage($"==============================  {artifact.Title}  ==============================", _artifactLogPath);
            LogMessage(" ", _artifactLogPath);
            //LogMessage("-------------------------- Misc data --------------------------");
            LogMessage($"CreatedDate: {artifact.CreatedDate}", _artifactLogPath);
            LogMessage($"TimeToGenerate: {artifact.TimeToGenerate}", _artifactLogPath);
            LogMessage($"Language: {artifact.Language}", _artifactLogPath);
            LogMessage($"ArtifactType: {artifact.ArtifactType}", _artifactLogPath);
            LogMessage($"NumTokensAnalyzed: {artifact.NumTokensAnalyzed}", _artifactLogPath);
            //LogMessage("------------------------- Token data -------------------------");
            LogMessage($"NumLanguageElementTags: {artifact.NumLanguageElementTags}", _artifactLogPath);
            LogMessage($"NumAncestorsMapped: {artifact.NumAncestorsMapped}", _artifactLogPath);
        }

        public static void LogException(Exception ex)
        {
            ClearLogFile(LogType.ExceptionLog);

            LogMessage(" ", _exceptionLogPath);
            LogMessage($"==============================  {ex.GetType()}  ==============================", _exceptionLogPath);
            LogMessage(" ", _exceptionLogPath);
            LogMessage($"Message: {ex.Message}", _exceptionLogPath);
            LogMessage($"Source: {ex.Source}", _exceptionLogPath);
            LogMessage($"StackTrace: {ex.StackTrace}", _exceptionLogPath);
        }

        public static void LogTokens(IEnumerable<NavToken> tokens)
        {
            ClearLogFile(LogType.TokenLog);

            foreach (var token in tokens)
            {
                LogToken(token);
            }
        }

        public static void LogText(string? text)
        {
            if (text is null)
            {
                return;
            }

            using StreamWriter writer = new(Path.Combine(projectRoot, _textLogPath), true);
            writer.WriteLine($"{text}");
        }

        public static void ClearLogFile(LogType logType)
        {
            string? logPath = logType switch
            {
                LogType.ArtifactLog => _artifactLogPath,
                LogType.ExceptionLog => _exceptionLogPath,
                LogType.TextLog => _textLogPath,
                LogType.TokenLog => _tokenLogPath,
                _ => null
            };

            if (logPath is null)
            {
                return;
            }

            using StreamWriter streamWriter = new(Path.Combine(projectRoot, logPath), false);
            streamWriter.Write("");
        }

        private static void LogToken(NavToken token)
        {
            LogMessage(" ", _tokenLogPath);
            LogMessage($"==============================  {token.Index}  |  {token.Text}  ==============================", _tokenLogPath);
            LogMessage(" ", _tokenLogPath);
            LogMessage("---------------------- Misc data ----------------------", _tokenLogPath);
            LogMessage($"Index: {token.Index}", _tokenLogPath);
            LogMessage($"Classification: {token.RoslynClassification ?? "..."}", _tokenLogPath);
            LogMessage($"HighlightColor: {token.HighlightColor ?? "..."}", _tokenLogPath);
            LogMessage("---------------------- Token data ----------------------", _tokenLogPath);
            LogMessage($"Text: {token.Text ?? "..."}", _tokenLogPath);
            LogMessage($"Kind: {token.Kind}", _tokenLogPath);
            //LogMessage("---------------------- Syntax data ----------------------", _tokenLogPath);
            //LogMessage($"ParentNodeKind: {token.ParentNodeKind ?? "..."}", _tokenLogPath);
            //LogMessage($"GrandParentNodeKind: {token.GrandParentNodeKind ?? "..."}", _tokenLogPath);
            //LogMessage($"GreatGrandParentNodeKind: {token.GreatGrandParentNodeKind ?? "..."}", _tokenLogPath);
            //LogMessage($"GreatGreatGrandParentNodeKind: {token.GreatGreatGrandParentNodeKind ?? "..."}", _tokenLogPath);
            //LogMessage($"OldestAncestorKind: {token.AncestorKinds.Ancestors.LastOrDefault().ToString() ?? "..."}", _tokenLogPath);

            var s = token.SemanticData;
            if (s is null)
            {
                LogMessage("=====================================================================================", _tokenLogPath);
                LogMessage(" ", _tokenLogPath);
                LogMessage(" ", _tokenLogPath);
                LogMessage(" ", _tokenLogPath);
                LogMessage(" ", _tokenLogPath);
                return;
            }

            LogMessage("---------------------- Location data ----------------------", _tokenLogPath);
            LogMessage($"IsInUploadedFile: {s.IsInUploadedFile}", _tokenLogPath);
            LogMessage($"IsInSourceCompilation: {s.IsInSourceCompilation}", _tokenLogPath);
            LogMessage($"IsInReferencedAssemblies: {s.IsInReferencedAssemblies}", _tokenLogPath);

            LogMessage("---------------------- Semantic data ----------------------", _tokenLogPath);

            // --- Symbol data ---
            LogMessage($"SymbolName: {(!string.IsNullOrEmpty(s.SymbolName) ? s.SymbolName : "...")}", _tokenLogPath);
            LogMessage($"SymbolKind: {s.SymbolKind.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"ContainingType: {s.ContainingType?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"ContainingNamespace: {(string.IsNullOrEmpty(s.ContainingNamespace) ? s.ContainingNamespace : "...")}", _tokenLogPath);
            LogMessage($"ContainingAssembly: {s.ContainingAssembly?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsDeclaredSymbol: {s.IsDeclaredSymbol}", _tokenLogPath);
            LogMessage($"IsAliasTargetSymbol: {s.IsAliasTargetSymbol}", _tokenLogPath);
            LogMessage($"IsOperation: {s.IsOperation}", _tokenLogPath);
            LogMessage($"IsTypeSymbol: {s.IsTypeSymbol}", _tokenLogPath);
            LogMessage($"IsConvertedTypeSymbol: {s.IsConvertedTypeSymbol}", _tokenLogPath);

            // --- Declarations ---
            var dSym = s.DeclaredSymbol;
            if (dSym is not null)
            {
                LogMessage("----------- Declared symbol -----------", _tokenLogPath);
                LogMessage($"DeclaredSymbolName: {dSym.Name ?? "..."}", _tokenLogPath);
                LogMessage($"DeclaredSymbolKind: {dSym.Kind.ToString() ?? "..."}", _tokenLogPath);
            }

            // --- Alias ---
            var aSym = s.AliasTargetSymbol;
            if (aSym is not null)
            {
                LogMessage("----------- Alias symbol -----------", _tokenLogPath);
                LogMessage($"IsAlias: {s.IsAlias.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"AliasName: {s.AliasName ?? "..."}", _tokenLogPath);
                LogMessage($"AliasTargetKind: {s.AliasTargetSymbol?.Kind.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"AliasTargetDisplayString: {s.AliasTargetName ?? "..."}", _tokenLogPath);
            }

            // --- Symbol types --- (expected for refs, not declarations)
            var tSym = s.TypeSymbol;
            if (tSym is not null)
            {
                LogMessage("----------- Symbol type -----------", _tokenLogPath);
                LogMessage($"IsFieldSymbol: {s.IsFieldSymbol.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"IsPropertySymbol: {s.IsPropertySymbol.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"IsLocalSymbol: {s.IsLocalSymbol.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"IsParameterSymbol: {s.IsParameterSymbol.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"IsMethodSymbol: {s.IsMethodSymbol.ToString() ?? "..."}", _tokenLogPath);
            }

            // --- Member-ish details ---
            LogMessage("----------- Member-ish details -----------", _tokenLogPath);
            LogMessage($"MemberType: {(!string.IsNullOrEmpty(s.MemberType) ? s.MemberType : "...")}", _tokenLogPath);
            LogMessage($"MemberTypeKind: {s.MemberTypeKind.ToString() ?? "..."}", _tokenLogPath);

            // --- Symbol characteristics ---
            LogMessage("----------- Symbol characteristics -----------", _tokenLogPath);
            LogMessage($"Accessibility: {s.Accessibility?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsAbstract: {s.IsAbstract?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsAsync: {s.IsAsync?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsConst: {s.IsConst?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsDefinition: {s.IsOriginalDefinition?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsDiscard: {s.IsDiscard?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsExtern: {s.IsExtern?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsForEachVar: {s.IsForEachVar?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsImplicitlyDeclared: {s.IsImplicitlyDeclared?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsIndexer: {s.IsIndexer?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsOptional: {s.IsOptional?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsOriginalDefinition: {s.IsOriginalDefinition?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsOverride: {s.IsOverride?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsReadOnly: {s.IsReadOnly?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsSealed: {s.IsSealed?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsStatic: {s.IsStatic?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsRequired: {s.IsRequired?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsUsingVar: {s.IsUsingVar?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsVirtual: {s.IsVirtual?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsVolatile: {s.IsVolatile?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsWriteOnly: {s.IsWriteOnly?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsExplicitlyNamedTupleElement: {s.IsExplicitlyNamedTupleElement?.ToString() ?? "..."}", _tokenLogPath);

            // --- Method only characteristics ---
            if (s.IsMethodSymbol)
            {
                LogMessage("----------- Methods only -----------", _tokenLogPath);
                LogMessage($"MethodKind: {s.MethodKind?.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"MethodSignature: {s.MethodSignature ?? "..."}", _tokenLogPath);
                //LogMessage($"MethodSignatureFullyQualified: {s.MethodSignatureFullyQualified ?? "..."}", _tokenLogPath);
                LogMessage($"IsGenericMethod: {s.IsGenericMethod?.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"IsExtensionMethod: {s.IsExtensionMethod?.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"IsReadOnly: {s.IsReadOnly.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"ReturnType: {s.ReturnType?.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"BaseType: {s.ReturnType?.BaseType?.ToString() ?? "..."}", _tokenLogPath);
            }

            if (s.TypeSymbol is not null)
            {
                // --- TypeInfo (original + converted) ---
                LogMessage("----------- TypeInfo -----------", _tokenLogPath);
                LogMessage($"TypeKind: {s.TypeKind?.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"ConvertedTypeKind: {s.ConvertedTypeKind?.ToString() ?? "..."}", _tokenLogPath);
            }

            if (s.Operation is not null)
            {
                // --- Operations (expected for refs) ---
                // OperationKind: FieldReference
                // OperationResultType: IGenerateArtifactWorkflow
                // OperationResultTypeFullyQualified: IGenerateArtifactWorkflow
                LogMessage("----------- Operations -----------", _tokenLogPath);
                LogMessage($"OperationKind: {s.OperationKind.ToString() ?? "..."}", _tokenLogPath);
                LogMessage($"OperationResultType: {s.OperationResultType ?? "..."}", _tokenLogPath);
            }

            LogMessage("=====================================================================================", _tokenLogPath);
            LogMessage(" ", _tokenLogPath);
            LogMessage(" ", _tokenLogPath);
            LogMessage(" ", _tokenLogPath);
            LogMessage(" ", _tokenLogPath);
        }

        private static void LogMessage(string message, string logPath)
        {
            using StreamWriter writer = new(Path.Combine(projectRoot, logPath), true);
            writer.WriteLine($"{message}");
        }
    }
}
