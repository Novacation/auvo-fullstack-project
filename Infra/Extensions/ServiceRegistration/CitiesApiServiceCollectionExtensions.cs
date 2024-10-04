using GloboClimaPlatform.Adapters.ExternalApis.OpenWeatherMap;
using GloboClimaPlatform.Infra.ExternalApis.OpenWeatherMap;

namespace GloboClimaPlatform.Infra.Extensions.ServiceRegistration;

public static class CitiesApiServiceCollectionExtensions
{
    public static IServiceCollection AddCitiesServices(this IServiceCollection services)
    {
        AddApiServices(services);
        return services;
    }

    private static IServiceCollection AddApiServices(IServiceCollection services)
    {
        services.AddTransient<IGetClimateDataByCityNameService, GetClimateDataByCityNameService>();
        return services;
    }

    
}