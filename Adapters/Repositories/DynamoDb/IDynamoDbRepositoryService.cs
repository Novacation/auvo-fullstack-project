using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;

namespace GloboClimaPlatform.Adapters.Repositories.DynamoDb;

public interface IDynamoDbRepositoryService
{
    public Task AddUser(GloboClimaPlatformDynamoDbModel newUser);
    public Task FavoriteCity(GloboClimaPlatformDynamoDbModel updatedUserData,string cityName);
    
    public Task<List<string>> GetFavoriteCities(string email, string userName);
    
    public Task FavoriteCountry(GloboClimaPlatformDynamoDbModel updatedUserData,string countryName);

    public Task<List<string>> GetFavoriteCountries(string email, string userName);
}