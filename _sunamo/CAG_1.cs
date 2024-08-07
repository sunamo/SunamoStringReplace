namespace SunamoStringReplace._sunamo;

internal class CAG
{
    internal static List<FromToStringReplace> EqualRanges<T>(List<T> contentOneSpace, List<T> r)
    {
        var result = new List<FromToStringReplace>();
        int? dx = null;
        var r_first = r[0];
        var startAt = 0;
        var valueToCompare = 0;
        for (var i = 0; i < contentOneSpace.Count; i++)
        {
            var _contentOneSpace = contentOneSpace[i];
            if (!dx.HasValue)
            {
                if (EqualityComparer<T>.Default.Equals(_contentOneSpace, r_first))
                {
                    dx = i + 1; // +2;
                    startAt = i;
                }
            }
            else
            {
                valueToCompare = dx.Value - startAt;
                if (r.Count > valueToCompare)
                {
                    if (EqualityComparer<T>.Default.Equals(_contentOneSpace, r[valueToCompare]))
                    {
                        dx++;
                    }
                    else
                    {
                        dx = null;
                        i--;
                    }
                }
                else
                {
                    var dx2 = (int)dx;
                    result.Add(new FromToStringReplace(dx2 - r.Count + 1, dx2, FromToUseStringReplace.None));
                    dx = null;
                }
            }
        }

        foreach (var item in result)
        {
            item.from--;
            item.to--;
        }

        return result;
    }
}