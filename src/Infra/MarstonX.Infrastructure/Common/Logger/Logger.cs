namespace MarstonX.Infrastructure.Common.Logger;

public static class Logger
{
    /// <summary>
    /// Logs an information message with the class name, method name, and message.
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="message">The information message.</param>
    public static void Information<TClass>(string methodName, string message)
    {
        string controllerName = typeof(TClass).Name;
        Log.Information($"Class: {controllerName}, Method: {methodName}, Message: {message}");
    }

    /// <summary>
    /// Logs an information message.
    /// </summary>
    /// <param name="message">The information message.</param>
    public static void Information(string message)
    {
        Log.Information(message);
    }

    /// <summary>
    /// Logs an error message with an optional exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="exception">The optional exception.</param>
    public static void Error(string message, Exception? exception = null)
    {
        Log.Error(exception, message);
    }

    /// <summary>
    /// Logs a warning message with the controller name, method name, and message.
    /// </summary>
    /// <param name="controllerName">The name of the controller.</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="message">The warning message.</param>
    public static void Warning(string controllerName, string methodName, string message)
    {
        Log.Warning($"Controller: {controllerName}, Method: {methodName}, Message: {message}");
    }
}
