namespace MarstonX.Application.Users.Commands;
public record DeleteUserCommand(int Id) : IRequest<bool>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _repository;
    public DeleteUserCommandHandler(IUserRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);
        if (user == null)
            return false; // User not found

        await _repository.DeleteAsync(request.Id);
        return true; // Successfully deleted
    }
}