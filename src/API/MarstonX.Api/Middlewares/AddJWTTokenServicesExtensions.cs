namespace MarstonX.Api.Middlewares;

public static class AddJWTTokenServicesExtensions
{
    public static void AddJWTTokenServices(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => {
            options.SaveToken = true;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["JWTSecretKey"])),
                ValidateIssuer = true,
                ValidIssuer = Configuration["JWTIssuer"],
                ValidateAudience = true,
                ValidAudience = Configuration["JWTIssuer"],
                RequireExpirationTime = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.FromMinutes(0),
            };
        });
    }
}
