namespace SunamoStringReplace._sunamo.SunamoEnums.Enums;

/// <summary>
/// Specifies the comparison method used for contains operations.
/// Used in SunamoCollectionsGenericStore and SunamoCollections.
/// </summary>
internal enum ContainsCompareMethod
{
    WholeInput,
    SplitToWords,

    /// <summary>
    ///     split to words and check for ! at [0]
    /// </summary>
    Negations
}