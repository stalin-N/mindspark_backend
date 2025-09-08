namespace MarstonX.Application.Users.Commands;
public record UpdateUserCommand(int Id, string FirstName, string LastName, string Email) : IRequest<bool>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _repository;
    public UpdateUserCommandHandler(IUserRepository repository) => _repository = repository;

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);
        if (user == null)
            return false; // Or throw exception

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        await _repository.UpdateAsync(user);
        return true; // Success
    }
}