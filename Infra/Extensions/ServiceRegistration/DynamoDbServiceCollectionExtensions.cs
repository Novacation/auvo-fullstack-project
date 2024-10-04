using GloboClimaPlatform.Adapters.Repositories.DynamoDb;
using GloboClimaPlatform.Application.Services.DynamoDb;
using GloboClimaPlatform.Core.UseCases.DynamoDb;
using GloboClimaPlatform.Infra.Repositories.DynamoDb;

namespace GloboClimaPlatform.Infra.Extensions.ServiceRegistration;

public static class DynamoDbServiceCollectionExtensions
{
    
    public static IServiceCollection AddDynamoDbServices(this IServiceCollection services)
    {
        AddRepository(services);
        AddUseCases(services);
        return services;
    }
    
    private static IServiceCollection AddRepository(IServiceCollection services)
    {
        services.AddTransient<IDynamoDbRepositoryService, DynamoDbRepositoryService>();
        return services;
    }

    private static IServiceCollection AddUseCases(IServiceCollection services)
    {
        services.AddTransient<IAddDynamoUserUseCase, AddDynamoUserService>();
        services.AddTransient<IFavoriteCityUseCase, FavoriteCityService>();
        services.AddTransient<IFavoriteCountryUseCase, FavoriteCountryService>();
        services.AddTransient<IGetFavoriteCitiesUseCase, GetFavoriteCitiesService>();
        services.AddTransient<IGetFavoriteCountriesUseCase, GetFavoriteCountriesService>();

        return services;
    }
}