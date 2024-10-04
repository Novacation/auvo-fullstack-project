using GloboClimaPlatform.Adapters.Repositories.DynamoDb;
using GloboClimaPlatform.Core.UseCases.DynamoDb;

namespace GloboClimaPlatform.Application.Services.DynamoDb;

public class GetFavoriteCountriesService(IDynamoDbRepositoryService dynamoDbRepositoryService) : IGetFavoriteCountriesUseCase
{
    public async Task<List<string>> Execute(string email, string userName)
    {
        return await dynamoDbRepositoryService.GetFavoriteCountries(email, userName);
    }
}