namespace MarstonX.Application.Exceptions;

public class NotFoundException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public NotFoundException(string message)
        : base(message, null, HttpStatusCode.NotFound)
    {
    }
}
