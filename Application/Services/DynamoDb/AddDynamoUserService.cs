using GloboClimaPlatform.Adapters.Repositories.DynamoDb;
using GloboClimaPlatform.Core.UseCases.DynamoDb;
using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;

namespace GloboClimaPlatform.Application.Services.DynamoDb;

public class AddDynamoUserService(IDynamoDbRepositoryService dynamoDbRepositoryService) : IAddDynamoUserUseCase
{
    public async Task Execute(GloboClimaPlatformDynamoDbModel newUser)
    {
        await dynamoDbRepositoryService.AddUser(newUser);
    }
}