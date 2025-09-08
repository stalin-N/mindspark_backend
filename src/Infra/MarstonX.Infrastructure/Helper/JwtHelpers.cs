namespace MarstonX.Infrastructure.Helper;

public static class JwtHelpers
{
    /// <summary>
    /// GetClaims
    /// </summary>
    /// <param name="userAccounts"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    public static IEnumerable<Claim> GetClaims(this UserTokenModel userAccounts, Guid Id)
    {
        IEnumerable<Claim> claims = new Claim[]
                {
            new Claim("Id",userAccounts.Id.ToString()),
            new Claim(ClaimTypes.Email, userAccounts.EmailId),
            new Claim(ClaimTypes.Expiration,DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt") ),
            new Claim(ClaimTypes.Role,userAccounts.Role)
                };
        return claims;
    }

    private static string _JWTSecretKey;
    private static string _JWTIssuer;
    private static string _JWTAudience;
    private static string _SSOClientID;
    public static void Initialize(IConfiguration configuration)
    {
        _JWTSecretKey = configuration["JWTSecretKey"];
        _JWTIssuer = configuration["JWTIssuer"];
        _JWTAudience = configuration["JWTIssuer"];
        _SSOClientID = configuration["SSOClientID"];
    }

    /// <summary>
    /// GetClaims
    /// </summary>
    /// <param name="userAccounts"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    public static IEnumerable<Claim> GetClaims(this UserTokenModel userAccounts, out Guid Id)
    {
        Id = Guid.NewGuid();
        return GetClaims(userAccounts, Id);
    }

    /// <summary>
    /// GenTokenkey
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string GenTokenkey(UserTokenModel model)
    {
        try
        {
            var UserToken = new UserTokenModel();
            if (model == null) throw new ArgumentException(nameof(model));

            var key = System.Text.Encoding.ASCII.GetBytes(_JWTSecretKey);
            Guid Id = Guid.Empty;
            DateTime expireTime = DateTime.UtcNow.AddMinutes(180);
            var JWToken = new JwtSecurityToken(
                issuer: _JWTIssuer,
                audience: _JWTAudience,
                claims: GetClaims(model, out Id),
                notBefore: new DateTimeOffset(DateTime.UtcNow).DateTime,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            return token;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static bool ValidateToken(string authToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    private static TokenValidationParameters GetValidationParameters()
    {
        var key = System.Text.Encoding.ASCII.GetBytes(_JWTSecretKey);
        return new TokenValidationParameters()
        {
            ValidateLifetime = false, // Because there is no expiration in the generated token
            ValidateAudience = true, // Because there is no audiance in the generated token
            ValidateIssuer = false,   // Because there is no issuer in the generated token
            ValidAudience = _SSOClientID,
            ValidateIssuerSigningKey = false,
            SignatureValidator = (token, parameters) =>
            {
                return new JwtSecurityToken(token);
            }
        };
    }
}
