using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;

namespace GloboClimaPlatform.Core.UseCases.DynamoDb;

public interface IAddDynamoUserUseCase
{
    public Task Execute(GloboClimaPlatformDynamoDbModel newUser);
}