using GloboClimaPlatform.Adapters.Repositories;
using GloboClimaPlatform.Application.DTOs.User;
using GloboClimaPlatform.Core.UseCases.User;
using GloboClimaPlatform.Infra.Entities;

namespace GloboClimaPlatform.Application.Services.User;

public class GetUserByLoginCredentialsService(IUsersRepositoryService usersRepositoryService)
    : IGetUserByLoginCredentialsUseCase
{
    public async Task<UsersEntity?> Execute(UserLoginDto userLoginDto) =>
        await usersRepositoryService.GetUserByLoginCredentials(userLoginDto);
}