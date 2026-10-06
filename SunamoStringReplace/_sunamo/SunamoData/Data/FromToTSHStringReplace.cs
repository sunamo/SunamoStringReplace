namespace SunamoStringReplace._sunamo.SunamoData.Data;

internal class FromToTSHStringReplace<T>
{
    internal bool IsEmpty { get; set; }

    protected long fromLong;

    internal FromToUseStringReplace FromToUse { get; set; } = FromToUseStringReplace.DateTime;

    protected long toLong;

    internal FromToTSHStringReplace()
    {
        var genericType = typeof(T);
        if (genericType == typeof(int)) FromToUse = FromToUseStringReplace.None;
    }

    private FromToTSHStringReplace(bool isEmpty) : this()
    {
        this.IsEmpty = isEmpty;
    }

    internal FromToTSHStringReplace(T from, T to, FromToUseStringReplace fromToUse = FromToUseStringReplace.DateTime) :
        this()
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }

    internal T From
    {
        get => (T)(dynamic)fromLong!;
        set => fromLong = (long)(dynamic)value!;
    }

    internal T To
    {
        get => (T)(dynamic)toLong!;
        set => toLong = (long)(dynamic)value!;
    }

    internal long FromL => fromLong;

    internal long ToL => toLong;
}
