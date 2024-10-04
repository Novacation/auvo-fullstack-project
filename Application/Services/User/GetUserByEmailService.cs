using GloboClimaPlatform.Adapters.Repositories;
using GloboClimaPlatform.Core.UseCases.User;
using GloboClimaPlatform.Infra.Entities;

namespace GloboClimaPlatform.Application.Services.User;

public class GetUserByEmailService(IUsersRepositoryService repositoryService) : IGetUserByEmailUseCase
{
    public async Task<UsersEntity?> Execute(string email) => await repositoryService.GetUserByEmail(email);
}