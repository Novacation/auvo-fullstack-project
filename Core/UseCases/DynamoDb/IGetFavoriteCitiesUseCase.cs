namespace GloboClimaPlatform.Core.UseCases.DynamoDb;

public interface IGetFavoriteCitiesUseCase
{
    public Task<List<string>> Execute(string email, string userName);
}