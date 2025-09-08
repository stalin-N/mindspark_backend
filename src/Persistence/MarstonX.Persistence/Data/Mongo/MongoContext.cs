namespace MarstonX.Persistence.Data.Mongo;
public class MongoContext
{
    private readonly IMongoDatabase _database;
    public MongoContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
        _database = client.GetDatabase("MarstonXDb");
    }

    public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
}