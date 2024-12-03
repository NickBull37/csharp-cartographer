using csharp_cartographer._03.Models.Tokens;

namespace csharp_cartographer._02.Utilities.Logging
{
    public static class TokenLogger
    {
        private static readonly string _logFilePath = "C:\\Users\\nbuli\\source\\repos\\csharp-cartographer\\csharp-cartographer\\csharp-cartographer\\02.Utilities\\Logging\\Logs\\TokenLog.txt";

        public static void LogTokenList(List<NavToken> tokenList)
        {
            ClearLogFile();

            LogStats(tokenList);

            foreach (var token in tokenList)
            {
                if (token.Tags.Count > 1 &&
                    token.Tags[0].Label == "IdentifierToken")
                {
                    LogToken(token);
                }

                //LogToken(token);
            }
        }

        public static void LogToken(NavToken token)
        {
            LogMessage(" ");
            LogMessage($"==============================  {token.Index}  |  {token.Text}  ==============================");
            //LogMessage(" ");
            //LogMessage($"    Index #{token.Index} {token.Text}    ");
            LogMessage(" ");
            LogMessage("------------------- Misc data -------------------");
            LogMessage($"Index: {token.Index}");
            LogMessage($"HighlightColor: {token.HighlightColor ?? "..."}");
            LogMessage(" ");
            LogMessage("------------------- Token data -------------------");
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
            LogMessage($"Value: {token.Value}");
            LogMessage(" ");
            LogMessage("------------------- Syntax data -------------------");
            LogMessage($"ParentNodeKind: {token.ParentNodeKind ?? "..."}");
            LogMessage($"ParentNodeType: {token.ParentNodeType ?? "..."}");
            LogMessage(" ");
            LogMessage($"GrandParentNodeKind: {token.GrandParentNodeKind ?? "..."}");
            LogMessage($"GrandParentNodeType: {token.GrandParentNodeType ?? "..."}");
            LogMessage(" ");
            LogMessage($"GreatGrandParentNodeKind: {token.GreatGrandParentNodeKind ?? "..."}");
            LogMessage($"GreatGrandParentNodeType: {token.GreatGrandParentNodeType ?? "..."}");
            LogMessage(" ");
            LogMessage("------------------- Semantic data -------------------");
            LogMessage($"SymbolName: {token.SymbolName ?? "..."}");
            LogMessage($"SymbolKind: {token.SymbolKind ?? "..."}");
            LogMessage($"ContainingType: {token.ContainingType ?? "..."}");
            LogMessage($"ContainingNamespace: {token.ContainingNamespace ?? "..."}");
            LogMessage($"TypeName: {token.TypeName ?? "..."}");
            LogMessage($"TypeKind: {token.TypeKind ?? "..."}");
            LogMessage($"IsNullable: {token.IsNullable}");
            LogMessage(" ");
            //LogMessage("=================================================================");
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
            using StreamWriter writer = new(_logFilePath, true);
            writer.WriteLine($"{message}");
        }

        private static void ClearLogFile()
        {
            using StreamWriter writer = new(_logFilePath, false);
            writer.Write("");
        }
    }
}
