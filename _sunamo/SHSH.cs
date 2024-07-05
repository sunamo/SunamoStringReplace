
namespace SunamoStringReplace._sunamo;
using SunamoStringReplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class SHSH
{
    internal static List<Tuple<int, int>> GetPairsStartAndEnd(List<int> occL, List<int> occR, ref List<int> onlyLeft, ref List<int> onlyRight)
    {
        var l = new List<Tuple<int, int>>();
        onlyLeft = occL.ToList();
        onlyRight = occR.ToList();
        for (int i = occR.Count - 1; i >= 0; i--)
        {
            int lastRight = occR[i];
            if (occL.Count == 0)
            {
                break;
            }
            var lastLeft = occL.Last();
            if (lastRight < lastLeft)
            {
                i++;
                // Na konci přebývá lastLeft
                // onlyLeft.Add(lastLeft);
                // I will remove it on end
                occL.RemoveAt(occL.Count - 1);
            }
            else
            {
                // když je lastLeft menší, znamená to že last right má svůj levý protějšek
                l.Add(new Tuple<int, int>(lastLeft, lastRight));
            }
        }
        occL = onlyLeft;
        //foreach (var item in l)
        //{
        //    occL.Remove(item.Item1);
        //}
        // occL = onlyLeft o pár řádků výše
        //onlyLeft.AddRange(occL);
        //l.Reverse();
        var addToAnotherCollection = new List<int>();
        var l2 = new List<Tuple<int, int>>();
        List<int> alreadyProcessedItem1 = new List<int>();
        for (int i = l.Count - 1; i >= 0; i--)
        {
            if (alreadyProcessedItem1.Contains(l[i].Item1))
            {
                addToAnotherCollection.Add(l[i].Item1);
                l2.Add(l[i]);
                l.RemoveAt(i);
                //continue;
            }
            alreadyProcessedItem1.Add(l[i].Item1);
        }
        //for (int i = l2.Count - 1; i >= 0; i--)
        //{
        //    if (l.Contains(l2[i]))
        //    {
        //        l2.RemoveAt(i);
        //    }
        //}
        addToAnotherCollection = addToAnotherCollection.Distinct().ToList();
        foreach (var item in addToAnotherCollection)
        {
            var count = alreadyProcessedItem1.Where(d => d == item).Count();
            //!alreadyProcessedItem1.Contains(item)
            if (count > 2)
            {
                var sele = l2.Where(d => d.Item1 == item).ToList();
                //for (int i = sele.Count() - 1; i >= 1; i--)
                //{
                //    l2.Remove(sele[i]);
                //}
                var dx2 = occL.IndexOf(sele[0].Item1);
                if (dx2 != -1)
                {
                    var dx3 = l.IndexOf(sele[0]);
                    l.Add(new Tuple<int, int>(occL[dx2 - 1], sele[0].Item2));
                }
            }
        }
        //l.AddRange(l2);
        occL.Sort();
        var result = l; //l.OrderByDescending(d => d.Item1).ToList();
                        //
        List<int> alreadyProcessed = new List<int>();
        int dx = -1;
        for (int y = 0; y < result.Count; y++)
        {
            var item = result[y];
            var i = item.Item1;
            if (alreadyProcessed.Contains(i))
            {
                dx = occL.IndexOf(i);
                if (dx != -1)
                {
                    i = occL[dx - 1];
                    result[i] = new Tuple<int, int>(i, result[y - 1].Item2);
                }
            }
            alreadyProcessed.Add(i);
        }
        onlyLeft = occL;
        onlyLeft = onlyLeft.Distinct().ToList();
        onlyRight = onlyRight.Distinct().ToList();
        foreach (var item in result)
        {
            onlyLeft.Remove(item.Item1);
            onlyRight.Remove(item.Item2);
        }
        result.Reverse();
        return result;
    }

    internal static List<int> ReturnOccurencesOfString(string vcem, string co)
    {

        List<int> Results = new List<int>();
        for (int Index = 0; Index < (vcem.Length - co.Length) + 1; Index++)
        {
            var subs = vcem.Substring(Index, co.Length);
            ////////DebugLogger.Instance.WriteLine(subs);
            // non-breaking space. &nbsp; code 160
            // 32 space
            char ch = subs[0];
            char ch2 = co[0];
            if (subs == AllStrings.space)
            {
            }
            if (subs == co)
                Results.Add(Index);
        }
        return Results;
    }

    internal static string RemoveEndingPairCharsWhenDontHaveStarting(string vr, string cbl, string cbr)
    {
        List<int> removeOnIndexes = new List<int>();
        var sb = new StringBuilder(vr);
        var occL = ReturnOccurencesOfString(vr, cbl);
        var occR = ReturnOccurencesOfString(vr, cbr);
        List<int> onlyLeft = null;
        List<int> onlyRight = null;
        var l = SHSH.GetPairsStartAndEnd(occL, occR, ref onlyLeft, ref onlyRight);
        onlyLeft.AddRange(onlyRight);
        onlyLeft.Sort();
        for (int i = onlyLeft.Count - 1; i >= 0; i--)
        {
            sb.Remove(onlyLeft[i], 1);
        }
        //if (occL.Count == 0)
        //{
        //    result = vr.SHReplace.Replace(AllStrings.rcub, string.Empty);
        //}
        //else
        //{
        //
        //    int left = -1;
        //    int right = -1;
        //    var onlyLeft = new List<int>();
        //    var pairs = SH.GetPairsStartAndEnd(occL, occR, ref onlyLeft);
        //    while (true)
        //    {
        //        if (occR.Count == 0)
        //        {
        //            break;
        //        }
        //        if (occL.Count == 0)
        //        {
        //            break;
        //        }
        //        left = occL.First();
        //        right = occR.First();
        //        if (right > left)
        //        {
        //            removeOnIndexes.Add(right);
        //            occR.RemoveAt(0);
        //        }
        //        else
        //        {
        //            // right, remove from right
        //            occR.RemoveAt(0);
        //        }
        //    }
        //    StringBuilder sb = new StringBuilder(vr);
        //    for (int i = removeOnIndexes.Count - 1; i >= 0; i--)
        //    {
        //        vr.Remove(removeOnIndexes[i], 1);
        //    }
        //    result = vr.ToLower();
        //}
        return sb.ToString();
    }
}

