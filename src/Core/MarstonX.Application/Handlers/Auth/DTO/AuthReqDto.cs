namespace MarstonX.Application.Handlers.Auth.DTO;

public class AuthReqDto
{
    [Required]
    public string token { get; set; }
}
