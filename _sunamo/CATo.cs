namespace SunamoStringReplace._sunamo;
using System.Collections.Generic;
using System.Linq;

internal class CATo
{


    internal static List<string> ToListString(params object[] t)
    {
        return t.ToList().ConvertAll(d => d.ToString());
    }
}