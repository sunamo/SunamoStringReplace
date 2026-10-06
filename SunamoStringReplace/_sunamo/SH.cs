namespace SunamoStringReplace._sunamo;

internal class SH
{
    internal static List<int> ReturnOccurencesOfString(string text, string what)
    {
        var results = new List<int>();
        for (var index = 0; index < text.Length - what.Length + 1; index++)
        {
            var substring = text.Substring(index, what.Length);
            if (substring == what)
                results.Add(index);
        }

        return results;
    }

    internal static List<Tuple<int, int>> GetPairsStartAndEnd(List<int> leftOccurrences, List<int> rightOccurrences, ref List<int> unmatchedLeft,
        ref List<int> unmatchedRight)
    {
        var pairs = new List<Tuple<int, int>>();
        unmatchedLeft = leftOccurrences.ToList();
        unmatchedRight = rightOccurrences.ToList();
        for (var i = rightOccurrences.Count - 1; i >= 0; i--)
        {
            var lastRight = rightOccurrences[i];
            if (leftOccurrences.Count == 0) break;
            var lastLeft = leftOccurrences.Last();
            if (lastRight < lastLeft)
            {
                i++;
                leftOccurrences.RemoveAt(leftOccurrences.Count - 1);
            }
            else
            {
                pairs.Add(new Tuple<int, int>(lastLeft, lastRight));
            }
        }

        leftOccurrences = unmatchedLeft;
        var duplicateLeftIndexes = new List<int>();
        var duplicatePairs = new List<Tuple<int, int>>();
        var processedLeftValues = new List<int>();
        for (var i = pairs.Count - 1; i >= 0; i--)
        {
            if (processedLeftValues.Contains(pairs[i].Item1))
            {
                duplicateLeftIndexes.Add(pairs[i].Item1);
                duplicatePairs.Add(pairs[i]);
                pairs.RemoveAt(i);
            }

            processedLeftValues.Add(pairs[i].Item1);
        }

        duplicateLeftIndexes = duplicateLeftIndexes.Distinct().ToList();
        foreach (var duplicateIndex in duplicateLeftIndexes)
        {
            var matchCount = processedLeftValues.Where(processed => processed == duplicateIndex).Count();
            if (matchCount > 2)
            {
                var selectedPairs = duplicatePairs.Where(pair => pair.Item1 == duplicateIndex).ToList();
                var leftIndex = leftOccurrences.IndexOf(selectedPairs[0].Item1);
                if (leftIndex != -1)
                {
                    pairs.Add(new Tuple<int, int>(leftOccurrences[leftIndex - 1], selectedPairs[0].Item2));
                }
            }
        }

        leftOccurrences.Sort();
        var result = pairs;
        var processedValues = new List<int>();
        var currentIndex = -1;
        for (var pairIndex = 0; pairIndex < result.Count; pairIndex++)
        {
            var pair = result[pairIndex];
            var leftValue = pair.Item1;
            if (processedValues.Contains(leftValue))
            {
                currentIndex = leftOccurrences.IndexOf(leftValue);
                if (currentIndex != -1)
                {
                    leftValue = leftOccurrences[currentIndex - 1];
                    result[leftValue] = new Tuple<int, int>(leftValue, result[pairIndex - 1].Item2);
                }
            }

            processedValues.Add(leftValue);
        }

        unmatchedLeft = leftOccurrences;
        unmatchedLeft = unmatchedLeft.Distinct().ToList();
        unmatchedRight = unmatchedRight.Distinct().ToList();
        foreach (var pair in result)
        {
            unmatchedLeft.Remove(pair.Item1);
            unmatchedRight.Remove(pair.Item2);
        }

        result.Reverse();
        return result;
    }

    internal static string RemoveEndingPairCharsWhenDontHaveStarting(string text, string openChar, string closeChar)
    {
        var resultBuilder = new StringBuilder(text);
        var leftOccurrences = ReturnOccurencesOfString(text, openChar);
        var rightOccurrences = ReturnOccurencesOfString(text, closeChar);
        List<int> unmatchedLeft = null!;
        List<int> unmatchedRight = null!;
        var matchedPairs = GetPairsStartAndEnd(leftOccurrences, rightOccurrences, ref unmatchedLeft, ref unmatchedRight);
        unmatchedLeft.AddRange(unmatchedRight);
        unmatchedLeft.Sort();
        for (var i = unmatchedLeft.Count - 1; i >= 0; i--) resultBuilder.Remove(unmatchedLeft[i], 1);
        return resultBuilder.ToString();
    }
}
