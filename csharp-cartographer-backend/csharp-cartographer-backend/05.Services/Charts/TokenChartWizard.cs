using csharp_cartographer_backend._01.Configuration.CSharpElements;
using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Charts
{
    public class TokenChartWizard : ITokenChartWizard
    {
        public void AddFactsAndInsightsToNavTokenCharts(List<NavToken> navTokens)
        {
            foreach (var token in navTokens)
            {
                foreach (var chart in token.Charts)
                {
                    if (chart is null)
                        continue;

                    foreach (var element in CSharpElements.Elements)
                    {
                        if (chart.Label.Equals(element.Label))
                        {
                            chart.Facts = element.Facts;
                            chart.Insights = element.Insights;
                            chart.Alias = element.Alias;
                        }
                    }
                }
            }
        }

        public void AddHighlightRangeToNavTokenCharts(List<NavToken> navTokens)
        {
            /*
             *  Charts are created with a Label and a list of SyntaxTokens. 
             */
            foreach (var token in navTokens)
            {
                foreach (var chart in token.Charts)
                {
                    bool isSingleToken = chart.Tokens.Count == 1;

                    if (isSingleToken)
                    {
                        var range = new HighlightRange
                        {
                            StartIndex = token.Index,
                            EndIndex = token.Index,
                        };

                        chart.HighlightRange = range;
                    }
                    else
                    {
                        SetChartHighlightRange(navTokens, chart);
                    }
                }
            }
        }

        private static void SetChartHighlightRange(List<NavToken> navTokens, TokenChart chart)
        {
            List<int> highlightIndices = [];

            // loop through syntax tokens in chart and create list of strings
            var tokenStrings = GetSyntaxTokenStrings(chart);

            if (tokenStrings.Count == 0)
                return;

            for (int i = 0; i <= navTokens.Count - tokenStrings.Count; i++)
            {
                bool isMatch = true;

                for (int j = 0; j < tokenStrings.Count; j++)
                {
                    // if nav token strings don't match strings in chart, move on
                    if (navTokens[i + j].Text != tokenStrings[j])
                    {
                        isMatch = false;
                        break;
                    }

                    // if strings do match, check that the spans match also in case strings appear more than once
                    if (chart.Tokens[0].FullSpan != navTokens[i].RoslynToken.FullSpan)
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                {
                    for (int j = 0; j < tokenStrings.Count; j++)
                    {
                        highlightIndices.Add(i + j);
                    }
                    break;
                }
            }

            var range = new HighlightRange
            {
                StartIndex = highlightIndices.First(),
                EndIndex = highlightIndices.Last(),
            };

            chart.HighlightRange = range;
        }

        private static List<string> GetSyntaxTokenStrings(TokenChart chart)
        {
            List<string> elementStrings = [];

            foreach (var token in chart.Tokens)
            {
                elementStrings.Add(token.Text);
            }

            return elementStrings;
        }
    }
}
