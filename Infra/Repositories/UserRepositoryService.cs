using GloboClimaPlatform.Adapters.Repositories;
using GloboClimaPlatform.Application.DTOs.User;
using GloboClimaPlatform.Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboClimaPlatform.Infra.Repositories;

public class UserRepositoryService(AppDbContext appDbContext) : IUsersRepositoryService
{
    public async Task Create(UserRegisterDto userRegisterDto, string jwt)
    {
        await appDbContext.UsersEntities.AddAsync(new()
        {
            Email = userRegisterDto.Email,
            Name = userRegisterDto.Name,
            Password = userRegisterDto.Password,
            JWT = jwt
        });

        await appDbContext.SaveChangesAsync();
    }

    public async Task UpdateUserJwt(UsersEntity userEntity, string jwt)
    {
       userEntity.JWT = jwt;
       await appDbContext.SaveChangesAsync();
    }

    public async Task<UsersEntity?> GetUserByEmail(string email) =>
        await appDbContext.UsersEntities.FirstOrDefaultAsync(user => user.Email.Equals(email));

    public async Task<UsersEntity?> GetUserByLoginCredentials(UserLoginDto userLoginDto) =>
        await appDbContext.UsersEntities.FirstOrDefaultAsync(user =>
            user.Email.Equals(userLoginDto.Email) && user.Password.Equals(userLoginDto.Password));
}