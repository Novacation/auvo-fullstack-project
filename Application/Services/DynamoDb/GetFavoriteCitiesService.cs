using GloboClimaPlatform.Adapters.Repositories.DynamoDb;
using GloboClimaPlatform.Core.UseCases.DynamoDb;

namespace GloboClimaPlatform.Application.Services.DynamoDb;

public class GetFavoriteCitiesService(IDynamoDbRepositoryService dynamoDbRepositoryService) : IGetFavoriteCitiesUseCase
{
    public async Task<List<string>> Execute(string email, string userName)
    {
        return await dynamoDbRepositoryService.GetFavoriteCities(email, userName);
    }
}