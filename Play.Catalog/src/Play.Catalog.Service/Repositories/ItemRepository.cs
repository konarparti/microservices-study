using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories;

public class ItemRepository
{
    private const string collectionName = "items";

    private readonly IMongoCollection<Item> dbCollection;

    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

    public ItemRepository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("Catalog");
        dbCollection = database.GetCollection<Item>(collectionName);
    }

    public async Task<IReadOnlyCollection<Item>> GetAllAsync()
    {
        return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }

    public async Task<Item?> GetAsync(Guid id)
    {
        var filter = filterBuilder.Eq(e => e.Id, id);
        return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);
        
        await dbCollection.InsertOneAsync(item);
    }

    public async Task UpdateAsync(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var filter = filterBuilder.Eq(e => e.Id, item.Id);

        await dbCollection.ReplaceOneAsync(filter, item);
    }

    public async Task RemoveAsync(Guid id)
    {
        var filter = filterBuilder.Eq(e => e.Id, id);

        await dbCollection.DeleteOneAsync(filter);
    }
}

