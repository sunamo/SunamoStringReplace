namespace SunamoStringReplace._sunamo.SunamoData.Data;

/// <summary>
/// Represents a from-to range with long values. Must have always entered both from and to.
/// None of event could have unlimited time.
/// </summary>
internal class FromToStringReplace : FromToTSHStringReplace<long>
{
    /// <summary>
    /// An empty instance of FromToStringReplace.
    /// </summary>
    internal static FromToStringReplace Empty = new(true);

    /// <summary>
    /// Initializes a new default instance.
    /// </summary>
    internal FromToStringReplace()
    {
    }

    /// <summary>
    /// Initializes an empty instance. Use <see cref="Empty"/> constant outside of class.
    /// </summary>
    /// <param name="isEmpty">Marks this instance as empty.</param>
    private FromToStringReplace(bool isEmpty)
    {
        this.IsEmpty = isEmpty;
    }

    /// <summary>
    /// Initializes a new instance with from and to values.
    /// </summary>
    /// <param name="from">The start value of the range.</param>
    /// <param name="to">The end value of the range.</param>
    /// <param name="fromToUse">The intended use type (default: DateTime).</param>
    internal FromToStringReplace(long from, long to, FromToUseStringReplace fromToUse = FromToUseStringReplace.DateTime)
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }
}
