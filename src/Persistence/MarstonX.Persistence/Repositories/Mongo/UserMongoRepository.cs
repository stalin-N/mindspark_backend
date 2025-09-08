namespace MarstonX.Persistence.Repositories.Mongo;
public class UserMongoRepository : IUsermongoRepository
{
    private readonly IMongoCollection<UserMongo> _collection;
    public UserMongoRepository(MongoContext context)
        => _collection = context.GetCollection<UserMongo>("Users");

    public async Task<UserMongo> GetByIdAsync(string id)
        => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task AddAsync(UserMongo user)
        => await _collection.InsertOneAsync(user);

    public async Task UpdateAsync(UserMongo user)
        => await _collection.ReplaceOneAsync(x => x.Id == user.Id, user);

    public async Task DeleteAsync(string id)
        => await _collection.DeleteOneAsync(x => x.Id == id);

    public async Task<IEnumerable<UserMongo>> GetAllAsync()
        => await _collection.Find(_ => true).ToListAsync();
}