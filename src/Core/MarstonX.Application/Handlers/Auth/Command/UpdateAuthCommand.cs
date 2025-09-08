namespace MarstonX.Application.Handlers.Auth.Command;

public record UpdateAuthCommand(AuthReqDto reqDto) : IRequest<UserResDto>
{
}
public class UpdateAuthCommandHandler : IRequestHandler<UpdateAuthCommand, UserResDto>
{
    private readonly IUserRepository _repository;
    public UpdateAuthCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserResDto> Handle(UpdateAuthCommand request, CancellationToken cancellationToken)
    {
        UserResDto userResDto = new UserResDto();
        try
        {

            bool isTokenValid = JwtHelpers.ValidateToken(request.reqDto.token);
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token;
            token = handler.ReadToken(request.reqDto.token) as JwtSecurityToken;
            var userID = string.Empty;
           
            if (!isTokenValid)
            {
                return null;
            }
            else
            {
                userID = token.Claims.First(claim => claim.Type == "upn" || claim.Type == "preferred_username").Value;
            }

            try
            {
                // Fetch the entity
                var entity = await _repository.GetByemailidAsync(userID.ToLower());
                if (entity == null)
                {
                    userResDto.Id = entity.Id;
                    userResDto.Token = null;
                    return userResDto;
                }

                UserTokenModel Token = new UserTokenModel();
                Token.Id = Convert.ToString(entity.Id);
                Token.EmailId = entity.Email;
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var resResult = JwtHelpers.GenTokenkey(Token);

                userResDto.Id = entity.Id;
                userResDto.LastLoginTime = DateTime.UtcNow;
                userResDto.Token = resResult;
                
                return userResDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
