namespace SunamoStringReplace._public.SunamoEnums.Enums;

/// <summary>
/// Specifies the format type for from-to range values.
/// </summary>
public enum FromToUseStringReplace
{
    /// <summary>
    /// DateTime format.
    /// </summary>
    DateTime,

    /// <summary>
    /// Unix timestamp format.
    /// </summary>
    Unix,

    /// <summary>
    /// Unix timestamp format for time only.
    /// </summary>
    UnixJustTime,

    /// <summary>
    /// No specific format.
    /// </summary>
    None
}