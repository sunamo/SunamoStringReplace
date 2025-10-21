// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoStringReplace._sunamo.SunamoEnums.Enums;

/// <summary>
///     Used in SunamoCollectionsGenericStore + SunamoCollections
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