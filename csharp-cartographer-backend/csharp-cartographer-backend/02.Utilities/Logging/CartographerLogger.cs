using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis;

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
            LogMessage("-------------------------- Misc data --------------------------", _tokenLogPath);
            LogMessage($"Index: {token.Index}", _tokenLogPath);
            LogMessage($"Classification: {token.RoslynClassification ?? "..."}", _tokenLogPath);
            LogMessage($"HighlightColor: {token.HighlightColor ?? "..."}", _tokenLogPath);
            LogMessage("------------------------- Token data -------------------------", _tokenLogPath);
            LogMessage($"Text: {token.Text ?? "..."}", _tokenLogPath);
            LogMessage($"Kind: {token.Kind}", _tokenLogPath);
            //LogMessage($"RoslynKind: {token.RoslynKind ?? "..."}", _tokenLogPath);
            //LogMessage($"Span: {token.Span}", _tokenLogPath);
            //if (token.LeadingTrivia.Count > 0)
            //{
            //    int count = 1;
            //    foreach (var trivia in token.LeadingTrivia)
            //    {
            //        if (string.IsNullOrWhiteSpace(trivia))
            //        {
            //            LogMessage($"LeadingTrivia #{count}: <spaces> :{trivia.Length}", _tokenLogPath);
            //        }
            //        else
            //        {
            //            LogMessage($"LeadingTrivia #{count}: {trivia}", _tokenLogPath);
            //        }
            //        count++;
            //    }
            //}
            //else
            //{
            //    LogMessage($"LeadingTrivia: ...", _tokenLogPath);
            //}
            //if (token.TrailingTrivia.Count > 0)
            //{
            //    int count = 1;
            //    foreach (var trivia in token.TrailingTrivia)
            //    {
            //        if (string.IsNullOrWhiteSpace(trivia))
            //        {
            //            LogMessage($"TrailingTrivia #{count}: <spaces> :{trivia.Length}", _tokenLogPath);
            //        }
            //        else
            //        {
            //            LogMessage($"TrailingTrivia #{count}: {trivia}", _tokenLogPath);
            //        }
            //        count++;
            //    }
            //}
            //else
            //{
            //    LogMessage($"TrailingTrivia: ...", _tokenLogPath);
            //}
            //LogMessage("------------------------- Syntax data -------------------------", _tokenLogPath);
            //LogMessage($"ParentNodeKind: {token.ParentNodeKind ?? "..."}", _tokenLogPath);
            //LogMessage($"GrandParentNodeKind: {token.GrandParentNodeKind ?? "..."}", _tokenLogPath);
            //LogMessage($"GreatGrandParentNodeKind: {token.GreatGrandParentNodeKind ?? "..."}", _tokenLogPath);
            var s = token.SemanticData;

            LogMessage("------------------------- Location data -------------------------", _tokenLogPath);
            LogMessage($"IsInSource: {s?.IsInSource.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsInMetadata: {s?.IsInMetadata.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsInUploadedFile: {s?.IsInUploadedFile.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"DeclaredInFilePath: {s?.DeclaredInFilePath ?? "..."}", _tokenLogPath);
            LogMessage("------------------------- Semantic data -------------------------", _tokenLogPath);
            // --- Symbol / declaration ---
            LogMessage($"SymbolName: {s?.SymbolName ?? "..."}", _tokenLogPath);
            LogMessage($"SymbolKind: {s?.SymbolKind?.ToString() ?? "..."}", _tokenLogPath);

            LogMessage($"DeclaredSymbolKind: {s?.DeclaredSymbol?.Kind.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"DeclaredSymbolName: {s?.DeclaredSymbol?.Name ?? "..."}", _tokenLogPath);
            LogMessage($"DeclaredSymbolDisplayString: {s?.DeclaredSymbol?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) ?? "..."}", _tokenLogPath);

            // --- Alias ---
            LogMessage($"IsAlias: {s?.IsAlias.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"AliasName: {s?.AliasName ?? "..."}", _tokenLogPath);
            LogMessage($"AliasTargetKind: {s?.AliasTargetSymbol?.Kind.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"AliasTargetDisplayString: {s?.AliasTargetDisplayString ?? "..."}", _tokenLogPath);

            // --- Symbol characteristics ---
            LogMessage($"Accessibility: {s?.Accessibility?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsStatic: {s?.IsStatic?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsAbstract: {s?.IsAbstract?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsVirtual: {s?.IsVirtual?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsOverride: {s?.IsOverride?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsSealed: {s?.IsSealed?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsAsync: {s?.IsAsync?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsExtern: {s?.IsExtern?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsImplicitlyDeclared: {s?.IsImplicitlyDeclared?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsDefinition: {s?.IsDefinition?.ToString() ?? "..."}", _tokenLogPath);

            // --- Member-ish details ---
            LogMessage($"MemberTypeDisplayString: {s?.MemberTypeDisplayString ?? "..."}", _tokenLogPath);
            LogMessage($"MemberTypeKind: {s?.MemberTypeKind.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"MethodSignature: {s?.MethodSignature ?? "..."}", _tokenLogPath);
            LogMessage($"MethodKind: {s?.MethodKind.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsGenericMethod: {s?.IsGenericMethod.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"ReturnType: base type: {s?.ReturnType?.BaseType?.ToString()} type kind: {s?.ReturnType?.TypeKind.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"IsReadOnly: {s?.IsReadOnly.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"TypeParameters: {s?.TypeParameters.ToString() ?? "..."}", _tokenLogPath);

            // --- TypeInfo (original + converted) ---
            LogMessage($"TypeKind: {s?.TypeKind.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"ConvertedTypeKind: {s?.ConvertedTypeKind.ToString() ?? "..."}", _tokenLogPath);

            LogMessage($"NullabilityFlowState: {s?.NullabilityFlowState?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"NullabilityAnnotation: {s?.NullabilityAnnotation?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"ConvertedNullabilityFlowState: {s?.ConvertedNullabilityFlowState?.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"ConvertedNullabilityAnnotation: {s?.ConvertedNullabilityAnnotation?.ToString() ?? "..."}", _tokenLogPath);

            // --- Constants ---
            LogMessage($"HasConstantValue: {s?.HasConstantValue.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"ConstantValue: {s?.ConstantValue ?? "..."}", _tokenLogPath);

            // --- Operations API ---
            LogMessage($"OperationKind: {s?.OperationKind.ToString() ?? "..."}", _tokenLogPath);
            LogMessage($"OperationResultType: {s?.OperationResultType ?? "..."}", _tokenLogPath);

            LogMessage("===========================================================================", _tokenLogPath);
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
