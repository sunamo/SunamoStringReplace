namespace SunamoStringReplace._sunamo.SunamoExceptions;

/// <summary>
/// Provides methods for throwing exceptions with context information.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws a custom exception with the specified message.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception.</param>
    /// <param name="secondMessage">An additional message to append.</param>
    /// <returns>True if the exception was generated, false otherwise.</returns>
    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exceptionText = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exceptionText, isReallyThrowing);
    }

    /// <summary>
    /// Throws an exception if two collections have different counts.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collections.</typeparam>
    /// <param name="firstCollectionName">The name of the first collection.</param>
    /// <param name="firstCollection">The first collection.</param>
    /// <param name="secondCollectionName">The name of the second collection.</param>
    /// <param name="secondCollection">The second collection.</param>
    /// <returns>True if the counts differ and exception was thrown.</returns>
    internal static bool DifferentCountInLists<T>(string firstCollectionName, IList<T> firstCollection, string secondCollectionName, IList<T> secondCollection)
    {
        return ThrowIsNotNull(
            Exceptions.DifferentCountInLists(FullNameOfExecutedCode(), firstCollectionName, firstCollection.Count, secondCollectionName, secondCollection.Count));
    }

    /// <summary>
    /// Throws an exception indicating two values are the same.
    /// </summary>
    /// <param name="firstName">The name of the first value.</param>
    /// <param name="secondName">The name of the second value.</param>
    /// <returns>True if the exception was generated.</returns>
    internal static bool IsTheSame(string firstName, string secondName)
    {
        return ThrowIsNotNull(Exceptions.IsTheSame(FullNameOfExecutedCode(), firstName, secondName));
    }

    /// <summary>
    /// Throws a "not implemented" exception for the current method.
    /// </summary>
    /// <returns>True if the exception was generated.</returns>
    internal static bool NotImplementedMethod()
    {
        return ThrowIsNotNull(Exceptions.NotImplementedMethod);
    }

    /// <summary>
    /// Gets the full name of the currently executed code (type and method).
    /// </summary>
    /// <returns>The full name of the executed code.</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Constructs the full name of executed code from type and method name.
    /// </summary>
    /// <param name="type">The type object (Type, MethodBase, or string).</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="isFromThrowEx">Whether the call originated from ThrowEx.</param>
    /// <returns>The full name in format "TypeName.MethodName".</returns>
    private static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type actualType)
        {
            typeFullName = actualType.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type resolvedType = type.GetType();
            typeFullName = resolvedType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the provided exception text is not null.
    /// </summary>
    /// <param name="exception">The exception text to check.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception.</param>
    /// <returns>True if the exception text was not null.</returns>
    internal static bool ThrowIsNotNull(string? exception, bool isReallyThrowing = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Invokes a function with the full name of executed code and throws if the result is not null.
    /// </summary>
    /// <param name="exceptionFactory">A function that generates the exception text given the execution context.</param>
    /// <returns>True if the exception was generated.</returns>
    internal static bool ThrowIsNotNull(Func<string, string?> exceptionFactory)
    {
        string? exceptionText = exceptionFactory(FullNameOfExecutedCode());
        return ThrowIsNotNull(exceptionText);
    }
}
