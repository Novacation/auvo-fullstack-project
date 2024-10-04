using GloboClimaPlatform.Application.DTOs.User;
using GloboClimaPlatform.Infra.Entities;

namespace GloboClimaPlatform.Core.UseCases.User;

public interface IGetUserByLoginCredentialsUseCase
{
    public Task<UsersEntity?> Execute(UserLoginDto userLoginDto);
}