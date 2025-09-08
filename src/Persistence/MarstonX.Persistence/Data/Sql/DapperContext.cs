namespace MarstonX.Persistence.Data.Sql;

public class DapperContext
{
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
        => _connectionString = configuration.GetConnectionString("SqlConnection");

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
