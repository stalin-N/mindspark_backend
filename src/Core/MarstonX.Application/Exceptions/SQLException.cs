namespace MarstonX.Application.Exceptions;

public class SQLException : ValidationException
{
    /// <summary>
    /// SQLException with error message.
    /// </summary>
    /// <param name="message">Error message.</param>
    public SQLException(string message)
        : base(message, null, HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    /// SQLException with exception message.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="errors">Error list.</param>
    /// <param name="exceptionMessage">exception message.</param>
    public SQLException(string message, List<ErrorModel>? errors, string exceptionMessage = null!)
       : base(message, errors, HttpStatusCode.InternalServerError, exceptionMessage)
    {
    }
}
