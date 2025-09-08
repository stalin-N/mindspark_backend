namespace MarstonX.Api.Middlewares;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;
    public PermissionHandler(
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        IUserRepository repository)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _repository = repository;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var user = _httpContextAccessor.HttpContext.User;
        if (user == null)
        {
            context.Fail();
            return;
        }

        var username = user.FindAll(c => c.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();

        if (string.IsNullOrEmpty(username))
        {
            context.Fail();
            return;
        }

        // Step 3: Find Council Entity
        var entity = await _repository.GetByemailidAsync(username);
        if (entity == null)
        {
            context.Fail();
            return;
        }

        //// Step 4: Validate Origin
        //var allowedOrigins = new List<string> { _configuration["CAIPCNChatbotUIURL"] }
        //    .Where(u => !string.IsNullOrWhiteSpace(u))
        //    .Select(u => u.TrimEnd('/'))
        //    .ToList();

        //bool originAllowed = allowedOrigins.Any(u =>
        //    origin.StartsWith(u, StringComparison.OrdinalIgnoreCase));

        //if (!originAllowed)
        //{
        //    context.Fail();
        //    return;
        //}

        // Step 5: Permission Check
        bool hasPermission = requirement.Permission switch
        {
            "user" => entity.Email.ToString().ToLower() == username.ToLower(),
            _ => false
        };

        if (hasPermission)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
