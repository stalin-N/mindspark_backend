namespace MarstonX.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DapperContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<MongoContext>();
        services.AddScoped<IUsermongoRepository, UserMongoRepository>();
        return services;
    }
}