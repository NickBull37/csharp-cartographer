using csharp_cartographer._03.Models.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer._05.Services.TokenTags
{
    public class TagIndexer : ITagIndexer
    {
        public TagIndexer()
        {
        }

        public void AddHighlightIndicesToTags(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                var tagCount = 1;
                foreach (var tag in token.Tags)
                {
                    if (tag.Tokens.Count == 0 || tagCount == 1)
                    {
                        tag.HighlightIndices.Add(token.Index);
                    }
                    else
                    {
                        GetElementIndices(navTokens, tag);
                    }
                    tagCount++;
                }
            }
        }

        private static void GetElementIndices(List<NavToken> navTokens, TokenTag tag)
        {
            if (tag.Label == "Simple Member Access Expression")
            {

            }

            List<int> highlightIndices = [];
            var elementTextStrings = GetElementStrings(tag);


            if (elementTextStrings.Count == 0 || navTokens.Count < elementTextStrings.Count)
            {
                return;
            }

            for (int i = 0; i <= navTokens.Count - elementTextStrings.Count; i++)
            {
                bool isMatch = true;

                for (int j = 0; j < elementTextStrings.Count; j++)
                {
                    if (navTokens[i + j].Text != elementTextStrings[j])
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                {
                    for (int j = 0; j < elementTextStrings.Count; j++)
                    {
                        highlightIndices.Add(i + j);
                    }
                    break;
                }
            }

            tag.HighlightIndices = highlightIndices;
        }

        private static List<string> GetElementStrings(TokenTag tag)
        {
            List<string> elementStrings = [];

            // trim endOfFile token from list
            if (tag.Tokens.Last().IsKind(SyntaxKind.EndOfFileToken))
            {
                tag.Tokens.RemoveAt(tag.Tokens.Count - 1);
            }

            // trim extra semicolon token from list
            if (tag.Tokens.Last().IsKind(SyntaxKind.SemicolonToken) && !tag.Label.EndsWith("Declaration"))
            {
                tag.Tokens.RemoveAt(tag.Tokens.Count - 1);
            }

            // correction - add lamda to element strings
            if (tag.Label == "Arrow Expression Clause")
            {
                elementStrings.Add("=>");
            }

            foreach (var roslynToken in tag.Tokens)
            {
                elementStrings.Add(roslynToken.Text);
            }

            // correction - add semi colon for local statements
            if (tag.Label == "LocalDeclarationStatement")
            {
                elementStrings.Add(";");
            }

            // correction - add semi colon for using directives
            if (tag.Label == "UsingDirective")
            {
                elementStrings.Add(";");
            }

            return elementStrings;
        }
    }
}
