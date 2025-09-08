namespace MarstonX.Application.Wrappers;

public class ResponseData<T> : IResponseData<T>
  where T : class
{
    /// <summary>
    /// Construct the response type.
    /// </summary>
    /// <param name="message">Response Message.</param>
    /// <param name="errors">Validation Errors.</param>
    /// <param name="exceptionMessage">Exception Messages.</param>
    public ResponseData(string message = "", List<ErrorModel>? errors = null, string? exceptionMessage = null)
    {
        Message = message;
        ValidationErrors = errors;
        ExceptionMessage = exceptionMessage;
    }

    public string? Message { get; set; }

    public string? ExceptionMessage { get; set; }

    public List<ErrorModel>? ValidationErrors { get; set; }

    /// <summary>
    /// Handle Success Response.
    /// </summary>
    /// <param name="successModel">Input Data string value.</param>
    /// <returns>It will return the success response.</returns>
    public T? Deserialize(string? successModel)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<T>(successModel!, options);
    }

    /// <summary>
    /// Handle Success list Response.
    /// </summary>
    /// <param name="successModel">Input Data string value.</param>
    /// <returns>It will return the success response.</returns>
    public List<T>? DeserializeListJson(string? successModel)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<List<T>>(successModel!, options);
    }

    /// <summary>
    /// Handle Update and Delete Response.
    /// </summary>
    /// <param name="message">Success Message.</param>
    /// <returns>Return the Updated successfully message.</returns>
    public ResponseMessage UpdateAndDeleteResponse(string message)
    {
        return new ResponseMessage { Message = message };
    }

    /// <summary>
    /// To fetch list of validation errors.
    /// </summary>
    /// <param name="errorMessage">list of validation errors.</param>
    /// <returns>Return the list of validation errors.</returns>
    public List<ErrorModel>? AddError(string errorMessage)
    {
        string errorsProperty = JsonDocument.Parse(errorMessage!).RootElement.GetProperty("Errors").ToString();
        return System.Text.Json.JsonSerializer.Deserialize<List<ErrorModel>>(errorsProperty);
    }

    /// <summary>
    /// Adding a errors.
    /// </summary>
    /// <param name="message">Error Message.</param>
    /// <param name="propertyName">Proerty Name.</param>
    /// <returns>It will return error list.</returns>
    public List<ErrorModel> AddError(string message, string propertyName)
    {
        return new List<ErrorModel> { new ErrorModel { PropertyName = propertyName, ErrorMessage = message } };
    }

    /// <summary>
    /// Return response for all common handlers.
    /// </summary>
    /// <param name="response">Input values.</param>
    /// <returns>return response.</returns>
    /// <exception cref="BadRequestException">bad request exception.</exception>
    /// <exception cref="SQLException">sql exception.</exception>
    public ResponseMessage ActionUpdateResponse(DBResponse response)
    {
        return (int?)response.Id switch
        {
            (int?)ExceptionCode.DBValidationError => throw new BadRequestException(Constant.ErrorMessage, AddError(response.Errors!)),
            (int?)ExceptionCode.DBException => throw new SQLException(Constant.ErrorMessage, null!, response.Errors!),
            _ => UpdateAndDeleteResponse(Constant.UpdateSuccessMessage)
        };
    }

    /// <summary>
    /// Action Delete Response.
    /// </summary>
    /// <param name="response">DB response data.</param>
    /// <returns>Delete response message.</returns>
    /// <exception cref="BadRequestException">If any validation error, It will throw BadRequestException.</exception>
    /// <exception cref="SQLException">If any SQL error, It will throw SQLException.</exception>
    public ResponseMessage ActionDeleteResponse(DBResponse response)
    {
        return (int?)response.Id switch
        {
            (int?)ExceptionCode.DBValidationError => throw new BadRequestException(Constant.ErrorMessage, AddError(response.Errors!)),
            (int?)ExceptionCode.DBException => throw new SQLException(Constant.ErrorMessage, null!, response.Errors!),
            _ => UpdateAndDeleteResponse(Constant.DeleteSuccessMessage)
        };
    }

    /// <summary>
    /// Return response for all common handlers.
    /// </summary>
    /// <param name="response">Input values.</param>
    /// <returns>return response.</returns>
    /// <exception cref="BadRequestException">bad request exception.</exception>
    /// <exception cref="SQLException">sql exception.</exception>
    public T? ActionResponse(DBResponse response)
    {
        return response.Id switch
        {
            (long?)ExceptionCode.NoContent => null,
            (long?)ExceptionCode.DBValidationError => throw new BadRequestException(Constant.ErrorMessage, AddError(response!.Errors!)),
            (long?)ExceptionCode.DBException => throw new SQLException(Constant.ErrorMessage, null!, response.Data!),
            _ => Deserialize(response.Data)
        };
    }
}
