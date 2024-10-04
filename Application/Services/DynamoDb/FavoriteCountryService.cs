using GloboClimaPlatform.Adapters.Repositories.DynamoDb;
using GloboClimaPlatform.Core.UseCases.DynamoDb;
using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;

namespace GloboClimaPlatform.Application.Services.DynamoDb;

public class FavoriteCountryService(IDynamoDbRepositoryService dynamoDbRepositoryService) : IFavoriteCountryUseCase
{
    public async Task Execute(GloboClimaPlatformDynamoDbModel updatedUserData, string countryName)
    {
        await dynamoDbRepositoryService.FavoriteCountry(updatedUserData, countryName);
    }
}