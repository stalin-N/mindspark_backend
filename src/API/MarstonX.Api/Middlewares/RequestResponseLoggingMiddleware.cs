namespace MarstonX.Api.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TelemetryClient _telemetryClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestResponseLoggingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the HTTP request pipeline.</param>
    /// <param name="telemetryClient">The Application Insights telemetry client.</param>
    public RequestResponseLoggingMiddleware(RequestDelegate next, TelemetryClient telemetryClient)
    {
        _next = next;
        _telemetryClient = telemetryClient;
    }

    /// <summary>
    /// Invokes the middleware logic to log the HTTP request and response.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        string requestBody = await ReadRequestBody(context.Request);
        LogTrace($"Incoming Request: {context.Request.Method} {context.Request.Path} {requestBody}");
        Stream originalBodyStream = context.Response.Body;
        using (Stream responseBodyStream = new MemoryStream())
        using (Stream responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;
            await _next(context);
            string responseBodyContent = await ReadResponseBody(context.Response);
            LogTrace($"Outgoing Response: {context.Response.StatusCode} {responseBodyContent}");
            await responseBody.CopyToAsync(originalBodyStream);

        }
    }

    /// <summary>
    /// Write the message in ApplicationInsights telemetry.
    /// </summary>
    /// <param name="message">The HTTP request.</param>
    private void LogTrace(string message)
    {
        var telemetry = new TraceTelemetry(message, SeverityLevel.Information);
        telemetry.Properties["ApplicationName"] = Constant.LogName;
        _telemetryClient.TrackTrace(telemetry);
    }
    /// <summary>
    /// Reads the body of the incoming HTTP request as a string.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <returns>The request body as a string.</returns>
    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();
        using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
        {
            string body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }
    }

    /// <summary>
    /// Reads the body of the HTTP response as a string.
    /// </summary>
    /// <param name="response">The HTTP response.</param>
    /// <returns>The response body as a string.</returns>
    private async Task<string> ReadResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return text;
    }
}
