using Amazon.DynamoDBv2.DataModel;
using GloboClimaPlatform.Adapters.Repositories.DynamoDb;
using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;

namespace GloboClimaPlatform.Infra.Repositories.DynamoDb;

public class DynamoDbRepositoryService(IDynamoDBContext dynamoDbContext) : IDynamoDbRepositoryService
{
    public async Task AddUser(GloboClimaPlatformDynamoDbModel newUser) =>
        await dynamoDbContext.SaveAsync<GloboClimaPlatformDynamoDbModel>(newUser);


    public async Task FavoriteCity(GloboClimaPlatformDynamoDbModel updatedUserData, string cityName)
    {
        var foundItem =
            await dynamoDbContext.LoadAsync<GloboClimaPlatformDynamoDbModel>(updatedUserData.PartitionKey,
                updatedUserData.SortKey);

        foundItem.Cities ??= new List<string>();

        if (!foundItem.Cities.Contains(cityName))
        {
            foundItem.Cities.Add(cityName);
            await dynamoDbContext.SaveAsync(foundItem);
        }
    }

    public async Task<List<string>> GetFavoriteCities(string email, string userName)
    {
        var foundItem = await dynamoDbContext.LoadAsync<GloboClimaPlatformDynamoDbModel>(email,
            userName);

        foundItem.Cities ??= new List<string>();

        return foundItem.Cities;
    }

    public async Task<List<string>> GetFavoriteCountries(string email, string userName)
    {
        var foundItem = await dynamoDbContext.LoadAsync<GloboClimaPlatformDynamoDbModel>(email,
            userName);

        foundItem.Countries ??= new List<string>();

        return foundItem.Countries;
    }

    public async Task FavoriteCountry(GloboClimaPlatformDynamoDbModel updatedUserData, string countryName)
    {
        var foundItem =
            await dynamoDbContext.LoadAsync<GloboClimaPlatformDynamoDbModel>(updatedUserData.PartitionKey,
                updatedUserData.SortKey);

        foundItem.Countries ??= new List<string>();

        if (!foundItem.Countries.Contains(countryName))
        {
            foundItem.Countries.Add(countryName);
            await dynamoDbContext.SaveAsync(foundItem);
        }
    }
}