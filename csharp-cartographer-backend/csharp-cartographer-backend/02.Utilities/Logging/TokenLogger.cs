using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._02.Utilities.Logging
{
    public enum LogType
    {
        TokenLog,
        ArtifactLog,
        ExceptionLog
    }

    public static class TokenLogger
    {
        private static readonly string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        private static readonly string _tokenLogPath = @"02.Utilities\Logging\Logs\TokenLog.txt";
        private static readonly string _artifactLogPath = @"02.Utilities\Logging\Logs\TokenListLog.txt";

        public static void ClearLogFile(LogType logType)
        {
            using StreamWriter tokenWriter = new(Path.Combine(projectRoot, _tokenLogPath), false);
            tokenWriter.Write("");

            using StreamWriter tokenListWriter = new(Path.Combine(projectRoot, _artifactLogPath), false);
            tokenListWriter.Write("");
        }

        public static void LogArtifact(Artifact artifact)
        {
            ClearLogFile(LogType.ArtifactLog);

            LogStats(artifact.NavTokens);

            foreach (var token in artifact.NavTokens)
            {
                LogToken(token);
            }
        }

        public static void LogToken(NavToken token)
        {
            LogMessage(" ");
            LogMessage($"==============================  {token.Index}  |  {token.Text}  ==============================");
            LogMessage(" ");
            LogMessage("-------------------------- Misc data --------------------------");
            LogMessage($"Index: {token.Index}");
            LogMessage($"Classification: {token.Classification ?? "..."}");
            LogMessage($"HighlightColor: {token.HighlightColor ?? "..."}");
            LogMessage("------------------------- Token data -------------------------");
            LogMessage($"Text: {token.Text ?? "..."}");
            LogMessage($"Kind: {token.Kind}");
            LogMessage($"RoslynKind: {token.RoslynKind ?? "..."}");
            LogMessage($"Span: {token.Span}");
            if (token.LeadingTrivia.Count > 0)
            {
                int count = 1;
                foreach (var trivia in token.LeadingTrivia)
                {
                    if (string.IsNullOrWhiteSpace(trivia))
                    {
                        LogMessage($"LeadingTrivia #{count}: <spaces> :{trivia.Length}");
                    }
                    else
                    {
                        LogMessage($"LeadingTrivia #{count}: {trivia}");
                    }
                    count++;
                }
            }
            else
            {
                LogMessage($"LeadingTrivia: ...");
            }
            if (token.TrailingTrivia.Count > 0)
            {
                int count = 1;
                foreach (var trivia in token.TrailingTrivia)
                {
                    if (string.IsNullOrWhiteSpace(trivia))
                    {
                        LogMessage($"TrailingTrivia #{count}: <spaces> :{trivia.Length}");
                    }
                    else
                    {
                        LogMessage($"TrailingTrivia #{count}: {trivia}");
                    }
                    count++;
                }
            }
            else
            {
                LogMessage($"TrailingTrivia: ...");
            }
            LogMessage("------------------------- Syntax data -------------------------");
            LogMessage($"ParentNodeKind: {token.ParentNodeKind ?? "..."}");
            LogMessage($"GrandParentNodeKind: {token.GrandParentNodeKind ?? "..."}");
            LogMessage($"GreatGrandParentNodeKind: {token.GreatGrandParentNodeKind ?? "..."}");
            LogMessage("------------------------- Semantic data -------------------------");
            LogMessage($"SymbolName: {token.SemanticData?.SymbolName ?? "..."}");
            LogMessage($"SymbolKind: {token.SemanticData?.SymbolKind ?? "..."}");
            LogMessage($"ContainingType: {token.SemanticData?.ContainingType ?? "..."}");
            LogMessage($"ContainingNamespace: {token.SemanticData?.ContainingNamespace ?? "..."}");
            LogMessage($"TypeName: {token.SemanticData?.TypeName ?? "..."}");
            LogMessage($"TypeKind: {token.SemanticData?.TypeKind ?? "..."}");
            LogMessage($"IsNullable: {token.SemanticData?.IsNullable.ToString() ?? "..."}");
            LogMessage("===========================================================================");
            LogMessage(" ");
            LogMessage(" ");
            LogMessage(" ");
        }

        private static void LogStats(List<NavToken> navTokens)
        {
            LogMessage(" ");
            LogMessage("TOKEN STATS");
            LogMessage("--------------------------------------");
            LogMessage(" ");
            LogMessage($"Count: {navTokens.Count}");
            LogMessage(" ");

        }

        private static void LogMessage(string message)
        {
            using StreamWriter writer = new(Path.Combine(projectRoot, _tokenLogPath), true);
            writer.WriteLine($"{message}");
        }
    }
}
