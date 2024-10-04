using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;

namespace GloboClimaPlatform.Core.UseCases.DynamoDb;

public interface IFavoriteCountryUseCase
{
    public Task Execute(GloboClimaPlatformDynamoDbModel updatedUserData, string countryName);

}