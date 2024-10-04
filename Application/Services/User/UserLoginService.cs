using GloboClimaPlatform.Adapters.Repositories;
using GloboClimaPlatform.Application.DTOs.User;
using GloboClimaPlatform.Core.UseCases.Jwt;
using GloboClimaPlatform.Core.UseCases.User;
using GloboClimaPlatform.Infra.Entities;

namespace GloboClimaPlatform.Application.Services.User;

public class UserLoginService(IUsersRepositoryService usersRepositoryService, IGenerateJwtUseCase generateJwtUseCase) : IUserLoginUseCase
{
    public async Task Execute(UsersEntity userEntity)
    {
        var jwt = generateJwtUseCase.Execute(userEntity.Email, userEntity.Name);
        await usersRepositoryService.UpdateUserJwt(userEntity, jwt);
    }
}