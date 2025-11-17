// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoStringReplace._sunamo.SunamoData.Data;

internal class FromToTSHStringReplace<T>
{
    internal bool empty;
    protected long fromL;
    internal FromToUseStringReplace ftUse = FromToUseStringReplace.DateTime;
    protected long toL;

    internal FromToTSHStringReplace()
    {
        var genericType = typeof(T);
        if (genericType == typeof(int)) ftUse = FromToUseStringReplace.None;
    }

    /// <summary>
    ///     Use Empty contstant outside of class
    /// </summary>
    /// <param name="empty"></param>
    private FromToTSHStringReplace(bool empty) : this()
    {
        this.empty = empty;
    }

    /// <summary>
    ///     A3 true = DateTime
    ///     A3 False = None
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="ftUse"></param>
    internal FromToTSHStringReplace(T from, T to, FromToUseStringReplace ftUse = FromToUseStringReplace.DateTime) :
        this()
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }

    internal T from
    {
        get => (T)(dynamic)fromL;
        set => fromL = (long)(dynamic)value;
    }

    internal T to
    {
        get => (T)(dynamic)toL;
        set => toL = (long)(dynamic)value;
    }

    internal long FromL => fromL;
    internal long ToL => toL;
}