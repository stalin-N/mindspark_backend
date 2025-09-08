namespace MarstonX.Application.Users.Commands;
public record CreateUserCommand(string FirstName, string LastName, string Email) : IRequest<int>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _repository;
    public CreateUserCommandHandler(IUserRepository repository) => _repository = repository;

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        return await _repository.AddAsync(user);
    }
}