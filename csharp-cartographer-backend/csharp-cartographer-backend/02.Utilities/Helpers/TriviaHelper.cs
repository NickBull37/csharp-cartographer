using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._02.Utilities.Helpers
{
    public class TriviaHelper
    {
        public static List<string> GetLeadingTrivia(SyntaxToken roslynToken)
        {
            if (!roslynToken.HasLeadingTrivia)
                return [];

            List<string> triviaToAdd = [];

            foreach (var trivia in roslynToken.LeadingTrivia)
            {
                switch (trivia.Kind())
                {
                    case SyntaxKind.SingleLineDocumentationCommentTrivia:
                        triviaToAdd.AddRange(GetLeadingSingleLineDocumentationCommentTrivia(trivia));
                        break;
                    case SyntaxKind.MultiLineCommentTrivia:
                        triviaToAdd.AddRange(GetLeadingMultilineCommentTrivia(trivia));
                        break;
                    case SyntaxKind.EndOfLineTrivia:
                        triviaToAdd.Add(GetLeadingEndOfLineTrivia(trivia));
                        break;
                    case SyntaxKind.RegionDirectiveTrivia:
                    case SyntaxKind.EndRegionDirectiveTrivia:
                        triviaToAdd.AddRange(GetLeadingRegionDirectiveTrivia(trivia));
                        break;
                    default:
                        triviaToAdd.Add(trivia.ToString());
                        break;
                }
            }

            return triviaToAdd;
        }

        public static List<string> GetTrailingTrivia(SyntaxToken roslynToken)
        {
            if (!roslynToken.HasTrailingTrivia)
                return [];

            List<string> triviaToAdd = [];
            foreach (var trivia in roslynToken.TrailingTrivia)
            {
                // handle trailing trivia that contains "\n" instead of "\r\n"
                if (trivia.IsKind(SyntaxKind.EndOfLineTrivia))
                {
                    triviaToAdd.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                    continue;
                }
                triviaToAdd.Add(trivia.ToString());
            }
            return triviaToAdd;
        }

        private static string GetLeadingEndOfLineTrivia(SyntaxTrivia trivia)
        {
            /*
             *  Normal EndOfLineTrivia spans have a length of 2. For some reason, some
             *  files will generate EndOfLineTrivia with a length of 1... which turns
             *  every blank line in the file into a single space. Insert blank line
             *  manually and skip trivia from token.
             *  
             *  see AncestorNodeKinds.cs
             */

            if (trivia.FullSpan.Length == 1)
                return SyntaxFactory.EndOfLine("\r\n").ToString();
            else
                return trivia.ToString();
        }

        private static List<string> GetLeadingSingleLineDocumentationCommentTrivia(SyntaxTrivia trivia)
        {
            List<string> triviaToAdd = [];
            var triviaString = "///" + trivia.ToString();

            var occurrences = StringHelpers.CountOccurrences(triviaString, "///");
            if (occurrences == 1)
            {
                triviaToAdd.Add(triviaString);
                triviaToAdd.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
            }
            else
            {
                var newStrings = triviaString.Split("\r\n");

                var count = 1;
                var numOfStrings = newStrings.Length;
                foreach (var newString in newStrings)
                {
                    // handle scenarios where comments have extra spaces
                    if (StringHelpers.HasSequentialSpaces(newString))
                    {
                        var spacesString = StringHelpers.PullSequentialSpaces(newString);
                        triviaToAdd.Add(spacesString);
                    }

                    triviaToAdd.Add(newString.Trim());
                    if (count < numOfStrings)
                    {
                        // add additional line break trivia for multi-line comments
                        triviaToAdd.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                    }
                    count++;
                }
            }
            return triviaToAdd;
        }

        private static List<string> GetLeadingMultilineCommentTrivia(SyntaxTrivia trivia)
        {
            List<string> triviaToAdd = [];
            var triviaString = trivia.ToString();
            var newStrings = triviaString.Split("\r\n");

            var count = 1;
            var numOfStrings = newStrings.Length;
            foreach (var newString in newStrings)
            {
                // check if string has sequential spaces
                // if so, cut them and create a new space trivia with them
                if (StringHelpers.HasSequentialSpaces(newString))
                {
                    var spacesString = StringHelpers.PullSequentialSpaces(newString);
                    triviaToAdd.Add(spacesString);
                }

                // add new trivia strings
                triviaToAdd.Add(newString.Trim());
                if (count < numOfStrings)
                {
                    triviaToAdd.Add(SyntaxFactory.EndOfLine("\r\n").ToString());
                }
                count++;
            }
            return triviaToAdd;
        }

        private static List<string> GetLeadingRegionDirectiveTrivia(SyntaxTrivia trivia)
        {
            return [trivia.ToString(), SyntaxFactory.EndOfLine("\r\n").ToString()];
        }
    }
}
