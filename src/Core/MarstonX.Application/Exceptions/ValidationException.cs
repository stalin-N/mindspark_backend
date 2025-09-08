namespace MarstonX.Application.Exceptions;

public class ValidationException : Exception
{
    public List<ErrorModel> Errors { get; }

    public HttpStatusCode StatusCode { get; }

    public bool Status { get; }

    public string ExceptionMessage { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException()
        : base(Constant.CommonValidationError)
    {
        Errors = new List<ErrorModel>();
    }

    /// <summary>
    /// ValidationException class.
    /// </summary>
    /// <param name="message">message.</param>
    /// <param name="errors">errors.</param>
    /// <param name="statusCode">statusCode.</param>
    public ValidationException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
       : base(message)
    {
        Errors = new List<ErrorModel>
    {
        new ErrorModel() { ErrorMessage = message }
    };
        Status = false;
        StatusCode = statusCode;
    }

    /// <summary>
    /// ValidationException class.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="errors">Errors.</param>
    /// <param name="statusCode">StatusCode.</param>
    /// <param name="exceptionMessage">ExceptionMessage</param>
    public ValidationException(string message, List<ErrorModel>? errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string exceptionMessage = null!)
      : base(message)
    {
        if (errors != null && errors.Count > 0)
        {
            Errors = new List<ErrorModel>();
            foreach (var error in errors)
            {
                Errors.Add(new ErrorModel()
                {
                    PropertyName = error.PropertyName,
                    ErrorMessage = error.ErrorMessage,
                });
            }
        }

        StatusCode = statusCode;
        ExceptionMessage = exceptionMessage;
    }

    /// <summary>
    /// Adding the multiple Errors.
    /// </summary>
    /// <param name="failures">failures inputs.</param>
    public ValidationException(IEnumerable<ErrorModel> failures)
        : this()
    {
        foreach (var failure in failures)
        {
            Errors.Add(new ErrorModel()
            {
                PropertyName = failure.PropertyName,
                ErrorMessage = failure.ErrorMessage,
            });
        }
    }
}

/// <summary>
/// ErrorModel to add the PropertyName and ErrorMessage details.
/// </summary>
public class ErrorModel
{
    public string? PropertyName { get; set; }

    public string? ErrorMessage { get; set; }
}
