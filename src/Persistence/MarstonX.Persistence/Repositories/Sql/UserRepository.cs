namespace MarstonX.Persistence.Repositories.Sql;
public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;
    public UserRepository(DapperContext context) => _context = context;

    public async Task<User> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT * FROM Users WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task<int> AddAsync(User user)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO Users (FirstName, LastName, Email, CreatedAt) 
                    VALUES (@FirstName, @LastName, @Email, @CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
        return await connection.ExecuteScalarAsync<int>(sql, user);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE Users SET FirstName = @FirstName, 
                    LastName = @LastName, Email = @Email WHERE Id = @Id";
        await connection.ExecuteAsync(sql, user);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Users WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
        return true;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT * FROM Users";
        return await connection.QueryAsync<User>(sql);
    }

    public async Task<User> GetByemailidAsync(string emailid)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT * FROM Users WHERE Email = @emailid";
        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = emailid });
    }
}