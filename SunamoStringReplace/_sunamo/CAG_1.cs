namespace SunamoStringReplace._sunamo;

/// <summary>
/// Internal collection helper class providing generic collection algorithms.
/// </summary>
internal class CAG
{
    /// <summary>
    /// Finds contiguous ranges where elements in the content list match the pattern list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the lists.</typeparam>
    /// <param name="contentList">The content list to search in.</param>
    /// <param name="patternList">The pattern list to match against.</param>
    /// <returns>A list of ranges where the pattern was found in the content.</returns>
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
