namespace SunamoStringReplace._sunamo.SunamoEnums.Enums;

// Used in SunamoCollectionsGenericStore and SunamoCollections.
internal enum ContainsCompareMethod
{
    WholeInput,
    SplitToWords,

    // split to words and check for ! at [0]
    Negations
}
