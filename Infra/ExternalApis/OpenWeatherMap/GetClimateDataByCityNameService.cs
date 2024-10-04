using GloboClimaPlatform.Adapters.ExternalApis.OpenWeatherMap;
using GloboClimaPlatform.Infra.ExternalApis.ResponsesDTOs.OpenWeather;
using Newtonsoft.Json;

namespace GloboClimaPlatform.Infra.ExternalApis.OpenWeatherMap;

public class GetClimateDataByCityNameService(
    IConfiguration configuration,
    HttpClient httpClient)
    : IGetClimateDataByCityNameService
{
    public async Task<GetClimateDataByCityNameApiResponseDto?> Execute(string cityName)
    {
        var openWeatherMapApiKey = configuration["OpenWeatherMapApiKey"]!;

        var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={openWeatherMapApiKey}";

        var response = await httpClient.GetAsync(apiUrl);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        
        // Deserialize the JSON response to List<CountryDto>
        var cityClimateData = JsonConvert.DeserializeObject<GetClimateDataByCityNameApiResponseDto>(responseBody);

        return cityClimateData;
    }
}