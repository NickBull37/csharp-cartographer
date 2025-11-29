using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._02.Utilities.Logging
{
    public static class TokenLogger
    {
        private static readonly string _logFilePath = "C:\\Users\\nbuli\\source\\repos\\csharp-cartographer-be\\csharp-cartographer-be\\csharp-cartographer-be\\02.Utilities\\Logging\\Logs\\TokenLog.txt";

        public static void LogTokenList(List<NavToken> tokenList)
        {
            ClearLogFile();

            LogStats(tokenList);

            foreach (var token in tokenList)
            {
                if (token.HighlightColor == "color-red")
                {
                    LogToken(token);
                }

                //if (token.Charts.Count > 2 &&
                //    token.Charts[0].Label == "IdentifierToken"
                //    && token.Charts[1].Label == "IdentifierName"
                //    && token.Charts[2].Label == "SimpleMemberAccessExpression")
                //{
                //    LogToken(token);
                //}

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
            LogMessage(" ");
            LogMessage("------------------- Syntax data -------------------");
            LogMessage($"ParentNodeKind: {token.ParentNodeKind ?? "..."}");
            LogMessage($"GrandParentNodeKind: {token.GrandParentNodeKind ?? "..."}");
            LogMessage($"GreatGrandParentNodeKind: {token.GreatGrandParentNodeKind ?? "..."}");
            LogMessage("------------------- Semantic data -------------------");
            LogMessage($"SymbolName: {token.SemanticData?.SymbolName ?? "..."}");
            LogMessage($"SymbolKind: {token.SemanticData?.SymbolKind ?? "..."}");
            LogMessage($"ContainingType: {token.SemanticData?.ContainingType ?? "..."}");
            LogMessage($"ContainingNamespace: {token.SemanticData?.ContainingNamespace ?? "..."}");
            LogMessage($"TypeName: {token.SemanticData?.TypeName ?? "..."}");
            LogMessage($"TypeKind: {token.SemanticData?.TypeKind ?? "..."}");
            LogMessage($"IsNullable: {token.SemanticData?.IsNullable.ToString() ?? "..."}");
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
