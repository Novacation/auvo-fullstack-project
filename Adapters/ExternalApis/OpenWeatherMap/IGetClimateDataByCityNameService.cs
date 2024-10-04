using GloboClimaPlatform.Infra.ExternalApis.ResponsesDTOs.OpenWeather;

namespace GloboClimaPlatform.Adapters.ExternalApis.OpenWeatherMap;

public interface IGetClimateDataByCityNameService
{
    public Task<GetClimateDataByCityNameApiResponseDto?> Execute(string cityName);
}