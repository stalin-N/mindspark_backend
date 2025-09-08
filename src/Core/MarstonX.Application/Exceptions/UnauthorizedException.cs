namespace MarstonX.Application.Exceptions;

public class UnauthorizedException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public UnauthorizedException(string message)
       : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}
