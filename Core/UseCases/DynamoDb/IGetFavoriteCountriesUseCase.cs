namespace GloboClimaPlatform.Core.UseCases.DynamoDb;

public interface IGetFavoriteCountriesUseCase
{
    public Task<List<string>> Execute(string email, string userName);
}