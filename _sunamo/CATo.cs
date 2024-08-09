using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoStringReplace._sunamo;
internal class CATo
{
    public static List<T> ToList<T>(params T[] t)
    {
        return t.ToList();
    }

    public static T[] ToArray<T>(params T[] t)
    {
        return t;
    }

    public static List<string> ToListString(params object[] t)
    {
        return t.ToList().ConvertAll(d => d.ToString());
    }
}
