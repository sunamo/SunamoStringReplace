
using SunamoEnums.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SunamoStringReplace._sunamo
{
    internal class CAGSH
    {
        internal static List<FromTo> EqualRanges<T>(List<T> contentOneSpace, List<T> r)
        {
            List<FromTo> result = new List<FromTo>();
            int? dx = null;
            var r_first = r[0];
            int startAt = 0;
            int valueToCompare = 0;
            for (int i = 0; i < contentOneSpace.Count; i++)
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
                        int dx2 = (int)dx;
                        result.Add(new FromTo(dx2 - r.Count + 1, dx2, FromToUse.None));
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
}
