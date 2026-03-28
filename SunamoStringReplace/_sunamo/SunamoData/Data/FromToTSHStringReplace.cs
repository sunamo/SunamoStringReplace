namespace SunamoStringReplace._sunamo.SunamoData.Data;

/// <summary>
/// Base class representing a from-to range with generic type support.
/// </summary>
/// <typeparam name="T">The type of the range values.</typeparam>
internal class FromToTSHStringReplace<T>
{
    /// <summary>
    /// Whether this instance represents an empty range.
    /// </summary>
    internal bool IsEmpty { get; set; }

    /// <summary>
    /// The backing field for the From value stored as long.
    /// </summary>
    protected long fromLong;

    /// <summary>
    /// The intended use of this from-to range.
    /// </summary>
    internal FromToUseStringReplace FromToUse { get; set; } = FromToUseStringReplace.DateTime;

    /// <summary>
    /// The backing field for the To value stored as long.
    /// </summary>
    protected long toLong;

    /// <summary>
    /// Initializes a new empty instance and sets the default FromToUse based on the generic type.
    /// </summary>
    internal FromToTSHStringReplace()
    {
        var genericType = typeof(T);
        if (genericType == typeof(int)) FromToUse = FromToUseStringReplace.None;
    }

    /// <summary>
    /// Initializes a new empty instance. Use the Empty constant outside of class.
    /// </summary>
    /// <param name="isEmpty">Marks this instance as empty.</param>
    private FromToTSHStringReplace(bool isEmpty) : this()
    {
        this.IsEmpty = isEmpty;
    }

    /// <summary>
    /// Initializes a new instance with from and to values.
    /// </summary>
    /// <param name="from">The start value of the range.</param>
    /// <param name="to">The end value of the range.</param>
    /// <param name="fromToUse">The intended use type (default: DateTime).</param>
    internal FromToTSHStringReplace(T from, T to, FromToUseStringReplace fromToUse = FromToUseStringReplace.DateTime) :
        this()
    {
        this.From = from;
        this.To = to;
        this.FromToUse = fromToUse;
    }

    /// <summary>
    /// The start value of the range.
    /// </summary>
    internal T From
    {
        get => (T)(dynamic)fromLong!;
        set => fromLong = (long)(dynamic)value!;
    }

    /// <summary>
    /// The end value of the range.
    /// </summary>
    internal T To
    {
        get => (T)(dynamic)toLong!;
        set => toLong = (long)(dynamic)value!;
    }

    /// <summary>
    /// The start value as a long.
    /// </summary>
    internal long FromL => fromLong;

    /// <summary>
    /// The end value as a long.
    /// </summary>
    internal long ToL => toLong;
}
