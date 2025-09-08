namespace MarstonX.Api.Middlewares;

public static class ConfigureApiVersioning
{
    /// <summary>
    /// Add configuration for API versioning.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the configuration to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                 //new QueryStringApiVersionReader("api-version"),
                 //new HeaderApiVersionReader("X-Version"),
                 new MediaTypeApiVersionReader("x-version"));
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen();
        services.ConfigureOptions<NamedSwaggerGenOptions>();
        return services;
    }
}
