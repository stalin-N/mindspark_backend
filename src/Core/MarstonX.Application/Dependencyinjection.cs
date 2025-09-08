namespace MarstonX.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Adds application-specific services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient(typeof(IResponseData<>), typeof(ResponseData<>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
