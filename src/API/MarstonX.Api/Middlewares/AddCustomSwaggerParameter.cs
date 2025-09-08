namespace MarstonX.Api.Middlewares;

public static class ConfigureSwagger
{
    /// <summary>
    /// Adds a custom Swagger parameter to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the custom Swagger parameter to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCustomSwaggerParameter(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            // opt.OperationFilter<AddCustomHeaderParameter>(); // register the filter
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = string.Empty,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
        });

        return services;
    }
}
