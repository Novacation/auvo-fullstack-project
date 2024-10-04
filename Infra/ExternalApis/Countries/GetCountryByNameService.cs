using GloboClimaPlatform.Adapters.ExternalApis.Countries;
using GloboClimaPlatform.Application.DTOs.Countries;
using GloboClimaPlatform.Infra.ExternalApis.ResponsesDTOs;
using Newtonsoft.Json;

namespace GloboClimaPlatform.Infra.ExternalApis.Countries;

public class GetCountryByNameService( HttpClient httpClient) : IGetCountryByNameService
{
    public async Task<CountryByNameApiResponseDto?> Execute(GetCountryByNameDto countryByNameDto)
    {
        var apiUrl = $"https://restcountries.com/v3.1/name/{countryByNameDto.CountryName}?fullText=true";

        var response = await httpClient.GetAsync(apiUrl);
        
        response.EnsureSuccessStatusCode();
        
        var responseBody = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON response to List<CountryDto>
        var country = JsonConvert.DeserializeObject<List<CountryByNameApiResponseDto>>(responseBody);

        return country[0];
    }
}