using GloboClimaPlatform.Application.DTOs.User;

namespace GloboClimaPlatform.Core.UseCases.User;

public interface IUserRegisterUseCase
{
    public Task<string> Execute(UserRegisterDto userRegisterDto);
}