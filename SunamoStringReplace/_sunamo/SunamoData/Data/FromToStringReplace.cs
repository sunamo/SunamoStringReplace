namespace SunamoStringReplace._sunamo.SunamoData.Data;

internal class FromToStringReplace : FromToTSHStringReplace<long>
{
    internal static FromToStringReplace Empty = new(true);

    internal FromToStringReplace()
    {
    }

    private FromToStringReplace(bool isEmpty)
    {
        this.IsEmpty = isEmpty;
    }

    internal FromToStringReplace(long from, long to, FromToUseStringReplace fromToUse = FromToUseStringReplace.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }
}
