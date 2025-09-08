namespace MarstonX.Application.IRepository.Mongo;

public interface IUsermongoRepository
{
    Task<UserMongo> GetByIdAsync(string id);
    Task<IEnumerable<UserMongo>> GetAllAsync();
    Task AddAsync(UserMongo entity);
    Task UpdateAsync(UserMongo entity);
    Task DeleteAsync(string id);
}
