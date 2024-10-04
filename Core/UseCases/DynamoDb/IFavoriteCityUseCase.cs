using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;

namespace GloboClimaPlatform.Core.UseCases.DynamoDb;

public interface IFavoriteCityUseCase
{
    public Task Execute(GloboClimaPlatformDynamoDbModel updatedUserData, string cityName);
}