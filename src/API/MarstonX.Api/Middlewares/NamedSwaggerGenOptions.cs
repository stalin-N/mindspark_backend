namespace MarstonX.Api.Middlewares;

public class NamedSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedSwaggerGenOptions"/> class.
    /// </summary>
    /// <param name="provider">The API version description provider.</param>
    public NamedSwaggerGenOptions(IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }

    /// <summary>
    /// Configures the SwaggerGenOptions with the specified name.
    /// </summary>
    /// <param name="name">The name of the configuration.</param>
    /// <param name="options">The SwaggerGenOptions to configure.</param>
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }


    /// <summary>
    /// Configures the SwaggerGenOptions by adding swagger documents for every API version discovered.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions instance to configure.</param>
    public void Configure(SwaggerGenOptions options)
    {

        // Add swagger document for every API version discovered
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
        }
    }

    /// <summary>
    /// Represents the information about an OpenAPI document.
    /// </summary>
    private OpenApiInfo CreateVersionInfo(
            ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = $"Magnus-{description.GroupName}",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
