namespace SunamoStringReplace;


/// <summary>
///     Must have always entered both from and to
///     None of event could have unlimited time!
/// </summary>
internal class FromToStringReplace : FromToTSHStringReplace<long>
{
    internal static FromToStringReplace Empty = new(true);
    internal FromToStringReplace()
    {
    }
    /// <summary>
    ///     Use Empty contstant outside of class
    /// </summary>
    /// <param name="empty"></param>
    private FromToStringReplace(bool empty)
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
    internal FromToStringReplace(long from, long to, FromToUseStringReplace ftUse = FromToUseStringReplace.DateTime)
    {
        this.from = from;
        this.to = to;
        this.ftUse = ftUse;
    }
}