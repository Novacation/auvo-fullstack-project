using GloboClimaPlatform.Infra.Entities;

namespace GloboClimaPlatform.Core.UseCases.User;

public interface IGetUserByEmailUseCase
{
    public Task<UsersEntity?> Execute(string email);
}