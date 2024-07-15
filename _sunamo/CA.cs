using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoStringReplace._sunamo;
internal class CA
{
    internal static List<string> Trim(List<string> l)
    {
        for (var i = 0; i < l.Count; i++) l[i] = l[i].Trim();
        return l;
    }
}
