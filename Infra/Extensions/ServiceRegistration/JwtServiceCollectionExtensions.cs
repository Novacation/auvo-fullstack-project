using GloboClimaPlatform.Application.Services.Jwt;
using GloboClimaPlatform.Core.UseCases.Jwt;

namespace GloboClimaPlatform.Infra.Extensions.ServiceRegistration;

public static class JwtServiceCollectionExtensions
{
    public static IServiceCollection AddJwtServices(this IServiceCollection services)
    {
        AddJwtUseCases(services);

        return services;
    }

    private static IServiceCollection AddJwtUseCases(IServiceCollection services)
    {
        services.AddTransient<IGenerateJwtUseCase, GenerateJwtUseCase>();

        return services;
    }
}