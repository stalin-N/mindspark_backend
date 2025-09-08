namespace MarstonX.Application.Users.Queries;
public record GetUserQuery(int Id) : IRequest<User>;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
{
    private readonly IUserRepository _repository;
    public GetUserQueryHandler(IUserRepository repository) => _repository = repository;

    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}