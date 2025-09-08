namespace MarstonX.Application.Handlers.Auth.DTO;

public class UserResDto
{
    public int Id { get; set; }
    public DateTime LastLoginTime { get; set; }
    public string Token { get; set; }
}
