namespace MarstonX.Api.Middlewares;

public static class ConfigureCustomRoles
{
    public static IServiceCollection AddAuthorizationCustomRoles(this IServiceCollection services)
    {

        services.AddAuthorization(options =>
        {
            options.AddPolicy("UserAccess", policy =>
                policy.Requirements.Add(new PermissionRequirement("user")));
        });

        services.AddTransient<IAuthorizationHandler, PermissionHandler>();

        return services;
    }
}
