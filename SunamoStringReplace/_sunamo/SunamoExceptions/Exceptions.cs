namespace SunamoStringReplace._sunamo.SunamoExceptions;

/// <summary>
/// Provides methods for generating exception messages with context information.
/// </summary>
internal sealed partial class Exceptions
{
    /// <summary>
    /// Formats the context prefix for exception messages.
    /// </summary>
    /// <param name="before">The context prefix text.</param>
    /// <returns>The formatted prefix or empty string if the prefix is null or whitespace.</returns>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Gets the type name, method name, and stack trace of the current exception location.
    /// </summary>
    /// <param name="isFillAlsoFirstTwo">Whether to also fill type and method name from the first non-ThrowEx frame.</param>
    /// <returns>A tuple containing type name, method name, and stack trace.</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        string typeName = string.Empty;
        string methodName = string.Empty;
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (isFillAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out typeName, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(typeName, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts the type name and method name from a stack trace line.
    /// </summary>
    /// <param name="line">The stack trace line to parse.</param>
    /// <param name="typeName">The extracted type name.</param>
    /// <param name="methodName">The extracted method name.</param>
    internal static void TypeAndMethodName(string line, out string typeName, out string methodName)
    {
        var afterAt = line.Split("at ")[1].Trim();
        var beforeParenthesis = afterAt.Split("(")[0];
        var parts = beforeParenthesis.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        typeName = string.Join(".", parts);
    }

    /// <summary>
    /// Gets the name of the calling method at the specified stack depth.
    /// </summary>
    /// <param name="depth">The stack frame depth to retrieve.</param>
    /// <returns>The calling method name.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    /// <summary>
    /// Generates an error message for when two values are the same.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    /// <param name="firstName">The name of the first value.</param>
    /// <param name="secondName">The name of the second value.</param>
    /// <returns>The formatted error message.</returns>
    internal static string? IsTheSame(string before, string firstName, string secondName)
    {
        return CheckBefore(before) + $"{firstName} and {secondName} has the same value";
    }

    /// <summary>
    /// Generates a custom error message with context.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    /// <param name="message">The custom error message.</param>
    /// <returns>The formatted error message.</returns>
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }

    /// <summary>
    /// Generates a "not implemented" error message.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    /// <returns>The formatted error message.</returns>
    internal static string? NotImplementedMethod(string before)
    {
        return CheckBefore(before) + "Not implemented method.";
    }

    /// <summary>
    /// Generates an error message for when two collections have different counts.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    /// <param name="firstCollectionName">The name of the first collection.</param>
    /// <param name="firstCollectionCount">The count of the first collection.</param>
    /// <param name="secondCollectionName">The name of the second collection.</param>
    /// <param name="secondCollectionCount">The count of the second collection.</param>
    /// <returns>The formatted error message, or null if counts are equal.</returns>
    internal static string? DifferentCountInLists(string before, string firstCollectionName, int firstCollectionCount, string secondCollectionName, int secondCollectionCount)
    {
        if (firstCollectionCount != secondCollectionCount)
            return CheckBefore(before) + " different count elements in collection" + " " +
            string.Concat(firstCollectionName + "-" + firstCollectionCount) + " vs. " +
            string.Concat(secondCollectionName + "-" + secondCollectionCount);
        return null;
    }
}
