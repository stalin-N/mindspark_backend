namespace MarstonX.Application.IRepository.Sql;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByemailidAsync(string emailid);
    Task<IEnumerable<User>> GetAllAsync();
    Task<int> AddAsync(User entity);
    Task<bool> UpdateAsync(User entity);
    Task<bool> DeleteAsync(int id);
}
