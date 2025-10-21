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
        var type = typeof(type);
        if (type == typeof(int)) ftUse = FromToUseStringReplace.None;
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
    internal FromToTSHStringReplace(type from, type to, FromToUseStringReplace ftUse = FromToUseStringReplace.DateTime) :
        this()
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }

    internal type from
    {
        get => (type)(dynamic)fromL;
        set => fromL = (long)(dynamic)value;
    }

    internal type to
    {
        get => (type)(dynamic)toL;
        set => toL = (long)(dynamic)value;
    }

    internal long FromL => fromL;
    internal long ToL => toL;
}