namespace MarstonX.Application.Exceptions;

public class NoContentException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoContentException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public NoContentException(string message)
       : base(message, null, HttpStatusCode.NoContent)
    {
    }
}
