namespace GloboClimaPlatform.Core.UseCases.Jwt;

public interface IGenerateJwtUseCase
{
    public string Execute(string email, string name);
}