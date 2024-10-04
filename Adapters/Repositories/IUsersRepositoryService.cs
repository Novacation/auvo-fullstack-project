using GloboClimaPlatform.Application.DTOs.User;
using GloboClimaPlatform.Infra.Entities;

namespace GloboClimaPlatform.Adapters.Repositories;

public interface IUsersRepositoryService
{
    public Task Create(UserRegisterDto userRegisterDto, string jwt);
    
    public Task UpdateUserJwt(UsersEntity userEntity, string jwt);
    
    public Task<UsersEntity?> GetUserByEmail(string email);
    
    public Task<UsersEntity?> GetUserByLoginCredentials(UserLoginDto userLoginDto);
}