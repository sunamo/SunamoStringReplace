namespace SunamoStringReplace._sunamo;

internal class CAG
{
    internal static List<FromToStringReplace> EqualRanges<T>(List<T> contentList, List<T> patternList)
    {
        var result = new List<FromToStringReplace>();
        int? matchIndex = null;
        var firstPatternElement = patternList[0];
        var startAt = 0;
        var comparisonOffset = 0;
        for (var i = 0; i < contentList.Count; i++)
        {
            var contentElement = contentList[i];
            if (!matchIndex.HasValue)
            {
                if (EqualityComparer<T>.Default.Equals(contentElement, firstPatternElement))
                {
                    matchIndex = i + 1;
                    startAt = i;
                }
            }
            else
            {
                comparisonOffset = matchIndex.Value - startAt;
                if (patternList.Count > comparisonOffset)
                {
                    if (EqualityComparer<T>.Default.Equals(contentElement, patternList[comparisonOffset]))
                    {
                        matchIndex++;
                    }
                    else
                    {
                        matchIndex = null;
                        i--;
                    }
                }
                else
                {
                    var matchEnd = (int)matchIndex;
                    result.Add(new FromToStringReplace(matchEnd - patternList.Count + 1, matchEnd, FromToUseStringReplace.None));
                    matchIndex = null;
                }
            }
        }

        foreach (var range in result)
        {
            range.From--;
            range.To--;
        }

        return result;
    }
}
