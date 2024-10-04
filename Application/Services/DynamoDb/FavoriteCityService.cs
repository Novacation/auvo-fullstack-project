using GloboClimaPlatform.Adapters.Repositories.DynamoDb;
using GloboClimaPlatform.Core.UseCases.DynamoDb;
using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;

namespace GloboClimaPlatform.Application.Services.DynamoDb;

public class FavoriteCityService(IDynamoDbRepositoryService dynamoDbRepositoryService) : IFavoriteCityUseCase
{
    public async Task Execute(GloboClimaPlatformDynamoDbModel updatedUserData, string cityName)
    {
        await dynamoDbRepositoryService.FavoriteCity(updatedUserData, cityName);
    }
}