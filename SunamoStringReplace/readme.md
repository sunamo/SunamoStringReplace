# SunamoStringReplace

A .NET library providing comprehensive string replacement methods including single/multiple replacements, case-insensitive replacements, whitespace handling, regex-based replacements, and index-based replacements.

## Features

- **ReplaceAll / ReplaceAll2** - Replace one or more search strings with a single replacement
- **ReplaceOnce** - Replace only the first occurrence of a search string
- **ReplaceFromEnd** - Replace occurrences starting from the end of the text
- **ReplaceByIndex** - Replace at a specific character index
- **ReplaceAllCaseInsensitive** - Case-insensitive replacement
- **ReplaceWhiteSpaces** - Replace or remove whitespace characters
- **ReplaceAllDoubleSpaceToSingle** - Normalize multiple spaces to single spaces
- **ReplaceMany** - Bulk replacement using a mapping string with arrow notation (`from->to`)
- **ReplaceTypedWhitespacesForNormal** - Convert escape sequences (`\n`, `\t`, `\r`) to actual characters
- **ReplaceInLine** - Replace within a specific line of multi-line text

## Installation

```
dotnet add package SunamoStringReplace
```

## Target Frameworks

`net10.0;net9.0;net8.0`

## Links

- [NuGet](https://www.nuget.org/profiles/sunamo)
- [GitHub](https://github.com/sunamo/PlatformIndependentNuGetPackages)
- [Developer site](https://sunamo.cz)

Feature requests / bug reports: [Email](mailto:radek.jancik@sunamo.cz) or on GitHub
