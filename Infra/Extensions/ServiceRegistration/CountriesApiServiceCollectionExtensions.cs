using GloboClimaPlatform.Adapters.ExternalApis.Countries;
using GloboClimaPlatform.Infra.ExternalApis.Countries;

namespace GloboClimaPlatform.Infra.Extensions.ServiceRegistration;

public static class CountriesApiServiceCollectionExtensions
{
    public static IServiceCollection AddCountriesApiServices(this IServiceCollection services)
    {
        services.AddTransient<IGetCountryByNameService, GetCountryByNameService>();
        
        return services;
    }
}