using GloboClimaPlatform.Adapters.Repositories;
using GloboClimaPlatform.Application.DTOs.User;
using GloboClimaPlatform.Core.UseCases.Jwt;
using GloboClimaPlatform.Core.UseCases.User;

namespace GloboClimaPlatform.Application.Services.User;

public class UserRegisterService(IUsersRepositoryService usersRepositoryService, IGenerateJwtUseCase generateJwtUseCase) : IUserRegisterUseCase
{
    public async Task<string> Execute(UserRegisterDto userRegisterDto)
    {
        var jwt = generateJwtUseCase.Execute(userRegisterDto.Email, userRegisterDto.Name);
        await usersRepositoryService.Create(userRegisterDto, jwt);

        return jwt;
    }
}