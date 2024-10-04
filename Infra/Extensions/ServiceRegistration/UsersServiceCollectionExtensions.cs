using GloboClimaPlatform.Adapters.Repositories;
using GloboClimaPlatform.Application.Services.User;
using GloboClimaPlatform.Core.UseCases.User;
using GloboClimaPlatform.Infra.Repositories;

namespace GloboClimaPlatform.Infra.Extensions.ServiceRegistration;

public static class UsersServiceCollectionExtensions
{
    public static IServiceCollection AddUsersServices(this IServiceCollection services)
    {
        AddRepository(services);
        AddUseCases(services);

        return services;
    }

    private static IServiceCollection AddRepository(IServiceCollection services)
    {
        services.AddTransient<IUsersRepositoryService, UserRepositoryService>();

        return services;
    }

    private static IServiceCollection AddUseCases(IServiceCollection services)
    {
        services.AddTransient<IUserRegisterUseCase, UserRegisterService>();
        services.AddTransient<IUserLoginUseCase, UserLoginService>();
        services.AddTransient<IGetUserByEmailUseCase, GetUserByEmailService>();
        services.AddTransient<IGetUserByLoginCredentialsUseCase, GetUserByLoginCredentialsService>();

        return services;
    }
}