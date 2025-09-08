namespace MarstonX.Infrastructure.Common.Logger;

public static class LoggingConfig
{
    /// <summary>
    /// Extension method to add SeriLog configuration to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the SeriLog configuration to.</param>
    /// <param name="builder">The WebApplicationBuilder used to retrieve configuration values.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddSeriLogConfig(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string? instrumentationKey = builder.Configuration.GetSection(Constant.ApplicationInsightsConnectionstring).Value;
        builder.Services.AddLogging();
        Log.Logger = new LoggerConfiguration()
     .MinimumLevel.Information()
     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
     .Enrich.FromLogContext()
     .Enrich.WithProperty("ApplicationName", Constant.LogName)
     .WriteTo.Console()
     .WriteTo.ApplicationInsights(
         new TelemetryConfiguration { ConnectionString = instrumentationKey },
         new TraceTelemetryConverter())
     .ReadFrom.Configuration(builder.Configuration)
     .CreateLogger();
        builder.Host.UseSerilog();
        return services;
    }
}
