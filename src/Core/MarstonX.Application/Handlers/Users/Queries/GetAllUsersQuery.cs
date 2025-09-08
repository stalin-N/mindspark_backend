namespace MarstonX.Application.Users.Queries;
public record GetAllUsersQuery : IRequest<IEnumerable<User>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly IUserRepository _repository;
    public GetAllUsersQueryHandler(IUserRepository repository) => _repository = repository;

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        => await _repository.GetAllAsync();
}