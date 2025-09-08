namespace MarstonX.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorHandlerMiddleware"/> class.
    /// </summary>
    /// <param name="next">next.</param>
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    ///  The Exception handler so that all types of unhandled exceptions can be caught in this handler.
    /// </summary>
    /// <param name="context">context.</param>
    /// <returns>It will return Status Code and Exception details.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = Constant.ContentType;
            var responseModel = new ResponseData<string>() { Message = Constant.ErrorMessage, ValidationErrors = null, ExceptionMessage = null };
            switch (error)
            {
                case SqlException:
                    Logger.Error($"{error}");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Message = Constant.InvalidDb;
                    responseModel!.ExceptionMessage = error.Message.ToString();
                    break;
                case OutOfMemoryException:
                    Logger.Error($"{error}");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Message = Constant.OutOfMemory;
                    responseModel!.ExceptionMessage = error.Message.ToString();
                    break;
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    responseModel.Message = Constant.UnAuthorizedAccess;
                    responseModel!.ExceptionMessage = error.Message.ToString();
                    break;
                case BrokenCircuitException:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Message = Constant.BrokenCircuitException;
                    responseModel!.ExceptionMessage = Constant.BrokenCircuitException;
                    break;
                case FluentValidation.ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    if (e.Errors != null)
                    {
                        responseModel.ValidationErrors = new List<ErrorModel>();
                        foreach (var item in e.Errors)
                        {
                            string? propertyName = item.PropertyName.Split('.').Last();
                            responseModel.ValidationErrors.Add(new ErrorModel { ErrorMessage = item.ErrorMessage, PropertyName = propertyName });
                        }
                    }

                    break;
                case ValidationException e:
                    response.StatusCode = (int)e.StatusCode;
                    responseModel.ExceptionMessage = e.ExceptionMessage;
                    if (e.Errors != null)
                    {
                        responseModel.ValidationErrors = new List<ErrorModel>();
                        foreach (var item in e.Errors)
                        {
                            string? propertyName = item?.PropertyName?.Split('.').Last();
                            responseModel.ValidationErrors.Add(new ErrorModel { ErrorMessage = item?.ErrorMessage, PropertyName = propertyName });
                        }
                    }

                    break;

                default:
                    // Unhandled error
                    Logger.Error($"{error}");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Message = Constant.ErrorMessage;
                    responseModel!.ExceptionMessage = error.Message.ToString();
                    break;
            }

            // write logs into Serilog and appinsight
            Log.Error(error, responseModel!.Message);
            await response.WriteAsJsonAsync(responseModel);
        }
    }
}
